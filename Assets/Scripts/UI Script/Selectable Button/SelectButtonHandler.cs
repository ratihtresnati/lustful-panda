using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LusfulPanda.Selectable;
using UnityEngine.EventSystems;
using DG.Tweening;

public class SelectButtonHandler : MonoBehaviour, ISelectButton, IPointerEnterHandler//, IPointerExitHandler //IPointerClickHandler 
{
    public GameObject SelectedButton { get; set; }

    [Header("Animation UI")]
    public float scaleMultiplier = 1.1f;
    public float animationDuration = 0.2f;

    public ButtonSelected buttonSelected;
    public GameObject _lastButton;
    public bool IsMouse { get; set; }

    private void Awake()
    {
        buttonSelected = GetComponent<ButtonSelected>();
    }
    private void Update()
    {
        IsMouse = false;
    }
    public void LoadScene(int sceneInt)
    {
        Loading.instance.LoadScene(sceneInt);
    }

    public void OpenPage(int activePage, params GameObject[] allPages)
    {
        if (activePage < 0 || activePage >= allPages.Length)
        {
            return;
        }

        for (int i = 0; i < allPages.Length; i++)
        {
            if (i == activePage)
            {
                allPages[i].SetActive(false); 
            }
            else
            {
                allPages[i].SetActive(true);
            }
        }
    }

    public void FirstButton(ButtonSelected buttonSelected)
    {
        SelectedButton = buttonSelected.buttonPage[0];
        EventSystem.current.SetSelectedGameObject(SelectedButton);
    }

    public void SelectButton()
    {
        foreach (GameObject button in buttonSelected.buttonPage)
        {
            if (button != SelectedButton)
            {
                OnPointerExit(button);
            }

            SelectedButton = EventSystem.current.currentSelectedGameObject;
            OnPointerEnter(SelectedButton);
        
            if (IsMouse == true)
            {
                SelectedButton = _lastButton;
                OnPointerEnter(SelectedButton);
                EventSystem.current.SetSelectedGameObject(SelectedButton);
            }
        }

        // Debug.Log(SelectedButton);
    }

    public void UnselectButton()
    {
        OnPointerExit(SelectedButton);
    }

    public void OnPointerEnter(GameObject sceneButton)
    {
        sceneButton.transform.DOKill(); 
        sceneButton.transform.DOScale(new Vector3(scaleMultiplier, scaleMultiplier, scaleMultiplier), animationDuration).SetUpdate(true);
    }

    public void OnPointerExit(GameObject sceneButton)
    {
        sceneButton.transform.DOKill(); 
        sceneButton.transform.DOScale(Vector3.one, animationDuration).SetUpdate(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
        {

            Debug.Log(result.gameObject.name);
            if (buttonSelected.buttonPage.Contains(result.gameObject))
            {
                foreach (GameObject button in buttonSelected.buttonPage)
                {
                    if (button == result.gameObject)
                    {
                        Debug.Log(button);
                        _lastButton = button; 
                        IsMouse = true;
                    }
                }
            }
        }
    }
}
