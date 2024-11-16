using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speedometer : MonoBehaviour
{
    [Range(0,1)]
    public float revs;
    public Quaternion rotation1;
    public Quaternion rotation2;

    private void Start()
    {
        rotation1 = Quaternion.Euler(0, 0, 0);
        rotation2 = Quaternion.Euler(0, 0, -180);
    }

    private void Update()
    {
        transform.rotation = Quaternion.Slerp(rotation1, rotation2, revs);
    }
}
