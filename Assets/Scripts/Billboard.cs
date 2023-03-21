using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Transform cameraTransform;
    void Start()
    {
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    void Update()
    {
        transform.LookAt(cameraTransform.position);

    }
}
