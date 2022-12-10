using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryScript : MonoBehaviour
{
    public GameObject[] inventorySlots;
    public TextMeshProUGUI[] slotText;
    public Color[] colors;
    private List<string> names = new List<string>();
    private Stack<string> inventory = new Stack<string>();
    private float currentSize;
    private bool isFull = false;
    private bool isEmpty = true;
    private int healthCounter = 0;
    private GameManager gameManager;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        if(inventory.Count == currentSize)
        {
            isFull = true;
        }
        else
        {
            isFull = false;
        }

        if(inventory.Count == 0)
        {
            isEmpty = true;
        }
        else
        {
            isEmpty = false;
        }
    }
    // Sets inventory size
    public void SetSize(float size)
    {
        currentSize = size;

        for(int i = 0; i < currentSize; i++)
        {
            inventorySlots[i].SetActive(true);
        }
    }
    // Returns healthCounter
    public int GetHealthCount()
    {
        return healthCounter;
    }
    // Returns true if inventory is full
    public bool Full()
    {
        return isFull;
    }
    // Returns true if inventory is empty
    public bool Empty()
    {
        return isEmpty;
    }
    // Adds object to inventory by storing its name
    public void Add(GameObject obj)
    {
        string name = obj.name.Replace("(Clone)", "");

        if (name == "Health")
        {
            healthCounter++;
        }
        else if (obj.tag == "Enemy")
        {
            // playerController uses the slash to see where the color data begins
            name += "/" + (int)obj.GetComponent<EnemyScript>().eColors;
        }
        
        inventory.Push(name);

        Destroy(obj);

        gameManager.SuctionPoints();

        names.Add(name);
        DisplayInventory();
    }
    // Removes object name from inventory and returns it
    public string Remove()
    {
        if(inventory.Peek() == "Health")
        {
            healthCounter--;
        }

        names.RemoveAt(names.Count - 1);
        DisplayInventory();

        return inventory.Pop();
    }
    // Handles the visual of the items in the inventory
    private void DisplayInventory()
    {
        int j = 0;
        for(int i = names.Count - 1; i > -1; i--)
        {
            if(names[i] == "Health" || names[i] == "Shot")
            {
                slotText[j].text = names[i];
                slotText[j].color = colors[0];
            }
            else
            {
                int dataIndex = names[i].IndexOf("/");
                int color = int.Parse(names[i].Substring(dataIndex + 1));
                string deleteSection = names[i].Substring(names[i].IndexOf(" ")); // Enemy's name is Enemy Ranged. this removes everything after the character y
                slotText[j].text = names[i].Replace(deleteSection, "");
                slotText[j].color = colors[color];
            }

            j++;
        }

        while(j < currentSize)
        {
            slotText[j].text = "";
            j++;
        }
    }
}
