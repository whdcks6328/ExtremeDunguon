using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletRotator : MonoBehaviour
{
    private PlayerShooter playerShooter;
    private Vector3 enemy;
    
    void Start()
    {
        var player = GameObject.FindWithTag("Player");
        playerShooter = player.GetComponent<PlayerShooter>();
        enemy = playerShooter.hitPos;
        transform.LookAt(enemy);
    }
}
