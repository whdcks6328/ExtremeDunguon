using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    //現在弾の種類を知らない為一種類だけ仮定してみた。
    [SerializeField] GameObject[] PlayerBullets;
    private float speed, angle;
    public Vector3 hitPos;
    [SerializeField] private float[] rate = { 0.5f, 0.5f, 0.38f };
    float timeCounter = 0;

    private AudioSource audioSource;
    public AudioClip Homing;
    public AudioClip Constant;
    public AudioClip Laser;

    [SerializeField]
    Transform shotT;


    //武器の種類
    public enum weapon
    {
        homing = 0, constant = 1, laser = 2
    }
    public weapon playerWeapon;　//今プレイヤーが装備してる武器
    public int level;　//今の武器レベル

    private void SelectShoot()
    {
        switch (playerWeapon)
        {
            case weapon.homing:
                if (timeCounter >= rate[0])
                {
                    speed = 7f;
                    audioSource.volume = 0.1f;
                    ConstantShoot(level, 0, 2, Homing);
                    timeCounter = 0;
                    audioSource.volume = 1.0f;
                }
                break;
            case weapon.constant:
                if (timeCounter >= rate[1])
                {
                    speed = 12f;
                    ConstantShoot(level, 2, 5, Constant);
                    timeCounter = 0;
                }
                break;
            case weapon.laser:
                if (timeCounter >= rate[2] / (level * 1.3f))
                {
                    speed = 12f;
                    Shoot(1, 1, Laser);
                    timeCounter = 0;
                }
                break;
        }
    }

    void ConstantShoot(int lv, int type, int damage, AudioClip sound)
    {
        audioSource.clip = sound;
        audioSource.Play();
        GameObject Bullet = Instantiate(PlayerBullets[type]);
        Bullet.GetComponent<Bullet>().SetBulletStatus(shotT.position, speed, angle, damage, 0, 2);
        if (lv == 1)
            return;
        Bullet = Instantiate(PlayerBullets[type]);
        Bullet.GetComponent<Bullet>().SetBulletStatus(shotT.position, speed, angle + 15f, damage, 0, 2);

        Bullet = Instantiate(PlayerBullets[type]);
        Bullet.GetComponent<Bullet>().SetBulletStatus(shotT.position, speed, angle - 15f, damage, 0, 2);
        if (lv == 2)
            return;

        Bullet = Instantiate(PlayerBullets[type]);
        Bullet.GetComponent<Bullet>().SetBulletStatus(shotT.position, speed, angle + 30f, damage, 0, 2);

        Bullet = Instantiate(PlayerBullets[type]);
        Bullet.GetComponent<Bullet>().SetBulletStatus(shotT.position, speed, angle - 30f, damage, 0, 2);
        if (lv == 3)
            return;

        Bullet = Instantiate(PlayerBullets[type]);
        Bullet.GetComponent<Bullet>().SetBulletStatus(shotT.position, speed, angle + 45f, damage, 0, 2);

        Bullet = Instantiate(PlayerBullets[type]);
        Bullet.GetComponent<Bullet>().SetBulletStatus(shotT.position, speed, angle - 45f, damage, 0, 2);
        if (lv == 4)
            return;

        Bullet = Instantiate(PlayerBullets[type]);
        Bullet.GetComponent<Bullet>().SetBulletStatus(shotT.position, speed, angle + 60f, damage, 0, 2);

        Bullet = Instantiate(PlayerBullets[type]);
        Bullet.GetComponent<Bullet>().SetBulletStatus(shotT.position, speed, angle - 60f, damage, 0, 2);

    }

    private void Start()
    {
        level = 1;
        playerWeapon = weapon.constant;
        audioSource = GetComponent<AudioSource>();
    }

    //引数は調整するかもしれない
    public void Shoot(int type, int damage, AudioClip sound)
    {
        audioSource.clip = sound;
        audioSource.Play();
        GameObject Bullet = Instantiate(PlayerBullets[type]);
        Bullet.GetComponent<Bullet>().SetBulletStatus(shotT.position, speed, angle, damage, 1);
    }

    private void Update()
    {
        //カメラからカーソルへRayを飛ばし、ぶつかった座標（hitPos）を得る。
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int layerMask = (1 << LayerMask.NameToLayer("HitPanel")); //適当なレイヤーマスクを設定する

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            hitPos = hit.point;
            hitPos.y = shotT.position.y;
        }

        //hitPosへの角度を得る。
        Quaternion lookRotation = Quaternion.LookRotation(hitPos - transform.position, Vector3.up);
        angle = lookRotation.eulerAngles.y;

        //transform.Rotate(0f, angle - transform.rotation.eulerAngles.y, 0f);
        angle = 90f - angle;

        timeCounter += Time.deltaTime;

        if (Input.GetMouseButton(0))
        {
            SelectShoot();
        }
    }
}
