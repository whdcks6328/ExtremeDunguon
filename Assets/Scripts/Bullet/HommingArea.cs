using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ホーミングエリアの設定。ホーミング力は物凄く強い。
public class HommingArea : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Vector3 toEnemy = other.gameObject.transform.position - transform.root.position;
            float newAngle = Mathf.Atan2(toEnemy.z, toEnemy.x) * Mathf.Rad2Deg;
            transform.root.GetComponent<Bullet>().setAngle(newAngle);
        }
    }
}
