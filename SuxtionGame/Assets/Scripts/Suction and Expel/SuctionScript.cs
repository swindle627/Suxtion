using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuctionScript : MonoBehaviour
{
    private GameObject suctionPoint;
    private float xSpeed, ySpeed, zSpeed;

    private void Start()
    {
        suctionPoint = FindObjectOfType<StoreObject>().gameObject;
    }
    public void SetSuctionSpeed(float x, float y, float z)
    {
        xSpeed = x;
        ySpeed = y;
        zSpeed = z;
    }
    // When objects are in range they get attracted to the suction point
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Suckable" || other.gameObject.tag == "Enemy")
        {
            // The speed value on the y axis needs to be faster than on the x and z axis so the objects move towards the y value of the suctionPoint faster
            Vector3 objTransform = other.transform.position;
            objTransform.x = Vector3.MoveTowards(objTransform, suctionPoint.transform.position, xSpeed).x;
            objTransform.y = Vector3.MoveTowards(objTransform, suctionPoint.transform.position, ySpeed).y;
            objTransform.z = Vector3.MoveTowards(objTransform, suctionPoint.transform.position, xSpeed).z;
            other.transform.position = objTransform;
        }
    }
}
