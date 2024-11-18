using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    public GameObject menuGameObject;
    private bool exit = false;
    private float _resetTimer = 0f;
    private float _resetDelay = 0.5f;
    
    [Header("Animation UI")]
    public float scaleMultiplier = 1.1f;
    public float animationDuration = 0.2f;
    
    // Daftar tombol dan scene yang akan dimuat
    public SceneButton[] sceneButtons;
    private GameObject _selectedButton;
    private Mouse _mouse;
    public bool IsMouse { get; set; }

    void Start()
    {
        InitializeButtonSelect();
        _mouse = FindObjectOfType<Mouse>();
    }

    private void Update()
    {
        LoadingDelay();

        foreach (SceneButton sceneButton in sceneButtons)
        {
            int index = sceneButton.sceneIndex; // Simpan indeks lokal untuk digunakan dalam lambda

            UpdateButtonSelected(sceneButton);

            if (_selectedButton == sceneButton.button.gameObject)
            {
                OnPointerEnter(sceneButton);
                if (InputManager.instance.ButtonClickInput)
                {   
                    if (sceneButton.isExitButton == true )
                    {
                        ExitApplication();
                        Debug.Log("exit");
                        exit = true;
                    }
                    else
                    {
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

    private void LoadingDelay()
    {
        //harus pake ini, karna kalo engga dia bakalan auto klik button padahal dia masih loading  
        if (Loading.instance.IsLoading == true) 
        {
            _resetTimer += Time.deltaTime; 
            if (_resetTimer < _resetDelay) return;

            Loading.instance.IsLoading = false;
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
        Loading.instance.LoadScene(sceneIndex);
    }

    // Fungsi untuk keluar dari aplikasi
    public void ExitApplication()
    {
        Application.Quit();
        Debug.Log("Application has been exited."); // Hanya berfungsi di editor atau build yang didukung
    }
}