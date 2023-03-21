using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class EnemyCollision : MonoBehaviour
{
    public GameObject[] items;
    private PlayerController playerController;
    
    private void Start()
    {
        var player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            int rate = UnityEngine.Random.Range(0, 10);
            if(rate <= 3)
            {
                Instantiate(items[rate],transform.position, Quaternion.identity);
            }

            playerController.hp -= 10;
            Destroy(gameObject);
        }
    }
}
