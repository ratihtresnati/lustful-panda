using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverSementara : MonoBehaviour
{
    public GameObject SelesaiSementara;

    private void OnTriggerEnter(Collider other)
    {
        Time.timeScale = 0;
        SelesaiSementara.SetActive(true);
    }
}
