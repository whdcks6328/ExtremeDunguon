using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    GameObject[] bullets;
    void Start()
    {
        bullets = GameObject.FindGameObjectWithTag("BulletResources").GetComponent<BulletResources>().bullets;
    }


    //全方向に弾を発射。number>0で要望
    public void ShootPatternOne(int number)//十字方向でたまを出すやつ。
    {
        GameObject bullet;
        float basedAngle = (float)(360 / number);
        for (int i = 0; i < number; i++)
        {
            bullet = Instantiate(bullets[0]);
            bullet.GetComponent<Bullet>().SetBulletStatus(transform.position, 1.5f, basedAngle * i, 10, 1);
        }
    }

    //自機狙いの奇数レーン
    public void ShootPatternTwo(int laneNumber, float baseAngle, float speed)//自機狙い
    {
        Vector3 toPlayer = GameObject.FindWithTag("Player").transform.position - this.transform.position;
        float angle = Mathf.Atan2(toPlayer.normalized.z, toPlayer.normalized.x) * Mathf.Rad2Deg;
        GameObject bullet = Instantiate(bullets[0]);
        bullet.GetComponent<Bullet>().SetBulletStatus(transform.position, speed, angle, 10, 1, 3);
        int maxIndex = (laneNumber - 1) / 2;
        for (int i = 1; i <= maxIndex; i++)
        {
            bullet = Instantiate(bullets[0]);
            bullet.GetComponent<Bullet>().SetBulletStatus(transform.position, speed, angle + baseAngle * i, 10, 1, 3);
            bullet = Instantiate(bullets[0]);
            bullet.GetComponent<Bullet>().SetBulletStatus(transform.position, speed, angle - baseAngle * i, 10, 1, 3);
        }
    }
    public void Ring(int ringCount, float speed)
    {
        Vector3 toPlayer = GameObject.FindWithTag("Player").transform.position - this.transform.position;
        float angle = Mathf.Atan2(toPlayer.normalized.z, toPlayer.normalized.x) * Mathf.Rad2Deg;
        GameObject bullet;
        float baseAngle = 360f / (ringCount - 1);
        for (int i = 0; i <= ringCount - 1; i++)
        {
            bullet = Instantiate(bullets[1]);
            bullet.GetComponent<Bullet>().SetBulletStatus(transform.position, speed, angle + baseAngle * i, 10, 1, 3);
        }
    }
    public void IdleAndProceed(Vector3 pos, float angle, int bulletType)
    {
        GameObject bullet;
        bullet = Instantiate(bullets[bulletType]);
        bullet.GetComponent<Bullet>().SetBulletStatus(pos, 2f, angle, 10, 4, 3);
    }

    public void RingAndStraight(int ringCount, float speed)
    {
        Vector3 toPlayer = GameObject.FindWithTag("Player").transform.position - this.transform.position;
        float angle = Mathf.Atan2(toPlayer.normalized.z, toPlayer.normalized.x) * Mathf.Rad2Deg;
        GameObject bullet;
        float baseAngle = 360f / (ringCount);
        for (int i = 0; i < ringCount; i++)
        {
            bullet = Instantiate(bullets[3]);
            bullet.GetComponent<Bullet>().SetBulletStatus(transform.position, speed, angle + baseAngle * i, 10, 5, 2);
        }
    }
    public void RingAndCurve(int ringCount, bool isCW)
    {
        GameObject bullet;
        float baseAngle = 360f / (ringCount);
        for (int i = 1; i <= ringCount; i++)
        {
            bullet = Instantiate(bullets[(isCW ? 0 : 1)]);
            bullet.GetComponent<Bullet>().SetBulletStatus(transform.position, 3, baseAngle * i * (isCW ? -1 : 1), 10, 6, 3);
        }
    }
    public void Swell(float offset)
    {
        Vector3 toPlayer = GameObject.FindWithTag("Player").transform.position - this.transform.position;
        float angle = Mathf.Atan2(toPlayer.normalized.z, toPlayer.normalized.x) * Mathf.Rad2Deg + offset;
        GameObject bullet = Instantiate(bullets[0]);
        bullet.GetComponent<Bullet>().SetBulletStatus(transform.position, 3, angle, 10, 7, 3);
    }
    public void Proceed(Vector3 vec)
    {
        GameObject bullet = Instantiate(bullets[0]);
        bullet.GetComponent<Bullet>().SetBulletStatus(transform.position, 3, 0, 10, 8, 3);
        bullet.GetComponent<Bullet>().vec = vec;
    }

}



