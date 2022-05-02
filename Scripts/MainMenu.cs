using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public Button Load;

    // Start is called before the first frame update
    public void Start(){

        Debug.Log("Start");
        Load.enabled = false;
        ExsitingPlayerCheck();

    }

    // Update is called once per frame
    public void Exit(){

        Debug.Log("Exit");
        Application.Quit();

    }

    public void ExsitingPlayerCheck()
    {

        string[] lines = File.ReadAllLines("Player.txt");

        string[] splitline = lines[0].Split(',');

        //check there is an existing player before unlocking load button
        if (splitline[0] != "Name" && splitline[1] != "CompanyName")
        {

            Load.enabled = true;

        }

    }
}
