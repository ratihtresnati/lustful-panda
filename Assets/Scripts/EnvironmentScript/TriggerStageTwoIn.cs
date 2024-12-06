using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStageTwoIn : MonoBehaviour
{
    public TriggerRoofStageTwo StageTwo;

    private void OnTriggerEnter(Collider other)
    {
        if (StageTwo.IsShow == true)
        {
            if (other.CompareTag("Player"))
            {
                StageTwo.HideRoof();
            }
        }
    }
}
