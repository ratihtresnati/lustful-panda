using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverSementara : MonoBehaviour
{
    public GameObject SelesaiSementara;
    private bool _triggered = false;

    private void Start()
    {
        _triggered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Time.timeScale = 0;
        // SelesaiSementara.SetActive(true);
        if(_triggered == false)
        {
            _triggered = true;
            Loading.instance.LoadScene(4);
            SaveSystemJSON.Instance.DeleteSaveData();
        }
        
    }
}
