using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeShop : MonoBehaviour
{
    [SerializeField] private playerController player;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI currentPenalty;
    [SerializeField] private TextMeshProUGUI newPenalty;
    [SerializeField] private GameObject shopUI;

    // Shop sections
    [Header("Shop Sections")]
    [SerializeField] private GameObject suctionRange;
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject expelRange;
    [SerializeField] private GameObject speed;
    [SerializeField] private GameObject piercingShot;
    //[SerializeField] private GameObject healthRefill;

    // Values for the various upgrade levels
    [Header("Suction Ranges")]
    [SerializeField] private float suctionLevel1;
    [SerializeField] private float suctionLevel2;
    [SerializeField] private float suctionLevel3;
    [SerializeField] private float suctionLevel4;

    [Header("Expel Ranges")]
    [SerializeField] private float expelLevel1;
    [SerializeField] private float expelLevel2;
    [SerializeField] private float expelLevel3;
    [SerializeField] private float expelLevel4;

    [Header("Speed Values")]
    [SerializeField] private float speedLevel1;
    [SerializeField] private float speedLevel2;
    [SerializeField] private float speedLevel3;
    [SerializeField] private float speedLevel4;

    [Header("Piercing Shot Values")]
    [SerializeField] private float pierceLevel1;
    [SerializeField] private float pierceLevel2;
    [SerializeField] private float pierceLevel3;
    [SerializeField] private float pierceLevel4;

    [Header("Inventory Values")]
    [SerializeField] private float inventoryLevel1;
    [SerializeField] private float inventoryLevel2;
    [SerializeField] private float inventoryLevel3;
    [SerializeField] private float inventoryLevel4;

    // Arrays for each of the upgrade paths
    private float[] suctionUpgrades = new float[4];
    private float[] inventoryUpgrades = new float[4];
    private float[] expelUpgrades = new float[4];
    private float[] speedUpgrades = new float[4];
    private float[] pierceUpgrades = new float[4];

    // Arrays for the upgrade buttons
    private Button[] suctionButtons = new Button[4];
    private Button[] inventoryButtons = new Button[4];
    private Button[] expelButtons = new Button[4];
    private Button[] speedButtons = new Button[4];
    private Button[] piercingButtons = new Button[4];

    // used to track if the player has enough upgrade points before upgrading
    private bool doUpgrade = false;

    // used for refillPenalties
    private float healthAdded;
    private float refillPenalty = 0;
    private float refillMultiplier = 0;

    private bool shopIsActive;

    private void Start()
    {
        InitialSetup();
        SetInitialValues();
        shopIsActive = false;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            shopIsActive = !shopIsActive;
        }

        if(shopIsActive)
        {
            healthAdded = 100 - player.health;
            currentPenalty.text = "Current Health Penalty:\n" + (refillPenalty * refillMultiplier);
            if(player.health != 100)
            {
                newPenalty.text = "New Health Penalty:\n" + ((refillPenalty + healthAdded) * (refillMultiplier + 1));
            }
            else
            {
                newPenalty.text = "";
            }
            shopUI.SetActive(true);
        }
        else
        {
            shopUI.SetActive(false);
        }
    }
    // setting up upgrade arrays at start
    private void InitialSetup()
    {
        suctionUpgrades[0] = suctionLevel1;
        suctionUpgrades[1] = suctionLevel2;
        suctionUpgrades[2] = suctionLevel3;
        suctionUpgrades[3] = suctionLevel4;
        suctionButtons[0] = suctionRange.transform.GetChild(0).GetComponent<Button>();
        suctionButtons[1] = suctionRange.transform.GetChild(1).GetComponent<Button>();
        suctionButtons[2] = suctionRange.transform.GetChild(2).GetComponent<Button>();
        suctionButtons[3] = suctionRange.transform.GetChild(3).GetComponent<Button>();

        inventoryUpgrades[0] = inventoryLevel1;
        inventoryUpgrades[1] = inventoryLevel2;
        inventoryUpgrades[2] = inventoryLevel3;
        inventoryUpgrades[3] = inventoryLevel4;
        inventoryButtons[0] = inventory.transform.GetChild(0).GetComponent<Button>();
        inventoryButtons[1] = inventory.transform.GetChild(1).GetComponent<Button>();
        inventoryButtons[2] = inventory.transform.GetChild(2).GetComponent<Button>();
        inventoryButtons[3] = inventory.transform.GetChild(3).GetComponent<Button>();

        expelUpgrades[0] = expelLevel1;
        expelUpgrades[1] = expelLevel2;
        expelUpgrades[2] = expelLevel3;
        expelUpgrades[3] = expelLevel4;
        expelButtons[0] = expelRange.transform.GetChild(0).GetComponent<Button>();
        expelButtons[1] = expelRange.transform.GetChild(1).GetComponent<Button>();
        expelButtons[2] = expelRange.transform.GetChild(2).GetComponent<Button>();
        expelButtons[3] = expelRange.transform.GetChild(3).GetComponent<Button>();

        speedUpgrades[0] = speedLevel1;
        speedUpgrades[1] = speedLevel2;
        speedUpgrades[2] = speedLevel3;
        speedUpgrades[3] = speedLevel4;
        speedButtons[0] = speed.transform.GetChild(0).GetComponent<Button>();
        speedButtons[1] = speed.transform.GetChild(1).GetComponent<Button>();
        speedButtons[2] = speed.transform.GetChild(2).GetComponent<Button>();
        speedButtons[3] = speed.transform.GetChild(3).GetComponent<Button>();

        pierceUpgrades[0] = pierceLevel1;
        pierceUpgrades[1] = pierceLevel2;
        pierceUpgrades[2] = pierceLevel3;
        pierceUpgrades[3] = pierceLevel4;
        piercingButtons[0] = piercingShot.transform.GetChild(0).GetComponent<Button>();
        piercingButtons[1] = piercingShot.transform.GetChild(1).GetComponent<Button>();
        piercingButtons[2] = piercingShot.transform.GetChild(2).GetComponent<Button>();
        piercingButtons[3] = piercingShot.transform.GetChild(3).GetComponent<Button>();
    }
    // sending initial values to player controller
    private void SetInitialValues()
    {
        player.SetSuctionRange(suctionUpgrades[0]);
        player.SetInventoryCount(inventoryUpgrades[0]);
        player.SetExpelRange(expelUpgrades[0]);
        player.SetPierceCount(pierceUpgrades[0]);
        player.SetSpeed(speedUpgrades[0]);
    }
    // checks to see if player has enough upgrade points before upgrading
    // if they do it subtracts the necessary points from upgrade points
    // this method is call first by all upgrade buttons
    public void CheckUpgradePoints(float cost)
    {
        if(gameManager.upgradePoints > cost)
        {
            gameManager.upgradePoints -= cost;
            doUpgrade = true;
        }
        else
        {
            doUpgrade = false;
        }
    }
    // used to upgrade suction range
    public void SuctionUpgrade(int level)
    {
        if(doUpgrade)
        {
            player.SetSuctionRange(suctionUpgrades[level]);
            suctionButtons[level].interactable = false;
            doUpgrade = false;
        }
    }
    // used to upgrade expel range
    public void ExpelUpgrade(int level)
    {
        if (doUpgrade)
        {
            player.SetExpelRange(expelUpgrades[level]);
            expelButtons[level].interactable = false;
            doUpgrade = false;
        }
    }
    // used to upgrade inventory size
    public void InventoryUpgrade(int level)
    {
        if (doUpgrade)
        {
            player.SetInventoryCount(inventoryUpgrades[level]);
            inventoryButtons[level].interactable = false;
            doUpgrade = false;
        }
    }
    // used to upgrade pierce count
    public void PierceUpgrade(int level)
    {
        if (doUpgrade)
        {
            player.SetPierceCount(pierceUpgrades[level]);
            piercingButtons[level].interactable = false;
            doUpgrade = false;
        }
    }
    // used to upgrade player speed
    public void SpeedUpgrade(int level)
    {
        if (doUpgrade)
        {
            player.SetSpeed(speedUpgrades[level]);
            speedButtons[level].interactable = false;
            doUpgrade = false;
        }
    }
    // used to refill player health
    public void RefillHealth()
    {
        if(player.health != 100)
        {
            player.health += healthAdded;
            refillPenalty += healthAdded;
            refillMultiplier++;
        }
    }
}

