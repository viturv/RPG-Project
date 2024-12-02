using RPG.Core;
using UnityEngine;
using UnityEngine.AI;


namespace RPG.Movement
{
    public class Mover : MonoBehaviour , IAction
    {

        Transform target;
        [SerializeField] float MaxSpeed = 4.5f;
        //[SerializeField] float MinSpeed = 4.5f;

        NavMeshAgent navMeshAgent;
        Health health;

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }
    
        void Update()
        {
            navMeshAgent.enabled = !health.IsDead();

            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 Destination , float SpeedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(Destination , SpeedFraction);
        }

        public void MoveTo(Vector3 Destination , float SpeedFraction)
        {
            navMeshAgent.destination = Destination ;
            navMeshAgent.speed = MaxSpeed * Mathf.Clamp01(SpeedFraction) ;
            navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        } 
    
        private void UpdateAnimator()
        {
            Vector3 Velocity = navMeshAgent.velocity;
            Vector3 LocalVelocity = transform.InverseTransformDirection(Velocity);
            float Speed =LocalVelocity.z;
            GetComponent<Animator>().SetFloat("ForwardSpeed",Speed);
        }

    }

}