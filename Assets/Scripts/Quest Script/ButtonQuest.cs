using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonQuest : MonoBehaviour
{
    public QuestScriptable scriptableQuest;
    public Button button;
    public GameObject buttonOn;
    public GameObject buttonOff;
    public GameObject Tab;

    private void Awake()
    {
        scriptableQuest = Object.FindObjectOfType<QuestScriptable>();
    }

    public void Button(bool on)
    {
        buttonOn.SetActive(on);
        // Tab.SetActive(on);
        buttonOff.SetActive(!on); 
        // ShowData();
    }

    public void ShowData()
    {
        // PanelQuest.Instance.ShowQuestDetails(scriptableQuest);
        // UIQuestLog.instance.ShowQuestDetails(scriptableQuest.idQuest);
    }
}
