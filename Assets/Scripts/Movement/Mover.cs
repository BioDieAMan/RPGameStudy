using UnityEngine;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        public float Speed;

        // Update is called once per frame
        void Update()
        {
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            // Speed = GetComponent<PlayerController>().Speed;
            GetComponent<Animator>().SetFloat("Speed", Speed);
        }
    }
}
