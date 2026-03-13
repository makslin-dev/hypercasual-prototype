using UnityEngine;

public static class InputManager 
{
    private static GameController _gameInput;

    public static void EnableGameController()
    {
        if (_gameInput == null)
        {
            _gameInput = new GameController();  
        }
        _gameInput.Enable();
    }
    public static void DisableGameController()
    {
        if (_gameInput == null)
        {
            Debug.LogWarning("GameInput never initialized");
            return;
        }
        _gameInput.Disable();
    }
    public static void SubscribeToGameController(GameController.IPlayerActions subscriber)
    {
        if (_gameInput == null)
            EnableGameController();
        _gameInput.Player.SetCallbacks(subscriber);
    }
    public static void UnsubscribeFromGameController(GameController.IPlayerActions subscriber)
    {
        _gameInput.Player.RemoveCallbacks(subscriber);
    }
}
