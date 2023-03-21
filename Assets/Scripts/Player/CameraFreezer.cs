using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFreezer : MonoBehaviour
{
    Vector3 playerPos;
    Vector3 cameraBuff;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cameraBuff = new Vector3(0f, 15f, -4f);
    }
    private void FixedUpdate()
    {
        playerPos = GameObject.FindWithTag("Player").transform.position;
        playerPos += cameraBuff;
        transform.position = playerPos;
    }
}
