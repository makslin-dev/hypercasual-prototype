using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static GameController;

public class GameInputHandler : MonoBehaviour, IPlayerActions
{
    public Action OnBlockPlaced;
    private void Awake()
    {
        InputManager.EnableGameController();
        InputManager.SubscribeToGameController(this);
    }
    public void OnPlaceBlock(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (IsPointerOverUI())
            {
                return;
            }

            OnBlockPlaced?.Invoke();
        }
    }

    private bool IsPointerOverUI()
    {
        Vector2 pointerPosition = Pointer.current != null ? Pointer.current.position.ReadValue() : Vector2.zero;

        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = pointerPosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        return results.Count > 0;
    }


}
