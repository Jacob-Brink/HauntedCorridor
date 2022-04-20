using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1f;
    public float Jump = 1f;
    public Vector3 massOffset = new Vector3(0, 0, 0);
    public int maxFlips = 2;
    private int flipsLeft = 0;

    public GameObject Corridor;
   
    Vector3 m_Movement;
    Rigidbody m_Rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.centerOfMass = transform.position + massOffset;
    }

    // Update is called once per frame
    void Update()
    {
        bool onGround = Physics.Raycast(transform.position, Vector3.down, 0.55f);

        float horizontal = Input.GetAxis("Horizontal");
        m_Movement = horizontal * transform.right + transform.forward;
        m_Movement *= 0.01f;

        if (!onGround)
        {
            m_Movement *= 0.25f;
        }


        if (onGround)
        {
            // reset flipsLeft
            flipsLeft = maxFlips;

            // player can only jump if on ground
            if (Input.GetKeyDown("space"))
            {
                m_Rigidbody.AddForce(Vector3.up * Jump, ForceMode.Impulse);
            }

        }

        // flip left only if player has flips, is in the air, and pressed j
        if (Input.GetKeyDown("j") && !onGround && flipsLeft > 0)
        {
            Corridor.GetComponent<CorridorRotation>().flipLeft();
            flipsLeft -= 1;
        }

        // flip right only if player has flips, is in the air, and pressed k
        if (Input.GetKeyDown("k") && !onGround && flipsLeft > 0)
        {
            Corridor.GetComponent<CorridorRotation>().flipRight();
            flipsLeft -= 1;
        }

        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement);
    }

}
