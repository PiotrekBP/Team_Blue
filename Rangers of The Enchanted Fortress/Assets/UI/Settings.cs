using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public GameObject SUI;
    private bool MyProperty=false;
    public void SetIngs()
    {
        if (!MyProperty)
        {
            SUI.SetActive(true);
            MyProperty = true;
        }
        else
        {
            SUI.SetActive(false);
            MyProperty = false;
        }
    }
        
}