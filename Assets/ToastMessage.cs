using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToastMessage : MonoSingleton<ToastMessage>
{
    //static public ToastMessage instance;

    AndroidJavaObject toastObject;
    AndroidJavaObject currentActivity;

    public void showAndroidToast(string text)
    {
        //create a Toast class object
        AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");

        //create an array and add params to be passed
        object[] toastParams = new object[3];
        AndroidJavaClass unityActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        //toastParams[0] = unityActivity.GetStatic<AndroidJavaObject>("currentActivity");
        currentActivity = unityActivity.GetStatic<AndroidJavaObject>("currentActivity");
        toastParams[0] = currentActivity;
        toastParams[1] = text;
        toastParams[2] = toastClass.GetStatic<int>("LENGTH_SHORT");

        //call static function of Toast class, makeText
        toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", toastParams);
        //show toast
        toastObject.Call("show");

    }

    public void CancelToast()
    {
        currentActivity.Call("runOnUiThread", 
        	new AndroidJavaRunnable(() =>
	        {
	            if (toastObject != null) toastObject.Call("cancel");
	        }));
    }
}
