using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1f;
    public Vector3 massOffset = new Vector3(0, 0, 0);
    public Collider baseCollider;

    private float SCALING = 0.04f;
    Vector3 m_Movement;
    Rigidbody m_Rigidbody;

    public float gravity = 9.8f;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.centerOfMass = transform.position + massOffset;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        m_Movement = horizontal * transform.right + transform.forward * 1;
        m_Movement *= 0.2f;


        Vector3 newVelocity = new Vector3(0, 0, 0);

        if (Input.GetKeyDown("j"))
        {
            Physics.gravity = Quaternion.AngleAxis(-90, Vector3.forward) * Physics.gravity;
            newVelocity += transform.up;
            //m_Rigidbody.AddTorque(transform.forward * -2000);
        }
        else if (Input.GetKeyDown("k"))
        {
            Physics.gravity = Quaternion.AngleAxis(90, Vector3.forward) * Physics.gravity;
            newVelocity += transform.up;

            //m_Rigidbody.AddTorque(transform.forward * 2000);
        }

        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement);
        m_Rigidbody.AddForce(newVelocity * 100);

    }

    public void TouchGround()
    {
        //print("Ground Collision");
        //m_Rigidbody.angularVelocity = Vector3.zero;
        //m_Rigidbody.ResetInertiaTensor();
    }

}
