using UnityEngine;
using RPG.Control;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        private float Speed;

        // Update is called once per frame
        void Update()
        {
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            Speed = GetComponent<PlayerController>().Speed;
            GetComponent<Animator>().SetFloat("Speed", Speed);
        }
    }
}
