using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRoofStageTwo : MonoBehaviour
{
    public string Tag1;
    public string Tag2;
    [SerializeField] private GameObject _roofTop;
    [SerializeField] private GameObject _window;
    public bool IsShow;


    public void ShowRoof()
    {
        _roofTop.SetActive(true);
        _window.SetActive(true);
        IsShow = true;
    }
    
    public void HideRoof()
    {
        _roofTop.SetActive(false);
        _window.SetActive(false);
        IsShow = false;
    }
   
}
