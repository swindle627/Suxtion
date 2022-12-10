using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuctionScript : MonoBehaviour
{
    private GameObject suctionPoint;
    private playerController player;
    private float xSpeed, ySpeed, zSpeed;

    private void Start()
    {
        suctionPoint = FindObjectOfType<StoreObject>().gameObject;
        player = FindObjectOfType<playerController>();
    }
    private void Update()
    {
        xSpeed = player.suctionSpeed;
        zSpeed = player.suctionSpeed;
        ySpeed = 1;
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
            objTransform.z = Vector3.MoveTowards(objTransform, suctionPoint.transform.position, zSpeed).z;
            other.transform.position = objTransform;
        }
    }
}
