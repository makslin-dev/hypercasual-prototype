using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BlockSpawner _blockSpawner;
    [SerializeField] private GameInputHandler _gameInput;

    private BlockController _currentBlock;
    private void Awake()
    {
        _gameInput.OnBlockPlaced += StartNextBlock;
    }
    private void StartNextBlock()
    {
        if (_currentBlock != null)
        {
            _currentBlock.StopMoving();
        }
        _currentBlock = _blockSpawner.SpawnBlock();
    }
}
