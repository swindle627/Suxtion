using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float health  = 100;
    public float inventoryCount, pierceCount, suctionRange, expelRange, speed;

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    private void Update()
    {
        MovementGlobalForward();
        LookAtPosition();
    }
    private void MovementGlobalForward()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= Vector3.right * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= Vector3.forward * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }
    private void MovementLocalForward()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
    }
    private void LookAtPosition()
    {
        Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if(ground.Raycast(camRay, out rayLength))
        {
            Vector3 lookDir = camRay.GetPoint(rayLength);
            transform.LookAt(new Vector3(lookDir.x, transform.position.y, lookDir.z));
        }
    }
    // used to set inventory amount
    public void SetInventoryCount(float count)
    {
        inventoryCount = count;
    }
    // used to set pierce count
    public void SetPierceCount(float count)
    {
        pierceCount = count;
    }
    // used to set suction range
    public void SetSuctionRange(float range)
    {
        suctionRange = range;
    }
    // used to set expel range
    public void SetExpelRange(float range)
    {
        expelRange = range;
    }
    // used to set player speed
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    // used to set player health
    public void SetHealth(float health)
    {
        this.health = health;
    }
    // used to get player health
    public void GetHealth(float health)
    {
        this.health = health;
    }
}
