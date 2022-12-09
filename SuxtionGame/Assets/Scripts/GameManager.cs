using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // when game ends just turn off the player script and all enemy scripts

    public float upgradePoints = 0;
    public float basicKillValue = 100; // kill enemy with object
    public float enemyKillValue = 200; // kill enemy with an enemy
    public float colorKillValue = 400; // kill enemy with an enemy of matching color

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
