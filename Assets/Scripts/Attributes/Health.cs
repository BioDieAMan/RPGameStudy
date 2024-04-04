using UnityEngine;


namespace RPG.Attributes
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float health = 100f;

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
            if (health == 0)
            {
                Die();
            }
            Debug.Log("Damage taken: " + damage);
            Debug.Log("Current health: " + health);
        }

        private void Die()
        {
            // GetComponent<Animator>().SetTrigger("die");
            Debug.Log("Dead");
        }
    }
}
