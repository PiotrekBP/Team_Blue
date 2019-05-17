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
    public GameObject wincan;
    public SoundManager SA;
    public AudioSource AS;
    public AudioSource BG1;
    public AudioSource BG2;
    public AudioSource BG3;
    public static bool Die = false;
    public static bool Win = false;
    public Animator Blind;
    public Animator Death;
    private Animator animator;
    public static bool isInShadow = true;
    public RenderTexture lightInput;
    public float lightLevel;
    private float counter=0;
    private bool aplying = false;

    void Start()
    {
        animator = transform.GetComponentInParent<Animator>();
    }
    void Update()
    {
        if (!Menu.isInteracting)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                Menu.goToMenu = true;
            }
            if (Die)
            {
                
                SA.StopSound(BG1);
                SA.StopSound(BG2);
                SA.StopSound(BG3);
                SA.PlaySound("main_death_sfx",AS);
                Debug.Log("Death");
                Blind.SetTrigger("Death");
                Death.SetTrigger("Death");
                StartCoroutine(WaitSec());
                Die = false;
            }
            if (Win)
            {
                
                SA.StopSound(BG1);
                SA.StopSound(BG2);
                SA.StopSound(BG3);
                AS.volume = 1f;
                SA.PlaySound("///", AS);
                wincan.SetActive(true);
                StartCoroutine(Wait3Sec());
                Win = false;
                Debug.Log("Win");
            }
            RenderTexture tempTexture = RenderTexture.GetTemporary(lightInput.width, lightInput.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
            Graphics.Blit(lightInput, tempTexture);
            RenderTexture previous = RenderTexture.active;
            RenderTexture.active = tempTexture;
            Texture2D temp2DTexture = new Texture2D(lightInput.width, lightInput.height);
            temp2DTexture.ReadPixels(new Rect(0, 0, tempTexture.width, tempTexture.height), 0, 0);
            temp2DTexture.Apply();

            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(tempTexture);

            Color32[] colors = temp2DTexture.GetPixels32();
            Destroy(temp2DTexture);

            lightLevel = 0f;
            for (int i = 0; i < colors.Length; i++)
            {
                lightLevel += ((0.2426f * colors[i].r) + (0.7152f * colors[i].g) + (0.0722f * colors[i].b));
            }

            if ((lightLevel - 5700000f) >= 0)
            {
                Debug.Log("in light");
                if (isInShadow)
                {
                    Blind.SetTrigger("Blin");
                }
                //Debug.Log(counter);   
                counter += Time.deltaTime;
                if (counter > 3f)
                {

                    Die = true;
                }
                isInShadow = false;

            }
            else
            {
                Debug.Log("in shadow");
                if (!isInShadow)
                {
                    //stopanimation
                    Blind.SetTrigger("InShadow");
                    //counter = 0;
                }
                counter = 0;
                isInShadow = true;

            }
        }


        IEnumerator WaitSec()
        {
            yield return new WaitForSeconds(1f);
            //reload Scene

        }
        IEnumerator Wait3Sec()
        {
            yield return new WaitForSeconds(1f);
            //reload Scene

        }
    }
}
