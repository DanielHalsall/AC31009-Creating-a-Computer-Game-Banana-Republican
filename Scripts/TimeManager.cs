using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;

public class TimeManager : MonoBehaviour
{

    public static Action OnDayChanged;
    public static Action OnMonthChanged;
    public static Action OnYearChanged;

    // public actions for the changes in the time and date
    public static int Day;
    public static int Month;
    public static int Year;

    // private variables for this class
    // the maximum date values
    // max day is 30 because months shouldn't have more or less than that
    private int maxDay = 30;
    private int maxMonth = 12;
    private float timer;
    private float dayToRealTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("time start");
        Timeload();
        //call time load function
        timer = dayToRealTime;

    }

    public void Timeload()
    {

        string[] lines = File.ReadAllLines("Player.txt");
        string[] splitline = lines[1].Split(',');

        Day = Int32.Parse(splitline[0]);
        Month = Int32.Parse(splitline[1]);
        Year = Int32.Parse(splitline[2]);
        //write to file all the elements of line by rebuilding the lines array

        Debug.Log("loaded date" + Day + Month + Year);

    }

    public void SaveDate()
    {

        string[] lines = File.ReadAllLines("Player.txt");
        string[] splitline = lines[1].Split(',');

        //write to file all the elements of line by rebuilding the lines array

        Debug.Log("loaded date" + Day + Month + Year);

        string output = splitline[0] + "," + splitline[1] + "," + splitline[2];

        using (StreamWriter writer = new StreamWriter("Player.txt"))
        {

            writer.Flush();
            writer.WriteLine(lines[0]);
            writer.WriteLine(output);
            writer.Close();

        }

    }

    // Update is called once per frame
    // Here we update the days based on the setting of delta time
    // We will cycle through the 7 week days, incrementing the week index then increasing the month index
    // this is the basis of the levels
    void Update()
    {

        timer -= Time.deltaTime;


        if(timer <= 0)
        {

            Day++;
            OnDayChanged?.Invoke();

            if (Day >= maxDay)
            {

                Day = 0;
                Month++;
                OnMonthChanged?.Invoke();

                if(Month >= maxMonth)
                {

                    Month = 0;
                    Year++;
                    OnYearChanged?.Invoke();

                }

            }

            timer = dayToRealTime;

        }

    }
}
