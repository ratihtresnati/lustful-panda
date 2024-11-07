using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchSensor : MonoBehaviour
{

    public float radiusCatch;
    [Range(0, 360)]
    public float angleCatch;

    public GameObject playerSouldCatch;


    public LayerMask targetMaskCatah;
    public LayerMask obstructionMaskCatch;

    public bool catchPlayer;


    void Start()
    {
        playerSouldCatch = GameObject.FindGameObjectWithTag("PandaMC");
        StartCoroutine(FOVCatch());
    }

    private IEnumerator FOVCatch()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            CatchPlayer();
        }
    }

    private void CatchPlayer()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radiusCatch, targetMaskCatah);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angleCatch / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMaskCatch))
                    catchPlayer = true;
                else
                    catchPlayer = false;
            }

        }
        else
            catchPlayer = false;
    }
}
