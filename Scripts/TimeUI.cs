using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeUI : MonoBehaviour
{

    public TextMeshProUGUI timeText;

    private void OnEnable()
    {

        TimeManager.OnDayChanged += UpdateTime;
        TimeManager.OnMonthChanged += UpdateTime;
        TimeManager.OnYearChanged += UpdateTime;
        Debug.Log("time enabled");

    }

    private void OnDisable()
    {

        TimeManager.OnDayChanged -= UpdateTime;
        TimeManager.OnMonthChanged -= UpdateTime;
        TimeManager.OnYearChanged -= UpdateTime;
        Debug.Log("time disabled");

    }

    private void UpdateTime()
    {

        //update the timer on the UI
        timeText.text = $"{TimeManager.Day:00}:{TimeManager.Month:00}:{TimeManager.Year:0000}";

    }

}
