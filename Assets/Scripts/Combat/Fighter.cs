using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat 
{
    public class Fighter: MonoBehaviour , IAction
    {

        [SerializeField] float WeaponRange = 2f;
        [SerializeField] float TimeBetweenAttacks = 1f;
        [SerializeField] float WeaponDamage = 5f;

        Health target;
        float TimesinceLastAttack= Mathf.Infinity;
 
        private void Update()
        {
            TimesinceLastAttack += Time.deltaTime;
            
            
            if(target == null) return;
            if(target.IsDead())return;

            if ( !GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position , 1f);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            if(TimeBetweenAttacks < TimesinceLastAttack)
            {
                transform.LookAt(target.transform);
                //This Wil Trigger Hit() Event    
                TriggerAttack();
                TimesinceLastAttack = 0f;
            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("CancelAttack");
            GetComponent<Animator>().SetTrigger("Attack");

        }

        //Animation Event
        void Hit()
        {
            if(target==null) return;
            target.TakeDamage(WeaponDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < WeaponRange;
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if(combatTarget == null)
            {
                return false;
            }
            Health TargetToTest = combatTarget.GetComponent<Health>();
            return TargetToTest!=null && !TargetToTest.IsDead();
        }

        
        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
            GetComponent<Mover>().Cancel();

        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("Attack");
            GetComponent<Animator>().SetTrigger("CancelAttack");

        }
    }
}