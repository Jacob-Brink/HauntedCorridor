using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorRotation : MonoBehaviour
{

    public float rotationDuration = 4f;
    private int rotationAngle = 90;
    private int targetRotation = 0;
    private float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = rotationAngle / rotationDuration;
    }

    // Update is called once per frame
    void Update()
    {
        // figure out target rotation
        if (Input.GetKeyDown("j"))
        {
            targetRotation = (targetRotation + 90) % 360;
        } else if (Input.GetKeyDown("k"))
        {
            targetRotation = (targetRotation - 90) % 360;
        }

        // try to interpolate to target from current rotation
        // player will treat above controls as changing gravity
        // which means we can get away with the corridor rotating the opposite direction
        // in case the player spams one control 3 or 5 times, we only rotate 90 degrees
        if (transform.eulerAngles.z <= targetRotation+1 && transform.eulerAngles.z >= targetRotation-1)
        {

        } else
        {
            print("Rotate?");
            transform.Rotate(0, 0, 90 * Time.deltaTime);
        }

    }

}
