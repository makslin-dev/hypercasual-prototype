using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private BlockSpawner _blockSpawner;
    [SerializeField] private GameInputHandler _gameInput;
    [SerializeField] private BlockController _startBlock;
    private BlockController _currentBlock;
    private BlockController _lastBlock;
    private bool _gameStarted;
    
    public Action OnNextBlockStart;
    private void Awake()
    {
        Instance = this;
        _gameInput.OnBlockPlaced += SetStartedTrue;    
    }
    private void Update()
    {
        if (_gameStarted)
        {
            StartGame();
        }
    }
    private void StartGame()
    {
        _startBlock.StopMoving();
        _lastBlock = _startBlock;
        _gameInput.OnBlockPlaced += StartNextBlock;
        StartNextBlock();
        _gameInput.OnBlockPlaced -= SetStartedTrue;
        _gameStarted = false;
    }
    private void SetStartedTrue()
    {
        _gameStarted = true;
    }
    private void StartNextBlock()
    {
        if (_currentBlock != null)
        {
            _currentBlock.StopMoving();
            print(_blockSpawner.CurrentAxis + "Current axis");
            bool success = _currentBlock.CutBlock(_lastBlock,_blockSpawner.CurrentAxis); 

            _lastBlock = _currentBlock;
        }
        var lastBlockLocalScaleX = _lastBlock.transform.localScale.x;
        var lastBlockLocalScaleZ = _lastBlock.transform.localScale.z;
        OnNextBlockStart?.Invoke();
        _currentBlock = _blockSpawner.SpawnBlock(_lastBlock, lastBlockLocalScaleX, 
            lastBlockLocalScaleZ, _blockSpawner.CurrentAxis);    
    }
}
