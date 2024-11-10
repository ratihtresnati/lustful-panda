using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentPosition : MonoBehaviour
{
    [SerializeField] private GameObject _positionParent;
    public Transform PositionParent()
    {
        return _positionParent.transform;
    }
}
