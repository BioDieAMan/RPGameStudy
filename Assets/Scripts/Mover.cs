using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    public float m_speed = 10f;
    private Vector3 targetDirection;
    // Update is called once per frame
    void Update()
    {
        MoveByKey(KeyCode.UpArrow);
        MoveByKey(KeyCode.DownArrow);
        MoveByKey(KeyCode.LeftArrow);
        MoveByKey(KeyCode.RightArrow);
    }

    private void MoveToCursor()
    {

        Ray lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(lastRay, out hit))
        {
            GetComponent<NavMeshAgent>().destination = hit.point;
        }

    }


    private void UpdateAnimator()
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;


        GetComponent<Animator>().SetFloat("forwardSpeed", speed);
    }

    private void MoveByKey(KeyCode key)
    {
        // if (Input.GetKeyDown(key))
        // {
        //     if (key == KeyCode.DownArrow)
        //     {

        //     }
        // }


        if (Input.GetKey(key))
        {
            if (key == KeyCode.DownArrow)
            {
                targetDirection = -transform.right;
            }
            else if (key == KeyCode.LeftArrow)
            {
                targetDirection = -transform.right;
            }
            else if (key == KeyCode.RightArrow)
            {
                targetDirection = transform.right;
            }

            if (targetDirection == null)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.forward), 2f * Time.deltaTime);
            else transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDirection), 2f * Time.deltaTime);

            transform.position = transform.position + transform.forward * m_speed * Time.deltaTime;

            GetComponent<Animator>().SetFloat("forwardSpeed", m_speed);
        }

        if (Input.GetKeyUp(key))
        {
            GetComponent<Animator>().SetFloat("forwardSpeed", 0); targetDirection = transform.forward;
        }
    }
}
