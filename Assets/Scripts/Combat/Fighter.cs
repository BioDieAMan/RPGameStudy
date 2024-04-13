using UnityEngine;
using RPG.Attributes;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 5f;
        [SerializeField] float timeBetweenAttacks = 1f;

        [SerializeField] float weaponDamage = 8f;
        CombatTarget Target;
        float timeSinceLastAttack = Mathf.Infinity;

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            AnimatorStateInfo stateInfo = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
            if (stateInfo.normalizedTime >= 1.0f && stateInfo.IsName("Attack")) GetComponent<Animator>().SetTrigger("stopAttack");

        }

        public void Attack(CombatTarget combatTarget)
        {
            Target = combatTarget;

            if (!CanBeAttacked(Target)) Debug.Log("not attacking target");
            else
            {
                Debug.Log("attacking target");
                AttackBehaviour();
            }
        }
        public void Cancel()
        {
            GetComponent<Animator>().SetTrigger("stopAttack");
            Target = null;
        }
        public bool CanBeAttacked(CombatTarget Target)
        {
            return Target != null && !Target.GetComponent<Health>().IsDead() && IsInRange(Target);
        }

        public float GetWeaponRange()
        {
            return weaponRange;
        }

        private bool IsInRange(CombatTarget target)
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }


        private void AttackBehaviour()
        {
            transform.LookAt(Target.transform);
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                // This will trigger the Hit() event.
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        //Animation event
        void Hit()
        {
            if (Target == null) return;
            Target.GetComponent<Health>().TakeDamage(weaponDamage);
        }


        private void TriggerAttack()
        {
            GetComponent<ActionScheduler>().StartAction(this);
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }
    }
}
