using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    
    public UnityEngine.Video.VideoPlayer vp;
    public void Play()
    {
        StartCoroutine(WaitforVid());
        vp.Play();
        Debug.Log("creditsvideo");

    }
    IEnumerator WaitforVid()
    {
        yield return new WaitForSecondsRealtime(1f);
        vp.Stop();
    }
}
