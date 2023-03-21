using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{

    private Rigidbody enemyRb;
    private Rigidbody playerRb;
    private GameObject player;
    public float speed;
    private float time;
    public bool isActive;
    public GameObject enemyWeapon;
    
    public enum moveType
    {
        Freeze, Chase, Line
    }
    [SerializeField] public moveType type;

    
    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        enemyWeapon.active = false;
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
        playerRb = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            enemyWeapon.active = true;
            switch (type)
            {
                case moveType.Freeze :
                    break;
                
                case moveType.Chase :
                    Chase();
                    break;
                case moveType.Line :
                    Line();
                    break;
            }
        }
        else
        {
            enemyWeapon.active = false;
        }
    }
    

    private void Chase()
    {
        enemyRb.position = Vector3.MoveTowards(enemyRb.position, playerRb.position, speed * Time.deltaTime);
        Quaternion lookRotation = Quaternion.LookRotation(player.transform.position - transform.position, Vector3.up);
        lookRotation.z = 0;
        lookRotation.x = 0;
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 1.0f);   
    }
    
    private void Line()
    {
        time += Time.deltaTime;

        transform.localPosition += new Vector3(speed * Time.deltaTime, 0, 0);
        if (time >= 1.0f)
        {
            speed *= -1;
            time = 0;
        }
    }

}
