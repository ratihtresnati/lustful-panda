using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    public GameObject menuGameObject;
    public GameObject settingGameObject;
    private bool exit = false;
    private float _resetTimer = 0f;
    private float _resetDelay = 1f;
    
    [Header("Animation UI")]
    public float scaleMultiplier = 1.1f;
    public float animationDuration = 0.2f;
    
    // Daftar tombol dan scene yang akan dimuat
    public SceneButton[] sceneButtons;
    private GameObject _selectedButton;
    private bool _isSetting;

    private Mouse _mouse;
    public bool IsMouse { get; set; }

    AudioManager audioManager;
    private SaveSystemJSON saveSystem; // Tambahkan referensi untuk SaveSystemJSON

    private void Awake() 
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        saveSystem = GameObject.FindObjectOfType<SaveSystemJSON>(); // Temukan SaveSystemJSON
    }

    void Start()
    {
        InitializeButtonSelect();
        _mouse = FindObjectOfType<Mouse>();
        settingGameObject.SetActive(false);
    }

    private void Update()
    {
        if (Loading.instance.IsLoading == true) 
        {
            _resetTimer += Time.deltaTime; 
            if (_resetTimer < _resetDelay) return;

            Loading.instance.IsLoading = false;
        }

        foreach (SceneButton sceneButton in sceneButtons)
        {
            int index = sceneButton.sceneIndex; 

            UpdateButtonSelected(sceneButton);

            if (_selectedButton == sceneButton.button.gameObject)
            {
                OnPointerEnter(sceneButton);
                if (InputManager.instance.ButtonClickInput)
                {   
                    audioManager.Play("ButtonClick");
                    if (sceneButton.isExitButton == true )
                    {
                        ExitApplication();
                        Debug.Log("exit");
                        exit = true;
                    }
                    else if (sceneButton.isSettingButton == true)
                    {
                        _isSetting = true;
                        settingGameObject.SetActive(true);
                        SettingsManager.instance.FirstSelected();
                    }
                    else
                    {
                        // Load Scene dan load data jika ada
                        LoadScene(index);
                        Debug.Log("load");
                    }
                }
            }
            else
            {
                OnPointerExit(sceneButton);
            }
        }

        if(InputManager.instance.PauseInput && _isSetting == true)
        {
            settingGameObject.SetActive(false);
            _selectedButton = sceneButtons[0].button.gameObject;
        }
    }

    private void InitializeButtonSelect()
    {
        if (_selectedButton == null && sceneButtons.Length > 0)
        {
            _selectedButton = sceneButtons[0].button.gameObject;
            EventSystem.current.SetSelectedGameObject(_selectedButton);
        }
    }

    private void UpdateButtonSelected(SceneButton sceneButton)
    {
        if (IsMouse == true) 
        {
            _selectedButton = _mouse.LastHoveredButton();
            EventSystem.current.SetSelectedGameObject(_mouse.LastHoveredButton());
        }
        else
        {
            _selectedButton = EventSystem.current.currentSelectedGameObject;
        }
    }

    public void OnPointerEnter(SceneButton sceneButton)
    {
        sceneButton.button.gameObject.transform.DOKill(); 
        sceneButton.button.gameObject.transform.DOScale(new Vector3(scaleMultiplier, scaleMultiplier, scaleMultiplier), animationDuration);
    }

    public void OnPointerExit(SceneButton sceneButton)
    {
        sceneButton.button.gameObject.transform.DOKill(); 
        sceneButton.button.gameObject.transform.DOScale(Vector3.one, animationDuration);
    }

    // Fungsi untuk memuat scene berdasarkan indeks
    public void LoadScene(int sceneIndex)
    {
        foreach (SceneButton sceneButton in sceneButtons)
        {
            DOTween.Kill(sceneButton.button.gameObject.transform);
        }

        menuGameObject.SetActive(false);

        // Muat game data sebelum scene dimuat
        if (saveSystem != null)
        {
            Debug.Log("Load game data.");
            saveSystem.LoadGame();  // Muat game
        }
        else
        {
            Debug.LogError("SaveSystemJSON not found! Cannot load saved data.");
        }

        Loading.instance.LoadScene(sceneIndex); // Memuat scene setelah data game dimuat
    }
    // Fungsi untuk keluar dari aplikasi
    public void ExitApplication()
    {
        Application.Quit();
        Debug.Log("Application has been exited.");
    }
}
