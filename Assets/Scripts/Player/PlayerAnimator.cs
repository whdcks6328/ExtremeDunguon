using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator animator;
    // Update is called once per frame
    void Update()
    {
        animator.SetBool("WALK",false);
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            animator.SetBool("WALK",true);
        }
    }
}
