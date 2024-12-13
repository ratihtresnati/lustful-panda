using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public int nextSceneName;
    public GameObject skipButton;
    private bool inputReceived = false;

    void Start()
    {
        if (skipButton != null)
        {
            skipButton.SetActive(false);
        }

        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnd;
        }
    }

    void Update()
    {
        if (!inputReceived && (Input.anyKeyDown || Input.GetMouseButtonDown(0)))
        {
            inputReceived = true;
            ShowSkipButton();
        }
    }

    public void SkipVideo()
    {
        Loading.instance.LoadScene(nextSceneName);
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        Loading.instance.LoadScene(nextSceneName);
    }

    void ShowSkipButton()
    {
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
