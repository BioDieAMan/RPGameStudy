using RPG.Combat;
using RPG.Core;
using UnityEngine;
using RPG.Attributes;
using Cinemachine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour, IAction
    {
        private Vector3 TargetDirection;
        // ActionStore actionStore;
        public float Speed;
        private float MaxSpeed = 10f;
        private float MinSpeed = 0f;
        private readonly float Acceleration = 10f;
        private GameObject VirtualCam;

        private void Awake()
        {
            VirtualCam = GameObject.FindWithTag("MainCamera");
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
            if (GetDirectionKeyDown()) GetComponent<ActionScheduler>().StartAction(this);

            if (Input.GetKey(KeyCode.D)) TargetDirection = VirtualCam.transform.right;
            else if (Input.GetKey(KeyCode.A)) TargetDirection = -VirtualCam.transform.right;
            else if (Input.GetKey(KeyCode.S)) TargetDirection = -VirtualCam.transform.forward;
            else if (Input.GetKey(KeyCode.W)) TargetDirection = VirtualCam.transform.forward;

            if (GetDirectionKey())
            {
                if (TargetDirection != Vector3.zero) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(TargetDirection), 2f * Time.deltaTime);
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

            // if (Input.GetMouseButtonDown(0)) GetComponent<Fighter>().Attack();

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
            GetComponent<Animator>().SetFloat("Speed", Speed);
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
