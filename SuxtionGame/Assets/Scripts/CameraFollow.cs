using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<playerController>().gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position;
    }
}
