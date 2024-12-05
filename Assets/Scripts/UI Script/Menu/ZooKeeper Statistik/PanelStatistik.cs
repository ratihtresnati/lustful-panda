using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PanelStatistik : MonoBehaviour
{
    public static PanelStatistik Instance;
    public Image headNPC;
    public Image bodyNPC;
    public TextMeshProUGUI name;
    public TextMeshProUGUI radiusPercent;
    public TextMeshProUGUI speedPercent;
    public TextMeshProUGUI keterangan;
    public Image radiusBar;
    public Image speedBar;

    public float maxRadius = 100f; 
    public float maxSpeed = 100f; 
    
    private void Awake()
    {
        Instance = this;
    }
    public void ShowData(ScriptableZooKeper npc)
    {
        // headNPC.sprite = npc.imgNPC; 
        bodyNPC.sprite = npc.imgFullBody;
        name.text = npc.nameNPC; 
        keterangan.text = npc.keterangan;
        
        radiusBar.fillAmount = npc.radiusView / maxRadius; 
        speedBar.fillAmount = npc.speed / maxSpeed;

        radiusPercent.text = $"{npc.radiusView / 100 :P00}";
        speedPercent.text = $"{npc.speed / 100 :P0}";
    }
}
