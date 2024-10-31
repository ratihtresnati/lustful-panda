using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlMapPopup : MonoBehaviour
{
   
    public Dictionary<string, KeyCode> controls = new Dictionary<string, KeyCode>(){
        { "Interact Item", KeyCode.E },
        { "Interact Object", KeyCode.E },
        { "Jump", KeyCode.Space },
        { "Roll", KeyCode.Q }
    };
    public GameObject popupPanel; // Referensi panel popup
    public Transform buttonContainer; // Tempat tombol
    public GameObject controlButtonPrefab; // Prefab tombol 
    private string waitingForInput = null;

    void Start()
    {
        popupPanel.SetActive(false);
        GenerateControlButtons();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleControlMappingPanel();
        }
        // Deteksi tombol baru untuk mapping
        if (!string.IsNullOrEmpty(waitingForInput))
        {
            DetectKeyChange();
        }
    }

    public void ToggleControlMappingPanel()
    {
        popupPanel.SetActive(!popupPanel.activeSelf);
    }
    private void GenerateControlButtons()
    {
        foreach (var control in controls)
        {
            GameObject buttonObj = Instantiate(controlButtonPrefab, buttonContainer);
            TMP_Text labelText = buttonObj.transform.Find("ControlText").GetComponent<TMP_Text>();
            Button button = buttonObj.transform.Find("ControlButton").GetComponent<Button>();
            TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();

            // Set nama aksi dan tombol yang ter-map
            labelText.text = control.Key;
            buttonText.text = control.Value.ToString();

            // Tambahkan listener untuk perubahan kontrol
            button.onClick.AddListener(() => StartRebinding(control.Key, buttonText));
        }
    }
    private void StartRebinding(string controlName, TMP_Text buttonText)
    {
        waitingForInput = controlName;
        buttonText.text = "Press any key...";
    }
    private void DetectKeyChange()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    // Update mapping dengan tombol baru
                    controls[waitingForInput] = keyCode;
                    UpdateControlButtonText(waitingForInput, keyCode);
                    waitingForInput = null;
                    break;
                }
            }
        }
    }
    private void UpdateControlButtonText(string controlName, KeyCode keyCode)
    {
        foreach (Transform child in buttonContainer)
        {
            TMP_Text labelText = child.Find("ControlText").GetComponent<TMP_Text>();
            if (labelText.text == controlName)
            {
                TMP_Text buttonText = child.Find("ControlButton").GetComponentInChildren<TMP_Text>();
                buttonText.text = keyCode.ToString();
                break;
            }
        }
    }
}
