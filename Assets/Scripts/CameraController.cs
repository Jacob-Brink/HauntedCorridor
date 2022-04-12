using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;        //Public variable to store a reference to the player game object


    private Vector3 offset;            //Private variable to store the offset distance between the player and camera

    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = new Vector3(0, transform.position.y - player.transform.position.y, transform.position.z - player.transform.position.z);
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = player.transform.position + offset;
        var rotationVector = player.transform.rotation.eulerAngles;
        rotationVector.y = 0;  //this number is the degree of rotation around Z Axis
        rotationVector.x = 0;
        transform.rotation = Quaternion.Euler(rotationVector);
    }
}
