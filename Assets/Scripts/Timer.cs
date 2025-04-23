using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private bool timeFreezed = false;

    // Update is called once per frame
    void Update()
    {
        if (!timeFreezed)
        {
            timerText.text = Time.timeSinceLevelLoad.ToString();
        }
        
    }

    public void FreezeTime()
    {
        timeFreezed=true;
    }
}
