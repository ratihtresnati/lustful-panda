using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXTrigger : MonoBehaviour
{
    public VisualEffect effect;
    
    // Start is called before the first frame update
    void Start()
    {
        effect.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            effect.Play();
        }
    }
}
