using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
-Script for detecting if player is in shadow.
-Author: Piotr Bartosz
*/
//All working.
public class ShadowDetection : MonoBehaviour
{
    private Animator animator;
    public static bool isInShadow;
    public RenderTexture lightInput;
    public float lightLevel;
    private int counter=0;

    void Start()
    {
        animator = transform.GetComponentInParent<Animator>();
    }
    void Update()
    {
        RenderTexture tempTexture = RenderTexture.GetTemporary(lightInput.width, lightInput.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
        Graphics.Blit(lightInput, tempTexture);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = tempTexture;
        Texture2D temp2DTexture = new Texture2D(lightInput.width, lightInput.height);
        temp2DTexture.ReadPixels(new Rect(0,0,tempTexture.width, tempTexture.height),0,0);
        temp2DTexture.Apply();

        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(tempTexture);

        Color32[] colors = temp2DTexture.GetPixels32();
        Destroy(temp2DTexture);

        lightLevel = 0f;
        for(int i =0; i<colors.Length;i++)
        {
            lightLevel += ((0.2426f * colors[i].r) + (0.7152f * colors[i].g) + (0.0722f * colors[i].b));
        }
   
        if((lightLevel - 5700000f) >=0)
        {
            Debug.Log("in light");
            if(isInShadow)
            {
               // animator.SetTrigger("SlowBlindStart");   
                StartCoroutine("DeathCountDown");
                Debug.Log("start");
            }
            isInShadow = false;
            
        }
        else
        {
            Debug.Log("in shadow");
            if(!isInShadow)
            {
                //stopanimation
                //StopCoroutine("DeathCountDown");
                //counter = 0;
            }
            isInShadow = true;

        }
    }

    //Usunac i zamienic na petle time deltatime;
    public IEnumerator DeathCountDown()
    {
        counter++;
        Debug.Log(counter);
        if(counter>=3)
        {
            Debug.Log("OutofTime");
            //gameover;
        }
        Debug.Log("Works");
        yield return null;//new WaitForSeconds(1);
    }
}
