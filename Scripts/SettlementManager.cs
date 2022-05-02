using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;

public class SettlementManager : MonoBehaviour
{

    public TextMeshProUGUI Name;
    public TextMeshProUGUI Type;
    public TextMeshProUGUI Slots;
    public TextMeshProUGUI UsedSlots;
    public TextMeshProUGUI Faction;
    public TextMeshProUGUI Hq;
    public TextMeshProUGUI Resources;
    public Button SetHQBttn; 

    public static int hqSelector;
    public static string settlmentName;
    public static string[] nameLst = { "George Town", "Los Pollos", "San Bernadine", "Victoria" };
    public static int cityNameIndex;
    bool hqFound = false;

    // Start is called before the first frame update
    void Start()
    {

        ReadMainFile();

    }

    public void ReadMainFile()
    {

        //Read the settlments file
        //the hq if set will be shown

        int index = 0;

        string[] lines = File.ReadAllLines("Settlements.txt");

        foreach (string line in lines)
        {

            Debug.Log(line);
            string[] splitline = lines[index].Split(',');            

            if(splitline[1] == "1")
            {

                Name.text = $"{nameLst[index] + "(HQ)"}";
                hqFound = true;
                SetHQBttn.enabled = false;


            }

            index++;

        }

        if(hqFound != true)
        {

            Debug.Log("No Hq Found");

        }


    }

    public void setHQ()
    {

        Debug.Log(cityNameIndex);
        hqFound = true;

        string[] lines = File.ReadAllLines("Settlements.txt");

        string[] splitline = lines[cityNameIndex].Split(',');

        splitline[1] = "1";

        //write to file all the elements of line by rebuilding the lines array
        string output1 = splitline[0] + "," + splitline[1];
        Debug.Log(output1);
        lines[cityNameIndex] = output1;
        using (StreamWriter writer = new StreamWriter("Settlements.txt"))
        {
            foreach (string line in lines)
            {

                writer.WriteLine(line);

            }
        }


        Hq.text = $"{"Headquarters"}";

        string localfilename = "Settlement" + cityNameIndex + ".txt";

        string[] lines2 = File.ReadAllLines(localfilename);

        string[] splitline2 = lines2[0].Split(',');

        splitline2[4] = "1";

        string output2 = splitline2[0] + "," + splitline2[1] + "," + splitline2[2] + "," + splitline2[3] + "," + splitline2[4] + "," + splitline2[5];
        Debug.Log(output2);

        using (StreamWriter writer = new StreamWriter(localfilename))
        {
 
            writer.WriteLine(output2);
            writer.WriteLine(lines2[1]);
            writer.WriteLine(lines2[2]);

        }


    }

    // Update is called once per frame
    public void DisplayAttributes(int index)
    {

        //Each settlment has a text file
        //first line is the displayed attributes
        //line 2 is the current buildings
        //line 3 is current effects
        string filename = "Settlement" + index + ".txt";
        string[] lines = File.ReadAllLines(filename);
        string[] splitline = lines[0].Split(',');

        Type.text = $"{splitline[0]}";
        Slots.text = $"{splitline[1]}";
        Faction.text = $"{splitline[3]}";
        Resources.text = $"{lines[2]}";

        if (splitline[4] == "1")
        {

            Hq.text = $"{"Headquarters"}";
            Name.text = $"{nameLst[index] + " (HQ)"}";

        }
        else
        {

            Hq.text = $"{"HQ located elsewhere"}";
            Name.text = $"{nameLst[index]}";

        }

        cityNameIndex = index;

    }

    public bool CheckSlots(int index)
    {
        int slots = 0;
        int comparisson;

        string filename = "Settlement" + index + ".txt";
        string[] lines = File.ReadAllLines(filename);
        string[] splitline1 = lines[0].Split(',');
        string[] splitline2 = lines[1].Split(',');

        slots = Int32.Parse(splitline1[1]);

        for(int i = 0; i < slots; i++)
        {

            comparisson = Int32.Parse(splitline2[i]);

            if (comparisson == 0)
            {

                return true;

            }
        }

        
        return false;

    }

    public bool CheckResource(int index, int resourceid)
    {

        int comparisson;

        string filename = "Settlement" + index + ".txt";
        string[] lines = File.ReadAllLines(filename);
        string[] splitline1 = lines[2].Split(',');

        for (int i = 0; i < 4; i++)
        {

            comparisson = Int32.Parse(splitline1[i]);

            if (comparisson == resourceid)
            {

                return true;

            }
        }

        Debug.Log("Resource not found");
        return false;

    }
}