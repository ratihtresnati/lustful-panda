using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempcontrolmenu : MonoBehaviour
{
    public GameObject ControlPnael; // Referensi panel popup
    public bool panelkebuka;
    void Start()
    {
        ControlPnael.SetActive(false);
        panelkebuka = false;
    }

    void Update()
    {
        if (InputManager.instance.PauseInput)
        {
            if(panelkebuka == false){
                ControlPnael.SetActive(true);
                panelkebuka = true;}
            else{
                ControlPnael.SetActive(false);
                panelkebuka = false;}
        }
    }
}
