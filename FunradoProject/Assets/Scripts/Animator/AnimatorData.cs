using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorData : MonoBehaviour
{
    public Animator animator;

    public string idleAnimation;
    public string walkAnimation;
    public string attackAnimation;
    public string deathAnimation;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }
}
