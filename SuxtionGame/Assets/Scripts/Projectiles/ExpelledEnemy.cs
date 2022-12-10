using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpelledEnemy : EnemyScript
{
    [Header("Enemy Color Materials")]
    public Material[] enemyMaterials;

    [Header("Enemy To Spawn")]
    public GameObject spawnEnemy;

    private float speed = 15;
    private float timer = 0;
    private float range;
    private MeshRenderer meshRenderer;
    private GameManager gameManager;

    private void Start()
    {
        playerController pc = FindObjectOfType<playerController>();
        range = pc.expelRange;

        gameManager = FindObjectOfType<GameManager>();
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        meshRenderer.material = enemyMaterials[(int)eColors];

        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        timer += Time.deltaTime;

        if (timer > range)
        {
            GameObject enemy = Instantiate(spawnEnemy, gameObject.transform.position, gameObject.transform.rotation);
            enemy.GetComponent<EnemyScript>().Setup((int)eColors);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy" && timer < range)
        {
            if(eColors != other.GetComponent<EnemyScript>().eColors) // non-color match kill
            {
                gameManager.upgradePoints += gameManager.enemyKillValue;
                gameManager.KillPoints();
                gameManager.KillPoints();
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
            else // color match kill
            {
                gameManager.upgradePoints += gameManager.colorKillValue;
                gameManager.KillPoints();
                gameManager.KillPoints();
                gameManager.ColorMatchPoints();
                Collider[] enemies = other.GetComponent<EnemyScript>().Explode();

                foreach(Collider enemy in enemies)
                {
                    if(enemy.tag == "Enemy" && enemy.gameObject != other.gameObject)
                    {
                        gameManager.upgradePoints += gameManager.basicKillValue;
                        gameManager.KillPoints();
                        gameManager.AreaPoints();
                        Destroy(enemy.gameObject);
                    }
                }

                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
