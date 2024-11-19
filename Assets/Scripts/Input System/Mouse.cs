using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Mouse : MonoBehaviour
{
    private GameObject _lastButton;
    private MainMenu _mainMenu;

    private void Start()
    {
        _mainMenu = FindObjectOfType<MainMenu>();
    }

    private void Update()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        bool isHoveringButton = false;

        foreach (RaycastResult result in results)
        {
            SceneButton sceneButton = FindSceneButton(result.gameObject);
            if (sceneButton != null)
            {
                HandleSceneButtonHover(sceneButton);
                isHoveringButton = true;
            }
        }

        _mainMenu.IsMouse = isHoveringButton;
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