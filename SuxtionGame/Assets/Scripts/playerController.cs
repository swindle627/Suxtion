using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float health  = 100;

    public float inventoryCount, pierceCount, suctionRange, expelRange, speed;

    // Start is called before the first frame update
    void Start()
    {
        
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
