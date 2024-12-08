using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SelectableObject : MonoBehaviour
{
    private float scaleMultiplier = 1.1f;
    private float animationDuration = 0.2f;
    public void ScaleUp()
    {
        transform.DOScale(new Vector3(scaleMultiplier, scaleMultiplier, scaleMultiplier), animationDuration);
    }
    public void ScaleDown()
    {
        transform.DOScale(Vector3.one, animationDuration);
    }
}
