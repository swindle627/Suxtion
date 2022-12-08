using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private float speed = 15;
    public float damage;
    private float range;
    private float pointValue = 100;
    private float pierce;
    private float timer = 0;

    private void Start()
    {
        playerController pc = FindObjectOfType<playerController>();
        range = pc.expelRange;
        pierce = pc.pierceCount;
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
            Debug.Log("Dealt " + damage + " damage.");
            // get enemy current health
            // deal damage to enemy
            // if current health - damage is less than zero add pointValue to GameManager

            pierce--;

            if(pierce == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
