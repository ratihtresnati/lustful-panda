using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHoldPosition : MonoBehaviour
{
    [SerializeField] private GameObject _grabItemsGameObject;

    public Transform PositionParent()
    {
        return _grabItemsGameObject.transform;
    }
}
