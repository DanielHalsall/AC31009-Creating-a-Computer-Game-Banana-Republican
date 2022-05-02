using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;

public class BuildMenu : MonoBehaviour
{

    public static Action BuildModifyBalance;

    //The TMPGUI objects that are to be accessed
    //fufills all aspects of the build menu
    public TextMeshProUGUI Name1;
    public TextMeshProUGUI Name2;
    public TextMeshProUGUI Name3;
    public TextMeshProUGUI Name4;
    public TextMeshProUGUI Stats1;
    public TextMeshProUGUI Stats2;
    public TextMeshProUGUI Stats3;
    public TextMeshProUGUI Stats4;
    public TextMeshProUGUI Popup;
    public GameObject PopupPanel;
    public TextMeshProUGUI Build1txt;
    public TextMeshProUGUI Build2txt;
    public TextMeshProUGUI Build3txt;
    public TextMeshProUGUI Build4txt;

    //All the build buttons
    //To be disabled if building is built or slots are full
    public Button Build1;
    public Button Build2;
    public Button Build3;
    public Button Build4;

    //object declaration
    //some times works, other times does not
    //use for alternative class access
    public Balance balanceobj;
    public Factions factionobj;

    //Class specific variables
    private static string filename;
    public static int cityIndex;
    public static int currbal;

    public void DisplayAttributes()
    {

        //Each settlment has a buildings menu text file
        //These files decide what can and can't be built on a settlment
        //Each text file relates to the hard coded settlment ability
        //The abilities pertaining to a predetermined role
        //Towns = agriculture, Cities = heavy industry
        filename = "Buildings" + cityIndex + ".txt";

        string[] lines = File.ReadAllLines(filename);
        string[] splitline1 = lines[1].Split(',');

        Name1.text = $"{splitline1[0]}";
        Stats1.text = $"{"Cost $" + splitline1[1] + "\nMonthly cost " + splitline1[2] + "\nMonthly Income $" + splitline1[3] + "\nNeeded Resource " + splitline1[4] + "\nProduct " + splitline1[5] + "\nFaction Sponsor " + splitline1[6] + "\nSupport amount " + splitline1[7]}";

        //Next building
        string[] splitline2 = lines[2].Split(',');

        Name2.text = $"{splitline2[0]}";
        Stats2.text = $"{"Cost $" + splitline2[1] + "\nMonthly cost " + splitline2[2] + "\nMonthly Income $" + splitline2[3] + "\nNeeded Resource " + splitline2[4] + "\nProduct " + splitline2[5] + "\nFaction Sponsor " + splitline2[6] + "\nSupport amount " + splitline2[7]}";

        //Next building
        string[] splitline3 = lines[3].Split(',');

        Name3.text = $"{splitline3[0]}";
        Stats3.text = $"{"Cost $" + splitline3[1] + "\nMonthly cost " + splitline3[2] + "\nMonthly Income $" + splitline3[3] + "\nNeeded Resource " + splitline3[4] + "\nProduct " + splitline3[5] + "\nFaction Sponsor " + splitline3[6] + "\nSupport amount " + splitline3[7]}";

        //Next building
        string[] splitline4 = lines[4].Split(',');

        Name4.text = $"{splitline4[0]}";
        Stats4.text = $"{"Cost $" + splitline4[1] + "\nMonthly cost " + splitline4[2] + "\nMonthly Income $" + splitline4[3] + "\nNeeded Resource " + splitline4[4] + "\nProduct " + splitline4[5] + "\nFaction Sponsor " + splitline4[6] + "\nSupport amount " + splitline4[7]}";

        CityCheck();

    }

    public void setIndex(int index)
    {
        //Modify the city index
        cityIndex = index;

    }

    public void CityCheck()
    {

        //Read the Building file
        //The slots will be checked
        //If Slots are full, disable the build buttons and display message on buttons

        //slot increment is the number of slots in use
        int slotIncrement = 0;
        //slot index is the location of a building
        int slotIndex = 0;
        //slot val is the string of slot increment
        string slotVal;

        string[] lines = File.ReadAllLines("Settlement" + cityIndex + ".txt");
        string[] splitline1 = lines[0].Split(',');
        string maxSlot = splitline1[1];
        string[] splitline2 = lines[1].Split(',');
        Debug.Log("max slot and lines 1");
        Debug.Log(maxSlot);
        Debug.Log(lines[1]);

        foreach (string val in splitline2)
        {

            if (splitline2[slotIncrement] != "0")
            {

                slotIndex = slotIncrement;
                slotVal = slotIncrement.ToString();
                //Check if the max slots have been reached
                //if so lock the buttons so nothing can be built
                if(slotVal == maxSlot)
                {

                    Build1.enabled = false;
                    Build2.enabled = false;
                    Build3.enabled = false;
                    Build4.enabled = false;

                }

                //check the cities building slots
                //if there are buildings in the slots check which buildings they are
                //disable the build buttons for currently built buildings              
                switch (Int32.Parse(splitline2[slotIndex]))
                {
                    case 1:

                        Build1.enabled = false;
                        Build1txt.text = $"{"Owned"}";
                        Debug.Log("Button 1 Disabled");
                        break;

                    case 2:

                        Build2.enabled = false;
                        Build2txt.text = $"{"Owned"}";
                        Debug.Log("Button 2 Disabled");
                        break;

                    case 3:

                        Build3.enabled = false;
                        Build3txt.text = $"{"Owned"}";
                        Debug.Log("Button 3 Disabled");
                        break;

                    case 4:

                        Build4.enabled = false;
                        Build4txt.text = $"{"Owned"}";
                        Debug.Log("Button 4 Disabled");
                        break;

                }


                slotIncrement++;

            }
        }
    }

    //Here we will check the balance against the price of a building
    //If it is less than price refuse purchase
    //Check the settlement has the require resource to buy the building
    //Check the City has open slots

    public void Build(int buildingid)
    {
        //check balance is enough to buy the building
        int checkbal;
        checkbal= Balance.balance;
        int pricecompare;
        string resourcecomparebuilding;
        string resourcecomparesettlement;

        string filename1 = "Buildings" + cityIndex + ".txt";
        string filename2 = "Settlement" + cityIndex + ".txt";

        string[] lines1 = File.ReadAllLines(filename1);
        string[] lines2 = File.ReadAllLines(filename2);

        string[] splitline0 = lines1[buildingid].Split(',');
        string[] splitline1 = lines2[2].Split(',');

        pricecompare = Int32.Parse(splitline0[1]);
        resourcecomparebuilding = splitline0[5];



        if (checkbal < pricecompare)
        {

            //purchase fail, inform player
            Popup.text = $"{"You do not have the funds for this building"}";
            PopupPanel.SetActive(true);

        }
        else if(checkbal >= pricecompare)
        {

            int i = 0;
            while(i < 4)
            {

                resourcecomparesettlement = splitline1[i];

                if (resourcecomparesettlement != resourcecomparebuilding)
                {

                    //purchase fail, inform player
                    Popup.text = $"{"You do not have the correct resource for this building"}";
                    PopupPanel.SetActive(true);
                    i++;

                }
            }

            if(i >= 4)
            {

                //Buy the building
                Buy(resourcecomparebuilding, pricecompare, buildingid);

                //remove money from balance
                int newbal;
                newbal = Balance.balance;
                newbal = newbal - pricecompare;


                //Inform player of purchase
                Popup.text = $"{"You have purchased the building"}";
                PopupPanel.SetActive(true);

                //Disable the Button
                switch (buildingid)
                {
                    case 1:

                        Build1.enabled = false;
                        Build1txt.text = $"{"Owned"}";

                        break;

                    case 2:

                        Build2.enabled = false;
                        Build2txt.text = $"{"Owned"}";
                        break;

                    case 3:

                        Build3.enabled = false;
                        Build3txt.text = $"{"Owned"}";
                        break;

                    case 4:

                        Build4.enabled = false;
                        Build4txt.text = $"{"Owned"}";
                        break;

                }
            }
        }
    }

    public void Buy(string resource, int buildingprice, int buildingID)
    {

        //Open player balance and remove funds
        currbal = Balance.balance;
        currbal = currbal - buildingprice;
        BuildModifyBalance?.Invoke();

        //increase occupied slots by 1 in settlment file
        //Add building to building list
        //Add resource to resource list
        filename = "Settlement" + cityIndex + ".txt";

        string[] lines = File.ReadAllLines(filename);
        //accidentally flipped the variables with the accessing splitlines
        string[] splitline = lines[2].Split(',');
        string[] splitline1 = lines[1].Split(',');
        string[] splitline2 = lines[0].Split(',');
        bool resourceplaced = false;
        bool buildingplaced = false;

        //resouce and building slot indices
        int resourceindex = 0;
        int buildingindex = 0;

        //while to find the empty slot for the resource to occupy
        while(resourceplaced != true)
        {

            if (splitline[resourceindex] == "None")
            {

                Debug.Log("resourceindex");
                Debug.Log(splitline[resourceindex]);
                splitline[resourceindex] = resource;
                resourceplaced = true;

            }
            else
            {

                resourceindex++;

            }
        }

        //while to find the empty slot for the building to occupy
        while (buildingplaced != true)
        {

            if (splitline1[buildingindex] == "0")
            {

                splitline1[buildingindex] = buildingID.ToString();
                buildingplaced = true;
                
            }
            else
            {

                buildingindex++;

            }
        }

        //recreate the splitlines
        string output2 = splitline[0] + "," + splitline[1] + "," + splitline[2] + "," + splitline[3];

        //splitline1 can contain either 2 or 3 slots
        //use the settlements slots variable to find out how many to use
        string output1 = "";
        int slotmax = Int32.Parse(splitline2[1]);
        Debug.Log(slotmax);

        for (int i = 0; i < slotmax; i++)
        {

            if (i == (slotmax - 1))
            {

                output1 = output1 + splitline1[i];

            }
            else
            {

                output1 = output1 + splitline1[i] + ",";

            }

        }

        Debug.Log("modified outputs");
        Debug.Log(lines[0]);
        Debug.Log(output1);
        Debug.Log(output2);


        //using the found slots add the buildings attributes to them
        using (StreamWriter writer = new StreamWriter("Settlement" + cityIndex + ".txt"))
        {

            writer.Flush();
            writer.WriteLine(lines[0]);
            writer.WriteLine(output1);
            writer.WriteLine(output2);
            writer.Close();

        }


        //Call faction support function to calculate change in opinion
        //faction action
        FactionInteraction(buildingID);

    }
    
    //open the building file, using the building id find
        //factionid and faction relationship modifier
    public void FactionInteraction(int buildingID)
    {

        int relationshipmod;
        int factionID;

        filename = "Buildings" + cityIndex + ".txt";

        string[] lines = File.ReadAllLines(filename);
        string[] splitline = lines[buildingID].Split(',');

        relationshipmod = Int32.Parse(splitline[7]);
        factionID = Int32.Parse(splitline[6]);

        //call factionmodify with passed values
        factionobj.ModifyRelationship(relationshipmod, factionID);

    }
}
