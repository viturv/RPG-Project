using UnityEngine;

namespace RPG.Core
{

    public class Health : MonoBehaviour
    {
        [SerializeField] float HealthPoints = 100f;

        bool isDead = false;
        public bool IsDead ()
        {
            return isDead;
        } 

        public void TakeDamage(float Damage)
        {
            HealthPoints = Mathf.Max (HealthPoints - Damage , 0);
            //print("Health = " +  HealthPoints); 
            if(HealthPoints == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead)return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("Death");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }
}