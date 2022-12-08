using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    private Stack<string> inventory = new Stack<string>();
    private float currentSize;
    private bool isFull = false;
    private bool isEmpty = true;
    private int healthCounter = 0;

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
        inventory.Push(obj.name.Replace("(Clone)", ""));

        if(inventory.Peek() == "Health")
        {
            healthCounter++;
            Debug.Log("Health Count: " + healthCounter);
        }

        Destroy(obj);
        //Debug.Log("Stored " + inventory.Peek() + ". " + (currentSize - inventory.Count) + " slots free");
    }
    // Removes object name from inventory and returns it
    public string Remove()
    {
        //Debug.Log("Removing " + inventory.Peek() + ". " + (currentSize - inventory.Count - 1) + " slots free");
        if(inventory.Peek() == "Health")
        {
            healthCounter--;
        }

        return inventory.Pop();
    }
}
