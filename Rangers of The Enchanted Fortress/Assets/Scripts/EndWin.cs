using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndWin : MonoBehaviour
{
    void OnTriggerEnter(Collider player)
    {
        if("Player"==player.name)
        {
            ShadowDetection.Win = true;
        }
    }
}
