using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    int hp = 100;
    bool isDead = false;

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            //死亡判定
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            hp -= 10;
            if (hp <= 0) isDead = true;
        }
        if(other.gameObject.tag == "EnemyTeam")
        {
            hp -= other.gameObject.GetComponent<Bullet>().ShowDamage();
            if (hp <= 0) isDead = true;
        }
    }
}
