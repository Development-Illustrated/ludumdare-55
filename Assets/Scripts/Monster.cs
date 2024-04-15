using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public enum MonsterState
{
    Building,
    Passive,
    Angry,
    Loving
}

public class Monster : BaseMonster
{
    public override bool Activate()
    {
        bool doesMonsterMatchRequest = RequestManager.instance.CheckRequest(this);
        if (doesMonsterMatchRequest)
        {
            Destroy(gameObject);
            return true;
        }
        else
        {
            RequestManager.instance.DecreaseDuration(10);
            RequestManager.instance.DecreaseDelay(10);
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
            return false;
        }
    }
}
