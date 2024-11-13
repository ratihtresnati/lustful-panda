using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    // Array untuk menyimpan tombol dan indeks scene atau flag apakah tombol berfungsi sebagai tombol keluar
    [System.Serializable]
    public class SceneButton
    {
        public Button button;        // Tombol UI
        public int sceneIndex = -1;  // Indeks scene tujuan di Build Settings (-1 jika tombol ini adalah tombol keluar)
        public bool isExitButton;    // True jika tombol ini adalah tombol keluar
    }
    
    public GameObject menuGameObject;
    private bool exit = false;
    private float _resetTimer = 0f;
    [SerializeField] private float _resetDelay = 0.5f;
    

    // Daftar tombol dan scene yang akan dimuat
    public SceneButton[] sceneButtons;

    AudioManager audioManager;

    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
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
                if (InputManager.instance.ButtonClickInput )//&& InputManager.instance._interact == true)
                {
                    GameObject selectedButton = EventSystem.current.currentSelectedGameObject;
                    if (selectedButton == sceneButton.button.gameObject)
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
            }
    }

    // Fungsi untuk memuat scene berdasarkan indeks
    public void LoadScene(int sceneIndex)
    {
        // InputManager.PlayerInput.enabled = false; 
        // isLoading = true;
        menuGameObject.SetActive(false);

        // SceneManager.LoadScene(sceneIndex);
        Loading.instance.LoadScene(sceneIndex);
    }

    // Fungsi untuk keluar dari aplikasi
    private void ExitApplication()
    {
        // InputManager.PlayerInput.enabled = false; 
        Application.Quit();
        Debug.Log("Application has been exited."); // Hanya berfungsi di editor atau build yang didukung
    }
}