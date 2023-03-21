using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPController : MonoBehaviour
{
    public int enemyHP;
    public GameObject[] items;
    private PlayerController playerController;
    
    private void Start()
    {
        var player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
    }

    [SerializeField]
    GameObject particle;
    
    private AudioSource audioSource;
    public AudioClip Damage;
    public AudioClip Defeat;
    
    //死亡処理
    private void Die()
    {
        AudioSource.PlayClipAtPoint(Defeat,transform.position);
        int rate = UnityEngine.Random.Range(0, 15);
        if(rate <= 3)
        {
            Instantiate(items[rate],transform.position, Quaternion.identity);
        }
        GameObject p = Instantiate(particle);
        p.transform.position = transform.position;
        p.transform.localScale = transform.localScale;
        Destroy(p, 2f);
        Destroy(gameObject);
    }


    IEnumerator DamageColor()
    {
        SkinnedMeshRenderer smr = GetComponentInChildren<SkinnedMeshRenderer>();
        smr.material.SetColor("_EmissionColor", Color.red);
        yield return new WaitForSeconds(0.1f);
        smr.material.SetColor("_EmissionColor", Color.white);
        yield break;
    }

    private void OnTriggerEnter(Collider other)
    {
        //ダメージ受ける処理
        if (other.gameObject.tag == "PlayerTeam")
        {
            audioSource.PlayOneShot(Damage);
            StartCoroutine(DamageColor());
            enemyHP -= other.GetComponent<Bullet>().ShowDamage();
            if (enemyHP <= 0)
            {
                Die();
            }
        }else if (other.gameObject.tag == "Player")
        {
            playerController.hp -= 3;
        }
    }
}
