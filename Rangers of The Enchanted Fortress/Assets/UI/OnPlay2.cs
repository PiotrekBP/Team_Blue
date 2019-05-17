using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlay2 : MonoBehaviour
{
    
        public Canvas Intro;
        public UnityEngine.Video.VideoPlayer vp;
        public void Play()
        {
            StartCoroutine(WaitforVid2());

        }
        IEnumerator WaitforVid2()
        {
            yield return new WaitForSecondsRealtime(60f);
            Menu.isInteracting = false;
            Intro.enabled = false;
            vp.Stop();
        }
    
}
