using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlay : MonoBehaviour
{
    public Canvas Intro;
    public UnityEngine.Video.VideoPlayer vp;
    public void Play()
    {
        StartCoroutine(WaitforVid());
        
    }
    IEnumerator WaitforVid()
    {
        yield return new WaitForSecondsRealtime(1f);
        Menu.isInteracting = false;
        Intro.enabled = false;
        vp.Stop();
    }
}
