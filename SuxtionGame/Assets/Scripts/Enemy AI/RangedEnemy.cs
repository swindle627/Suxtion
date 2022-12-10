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
    private float backUpTimer = 0, projectileTimer = 0;
    private bool isBacking = false, fireProjectile = true;
    private float moveSpeed = 3;
    private GameManager gameManager;

    void Start()
    {
        player = FindObjectOfType<playerController>().gameObject;
        gameManager = FindObjectOfType<GameManager>();

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

        if(!fireProjectile && projectileTimer < 1)
        {
            projectileTimer += Time.deltaTime;
        }
        else
        {
            projectileTimer = 0;
            fireProjectile = true;
        }

        // make enemies faster as game goes on
        if(gameManager.GetLevel() == 2)
        {
            moveSpeed = 6;
        }
        else if(gameManager.GetLevel() == 3)
        {
            moveSpeed = 8;
        }
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
            transform.LookAt(player.transform);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
        else if(state == 1)
        {
            // enemy will stay still and fire at the player.
            transform.LookAt(player.transform);
            if (fireProjectile)
            {
                Instantiate(Resources.Load("Projectile"), transform.position, transform.rotation);
                fireProjectile = false;
            }
        }
        else if(state == 2)
        {
            // when the enemy is too close to the player they will wait 1.5 seconds before backing up
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, -moveSpeed * Time.deltaTime);
        }
    }
}
