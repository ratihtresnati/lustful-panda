using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class rolling : MonoBehaviour
{

    public Transform _orientation;
    public Transform _camera;
    private Rigidbody rb;
    private playerController pm;

    public float rollForce;
    public float rollUpawardForce;
    public float rollDuration;

    public float rolCd;
    private float rollCdTimer;


    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<playerController>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Rolling();
        }
    }

    private void Rolling()
    {
        Vector3 forceToApply = _orientation.forward * rollForce + _orientation.up * rollUpawardForce;

        rb.AddForce(forceToApply, ForceMode.Impulse);

        Invoke(nameof(ResetRoll), rollDuration);
    }

    private void ResetRoll()
    {

    }
}
