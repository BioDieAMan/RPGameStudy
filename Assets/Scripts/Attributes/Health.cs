using UnityEngine;


namespace RPG.Attributes
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float health = 100f;

        private bool isDead = false;

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

        public bool IsDead()
        {
            return isDead;
        }

        private void Die()
        {
            if (isDead) return;
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
        }
    }
}
