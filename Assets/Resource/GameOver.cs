using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    AudioClip[] se;
    [SerializeField]
    AudioSource audioSource;

    void Start()
    {
        audioSource.clip = se[Random.Range(0, 2)];
        audioSource.Play();
    }


}
