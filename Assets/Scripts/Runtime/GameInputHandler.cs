using System;
using UnityEngine;
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
            print("here");
            OnBlockPlaced.Invoke();
        }
    }

    
}
