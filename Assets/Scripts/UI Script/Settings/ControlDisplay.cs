using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ControlDisplay : MonoBehaviour
{
    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    [SerializeField] private TMP_Dropdown resolutionDropdown;

    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;

    private float currentRefreshRate;
    private int currentResolutionIndex = 0;

    void Start()
    {
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();

        resolutionDropdown.ClearOptions();
        currentRefreshRate = (float)Screen.currentResolution.refreshRateRatio.value;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if ((float)resolutions[i].refreshRateRatio.value == currentRefreshRate)
            {
                filteredResolutions.Add(resolutions[i]);
            }
        }

        filteredResolutions.Sort((a, b) =>
        {
            if (a.width != b.width)
                return b.width.CompareTo(a.width);
            else
                return b.height.CompareTo(a.height);
        });

        List<string> options = new List<string>();
        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            // Hanya menampilkan resolusi dalam format "Width x Height"
            string resolutionOption = filteredResolutions[i].width + "x" + filteredResolutions[i].height;
            options.Add(resolutionOption);

            if (filteredResolutions[i].width == Screen.width &&
                filteredResolutions[i].height == Screen.height &&
                (float)filteredResolutions[i].refreshRateRatio.value == currentRefreshRate)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex = 0;
        resolutionDropdown.RefreshShownValue();
        SetResolution(currentResolutionIndex);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }

    public static ControlDisplay instance;
    public GameObject[] Inputs;

    private void Update()
    {
        if (InputManager.instance.ButtonClickInput && SettingsManager.instance.IsSetting == true)
        {
            EventSystem.current.SetSelectedGameObject(SettingsManager.instance._firstButtonCD);
            
            GameObject selectedButton = EventSystem.current.currentSelectedGameObject;
            
            if (selectedButton != null)
            {
                int index = System.Array.IndexOf(Inputs, selectedButton);
                if (index >= 0 && index < Inputs.Length)
                {
                    Button BComponent = selectedButton.GetComponent<Button>();
                    if (BComponent != null)
                    {
                        BComponent.onClick.Invoke();
                    }
                    Toggle TComponent = selectedButton.GetComponent<Toggle>();
                    if (TComponent != null)
                    {
                        TComponent.isOn = !TComponent.isOn;
                    }

                    TMP_Dropdown DComponent = selectedButton.GetComponent<TMP_Dropdown>();
                    if (DComponent != null)
                    {
                        DComponent.Show();
                    }
                }
            }
        }
    }
}
