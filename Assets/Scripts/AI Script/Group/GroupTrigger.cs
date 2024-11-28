using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupTrigger : MonoBehaviour
{
    public AISensor LeaderSensor;
    public bool GroupCanSee;

    private void Update()
    {
        if ( LeaderSensor.canSeePlayer)
        {
            GroupCanSee = true;
        }
        else
        {
            GroupCanSee = false;
        }
    }
}
