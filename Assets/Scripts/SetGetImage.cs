using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;
using Unity.VisualScripting;

public class SetGetImage : MonoBehaviour
{
    public PlayerController PlayerController;

    public GameObject GameOver;

    public string Filename;
    
    public RenderTexture RT;
    public GameObject RenderCamera;
    public GameObject RI;

    public void GetImage()
    {
        Texture2D texture2D = new Texture2D(RT.width, RT.height, TextureFormat.ARGB32, false);
        RenderTexture.active = RT;
        texture2D.ReadPixels(new Rect(0, 0, RT.width, RT.height), 0, 0);
        texture2D.Apply();

        //string Path = Application.persistentDataPath + "/" + Filename + ".png";
        byte[] bytes = texture2D.EncodeToPNG();

       // File.WriteAllBytes(Path, bytes);
    }

   

    IEnumerator RenderProcess()
    {
        RenderCamera.SetActive(true);
        yield return new WaitForSeconds(0.01f);
        GetImage();
        yield return new WaitForSeconds(3f);
        RI.SetActive(true);
        /*
        yield return new WaitForSeconds(0.01f);
        SetImage();
        */

        yield return new WaitForSeconds(0.01f);
        GameOver.SetActive(true);
        Time.timeScale = 0;
        RenderCamera.SetActive(false);

    }
    
    private void Update()
    {
        if (PlayerController.GameOver)
        {
            InputManager.PlayerInput.enabled = false;
            StartCoroutine(RenderProcess());
        }
    }
}
