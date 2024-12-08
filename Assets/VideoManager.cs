using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI; // Import untuk manipulasi UI

public class VideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Drag VideoPlayer object here in the Inspector
    public string nextSceneName;   // The name of the next scene
    public GameObject skipButton;  // Drag Skip button GameObject here in the Inspector

    private bool inputReceived = false; // Track if player has given input

    void Start()
    {
        // Pastikan tombol Skip disembunyikan saat scene dimulai
        if (skipButton != null)
        {
            skipButton.SetActive(false);
        }

        if (videoPlayer != null)
        {
            // Subscribe ke event VideoPlayer saat video selesai
            videoPlayer.loopPointReached += OnVideoEnd;
        }
        else
        {
            Debug.LogError("VideoPlayer is not assigned!");
        }
    }

    void Update()
    {
        // Cek jika pemain memberikan input apapun
        if (!inputReceived && (Input.anyKeyDown || Input.GetMouseButtonDown(0)))
        {
            inputReceived = true;
            ShowSkipButton();
        }
    }

    public void SkipVideo()
    {
        // Langsung pindah ke scene berikutnya
        SceneManager.LoadScene(nextSceneName);
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Pindah scene saat video selesai
        SceneManager.LoadScene(nextSceneName);
    }

    void ShowSkipButton()
    {
        // Tampilkan tombol Skip
        if (skipButton != null)
        {
            skipButton.SetActive(true);
        }
    }

    void OnDestroy()
    {
        if (videoPlayer != null)
        {
            // Hapus subscription untuk mencegah memory leak
            videoPlayer.loopPointReached -= OnVideoEnd;
        }
    }
}
