using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;


namespace RPG.Control
{
    public class AiController :MonoBehaviour
    {
        [SerializeField] float ChaseDistance = 5f;
        [SerializeField] float SuspicionTime = 2f;
        [SerializeField] PatrolPath patrolPath ;
        [SerializeField] float WayPointTolerence = 1f;
        [SerializeField] float WayPointDwellTime = 2f;
        [Range (0,1)]
        [SerializeField] float PatrolSpeedFraction = 0.3f;


        Fighter fighter;
        GameObject player;
        Health health;
        Mover mover;

        Vector3 GuardPosition;
        float TimeSinceLLastSawPlayer = Mathf.Infinity;
        float TimeSinceLArrivedAtWaypoint = Mathf.Infinity;

        int CurrentWayPointIndex= 0;

        void Start()
        {
            player =  GameObject.FindWithTag("Player");   
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();

            GuardPosition = transform.position;
        }

        void Update()
        {

            if (health.IsDead()) return;


            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                AttackBehaviour();
            }
            else if (TimeSinceLLastSawPlayer < SuspicionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }

            UpdateTimes();

        }

        private void UpdateTimes()
        {
            TimeSinceLLastSawPlayer += Time.deltaTime;
            TimeSinceLArrivedAtWaypoint += Time.deltaTime;
        }

        public void PatrolBehaviour()
        {
            Vector3 nextPosition = GuardPosition;

            if(patrolPath!=null)
            {
                if(AtWaypoint())
                {
                    TimeSinceLArrivedAtWaypoint =0;
                    CycleWayPoint();
                }
                nextPosition = GetCurrentWayPoint();
            }

            if(TimeSinceLArrivedAtWaypoint > WayPointDwellTime)
            {
            mover.StartMoveAction(nextPosition , PatrolSpeedFraction);
            }
        }

        public bool AtWaypoint()
        {
            float DistanceAtWayPoint = Vector3.Distance(transform.position , GetCurrentWayPoint());
            return DistanceAtWayPoint < WayPointTolerence;
        }

        private void CycleWayPoint()
        {
            CurrentWayPointIndex = patrolPath.GetNextIndex(CurrentWayPointIndex);
        }

        private Vector3 GetCurrentWayPoint()
        {
            return patrolPath.GetWaypoint(CurrentWayPointIndex);
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            TimeSinceLLastSawPlayer = 0f;
            fighter.Attack(player);
        }


        private bool InAttackRangeOfPlayer ()
        {

            float DistanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return DistanceToPlayer < ChaseDistance;
        }
    

        // Called By Unity
        void OnDrawGizmosSelected()
        {
            Gizmos.color= new Color(1, 0, 0, 1);
            Gizmos.DrawWireSphere(transform.position, ChaseDistance);
        }
    }
}