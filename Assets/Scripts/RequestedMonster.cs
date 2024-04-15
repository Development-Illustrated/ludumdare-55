using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RequestedMonster : BaseMonster
{
    // Defaults to 25
    [HideInInspector] public int durationInSeconds;
    [SerializeField]
    private TextMeshProUGUI countdownText;

    private IEnumerator TimeoutRequest()
    {
        int counter = durationInSeconds;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
            countdownText.text = counter.ToString();
        }
        RequestManager.instance.TimeoutRequest(this);
    }

    // Override the Activate methdod from the Monster class
    public override bool Activate()
    {
        countdownText.text = durationInSeconds.ToString();
       
        StartCoroutine(TimeoutRequest());

        return true;
    }
}
