using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private float timer = 0;

    void Update()
    {
        transform.Translate(Vector3.forward * 10 * Time.deltaTime);

        if(timer < 1.5)
        {
            timer += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
