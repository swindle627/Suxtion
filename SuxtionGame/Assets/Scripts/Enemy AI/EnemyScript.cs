using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyScript : MonoBehaviour
{
    public enum EnemyColors { blank, red, green , purple }
    public EnemyColors eColors = new EnemyColors();
    public float aoeRadius = 6;
    protected GameObject player;

    // used to setup the color and health for active and expelled enemies
    public void Setup(int color)
    {
        eColors = (EnemyColors)color;
    }
    // returns an array of all enemies in AoE range
    public virtual Collider[] Explode()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, aoeRadius);

        return enemies;
    }
}
