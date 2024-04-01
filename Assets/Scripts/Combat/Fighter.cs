using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        public void Attack(CombatTarget target)
        {
            Debug.Log("Fighter.Attack() called" + target);
        }
    }

}
