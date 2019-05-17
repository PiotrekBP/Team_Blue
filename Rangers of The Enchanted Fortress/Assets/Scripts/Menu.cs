using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public Canvas MenuCanv;
    public Canvas Intro;
    public Canvas Win;

    public static bool goToMenu=false;
    public static bool goToIntro = false;
    public static bool goToWin = false;
    public static bool isInteracting = true;

    void Update()
    {
        if (goToMenu)
        { 
            isInteracting = true;
            MenuCanv.enabled = true;
            goToMenu = false;

        }
        else if(goToIntro)
        {
            Debug.Log("intro");
            isInteracting = true;
            Intro.enabled = true;
            MenuCanv.enabled = false;
            goToIntro = false;

        }
        else if(goToWin)
        {
            MenuCanv.enabled = false;
            Intro.enabled = false;
            isInteracting = true;
            Win.enabled = true;
            goToWin = false;

        }
    }

}
