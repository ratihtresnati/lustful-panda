using UnityEngine.UI;

[System.Serializable]
public class SceneButton
{
    public Button button;        // Tombol UI
    public int sceneIndex = -1;  // Indeks scene tujuan di Build Settings (-1 jika tombol ini adalah tombol keluar)
    public bool isExitButton;    // True jika tombol ini adalah tombol keluar
    public bool isSettingButton;
    public bool isNewButton;
}