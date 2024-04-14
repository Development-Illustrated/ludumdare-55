using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            SetState(MonsterState.Angry);
        }
    }
}
