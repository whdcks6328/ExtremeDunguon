using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    private EnemyHPController hpc;

    private Buttonamager b;
    // Start is called before the first frame update
    void Start()
    {
        hpc = GetComponent<EnemyHPController>();
        b = GetComponent<Buttonamager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hpc.enemyHP <= 0)b.GotoWinScene();
    }
}
