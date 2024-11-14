using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Mouse : MonoBehaviour
{
    public static Mouse Instance;
    public Camera cam;
    public Vector2 mousePos;
    private SceneButton lastHoveredButton;

    private void Start()
    {
        cam = Camera.main;
        Instance = this;
    }

    private void Update()
    {
        HandleMouseInput();
    }

    private void HandleMouseInput()
    {
        InputManager.PlayerInputManager.UI.Point.performed += OnMousePos;

        PointerEventData pointerData = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult result in results)
        {
            SceneButton sceneButton = FindSceneButton(result.gameObject);
            if (sceneButton != null)
            {
                HandleSceneButtonHover(sceneButton);
                return;
            }
        }
    }

    private SceneButton FindSceneButton(GameObject gameObject)
    {
        return System.Array.Find(MainMenu.Instance.sceneButtons, sb => sb.button.gameObject == gameObject);
    }

    private void HandleSceneButtonHover(SceneButton sceneButton)
    {
        MainMenu.Instance.OnPointerEnter(sceneButton);
        if (EventSystem.current.currentSelectedGameObject == sceneButton.button.gameObject)
        {
            MainMenu.Instance.IsMouse = true;
            lastHoveredButton = sceneButton;
            if (InputManager.instance.ButtonClickInput)
            {
                if (sceneButton.isExitButton)
                {
                    MainMenu.Instance.ExitApplication();
                }
                else
                {
                    MainMenu.Instance.LoadScene(sceneButton.sceneIndex);
                }
            }
        }
        else
        {
            MainMenu.Instance.IsMouse = false;
            lastHoveredButton = null;
        }
    }

    private void OnMousePos(InputAction.CallbackContext context)
    {
        mousePos = cam.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }
}