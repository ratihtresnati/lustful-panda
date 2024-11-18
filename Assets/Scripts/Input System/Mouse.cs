using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Mouse : MonoBehaviour
{
    private GameObject _lastButton;
    private MainMenu _mainMenu;
    private MenuManager _menuManager;
    [SerializeField] private bool _pauseMenu;
    private void Start()
    {
        _mainMenu = FindObjectOfType<MainMenu>();
        _menuManager = FindObjectOfType<MenuManager>();
    }

    private void Update()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        bool isHoveringButton = false;

        foreach (RaycastResult result in results)
        {
            if(_pauseMenu == true)
            {
                GameObject[] pauseButtons = _menuManager.ButtonPauseMenu();
                foreach (GameObject button in pauseButtons)
                {
                    if (result.gameObject == button)
                    {
                        HandleButtonHover(button);
                        isHoveringButton = true;
                        break;
                    }
                }
            }
            else
            {
                SceneButton sceneButton = FindSceneButton(result.gameObject);

                Debug.Log(result.gameObject);
                if (sceneButton != null)
                {
                    HandleSceneButtonHover(sceneButton);
                    isHoveringButton = true;
                }
            }
        }

        if (_mainMenu != null)
        {
            _mainMenu.IsMouse = isHoveringButton;
        }

        if (_menuManager != null)
        {
            _menuManager.IsMouse = isHoveringButton;
        }
    }

    private void HandleButtonHover(GameObject button)
    {
        _lastButton = button; 
    }

    private void HandleSceneButtonHover(SceneButton sceneButton)
    {
        _mainMenu.OnPointerEnter(sceneButton);
        _lastButton = sceneButton.button.gameObject; 
    }

    private SceneButton FindSceneButton(GameObject gameObject)
    {
        return System.Array.Find(_mainMenu.sceneButtons, sb => sb.button.gameObject == gameObject);
    }

    public GameObject LastHoveredButton()
    {
        return _lastButton;
    }
}