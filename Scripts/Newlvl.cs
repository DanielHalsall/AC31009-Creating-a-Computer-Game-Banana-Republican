using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;

public class Newlvl : MonoBehaviour
{
    
    public static bool playernamecomplete = false;
    public static bool coynamecomplete = false;

    //Start Button will be disabled on start and re-enabled on name completion
    public Button Start;

    public void OnEnable()
    {

        Start.enabled = false;

    }

    public void PlayerNameComplete(bool playertorf)
    {

        playernamecomplete = playertorf;
        Debug.Log(playernamecomplete);

    }

    public void CoyComplete(bool coytorf)
    {

        coynamecomplete = coytorf;
        Debug.Log(coynamecomplete);

    }

    public void NamesComleted()
    {

        if (playernamecomplete == true && coynamecomplete == true)
        {

            //activate the start button
            Start.enabled = true;

        }

    }

    //open each settlement and 
    public void SetCitiesToEmpty()
    {

        for(int j = 0; j < 2; j++)
        {

            string[] lines = File.ReadAllLines("Settlement" + j + ".txt");

            string[] splitline1 = lines[0].Split(',');
            string[] splitline2 = lines[1].Split(',');

            //write to file all the elements of line by rebuilding the lines array
            string output1 = splitline1[0] + "," + splitline1[1] + "," + "0" + "," + splitline1[3] + "," + "0" + "," + splitline1[5];
            string output2 = "";
            int increment = 0;
            Debug.Log(output1);

            foreach (string val in splitline2)
            {

                increment++;

            }

            Debug.Log(increment);

            increment = increment - 1;

            output2 = "0" + ",";

            for (int i = 0; i < increment; i++)
            {

                if (i == (increment - 1))
                {

                    Debug.Log(output2);
                    output2 = output2 + "0";

                }
                else
                {

                    Debug.Log(output2);
                    output2 = output2 + "0" + ",";

                }
            }

            string output3 = "None" + "," + "None" + "," + "None" + "," + "None";

            Debug.Log("passed for");
            Debug.Log(output2);
            Debug.Log(output1);
            lines[0] = output1;
            lines[1] = output2;
            lines[2] = output3;

            using (StreamWriter writer = new StreamWriter("Settlement" + j + ".txt"))
            {

                writer.Flush();
                writer.WriteLine(lines[0]);
                writer.WriteLine(lines[1]);
                writer.WriteLine(lines[2]);
                writer.Close();

            }
        }
        
    }

    //We will set all changed values to 0
    public void SetFactionsToNew()
    {

        string[] lines = File.ReadAllLines("Factions.txt");

        string[] splitline1 = lines[0].Split(',');
        string[] splitline2 = lines[1].Split(',');
        string[] splitline3 = lines[2].Split(',');

        //write to file all the elements of line by rebuilding the lines array
        string output1 = splitline1[0] + "," + "0";
        string output2 = splitline2[0] + "," + "0";
        string output3 = splitline3[0] + "," + "0";

        Debug.Log(output1);
        Debug.Log(output2);
        Debug.Log(output3);

        lines[0] = output1;
        lines[1] = output2;
        lines[2] = output3;
        using (StreamWriter writer = new StreamWriter("Factions.txt"))
        {

            writer.Flush();
            writer.WriteLine(lines[0]);
            writer.WriteLine(lines[1]);
            writer.WriteLine(lines[2]);
            writer.Close();

        }

    }

    public void ResetSettlementFile()
    {

        string[] lines = File.ReadAllLines("Settlements.txt");

        string[] splitline1 = lines[0].Split(',');
        string[] splitline2 = lines[1].Split(',');

        //write to file all the elements of line by rebuilding the lines array
        string output1 = "0" + "," + "0";
        string output2 = "1" + "," + "0";
        string output3 = "1" + "," + "0";
        string output4 = "0" + "," + "0";

        Debug.Log(output1);
        Debug.Log(output2);
        Debug.Log(output3);
        Debug.Log(output4);

        lines[0] = output1;
        lines[1] = output2;
        lines[2] = output3;
        lines[3] = output4;
        using (StreamWriter writer = new StreamWriter("Settlements.txt"))
        {

            writer.Flush();
            writer.WriteLine(lines[0]);
            writer.WriteLine(lines[1]);
            writer.WriteLine(lines[2]);
            writer.WriteLine(lines[3]);
            writer.Close();

        }

    }

    public void SetPlayerFile()
    {

        string[] lines = File.ReadAllLines("Player.txt");

        string[] splitline1 = lines[0].Split(',');
        string[] splitline2 = lines[1].Split(',');

        //write to file all the elements of line by rebuilding the lines array
        string output1 = splitline1[0] + "," + splitline1[1] + "," + "3000";
        string output2 = "01" + "," + "01" + "," + "1970";
        Debug.Log(output1);
        Debug.Log(output2);

        lines[0] = output1;
        lines[1] = output2;
        using (StreamWriter writer = new StreamWriter("Player.txt"))
        {

            writer.Flush();
            writer.WriteLine(lines[0]);
            writer.WriteLine(lines[1]);
            writer.Close();

        }


    }

}
