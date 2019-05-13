using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowDetection : MonoBehaviour
{
    public bool isInShadow;
    public RenderTexture lightInput;
    public float lightLevel;
    void Update()
    {
        RenderTexture tempTexture = RenderTexture.GetTemporary(lightInput.width, lightInput.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
        Graphics.Blit(lightInput, tempTexture);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = tempTexture;
        Texture2D temp2DTexture = new Texture2D(lightInput.width, lightInput.height);
        temp2DTexture.ReadPixels(new Rect(0,0,tempTexture.width, tempTexture.height),0,0);//////
        temp2DTexture.Apply();

        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(tempTexture);

        Color32[] colors = temp2DTexture.GetPixels32();
        Destroy(temp2DTexture);

        lightLevel = 0f;
        for(int i =0; i<colors.Length;i++)
        {
            lightLevel += ((0.2426f * colors[i].r) + (0.7152f * colors[i].g) + (0.0722f + colors[i].b));
        }
        Debug.Log(lightLevel);
    }
}
