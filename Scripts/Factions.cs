using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;

public class Factions : MonoBehaviour
{

    //Faction modify balance action
    public static Action FactionModifyBalance;


    //tmp elements to display infor to the player
    public TextMeshProUGUI Name1;
    public TextMeshProUGUI Name2;
    public TextMeshProUGUI Name3;
    public TextMeshProUGUI Stats1;
    public TextMeshProUGUI Stats2;
    public TextMeshProUGUI Stats3;

    //pop up for player alerts
    public TextMeshProUGUI Popup;
    public GameObject PopupPanel;

    //Class variables
    public int globalfactionID;


    //Factions will have benefits for their support
    //The player can interact with Factions to modify their "support value"
    //The support value will dictate what quests and benefits are available to the player

    public void ModifyRelationship(int relmodify, int factionid)
    {

        //only changes the faction relationship nothing else
        //file open
        //splitline on the faction id thats passed
        string[] lines = File.ReadAllLines("Factions.txt");
        string[] splitline = lines[factionid].Split(',');
        string newrelationval;
        int currrelationval = Int32.Parse(splitline[1]);

        //set the new relation value to an int
        //then set it to a string before adding to the splitline[val] it came from
        currrelationval = currrelationval + relmodify;
        newrelationval = currrelationval.ToString();
        splitline[1] = newrelationval;

        //recreate the line we have edited
        lines[factionid] = splitline[0] + "," + splitline[1] + "," + splitline[2];

        using (StreamWriter writer = new StreamWriter("Factions.txt"))
        {

            writer.Flush();
            writer.WriteLine(lines[0]);
            writer.WriteLine(lines[1]);
            writer.WriteLine(lines[2]);
            writer.Close();

        }

    }

    public void Interaction(int interactionID)
    {

        int relationship = CheckRelationship();
        int playerbal = 5000;
        //playerbal = Balance.balance;
        int newbal;
        int tries = 0;
        
        //hard code the interaction values as there is only 2 for the game
        //if more add to text file or array
        int interact1 = 5000;
        int interact2 = 2500;

        //call send bal to check if they have enough
        //check relationship is correct parameters for them to access
        //intimidate has to have a negative value
        //bribe must be a positive

        //check balance then progress
        if(interactionID == 1)
        {

            if (playerbal >= interact1)
            {

                //progress
                //must be positive
                Debug.Log("relationship1" + relationship);
                if (relationship > 0 && relationship < 90)
                {

                    //continue
                    ModifyRelationship(20, globalfactionID);
                    newbal = playerbal - interact1;
                    Debug.Log("newbal1" + newbal);
                    FactionModifyBalance?.Invoke();

                }
                else
                {

                    //wrong relationship val
                    Popup.text = $"{"You cannot intimidate this faction right now"}";
                    PopupPanel.SetActive(true);

                }


            }
            else
            {

                //add 1 to the purchase tries. if it reaches 2 inform the player they are broke
                tries++;

            }

        }
        else if(interactionID == 2)
        {

            if (playerbal >= interact2)
            {

                //must be negative
                Debug.Log("relationship2" + relationship);
                if (relationship < 0 && relationship >= -90)
                {

                    //continue
                    ModifyRelationship(10, globalfactionID);
                    newbal = playerbal - interact2;
                    Debug.Log("newbal2" + newbal);
                    FactionModifyBalance?.Invoke();

                }
                else
                {

                    //wrong relationship val
                    Popup.text = $"{"You cannot intimidate this faction right now"}";
                    PopupPanel.SetActive(true);

                }


            }
            else
            {

                //add 1 to the purchase tries. if it reaches 2 inform the player they are broke
                tries++;

            }

        }

        if(tries > 1)
        {

            Popup.text = $"{"You do not have the money for this"}";
            PopupPanel.SetActive(true);

        }
    }

    public void DisplayAttributes()
    {

        string[] lines = File.ReadAllLines("Factions.txt");
        string[] splitline0 = lines[0].Split(',');

        Name1.text = $"{splitline0[0]}";
        Stats1.text = $"{"Opinion " + splitline0[1]}";

        //Next Faction
        string[] splitline1 = lines[1].Split(',');

        Name2.text = $"{splitline1[0]}";
        Stats2.text = $"{"Opinion " + splitline1[1]}";

        //Next Faction
        string[] splitline2 = lines[2].Split(',');

        Name3.text = $"{splitline2[0]}";
        Stats3.text = $"{"Opinion " + splitline2[1]}";

    }

    public int CheckRelationship()
    {

        int relationship = 0;

        string[] lines = File.ReadAllLines("Factions.txt");
        string[] splitline1 = lines[globalfactionID].Split(',');

        relationship = Int32.Parse(splitline1[1]);

        return relationship;

    }
}
