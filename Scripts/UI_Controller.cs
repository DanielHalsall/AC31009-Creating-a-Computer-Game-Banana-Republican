using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Controller : MonoBehaviour
{

    public void Exit()
    {

        Debug.Log("Exit to Menu");
        int sceneval = 0;

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

}
