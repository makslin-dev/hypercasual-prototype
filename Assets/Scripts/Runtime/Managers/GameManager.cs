using System;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    [SerializeField] private BlockSpawner _blockSpawner;
    [SerializeField] private GameInputHandler _gameInput;
    [SerializeField] private BlockController _startBlock;
    [SerializeField] private AnimationCurve _speedCurve;
    [Header("Speed Settings")]
    [SerializeField] private float _startDuration = 1.5f;   
    [SerializeField] private float _minDuration = 0.35f;   
    [SerializeField] private float _durationStep = 0.5f;   
    [SerializeField] private float _moveDistance = 3f;
    private BlockController _currentBlock;
    private BlockController _lastBlock;
    private bool _gameStarted;
    private int _placedBlocks;

    private void Awake()
    {
        _gameInput.OnBlockPlaced += SetStartedTrue;    
    }
    private void Update()
    {
        if (_gameStarted)
        {
            StartGame();
            EventBus.StartGame();
        }
    }
    private void StartGame()
    {
        _placedBlocks = 0;
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
            _startBlock.transform.localScale.z, _blockSpawner.CurrentAxis, _moveDistance, GetCurrentDuration());
    }
    private void SpawnNextBlock()
    {
        if (_currentBlock != null)
        {
            _currentBlock.StopMoving();
            print(_blockSpawner.CurrentAxis + "Current axis");
            bool success = _currentBlock.CutBlock(_lastBlock, _blockSpawner.CurrentAxis);

            _lastBlock = _currentBlock;
            _placedBlocks++;
        }
        var lastBlockLocalScaleX = _lastBlock.transform.localScale.x;
        var lastBlockLocalScaleZ = _lastBlock.transform.localScale.z;
        EventBus.StartNextBlock();
        _currentBlock = _blockSpawner.SpawnBlock(_lastBlock, lastBlockLocalScaleX,
            lastBlockLocalScaleZ, _blockSpawner.CurrentAxis, _moveDistance, GetCurrentDuration());
    }
    private float GetCurrentDuration()
    {
        float t = Mathf.Clamp01(_placedBlocks / 50f);
        return Mathf.Lerp(_startDuration, _minDuration, _speedCurve.Evaluate(t));
    }
}
