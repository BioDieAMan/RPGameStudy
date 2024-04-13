using RPG.Combat;
using RPG.Core;
using UnityEngine;
using RPG.Attributes;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour, IAction
    {
        private Vector3 TargetDirection;
        // ActionStore actionStore;
        public float Speed;
        private float MaxSpeed = 10f;
        private float MinSpeed = 0f;
        private readonly float Acceleration = 20f;
        private bool IsMoving;

        private void Awake()
        {
            IsMoving = true;
        }

        private void Update()
        {
            if (GetComponent<Health>().IsDead()) return;
            MoveByKey();
            if (InteractWithCombat()) return;

            GetComponent<Animator>().SetFloat("Speed", Speed);
        }

        public void MoveByKey()
        {
            if (GetDirectionKeyDown())
            {
                GetComponent<ActionScheduler>().StartAction(this);
                IsMoving = true;
            }

            if (!IsMoving) return;

            if (Input.GetKey(KeyCode.D)) TargetDirection = transform.right;
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S)) TargetDirection = -transform.right;

            if (GetDirectionKey())
            {
                if (TargetDirection != Vector3.zero) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(TargetDirection), 2.6f * Time.deltaTime);
                Speed += Acceleration * Time.deltaTime;
                Speed = Mathf.Min(Speed, MaxSpeed);

                transform.position = transform.position + Speed * Time.deltaTime * transform.forward;
            }
            else
            {
                Speed -= Acceleration * Time.deltaTime;
                Speed = Mathf.Max(Speed, MinSpeed);
                if (Speed != 0) transform.position = transform.position + Speed * Time.deltaTime * transform.forward;
            }

            if (GetDirectionKeyUp()) TargetDirection = transform.forward;



        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (!GetComponent<Fighter>().CanBeAttacked(target)) continue;
                if (Input.GetMouseButtonDown(0)) GetComponent<Fighter>().Attack(target);
                return true;
            }
            return false;
        }

        public void Cancel()
        {
            Speed = 0;
            IsMoving = false;
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
