using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public static ChangeScene instance;
    //public SendEmail sendemail;
    //public string receipientEmailAddress = "fitf@mailinator.com";
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(instance);

        }
        //else
        //{
        instance = this;
        //}
    }

    //public void Start() 
    //{
    //    StartCoroutine(sendemail.PostData_Coroutine());
    //}

    public void SceneSwitch(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
