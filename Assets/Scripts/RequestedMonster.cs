using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestedMonster : BaseMonster
{
    // Defaults to 25
    [HideInInspector] public int durationInSeconds;

    private IEnumerator TimeoutRequest()
    {
        yield return new WaitForSeconds(durationInSeconds);
        RequestManager.instance.TimeoutRequest(this);
    }

    // Override the Activate methdod from the Monster class
    public override void Activate()
    {
        StartCoroutine(TimeoutRequest());
    }
}
