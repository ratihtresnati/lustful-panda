using UnityEngine;
using UnityEngine.UI;

public class ButtonSetting : MonoBehaviour
{
    public Button button;
    public GameObject buttonOn;
    public GameObject buttonOff;
    public GameObject Tab;

    public void Button(bool on)
    {
        buttonOn.SetActive(on);
        Tab.SetActive(on);
        buttonOff.SetActive(!on); 
    }
}