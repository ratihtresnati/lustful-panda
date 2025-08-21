using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddZookeeper : MonoBehaviour
{
    [SerializeField] private GameObject zookeeperPrefab;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            zookeeperPrefab.SetActive(!zookeeperPrefab.activeSelf);
        }
    }
    
}
