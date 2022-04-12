using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1f;
    public float Jump = 1f;
    public Vector3 massOffset = new Vector3(0, 0, 0);
    public Collider baseCollider;

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
        m_Movement = horizontal * transform.right + transform.forward;
        m_Movement *= 0.01f;

        bool onGround = Physics.Raycast(transform.position, Vector3.down, 0.55f);

        if ((Input.GetKeyDown("j") || Input.GetKeyDown("k")) && onGround)
        {
            m_Rigidbody.AddForce(Vector3.up * Jump, ForceMode.Impulse);
        }

        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement);
    }

}
