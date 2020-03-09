using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;

public class GoogleManager : MonoBehaviour
{

    public Text logtext;
    // Start is called before the first frame update
    void Start()
    {   
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();


        //
        if(Social.localUser.authenticated)
        {
            logtext.text = Social.localUser.id + ":" + Social.localUser.userName;
        }
        else
        {
            logtext.text = "Touch Trophy to Sign in";
        }
    }

    // Update is called once per frame
    public void LogIn()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if(success)
            {   
                Debug.Log("LogIn Success");
                logtext.text = Social.localUser.id + ":" + Social.localUser.userName;
            }
            else
            {
                Debug.Log("LogIn Failed");
            }
        });
    }

    public void LogOut()
    {
        ((PlayGamesPlatform)Social.Active).SignOut();
        logtext.text = "Touch Trophy to Sign in";
    }
}
