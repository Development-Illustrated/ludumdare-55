using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] AnimationClip animationClip;

    void Start()
    {
        animator.Play(animationClip.name);
    }
}
