using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;
    private bool right, left, up, down, noVertical, noHorizontal;
    private Rigidbody rb;
    private Transform t;
    public int hp = 100;
    public bool isDead;
    private UIIndicator uiIndicator;
    
    private AudioSource audioSource;
    public AudioClip Damage;
    public AudioClip Recovery;
    public AudioClip Weapon;


    Vector3 dir;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        t = GetComponent<Transform>();
        right = false;
        left = false;
        up = false;
        down = false;
        var ui = GameObject.FindWithTag("UI");
        uiIndicator = ui.GetComponent<UIIndicator>();
        audioSource = GetComponent<AudioSource>();

        dir = Vector3.zero;
    }

    float _t = 0;
    bool dirIsChanged = true;
    Vector3 prevDir;
    Vector3 prevDirForDetection;
    float prevAngle;

    void Update()
    {
        prevDirForDetection = dir;
        if (dirIsChanged)
        {
            prevDir = dir;
            prevAngle = transform.rotation.eulerAngles.y;
            _t = 0;
            dirIsChanged = false;
        }
        _t += Time.deltaTime * 3f;
        dir.x = (int)Input.GetAxisRaw("Horizontal");
        dir.z = (int)Input.GetAxisRaw("Vertical");
        dir = dir.normalized * speed;
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, prevAngle, 0), Quaternion.Euler(0, angle, 0), _t);
        if (prevDirForDetection != dir && !(dir.x == 0 && dir.z == 0))
        {
            dirIsChanged = true;
        }


    }
    void FixedUpdate()
    {
        rb.velocity = dir;
    }

    /*
    void Update()
    {
        //移動　入力を受け取る
        if (Input.GetKeyDown(KeyCode.A) || (Input.GetKey(KeyCode.A) && Input.GetKeyUp(KeyCode.D)))
        {
            left = true;
        }
        if (Input.GetKeyDown(KeyCode.D) || (Input.GetKey(KeyCode.D) && Input.GetKeyUp(KeyCode.A)))
        {
            right = true;
        }
        if (Input.GetKeyDown(KeyCode.W) || (Input.GetKey(KeyCode.W) && Input.GetKeyUp(KeyCode.S)))
        {
            up = true;
        }
        if (Input.GetKeyDown(KeyCode.S) || (Input.GetKey(KeyCode.S) && Input.GetKeyUp(KeyCode.W)))
        {
            down = true;
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            noHorizontal = true;
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            noVertical = true;
        }
        if (Input.GetKey(KeyCode.Q)) transform.Rotate(0f, 1f, 0f);
    }

    void FixedUpdate()
    {
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            rb.velocity = Vector3.zero;
        }
        //移動
        if (noHorizontal || left || right)
        {
            rb.velocity = new Vector3(0, 0,rb.velocity.z);
            noHorizontal = false;
        }
        if (noVertical || up || down)
        {
            rb.velocity = new Vector3(rb.velocity.x,0,0);
            noVertical = false;
        }

        if (left)
        {
            rb.velocity += Vector3.left * speed;
            left = false;
        }
        if (up)
        {
            rb.velocity += Vector3.forward * speed;
            up = false;
        }
        if (right)
        {
            rb.velocity += Vector3.right * speed;
            right = false;
        }

        if (down)
        {
            rb.velocity += Vector3.back * speed;
            down = false;
        }
        if (rb.velocity != Vector3.zero)
        {
            rb.velocity = rb.velocity.normalized * speed;
        }

    }*/

    private void OnTriggerEnter(Collider other)
    {
        //ダメージ受ける処理
        if (other.gameObject.tag == "EnemyTeam")
        {
            hp -= other.GetComponent<Bullet>().ShowDamage();
            audioSource.PlayOneShot(Damage);
            uiIndicator.SetHP(hp);
            if (hp <= 0)
            {
                GetComponent<Buttonamager>().GameOverScene();
            }
        }else if (other.gameObject.tag == "Item")
        {
            if (other.GetComponent<ItemManager>().itemType == ItemManager.ItemType.weaponItem)
            {
                audioSource.PlayOneShot(Weapon);
            }
            else
            {
                audioSource.PlayOneShot(Recovery);
            }
            
        }
    }

}
