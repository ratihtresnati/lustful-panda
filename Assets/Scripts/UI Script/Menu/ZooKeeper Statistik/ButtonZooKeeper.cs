using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonZooKeeper : MonoBehaviour
{
    public ScriptableZooKeper scriptableZooKeper;
    public Button button;
    public GameObject buttonOn;
    public GameObject buttonOff;
    public GameObject Tab;

    public void Button(bool on)
    {
        buttonOn.SetActive(on);
        Tab.SetActive(on);
        buttonOff.SetActive(!on); 
        ShowData();
    }

    public void ShowData()
    {
        PanelStatistik.Instance.ShowData(scriptableZooKeper);
    }
}
