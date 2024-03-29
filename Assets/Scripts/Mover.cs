using UnityEngine;

public class Mover : MonoBehaviour
{
    // public float m_speed = 10f;
    private Vector3 TargetDirection;
    private float Speed;
    private readonly float Acceleration = 20f;

    // Update is called once per frame
    void Update()
    {
        MoveByKey();
    }

    private void MoveByKey()
    {
        if (Input.GetKey(KeyCode.D)) TargetDirection = transform.right;
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S)) TargetDirection = -transform.right;

        if (GetDirectionKey())
        {
            if (TargetDirection == null)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.forward), 3f * Time.deltaTime);
            else transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(TargetDirection), 3f * Time.deltaTime);

            Speed += Acceleration * Time.deltaTime;
            Speed = Mathf.Min(Speed, 10f);

            transform.position = transform.position + Speed * Time.deltaTime * transform.forward;

            GetComponent<Animator>().SetFloat("forwardSpeed", Speed);
        }
        else Speed = 0;


        if (GetDirectionKeyUp())
        {
            GetComponent<Animator>().SetFloat("forwardSpeed", 0);
            TargetDirection = transform.forward;
        }
    }

    private bool GetDirectionKey()
    {
        return Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S);
    }
    private bool GetDirectionKeyUp()
    {
        return Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S);
    }
}
