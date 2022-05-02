using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;

public class Balance : MonoBehaviour
{

    public TextMeshProUGUI balancetxt;
    public TextMeshProUGUI Popup;
    public GameObject PopupPanel;
    public static int balance = 0;
    public static int monthspassed = 0;
    public static int settlementcount = 2;
    public BuildMenu build;

    //checks the month has changed through
    void Start()
    {

        Balanceload();
        TimeManager.OnMonthChanged += MonthlyIncome;
        BuildMenu.BuildModifyBalance += UpdateBalance;
        Factions.FactionModifyBalance += UpdateBalance;

    }

    public void Balanceload()
    {

        string[] lines = File.ReadAllLines("Player.txt");
        string[] splitline = lines[0].Split(',');

        balance = Int32.Parse(splitline[2]);
        //write to file all the elements of line by rebuilding the lines array

        DisplayBalance();

        if (balance == -100)
        {

            Popup.text = $"{"You Have ran out of money GAME OVER" + "\nYou can still stick around if you want to watch your wealth waste away!"}";
            PopupPanel.SetActive(true);

        }


    }

    // Start is called before the first frame update
    public void MonthlyIncome()
    {

        monthspassed++;
        //on month change calculate the amount of money the player gains or loses
        balance = balance + CalculateBuildingRevenue();
        Debug.Log("months passed " + monthspassed);
        //Calculate benefits from faction support
        //Factions will help with tax/wages/cutcorners which will save money


        //once all checks are done display
        DisplayBalance();

        //player file update
        string[] lines = File.ReadAllLines("Player.txt");
        string[] splitline = lines[0].Split(',');

        splitline[2] = balance.ToString();

        string output = splitline[0] + "," + splitline[1] + "," + splitline[2];

        using (StreamWriter writer = new StreamWriter("Player.txt"))
        {

            writer.Flush();
            writer.WriteLine(lines[0]);
            writer.WriteLine(output);
            writer.Close();

        }

    }

    public void DisplayBalance()
    {

        //Display the balance onto the balance text
        balancetxt.text = $"{"$"+balance:000000}";

    }

    public int SendBalance()
    {

        //Sends the current balance to any class or sub that needs it
        return balance;

    }

    //On purchase of a building or faction interaction or quest completion
    //modify the balance from here
    public void UpdateBalance()
    {

        balance = balance - BuildMenu.currbal;
        DisplayBalance();

    }

    public int CalculateBuildingRevenue()
    {

        int val = 0;
        int itterator;
        int buildingindex;
        string filename1;
        string filename2;

        for (int i = 0; i < settlementcount; i++)
        {

            itterator = 0;

            filename1 = "Settlement" + i + ".txt";

            string[] lines1 = File.ReadAllLines(filename1);
            string[] splitline1 = lines1[1].Split(',');

            foreach (string line in splitline1)
            {

                buildingindex = Int32.Parse(splitline1[itterator]);
                Debug.Log("building itterator" + itterator);

                //open the file and read in the desired buildings content
                filename2 = "Buildings" + i + ".txt";
                string[] lines2 = File.ReadAllLines(filename2);
                string[] splitline2 = lines2[buildingindex].Split(',');

                val = val + Int32.Parse(splitline2[2]);
                val = val + Int32.Parse(splitline2[3]);
                Debug.Log("val");
                Debug.Log(val);
                itterator++;

            }

        }

        //calculate how much the relationships will make
        int capitalistmod = 0;
        int communistmod = 0;
        int govmod = 0;

        string[] lines3 = File.ReadAllLines("Factions.txt");

        string[] splitline3 = lines3[0].Split(',');
        string[] splitline4 = lines3[1].Split(',');
        string[] splitline5 = lines3[2].Split(',');

        capitalistmod = Checkmodval(Int32.Parse(splitline3[1]));
        communistmod = Checkmodval(Int32.Parse(splitline4[1]));
        govmod = Checkmodval(Int32.Parse(splitline5[1]));

        val = val + capitalistmod + communistmod + govmod;

        return val;

    }

    public int Checkmodval(int modval)
    {

        //the value the opinion takes or adds to the balance
        int extent = 0;

        switch (modval)
        {
            case -100:

                extent = -1000;
                break;

            case -80:

                extent = -800;
                break;

            case -60:

                extent = -600;
                break;

            case -40:

                extent = -400;
                break;

            case -20:

                extent = -200;
                break;

            case 0:

                extent = 0;
                break;

            case 20:

                extent = 50;
                break;

            case 40:

                extent = 100;
                break;

            case 60:

                extent = 150;
                break;

            case 80:

                extent = 200;
                break;

            case 100:

                extent = 300;
                break;
        }

        return extent;

    }

}
