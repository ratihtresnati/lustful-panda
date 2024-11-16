using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
     public static MainMenu Instance;
    
    public GameObject menuGameObject;
    private bool exit = false;
    private float _resetTimer = 0f;
    [SerializeField] private float _resetDelay = 0.5f;
    

    public float scaleMultiplier = 1.1f;
    public float animationDuration = 0.2f;
    private GameObject selectedButton;
    private GameObject _lastSelectedButton;
    public bool IsMouse { get; set; }
    // Daftar tombol dan scene yang akan dimuat
    public SceneButton[] sceneButtons;

     private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        // Menambahkan listener ke setiap tombol
        foreach (SceneButton sceneButton in sceneButtons)
        {
            int index = sceneButton.sceneIndex; // Simpan indeks lokal untuk digunakan dalam lambda
            if (sceneButton.isExitButton)
            {
                sceneButton.button.onClick.AddListener(ExitApplication);
            }
            else
            {
                // Jika bukan tombol keluar, tambahkan listener untuk memuat scene
                // int index = sceneButton.sceneIndex; // Simpan indeks lokal untuk digunakan dalam lambda
                sceneButton.button.onClick.AddListener(() => LoadScene(index));
            }
        }
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
            int index = sceneButton.sceneIndex; // Simpan indeks lokal untuk digunakan dalam lambda

            if (IsMouse == true) 
            {
                EventSystem.current.SetSelectedGameObject(null);
                return;
            }
            else
            {
                selectedButton = EventSystem.current.currentSelectedGameObject;
            }

            if (selectedButton == sceneButton.button.gameObject)
            {
                OnPointerEnter(sceneButton);
                if (InputManager.instance.ButtonClickInput )//&& InputManager.instance._interact == true)
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

    public void OnPointerEnter(SceneButton sceneButton)
    {
        sceneButton.button.gameObject.transform.DOScale(new Vector3(scaleMultiplier, scaleMultiplier, scaleMultiplier), animationDuration);
    }

    public void OnPointerExit(SceneButton sceneButton)
    {
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
        // InputManager.PlayerInput.enabled = false; 
        Application.Quit();
        Debug.Log("Application has been exited."); // Hanya berfungsi di editor atau build yang didukung
    }
}