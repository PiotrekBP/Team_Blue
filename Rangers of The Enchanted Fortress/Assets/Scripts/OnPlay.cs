using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlay : MonoBehaviour
{
    public SoundManager SA;
    public Canvas Intro;
    [SerializeField]
    private float videotime;
    public UnityEngine.Video.VideoPlayer vp;
    public void Play()
    {
        StartCoroutine(WaitforVid(videotime));
        SA.PlayerCharacterSourceCalm.volume = 0;
        
    }
    IEnumerator WaitforVid(float videotime)
    {
        yield return new WaitForSecondsRealtime(videotime);//40
        Menu.isInteracting = false;
        Intro.enabled = false;
        vp.Stop();
        SA.PlayerCharacterSourceCalm.volume = 0.5f;
    }
}
