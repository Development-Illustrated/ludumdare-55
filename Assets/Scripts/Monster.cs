using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public enum MonsterState
{
    Passive,
    Angry
}

public class Monster : BaseMonster
{
    public override void Activate()
    {
        bool result = RequestManager.instance.CheckRequest(this);
        if (result)
        {
            Destroy(gameObject);
        }
        else
        {
            SetState(MonsterState.Angry);
        }
    }
}
