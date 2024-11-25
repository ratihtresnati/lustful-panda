using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance;
    // public GameObject[] Tabs;
    // public GameObject[] Buttons;
    [SerializeField] private ButtonSetting[] _buttonSetting;
    [SerializeField] private GameObject _controlMapBtn;
    [SerializeField] private GameObject _controlDisplayBtn;
    [SerializeField] private GameObject _controlAudioBtn;
    [SerializeField] private GameObject _controlMapMenu;
    [SerializeField] private GameObject _controlDisplayMenu;
    [SerializeField] public GameObject _controlAudioMenu;
    public GameObject _firstButtonCM;
    public GameObject _firstButtonCD;
    public GameObject _firstButtonCA;
    GameObject selectedButton;
    
    public bool IsSetting { get; private set; }
    public bool _isControlMap = false;
    private bool _isControlDisplay = false;
    public bool _isControlAudio = false;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        selectedButton = EventSystem.current.currentSelectedGameObject;

        foreach (var buttonSetting in _buttonSetting)
        {
            if (selectedButton == buttonSetting.button.gameObject)
            {
                int index = System.Array.IndexOf(_buttonSetting, buttonSetting);
                SelectButton(index);
                break;
            }
        }

            if (selectedButton != null)
            {
                if (selectedButton == _controlMapBtn && InputManager.instance.ButtonClickInput && _isControlMap == false)
                {
                    OpenControlMap();
                    
                }else if (selectedButton == _controlMapBtn && _isControlMap == true)
                {
                    CloseControlMap();
                }
                if (selectedButton == _controlDisplayBtn && InputManager.instance.ButtonClickInput && _isControlDisplay == false)
                {
                    OpenControlDisplay();
                }
                else if (selectedButton == _controlDisplayBtn && _isControlDisplay == true)
                {
                    CloseControlDisplay();
                }
                if (selectedButton == _controlAudioBtn && InputManager.instance.ButtonClickInput && _isControlAudio == false )
                {
                    OpenControlAudio();
                }
                else if (selectedButton == _controlAudioBtn && _isControlAudio == true)
                {
                    CloseControlAudio();
                }
            }
    }

    public void FirstSelected()
    {
        SelectButton(0);
    }

    public void SelectButton(int index)
    {
        selectedButton = _buttonSetting[index].button.gameObject;
        EventSystem.current.SetSelectedGameObject(_buttonSetting[index].button.gameObject);

        for (int i = 0; i < _buttonSetting.Length; i++)
        {
            if (i == index)
            {
                _buttonSetting[i].Button(true);
                // EventSystem.current.SetSelectedGameObject(_buttonSetting[i].gameObject);
            }
            else
            {
                _buttonSetting[i].Button(false);
            }
        }
    }

    private void OpenControlMap(){
        _controlMapMenu.SetActive(true);
        _isControlMap = true;
        IsSetting = true;

        // EventSystem.current.SetSelectedGameObject(_firstButtonCM);
    }
    private void OpenControlDisplay(){
        // _controlDisplayMenu.SetActive(true);
        _isControlDisplay = true;

        EventSystem.current.SetSelectedGameObject(_firstButtonCD);
    }
    private void OpenControlAudio(){
        // _controlAudioMenu.SetActive(true);
        _isControlAudio = true;

        EventSystem.current.SetSelectedGameObject(_firstButtonCA);
    }
    private void CloseControlMap(){
        // _controlMapMenu.SetActive(false);
        _isControlMap = false;
        Debug.Log("hai");
    }
    private void CloseControlDisplay(){
        // _controlDisplayMenu.SetActive(false);
        _isControlDisplay = false;
    }
    public void CloseControlAudio(){
        // _controlAudioMenu.SetActive(false);
        _isControlAudio = false;
    }
}
