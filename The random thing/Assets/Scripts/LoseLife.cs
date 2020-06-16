using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseLife : MonoBehaviour
{
    
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void LosingLife(int value)
    {
        if(value == 2)
        {
            animator.Play("vida");
        }
    }
    
}
