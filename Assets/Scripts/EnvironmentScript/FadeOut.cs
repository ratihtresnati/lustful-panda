using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{

    public float FadeSpeed;
    public float FadeAmout;

    float _originalOpacity;
    Material _material;
    public bool DoFade = false;

    void Start()
    {
        _material = GetComponent<Renderer>().material;
        _originalOpacity = _material.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        if (DoFade)
        {
            FadeNow();
        }
        else
        {
            ResetFade();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.CompareTag("PandaMC") || other.CompareTag("PandaRolling"))
        {
            DoFade = true;
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PandaMC") || other.CompareTag("PandaRolling"))
        {
            DoFade = false;
        }
    }

    void FadeNow()
    {
        Color currentColor = _material.color;
        Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b,
            Mathf.Lerp(currentColor.a, FadeAmout, FadeSpeed * Time.deltaTime));
        _material.color = smoothColor;
    }

    void ResetFade()
    {
        Color currentColor = _material.color;
        Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b,
            Mathf.Lerp(currentColor.a, _originalOpacity, FadeSpeed * Time.deltaTime));
        _material.color = smoothColor;
    }
}
