using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : EnemyScript
{
    [Header("Enemy Color Materials")]
    public Material[] enemyMaterials;

    public string temp;

    private MeshRenderer meshRenderer;
    private int state;
    private float distance; // distance from player
    private float backUpTimer = 0, chaseTimer = 0;
    private bool isBacking = false, canAttack = true;
    private float moveSpeed = 3;

    void Start()
    {
        player = FindObjectOfType<playerController>().gameObject;

        if(eColors == EnemyColors.blank)
        {
            eColors = (EnemyColors)Random.Range(1, 4);
        }

        meshRenderer = gameObject.GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        meshRenderer.material = enemyMaterials[(int)eColors];
        ChangeState();
        RunStates();
    }
    private void ChangeState()
    {
        if(distance > 14)
        {
            state = 0;
        }
        else if(distance > 10)
        {
            isBacking = false;
            backUpTimer = 0;
            state = 1;
        }
        else if(distance < 10 && isBacking == false)
        {
            backUpTimer += Time.deltaTime;

            if(backUpTimer > 1.5)
            {
                isBacking = true;
                state = 2;
                backUpTimer = 0;
            }
        }
    }
    private void RunStates()
    {
        if(state == 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
            temp = "Moving to player";
        }
        else if(state == 1)
        {
            // enemy will stay still and fire at the player. 
            temp = "Staying still and firing";
        }
        else if(state == 2)
        {
            // when the enemy is too close to the player they will wait 1.5 seconds before backing up
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, -moveSpeed * Time.deltaTime);
            temp = "Moving away from player";
        }
    }

    // Enemy won't attack while being sucked up and will resume attacking when it is not being sucked up
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Suction Range")
        {
            canAttack = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Suction Range")
        {
            canAttack = true;
        }
    }
}
