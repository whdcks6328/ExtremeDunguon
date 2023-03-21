using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject Burst;
    int damage, moveType;
    float speed, angle, scale;
    float _speed;
    Vector3 OriginPos;
    float variable;
    public Vector3 vec;
    public void SetBulletStatus(Vector3 pos, float speed, float angle, int damage, int moveType, float scale = 3, float variable = 0)
    {
        this.transform.position = pos;
        OriginPos = pos;
        this.speed = speed;
        this.angle = angle;
        this.damage = damage;
        this.moveType = moveType;
        this.scale = scale;
        this.variable = variable;
    }
    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = scale * Vector3.one;
        switch (moveType)
        {

            case 6:
                StartCoroutine(Curve());
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position - OriginPos).magnitude >= 30f) Destroy(this.gameObject);
        _speed = speed * Time.deltaTime;
        time += Time.deltaTime;

        switch (moveType)
        {
            case 0:
                PlayerBullet();
                break;
            case 1:
                MovePatternOne();
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                IdleAndProceed();
                break;
            case 5:
                GenerateStraight();
                break;
            case 6:
                break;
            case 7:
                Swell();
                break;
            case 8:
                Proceed();
                break;
            default:
                MovePatternOne();
                break;
        }

    }

    public int ShowDamage()
    {
        return damage;
    }
    public void setAngle(float value)
    {
        angle = value;
    }

    void PlayerBullet()
    {
        transform.rotation = Quaternion.Euler(0, -angle+90, 0);
        transform.Translate(_speed * Mathf.Cos(angle * Mathf.Deg2Rad), 0, _speed * Mathf.Sin(angle * Mathf.Deg2Rad), Space.World);
    }

    void MovePatternOne()
    {
        transform.Translate(((time < 0.2f) ? _speed * 3 : _speed) * Mathf.Cos(angle * Mathf.Deg2Rad), 0, ((time < 0.2f) ? _speed * 3 : _speed) * Mathf.Sin(angle * Mathf.Deg2Rad));
    }

    void IdleAndProceed()
    {
        float start = 2f;
        if (start < time)
        {
            transform.Translate(_speed * time * Mathf.Cos(angle * Mathf.Deg2Rad), 0, _speed * time * Mathf.Sin(angle * Mathf.Deg2Rad));
        }
    }

    void GenerateStraight()
    {
        if (time < 1f)
        {
            transform.Translate(_speed * Mathf.Cos(angle * Mathf.Deg2Rad), 0, _speed * Mathf.Sin(angle * Mathf.Deg2Rad));
        }
        else
        {
            GameObject bullet = GameObject.FindGameObjectWithTag("BulletResources").GetComponent<BulletResources>().bullets[0];
            for (int i = 6; i < 13; i++)
            {
                GameObject b = Instantiate(bullet);
                b.GetComponent<Bullet>().SetBulletStatus(transform.position, i / 2f, angle + 160, 10, 1, 3);
            }
            Destroy(gameObject);
        }
    }

    IEnumerator Curve()
    {
        Vector3 defPos = transform.position;

        while (true)
        {
            angle += (angle <= 0 ? -Time.deltaTime : Time.deltaTime) * 40f;
            transform.position = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)) * time * 2f + defPos;
            yield return null;
        }
    }

    void Swell()
    {
        transform.Translate(((time < 0.2f) ? _speed * 3 : _speed) * Mathf.Cos(angle * Mathf.Deg2Rad), 0, ((time < 0.2f) ? _speed * 3 : _speed) * Mathf.Sin(angle * Mathf.Deg2Rad));
        transform.localScale = Vector3.one * Mathf.Clamp(time * 4f + 2f, 0, 10);
    }
    void Proceed()
    {
        transform.Translate(vec * _speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(this.gameObject.tag == "PlayerTeam" && other.gameObject.tag == "Enemy")
        {
            BulletDestroy();
        }
        else if(this.gameObject.tag == "EnemyTeam" && other.gameObject.tag == "Player")
        {
            BulletDestroy();
        }
        else if(other.gameObject.tag == "Wall")
        {
            BulletDestroy();
        }
    }
    void BulletDestroy()
    {
        GameObject Particle = Instantiate(Burst, transform.position, Quaternion.identity);
        Destroy(Particle, 2.0f);
        Destroy(this.gameObject);
    }
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
