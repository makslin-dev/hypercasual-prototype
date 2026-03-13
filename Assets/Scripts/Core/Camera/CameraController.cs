using UnityEngine;


public class CameraController : MonoBehaviour
{
    [SerializeField] private GameInputHandler _gameInputHandler;
    [SerializeField] private Transform _cameraTarget;
    private Camera _mainCamera;
    private float _Yoffset = 1f;
    private void Awake()
    {
        _gameInputHandler.OnBlockPlaced += MoveUpByOffset;
    }
    private void MoveUpByOffset()
    {
        print("here");
         _cameraTarget.position = new Vector3(_cameraTarget.transform.position.x,
            _cameraTarget.transform.position.y+_Yoffset, _cameraTarget.transform.position.z);
    }
}
