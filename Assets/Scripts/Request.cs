using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestedMonster : Monster
{
    // Defaults to 25
    public int durationInSeconds;

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
