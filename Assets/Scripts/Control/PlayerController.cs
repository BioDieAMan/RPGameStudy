using RPG.Combat;
using RPG.Core;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Vector3 TargetDirection;
        // ActionStore actionStore;
        public float Speed;
        private float MaxSpeed = 10f;
        private float MinSpeed = 0f;
        private readonly float Acceleration = 20f;
        private void Update()
        {
            InteractWithCombat();
            MoveByKey();
        }

        public void MoveByKey()
        {
            if (GetDirectionKeyDown())
            {
                GetComponent<ActionScheduler>().StartAction(this);
            }
            if (Input.GetKey(KeyCode.D)) TargetDirection = transform.right;
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S)) TargetDirection = -transform.right;

            if (GetDirectionKey())
            {
                if (TargetDirection == null)
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.forward), 2.6f * Time.deltaTime);
                else transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(TargetDirection), 2.6f * Time.deltaTime);

                Speed += Acceleration * Time.deltaTime;
                Speed = Mathf.Min(Speed, MaxSpeed);

                transform.position = transform.position + Speed * Time.deltaTime * transform.forward;
            }
            else
            {
                Speed -= Acceleration * Time.deltaTime;
                Speed = Mathf.Max(Speed, MinSpeed);
                transform.position = transform.position + Speed * Time.deltaTime * transform.forward;
            }

            if (GetDirectionKeyUp())
            {
                TargetDirection = transform.forward;
            }
        }

        private void InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target);
                }
                return;
            }
        }
        private bool GetDirectionKeyDown()
        {
            return Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S);
        }

        private bool GetDirectionKey()
        {
            return Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S);
        }
        private bool GetDirectionKeyUp()
        {
            return Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S);
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
