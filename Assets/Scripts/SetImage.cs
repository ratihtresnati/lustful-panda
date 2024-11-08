using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;


public class SetImage : MonoBehaviour
{
    public RenderTexture RT;
    public RawImage RI;

    void Update()
    {
        RI = StaticData.RWStatic;

        SetImageTexture();
    }

    public void SetImageTexture()
    {
        Texture2D texture2D = new Texture2D(RT.width, RT.height);
        byte[] bytes = StaticData.StaticBytes;

        texture2D.LoadImage(bytes);
        texture2D.Apply();

        RI.texture = StaticData.StaticTexture2D;
    }
    
}
