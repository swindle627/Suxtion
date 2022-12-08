using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreObject : MonoBehaviour
{
    private InventoryScript inventory;

    private void Start()
    {
        inventory = FindObjectOfType<InventoryScript>();
    }
    // Adds objects to inventory when they collide with the suction point
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Suckable")
        {
            inventory.Add(other.gameObject);
        }
    }
}
