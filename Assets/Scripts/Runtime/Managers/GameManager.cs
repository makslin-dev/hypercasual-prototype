using System;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    [SerializeField] private BlockSpawner _blockSpawner;
    [SerializeField] private GameInputHandler _gameInput;
    [SerializeField] private BlockController _startBlock;
    private BlockController _currentBlock;
    private BlockController _lastBlock;
    private bool _gameStarted;
    
   
    private void Awake()
    {
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
        SpawnFirstBlock();
        _gameInput.OnBlockPlaced += SpawnNextBlock;
        _gameInput.OnBlockPlaced -= SetStartedTrue;
        _gameStarted = false;
    }
    private void SetStartedTrue()
    {
        _gameStarted = true;
    }
    private void SpawnFirstBlock()
    {
        _lastBlock = _startBlock;
        _currentBlock = _blockSpawner.SpawnBlock(_lastBlock, _startBlock.transform.localScale.x,
            _startBlock.transform.localScale.z, _blockSpawner.CurrentAxis);
    }
    private void SpawnNextBlock()
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
        EventBus.StartNextBlock();
        _currentBlock = _blockSpawner.SpawnBlock(_lastBlock, lastBlockLocalScaleX, 
            lastBlockLocalScaleZ, _blockSpawner.CurrentAxis);    
    }
}
