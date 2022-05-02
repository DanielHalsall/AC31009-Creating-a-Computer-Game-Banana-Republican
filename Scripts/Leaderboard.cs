using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LootLocker.Requests;
using TMPro;
using System;
using System.IO;

//Leaderboard class
//Here we will run as much of the leaderboard as possible
public class Leaderboard : MonoBehaviour
{

    public bool connected = false;
    public int ID = 2848;
    public int ListMax = 4;
    public Text[] Stats;
    public int score1;
    public string name;

    private void Start()
    {

        LootLockerSDKManager.StartGuestSession("Guest", (response) =>
         {

             //check if a connection can be made to the leaderboard system
             if (response.success)
             {

                 connected = true;
                 Debug.Log("Connection Successful");

             }
             else
             {

                 connected = false;
                 Debug.Log("Connection Failed to connect on start");

             }

         });

        GetScore();

    }

    public void GetScore()
    {

        string[] lines = File.ReadAllLines("Player.txt");
        string[] splitline = lines[0].Split(',');

        score1 = Int32.Parse(splitline[2]);
        name = splitline[0];

    }

    //on leaderboard open show scores
    public void ShowScore()
    {

        LootLockerSDKManager.GetScoreList(ID, ListMax, (response) =>
        {

            if (response.success)
            {

                LootLockerLeaderboardMember[] scores = response.items;

                for(int i = 0; i < scores.Length; i++)
                {

                    Stats[i].text = (scores[i].rank + ".  " + scores[i].member_id + " = " + scores[i].score);

                }

                if(scores.Length < ListMax)
                {

                    for(int j = scores.Length;j < ListMax; j++)
                    {

                        int itterate = j + 1;
                        Stats[j].text = itterate.ToString() + ".  none";

                    }

                }
                else
                {

                    Debug.Log("Failed to load");

                }

            }

        });
    }

    public void SubmitScore()
    {

        LootLockerSDKManager.SubmitScore(name, score1, ID, (response) =>
        {

            if (response.success)
            {

                connected = true;
                Debug.Log("Connection Successful");

            }
            else
            {

                connected = false;
                Debug.Log("Connection Failed to submit score");

             }

        });


    }
    

}
