using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    //public enum Test { a, b, c }
    //public Test testing;
    [Header("Suction setup")]
    [SerializeField] private float xSuctionSpeed;
    [SerializeField] private float ySuctionSpeed;
    [SerializeField] private float zSuctionSpeed;

    [Header("Expel setup")]
    [SerializeField] private Transform expelPoint;

    [Header("Player settings")]
    public float healthPackValue;
    public float healthPackTimer;

    // used by UpgradeShop to set expel range
    // used by PlayerProjectile to get expelRange
    // works by timer in PlayerProjectile

    [Header("Things for testing purposes")]
    public float health  = 100;
    public float pierceCount, expelRange, suctionRange, speed;
    public bool isPaused;
    public GameObject one, two, three;

    private Camera cam;
    private GameObject suction;
    private InventoryScript inventory;
    private float healthTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        cam = FindObjectOfType<Camera>();
        suction = FindObjectOfType<SuctionScript>().gameObject;
        inventory = gameObject.GetComponent<InventoryScript>();

        suction.GetComponent<SuctionScript>().SetSuctionSpeed(xSuctionSpeed, ySuctionSpeed, zSuctionSpeed);

        // remove this stuff later
        inventory.SetSize(3);
    }
    private void Update()
    {
        // handle this in game manager later
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isPaused = !isPaused;
        }

        if (!isPaused)
        {
            MovementGlobalForward();
            LookAtPosition();
            SuckObjects();
            ExpelObjects();
            AddHealth();
        }

        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            Instantiate(one);
            Instantiate(two);
            Instantiate(three);
        }
    }
    // Player movement
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
    // Player faces mouse cursor
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
    // Activates suction
    private void SuckObjects()
    {
        if(Input.GetKey(KeyCode.Mouse0) && !inventory.Full())
        {
            suction.SetActive(true);
        }
        else if(Input.GetKey(KeyCode.Mouse0))
        {
            // message saying inventory is full on canvas
            Debug.Log("Inventory Full");
            suction.SetActive(false);
        }
        else
        {
            suction.SetActive(false);
        }
    }
    // Activates expel
    private void ExpelObjects()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1) && !inventory.Empty())
        {
            // Uses Resources.Load to instantiate objects
            string projectile = inventory.Remove();
            Instantiate(Resources.Load(projectile), expelPoint.position, expelPoint.rotation);
        }
        else if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            // message saying inventory is empty on canvas
            Debug.Log("Inventory empty");
        }
    }
    // If the player isn't at full health and they have health packs in their inventory health will be added
    private void AddHealth()
    {

        if(health < 100 && healthTime > healthPackTimer)
        {
            float healthIncrease = health + healthPackValue * inventory.GetHealthCount();

            Debug.Log(healthPackValue * inventory.GetHealthCount() + " health added.");

            if(healthIncrease > 100)
            {
                health = 100;
            }
            else
            {
                health = healthIncrease;
            }

            healthTime = 0;
        }
        else
        {
            healthTime += Time.deltaTime;
        }
    }


    // Methods below primarily used by UpgradeShop
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
