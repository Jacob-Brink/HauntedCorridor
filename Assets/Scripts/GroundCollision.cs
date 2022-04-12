using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollision : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.name == "Panel")
        {
            GameObject player = transform.parent.gameObject;
            player.GetComponent<PlayerMovement>().TouchGround();
        }
    }

}
