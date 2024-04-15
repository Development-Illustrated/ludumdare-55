using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public enum MonsterState
{
    Building,
    Passive,
    Loving,
    Angry
}

public class Monster : BaseMonster
{
    public override void Activate()
    {
        bool doesMonsterMatchRequest = RequestManager.instance.CheckRequest(this);
        if (doesMonsterMatchRequest)
        {
            Destroy(gameObject);
        }
        else
        {
            this.GetComponent<NavMeshAgent>().enabled = true;
            BoxCollider[] colliders = this.GetComponents<BoxCollider>();
            Animator[] animators = this.GetComponents<Animator>();
            foreach(Animator animator in animators)
            {
                animator.enabled = true;
            }

            foreach(BoxCollider collider in colliders) 
            {
                collider.enabled = false;
            }
            SetState(MonsterState.Passive);
        }
    }
}
