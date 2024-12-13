using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Zookeeper", menuName = "ZooKeeper")]
public class ScriptableZooKeper : ScriptableObject
{
    public Sprite imgNPC;
    public Sprite imgFullBody;
    public string nameNPC;
    public float radiusView;
    public float speed;
    public float angle;
    public string keterangan;
}
