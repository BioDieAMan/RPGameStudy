using UnityEngine;

public class Mover : MonoBehaviour
{
    // public float m_speed = 10f;
    private Vector3 targetDirection;
    private float speed, acceleration = 20f;

    // Update is called once per frame
    void Update()
    {
        MoveByKey();
    }

    private void MoveByKey()
    {
        if (Input.GetKey(KeyCode.D)) targetDirection = transform.right;
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S)) targetDirection = -transform.right;


        if (GetDirectionKey())
        {
            if (targetDirection == null)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.forward), 3f * Time.deltaTime);
            else transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDirection), 3f * Time.deltaTime);

            speed += acceleration * Time.deltaTime;
            speed = Mathf.Min(speed, 10f);

            transform.position = transform.position + transform.forward * speed * Time.deltaTime;

            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }
        else speed = 0;


        if (GetDirectionKeyUp())
        {
            GetComponent<Animator>().SetFloat("forwardSpeed", 0);
            targetDirection = transform.forward;
        }
    }

    private bool GetDirectionKey()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S)) return true;
        return false;
    }
    private bool GetDirectionKeyUp()
    {
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S)) return true;
        return false;
    }
}
