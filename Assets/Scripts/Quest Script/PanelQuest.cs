using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PanelQuest : MonoBehaviour
{
    public static PanelQuest Instance;
    public TextMeshProUGUI name;
    public TextMeshProUGUI keterangan;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowData(QuestScriptable quest)
    {
        name.text = quest.nameQuest; 
        keterangan.text = quest.descriptionQuest;
    }
}
