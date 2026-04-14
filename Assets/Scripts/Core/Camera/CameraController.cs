using Cysharp.Threading.Tasks;
using System.Threading;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameInputHandler _gameInputHandler;
    [SerializeField] private CinemachineCamera _cinemachineCam;
    [SerializeField] private Transform _cameraTarget;
    [SerializeField] private float _zoomDuration = 2f;
    private Camera _mainCamera;
    private float _Yoffset = 1f;
    private float _minDistance = 10f;
    private float _distanceMultiplier = 0.4f;
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
    }
    private void UnsubscribeFromEvents()
    {
        _gameInputHandler.OnBlockPlaced -= MoveUpByOffset;
        EventBus.OnGameOver += HandleGameOver;
    }
    private void MoveUpByOffset()
    {
        _cameraTarget.position = new Vector3(_cameraTarget.transform.position.x,
           _cameraTarget.transform.position.y + _Yoffset, _cameraTarget.transform.position.z);
    }
    private void HandleGameOver()
    {
        _gameInputHandler.OnBlockPlaced -= MoveUpByOffset;
        AnimateCameraZoomOut(this.GetCancellationTokenOnDestroy()).Forget();
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
