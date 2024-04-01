using UnityEngine;
using RPG.Control;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        private Vector3 TargetDirection;
        private float Speed;
        private readonly float Acceleration = 20f;

        // Update is called once per frame
        void Update()
        {
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            Speed = GetComponent<PlayerController>().Speed;
            GetComponent<Animator>().SetFloat("forwardSpeed", Speed);
        }
    }
}
