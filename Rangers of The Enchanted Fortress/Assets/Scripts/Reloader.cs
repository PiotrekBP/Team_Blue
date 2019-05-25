using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reloader : MonoBehaviour
{
    public static Reloader instanceRef;


    private void Awake()
    {

        if (instanceRef == null)
        {
            instanceRef = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instanceRef != this)
            Destroy(gameObject);

        
    }
    public void Reload()
    {
        SceneManager.LoadScene(0);
    }



}
