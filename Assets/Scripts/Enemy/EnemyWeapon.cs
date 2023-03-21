using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public EnemyMover EnemyMover;

    public enum weaponType
    {
        DoNothing, MultiDirection, Aim, tako1, tako2, tako3, tako4, tako5, tako6
    }
    [SerializeField] public weaponType type;
    [SerializeField] public EnemyShooter shooter;
    [SerializeField, Header("Aimで使用")] public float angle;
    [SerializeField, Header("Tako5以外で使用")] public int bulletNum;
    private void OnEnable()
    { 
        
        switch (type)
        {
            case weaponType.DoNothing:
                break;
            case weaponType.MultiDirection:
                //十字弾撃たせる
                StartCoroutine("MultiDirection");
                break;
                
            case weaponType.Aim:
                //自機狙い弾撃たせる
                StartCoroutine("Aim");
                break;
            case weaponType.tako1:
                StartCoroutine("Tako1");
                break;
            case weaponType.tako2:
                StartCoroutine("Tako2");
                break;
            case weaponType.tako3:
                StartCoroutine("Tako3");

                break;
            case weaponType.tako4:
                StartCoroutine("Tako4");
                break;
            case weaponType.tako5:
                StartCoroutine("Tako5");
                break;
            case weaponType.tako6:
                StartCoroutine("Tako6");
                break;
        }
    }

    IEnumerator MultiDirection()
    {
        yield return null;
        while (true)
        {
            shooter.ShootPatternOne(bulletNum);
            yield return new WaitForSeconds(1.0f);
        }
    }

    IEnumerator Aim()
    {
        yield return null;
        while (true)
        {
            shooter.ShootPatternTwo(bulletNum,　angle, 3f);
            yield return new WaitForSeconds(2.0f);
        }
    }

    IEnumerator Tako1()
    {
        yield return null;
        while (true)
        {
            shooter.Ring(bulletNum, 2.5f);
            yield return new WaitForSeconds(0.2f);
            for (int i = 7; i < 13; i++)
            {
                shooter.ShootPatternTwo(1, 2, i / 2f);
            }
            yield return new WaitForSeconds(3.0f);
        }
    }
    IEnumerator Tako2()
    {
        yield return null;
        Vector3[] verts = new Vector3[5];
        for (int i = 0; i < 5; i++) verts[i] = new Vector3(Mathf.Cos(360 / 5f * i * Mathf.Deg2Rad), 0, Mathf.Sin(360 / 5f * i * Mathf.Deg2Rad)) * 3f;
        Vector3 pos;
        int[] vertOrder = { 0, 3, 1, 4, 2 ,0};
        while (true)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < bulletNum; j++)
                {
                    pos = Vector3.Lerp(verts[vertOrder[i]], verts[vertOrder[i + 1]], 1f / bulletNum * j);
                    shooter.IdleAndProceed(pos + transform.position, 30f * j, i % 2);
                    yield return new WaitForSeconds(0.01f);
                }
            }
            yield return new WaitForSeconds(3.0f);
        }
    }
    IEnumerator Tako3()
    {
        yield return null;
        while (true)
        {
            shooter.RingAndStraight(bulletNum, 3f);
            yield return new WaitForSeconds(3.0f);
        }
    }
    IEnumerator Tako4()
    {
        yield return null;
        bool CW = false;
        while (true)
        {
            CW = (CW ? false : true);
            shooter.RingAndCurve(bulletNum, CW);
            yield return new WaitForSeconds(1.4f);
        }
    }
    IEnumerator Tako5()
    {
        yield return null;
        int t = 0;
        while (true)
        {
            t++;
            if (t % 30 == 0)
            {
                yield return new WaitForSeconds(1f);
            }
            for (int i = 0; i < 3; i++)
            {
                shooter.Swell((i % 3) * 120f + UnityEngine.Random.Range(-20f, 20f));
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator Tako6()
    {
        yield return null;
        while (true)
        {
            float angle = 180f / bulletNum;
            for (int i = 0; i < bulletNum; i++)
            {
                shooter.Proceed(new Vector3(Mathf.Cos(angle * i * Mathf.Deg2Rad), Mathf.Sin(angle * i * Mathf.Deg2Rad), 0));
                shooter.Proceed(new Vector3(0, Mathf.Sin(angle * i * Mathf.Deg2Rad), Mathf.Cos(angle * i * Mathf.Deg2Rad)));

            }
            for (int i = 0; i < bulletNum*2; i++)
            {
                shooter.Proceed(new Vector3(Mathf.Cos(angle * i * Mathf.Deg2Rad), 0, Mathf.Sin(angle * i * Mathf.Deg2Rad)));
            }
            yield return new WaitForSeconds(2f);
        }
    }

}
