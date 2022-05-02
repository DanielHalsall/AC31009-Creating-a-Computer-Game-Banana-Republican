using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    
    public AudioMixer audioMixer;

    //setup the audio mixing for the settings

    public void SetVol(float volume){

        audioMixer.SetFloat("mainvol", volume);

    }
    //end of mixer code

    public void Graphics(int qualval){

        QualitySettings.SetQualityLevel(qualval);

    }

    public void togglefs(bool toggle){

        Screen.fullScreen = toggle;

    }

    public void Exit(){

        Debug.Log("Exit");
        Application.Quit();

    }
}
