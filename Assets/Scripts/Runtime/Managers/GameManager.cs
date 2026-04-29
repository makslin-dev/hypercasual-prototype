using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using Zenject;

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
    [SerializeField] private float _moveDistance = 3f;
    [Header("Starting Blocks")]
    [SerializeField] private TowerBase _towerBasePrefab;

    private BlockController _currentBlock;
    private BlockController _lastBlock;
    private TowerBase _currentTowerBase;
    private bool _isGameStarted;
    private bool _isGameOver;
    private int _placedBlocks;
    private Vector3 _towerBaseDefaultPosition = new Vector3(0, -7f, 4);

    private ColorManager _colorManager;
    [Inject]
    private void Construct(ColorManager colorManager)
    {
        _colorManager = colorManager;
    }
    private void OnEnable()
    {
        SubscribeToEvents();    
    }
    private void Start()
    {
        SpawnTowerBase();
    }
    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }
    private void SubscribeToEvents()
    {
        _gameInput.OnBlockPlaced += SetStartedTrue;
    }
    private void UnsubscribeFromEvents()
    {
        _gameInput.OnBlockPlaced -= SetStartedTrue;
        _gameInput.OnBlockPlaced -= SpawnNextBlock;
    }
    private void StartGame()
    {
        _placedBlocks = 0;
        _startBlock.StopMoving();
        SpawnFirstBlock();
        _gameInput.OnBlockPlaced += SpawnNextBlock;
    }
    private void SpawnTowerBase()
    {
        _currentTowerBase = Instantiate(_towerBasePrefab, _towerBaseDefaultPosition, Quaternion.identity);
        _currentTowerBase.Init(_colorManager);
        _startBlock = _currentTowerBase.GetTopBlock();
        _currentTowerBase.MoveToBasePosition();
    }
    private void SetStartedTrue()
    {
        if (_isGameOver)
        {
            RestartGame().Forget();
            return;
        }

        if (!_isGameStarted)
        {
            _isGameStarted = true;
            StartGame();
            EventBus.StartGame();

            _gameInput.OnBlockPlaced -= SetStartedTrue;
        }
    }
    private void SpawnFirstBlock()
    {
        _lastBlock = _startBlock;
        _currentBlock = _blockSpawner.SpawnBlock(_lastBlock, _startBlock.transform.localScale.x,
        _startBlock.transform.localScale.z, _blockSpawner.CurrentAxis, _moveDistance, GetCurrentDuration());
    }
    private void SpawnNextBlock()
    {
        if (_isGameOver)
        {
            return;
        }
        if (_currentBlock != null)
        {
            _currentBlock.StopMoving();
            bool success = _currentBlock.CutBlock(_lastBlock, _blockSpawner.CurrentAxis);
            if (!success)
            {
                EventBus.GameOver();
                _isGameOver = true;
                _gameInput.OnBlockPlaced -= SpawnNextBlock;
                _gameInput.OnBlockPlaced += SetStartedTrue;
                return;
            }
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
    private async UniTask RestartGame()
    {
        _isGameOver = false;
        _isGameStarted = false;
        _placedBlocks = 0;

        _currentTowerBase.ClearFromScene();
        _blockSpawner.ClearBlocks();
        Destroy(_currentTowerBase.gameObject);

        _gameInput.OnBlockPlaced -= SpawnNextBlock;
        _gameInput.OnBlockPlaced -= SetStartedTrue;

        EventBus.RestartGame();
        await UniTask.WaitForSeconds(1f);
        SpawnTowerBase();
        _gameInput.OnBlockPlaced += SetStartedTrue;
    }
}
