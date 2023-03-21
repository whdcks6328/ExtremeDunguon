using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    Transform shotT;
    PlayerShooter shooter;
    [SerializeField]
    Transform cursor;
    [SerializeField]
    LineRenderer line;


    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        shotT = player.transform.Find("shotPos");
        shooter = player.GetComponent<PlayerShooter>();

    }

    void Update()
    {
        Vector3 cursorPosForLine = cursor.position;
        cursorPosForLine.y = shotT.position.y;
        line.SetPosition(0, shotT.position);
        line.SetPosition(1, cursorPosForLine);
        cursor.position = shooter.hitPos;
        cursor.rotation = Quaternion.Euler(90f, -Mathf.Atan2(cursor.position.z - shotT.position.z, cursor.position.x - shotT.position.x) * Mathf.Rad2Deg + 90f, 0);

        cursor.localScale = Vector3.one * (Input.GetMouseButton(0) ? 4f : 3f);
    }

}
