using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if( other.gameObject.tag == "Player")
        {
            isActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    { 
        if( other.gameObject.tag == "Player")
        {
            isActive = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if( other.gameObject.tag == "Enemy")
        {
            other.GetComponent<EnemyMover>().isActive = isActive;
        }
    }
}
