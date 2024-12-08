using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStageTwoOut : MonoBehaviour
{
    public TriggerRoofStageTwo StageTwo;

    private void OnTriggerEnter(Collider other)
    {
        if (StageTwo.IsShow == false)
        {
            if (other.CompareTag(StageTwo.Tag1) || other.CompareTag(StageTwo.Tag2))
            {
                StageTwo.ShowRoof();
            }
        }
    }
}
