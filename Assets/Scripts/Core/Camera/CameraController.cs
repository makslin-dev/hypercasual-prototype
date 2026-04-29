using Cysharp.Threading.Tasks;
using System.Threading;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private const float DEFAULT_ORTHOGRAPHIC_SIZE = 10f;
    private readonly Vector3 DEFAULT_CAMERA_TARGET_POS = new Vector3(0,0,4);
    [SerializeField] private GameInputHandler _gameInputHandler;
    [SerializeField] private CinemachineCamera _cinemachineCam;
    [SerializeField] private Transform _cameraTarget;
    [SerializeField] private float _zoomDuration = 2f;
    [SerializeField] private CinemachinePositionComposer _cinemachinePosComposer;
    private CancellationTokenSource _zoomCts;
    private float _Yoffset = 1f;
    private float _minDistance = 10f;
    private void OnEnable()
    {
        SubscribeToEvents();
    }
    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }
    private void SubscribeToEvents()
    {
        _gameInputHandler.OnBlockPlaced += MoveUpByOffset;
        EventBus.OnGameOver += HandleGameOver;
        EventBus.OnGameRestarted += HandleGameRestart;
    }
    private void UnsubscribeFromEvents()
    {
        _gameInputHandler.OnBlockPlaced -= MoveUpByOffset;
        EventBus.OnGameOver -= HandleGameOver;
        EventBus.OnGameRestarted -= HandleGameRestart;
    }
    private void MoveUpByOffset()
    {
        _cameraTarget.position = new Vector3(_cameraTarget.transform.position.x,
           _cameraTarget.transform.position.y + _Yoffset, _cameraTarget.transform.position.z);
    }
    private void HandleGameOver()
    {
        _zoomCts?.Cancel(); 
        _zoomCts = new CancellationTokenSource();
        _gameInputHandler.OnBlockPlaced -= MoveUpByOffset;
        AnimateCameraZoomOut(_zoomCts.Token).Forget();
    }
    private void HandleGameRestart()
    {
        _zoomCts?.Cancel();
        AnimateRestoringToDefaultPosition().Forget();
    }
    private void MoveCameraTargetToDefault()
    {
        _cinemachinePosComposer.Damping = new Vector3(0, 0, 0);
        _cameraTarget.position = DEFAULT_CAMERA_TARGET_POS;
        _cinemachineCam.Lens.OrthographicSize = DEFAULT_ORTHOGRAPHIC_SIZE;
    }
    private async UniTask AnimateRestoringToDefaultPosition()
    {
        _cinemachinePosComposer.Damping = new Vector3(0, 0, 0);
        await UniTask.WaitForSeconds(1f);
        MoveCameraTargetToDefault();
        _gameInputHandler.OnBlockPlaced += MoveUpByOffset;

        await UniTask.Yield(); 
        _cinemachinePosComposer.Damping = new Vector3(1, 1, 1);
    }
    private async UniTask AnimateCameraZoomOut(CancellationToken token)
    {
        float height = _cameraTarget.position.y;

        Vector3 startPosition = _cameraTarget.position;
        float startSize = _cinemachineCam.Lens.OrthographicSize;

        Vector3 targetPosition = new Vector3(startPosition.x, height / 2f, startPosition.z);

        float padding = 4f;
        float angleCompensation = 1.2f;
        float targetSize = Mathf.Max(_minDistance, ((height / 2f) + padding) * angleCompensation);

        float elapsedTime = 0f;

        while (elapsedTime < _zoomDuration)
        {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / _zoomDuration;
            t = Mathf.SmoothStep(0f, 1f, t);

            _cameraTarget.position = Vector3.Lerp(startPosition, targetPosition, t);
            _cinemachineCam.Lens.OrthographicSize = Mathf.Lerp(startSize, targetSize, t);
            await UniTask.Yield(PlayerLoopTiming.Update, token);
        }

        _cameraTarget.position = targetPosition;
        _cinemachineCam.Lens.OrthographicSize = targetSize;
    }

}
