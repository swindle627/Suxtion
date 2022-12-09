using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private float speed = 15;
    private float range;
    private float pointValue = 100;
    private float pierce;
    private float timer = 0;
    private GameManager gameManager;

    private void Start()
    {
        playerController pc = FindObjectOfType<playerController>();
        range = pc.expelRange;
        pierce = pc.pierceCount;

        gameManager = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        timer += Time.deltaTime;

        if(timer > range)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            gameManager.upgradePoints += gameManager.basicKillValue;

            pierce--;

            if(pierce == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
