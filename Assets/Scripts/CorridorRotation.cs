using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorRotation : MonoBehaviour
{

    public float rotationSpeed = 5f;
    private float rotationAngle = 90f;
    private float targetRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void flipLeft()
    {
        targetRotation = (targetRotation + rotationAngle) % 360f;
    }

    public void flipRight()
    {
        targetRotation = (targetRotation - rotationAngle) % 360f;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 direction = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, targetRotation);
        Quaternion targetOrientation = Quaternion.Euler(direction);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetOrientation, Time.deltaTime * rotationSpeed);

    }

}
