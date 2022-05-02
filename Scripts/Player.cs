using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;

public class Player : MonoBehaviour
{

    //TMP objects used to display player attributes
    public TextMeshProUGUI PlayerName;
    public TextMeshProUGUI CoyName;
    public TextMeshProUGUI Balance;

    //Store the names once set until the game is closed
    public static string playername;
    public static string coyname;
    public static int playerBalance;

    //Set the player name in-game and in the text file
    public void SetPlayerName(string playernameparsed)
    {

        playername = playernameparsed;
        Debug.Log(playername);

        string[] lines = File.ReadAllLines("Player.txt");

        string[] splitline = lines[0].Split(',');

        //write to file all the elements of line by rebuilding the lines array
        string output1 = playername + "," + splitline[1] + "," + splitline[2];

        Debug.Log(output1);

        lines[0] = output1;
        using (StreamWriter writer = new StreamWriter("Player.txt"))
        {

            writer.Flush();
            writer.WriteLine(lines[0]);
            writer.WriteLine(lines[1]);
            writer.Close();

        }
    }

    public void SetCoyName(string coynameparsed)
    {

        coyname = coynameparsed;
        Debug.Log(coyname);

        string[] lines = File.ReadAllLines("Player.txt");

        string[] splitline = lines[0].Split(',');

        //write to file all the elements of line by rebuilding the lines array
        string output1 = splitline[0] + "," + coyname + "," + splitline[2];

        Debug.Log(output1);

        lines[0] = output1;
        using (StreamWriter writer = new StreamWriter("Player.txt"))
        {

            writer.Flush();
            writer.WriteLine(lines[0]);
            writer.WriteLine(lines[1]);
            writer.Close();

        }

    }

    public void ModfiyPlayerFile()
    {

        string[] lines = File.ReadAllLines("Settlements.txt");

        string[] splitline = lines[0].Split(',');

        //write to file all the elements of line by rebuilding the lines array
        string output1 = playername + "," + coyname + "," + splitline[2];
        Debug.Log(output1);

        using (StreamWriter writer = new StreamWriter("Player.txt"))
        {

            writer.WriteLine(output1);

        }
    }

    public void SetBalanceInGame(int parsedBal)
    {

        //increase the balance using the ingame calculator

    }

    public void DisplayAttributes()
    {

        string[] line = File.ReadAllLines("Player.txt");
        Debug.Log(line);
        string[] splitline = line[0].Split(',');

        PlayerName.text = $"{splitline[0]}";
        CoyName.text = $"{splitline[1]}";
        Balance.text = $"{splitline[2]}";

    }

}
