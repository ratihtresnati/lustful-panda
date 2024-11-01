using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRoof : MonoBehaviour
{
    public int playerTrigger = 2;
    private int _triggerCount = 0;
    [SerializeField] private string _tag1;
    [SerializeField] private string _tag2;
    [SerializeField] private GameObject _roofTop;
    [SerializeField] private bool _isShow;

    private void OnTriggerEnter(Collider other)
    {
        if (_isShow == true)
        {
            if (other.CompareTag("Player"))
            {
                _roofTop.SetActive(false);
                _isShow = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_isShow == false)
        {
            if (other.CompareTag(_tag1) || other.CompareTag(_tag2))
            {
                _roofTop.SetActive(true);
                _isShow = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // if(_isShow == true)
        // {
        //     if (other.CompareTag(_tag1) || other.CompareTag(_tag2))
        //     {
        //         _roofTop.SetActive(false);
        //         _isShow = false;
        //     }
        // }
        
    }
}
