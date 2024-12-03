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

        // Simpan status fullscreen ke PlayerPrefs
        PlayerPrefs.SetInt("IsFullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();

        // Sinkronkan toggle setelah perubahan
        SyncFullscreenToggle();
    }

    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;

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

        // Sinkronkan toggle fullscreen terlebih dahulu
        SyncFullscreenToggle();

        for (int i = 0; i < resolutions.Length; i++)
        {
            if ((float)resolutions[i].refreshRateRatio.value == currentRefreshRate)
            {
                float aspectRatio = (float)resolutions[i].width / resolutions[i].height;

                // Hanya masukkan resolusi dengan aspect ratio 16:9
                if (Mathf.Approximately(aspectRatio, 16f / 9f))
                {
                    filteredResolutions.Add(resolutions[i]);
                }
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
            string resolutionOption = filteredResolutions[i].width + "x" + filteredResolutions[i].height;
            options.Add(resolutionOption);
        }

        resolutionDropdown.AddOptions(options);

        // Ambil resolusi dari PlayerPrefs jika ada
        currentResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", 0);

        // Pastikan indeks valid
        if (currentResolutionIndex < 0 || currentResolutionIndex >= filteredResolutions.Count)
        {
            currentResolutionIndex = 0;
        }

        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // Atur resolusi berdasarkan pengaturan yang tersimpan
        SetResolution(currentResolutionIndex);

        // Tambahkan listener ke toggle
        if (fullscreenToggle != null)
        {
            fullscreenToggle.onValueChanged.AddListener(SetFullScreen);
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = filteredResolutions[resolutionIndex];
        bool isFullscreen = Screen.fullScreen;
        Screen.SetResolution(resolution.width, resolution.height, isFullscreen);

        // Simpan pengaturan ke PlayerPrefs
        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
        PlayerPrefs.Save();
    }

    private void SyncFullscreenToggle()
    {
        if (fullscreenToggle != null)
        {
            // Ambil status fullscreen dari PlayerPrefs atau gunakan default Screen.fullScreen
            bool isFullscreen = PlayerPrefs.GetInt("IsFullscreen", Screen.fullScreen ? 1 : 0) == 1;
            fullscreenToggle.isOn = isFullscreen;
        }
    }

    public static ControlDisplay instance;
    public GameObject[] Inputs;

    private void Update()
    {
        if (InputManager.instance.ButtonClickInput && SettingsManager.instance.IsSetting == true)
        {
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

