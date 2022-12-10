using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerController : MonoBehaviour
{
    [Header("Expel setup")]
    [SerializeField] private Transform expelPoint;

    [Header("Player settings")]
    public float healthPackValue;
    public float healthPackTimer;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("Player Stats")]
    public float health  = 100;
    public float pierceCount, expelRange, suctionSpeed, speed;

    private Camera cam;
    private GameObject suction;
    private InventoryScript inventory;
    private float healthTime = 0;
    private bool isPaused = false;
    private GameManager gameManager;
    private float enemyDamage = 5;

    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<Camera>();
        suction = FindObjectOfType<SuctionScript>().gameObject;
        inventory = gameObject.GetComponent<InventoryScript>();
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        isPaused = gameManager.GetPause();

        if(!isPaused)
        {
            MovementGlobalForward();
            LookAtPosition();
            SuckObjects();
            ExpelObjects();
            AddHealth();
        }

        healthText.text = health.ToString();

        if(gameManager.GetLevel() == 2)
        {
            enemyDamage = 10;
        }
        else if(gameManager.GetLevel() == 3)
        {
            enemyDamage = 15;
        }

        if(health <= 0)
        {
            gameManager.EndGame();
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

            if(projectile.Contains("Enemy"))
            {
                // color and enemy health data begin after the slash
                int dataIndex = projectile.IndexOf("/");

                // color starts at the first index after the slash
                int color = int.Parse(projectile.Substring(dataIndex + 1)); 

                // creates a substring of the data section and deletes it from the string
                string deleteSection = projectile.Substring(dataIndex);
                projectile = projectile.Replace(deleteSection, "");

                // spawns exepelled enemy projectile and sets it up
                GameObject enemy = (GameObject)Instantiate(Resources.Load(projectile), expelPoint.position, expelPoint.rotation);
                enemy.GetComponent<EnemyScript>().Setup(color);
            }
            else
            {
                Instantiate(Resources.Load(projectile), expelPoint.position, expelPoint.rotation);
            }
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

            //Debug.Log(healthPackValue * inventory.GetHealthCount() + " health added.");

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
    // used by game manager to pause player actions
    public void PauseManager(bool isPaused)
    {
        this.isPaused = isPaused;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy Projectile")
        {
            health -= enemyDamage;
            Destroy(other.gameObject);
        }
    }
}
