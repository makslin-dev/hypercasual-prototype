using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BlockSpawner _blockSpawner;
    [SerializeField] private GameInputHandler _gameInput;
    [SerializeField] private BlockController _startBlock;
    private BlockController _currentBlock;
    private BlockController _lastBlock;

    public Action OnNextBlockStart;
    private void Awake()
    {
        StartGame();
    }
    private void StartGame()
    {
        _startBlock.StopMoving();
        _lastBlock = _startBlock;
        _gameInput.OnBlockPlaced += StartNextBlock;
        StartNextBlock();
    }
    private void StartNextBlock()
    {
        if (_currentBlock != null)
        {
            _currentBlock.StopMoving(); 
            bool success = _currentBlock.CutBlock(_lastBlock); 

            _lastBlock = _currentBlock; 
        }
        var lastBlockLocalScale = _lastBlock.transform.localScale.x;
        _currentBlock = _blockSpawner.SpawnBlock(lastBlockLocalScale);
        OnNextBlockStart?.Invoke();
    }
}
