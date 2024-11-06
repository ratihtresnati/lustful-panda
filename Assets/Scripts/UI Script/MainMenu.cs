using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

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

    // Daftar tombol dan scene yang akan dimuat
    public SceneButton[] sceneButtons;
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
        bool pressed = false;
        foreach (SceneButton sceneButton in sceneButtons)
        {
            int index = sceneButton.sceneIndex; // Simpan indeks lokal untuk digunakan dalam lambda
            if (InputManager.instance.ButtonClickInput)
            {
                if (sceneButton.isExitButton == true)
                {
                    ExitApplication();
                    pressed = true;
                    break;
                }
                else
                {
                    LoadScene(index);
                    menuGameObject.SetActive(false);
                    pressed = true; 
                    break;
                }
            }
        }
    }

    // Fungsi untuk memuat scene berdasarkan indeks
    public void LoadScene(int sceneIndex)
    {
        // SceneManager.LoadScene(sceneIndex);
        Loading.instance.LoadScene(sceneIndex);
    }

    // Fungsi untuk keluar dari aplikasi
    private void ExitApplication()
    {
        Application.Quit();
        Debug.Log("Application has been exited."); // Hanya berfungsi di editor atau build yang didukung
    }
}