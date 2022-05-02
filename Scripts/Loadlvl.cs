using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loadlvl : MonoBehaviour
{

    public Newlvl newlvl;

    public void load()
    {
        int sceneval = 1;

        StartCoroutine(LoadAsynchronously(sceneval));

    }

    IEnumerator LoadAsynchronously(int sceneval)
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneval);

        while (!operation.isDone)
        {

            Debug.Log(operation.progress);

            yield return null;

        }
    }

    public void LoadNewlvl()
    {

        newlvl.SetFactionsToNew();
        newlvl.SetCitiesToEmpty();
        newlvl.ResetSettlementFile();

        int sceneval = 1;

        StartCoroutine(LoadAsynchronously(sceneval));


    }
}

