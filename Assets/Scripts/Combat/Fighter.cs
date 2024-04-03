using UnityEngine;
using RPG.Movement;
using RPG.Control;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] float WeaponRange = 5f;
        Transform Target;

        private void Update()
        {
        }

        public void Attack(CombatTarget combatTarget)
        {
            Target = combatTarget.transform;

            if (Target != null && !IsInRange(Target))
            {
                Debug.Log("not attacking target");
            }
            else
            {
                Debug.Log("attack target");
            }
        }

        private bool IsInRange(Transform target)
        {
            return Vector3.Distance(transform.position, target.position) < WeaponRange;
        }
    }

}
