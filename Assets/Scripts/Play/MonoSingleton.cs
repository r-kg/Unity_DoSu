using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance = null;


    
    public static T Instance
    {
        
        get
        {
            
             if(instance == null)
             {
                 GameObject obj;
                 obj = GameObject.Find(typeof(T).Name);
                 

                 if(obj == null)
                 {
                     obj = new GameObject(typeof(T).Name);
                     instance = obj.AddComponent<T>();
                     DontDestroyOnLoad(obj);
                 }
                 else
                 {
                     instance = obj.GetComponent<T>();
                 }
             }
             
             return instance;
             
        }
    }
    

    public void Load()
    {
        Debug.Log(gameObject.name + " Loaded");
    }


    void Start()
    {
        var obj = GameObject.FindObjectsOfType(typeof(T));
        if(obj.Length > 1)
        {
            DestroyImmediate(gameObject);
        }
    }

}
