    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControlDisplay : MonoBehaviour
{
    public void SetFullScreen (bool isFullscreen){
        Screen.fullScreen = isFullscreen;
    }

    public Dropdown resolutionDropdown;
    Resolution[] resolutions;

    void Start(){
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResulotionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++){
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height){
                currentResulotionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResulotionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void SetResulotion (int resulotionIndex){
        Resolution resolution = resolutions[resulotionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public static ControlDisplay instance;
    public GameObject[] Inputs;
    private void Update()
    {
        if (InputManager.instance.ButtonClickInput)
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

                    Dropdown DComponent = selectedButton.GetComponent<Dropdown>();
                    if (DComponent != null)
                    {
                        // Open the dropdown (simulate a click)
                        DComponent.Show();
                    }
                }
            }
        }
    }
}
