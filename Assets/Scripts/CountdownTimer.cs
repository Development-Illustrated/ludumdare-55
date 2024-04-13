using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public float countdownTime;
    public TMP_Text countdownText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (countdownTime > 1)
        {
            countdownTime -= Time.deltaTime;
            countdownText.text = Mathf.Floor(countdownTime).ToString();
        }
        else
        {
            countdownText.text = "GO!";
            // enable player
        }
    }
}
