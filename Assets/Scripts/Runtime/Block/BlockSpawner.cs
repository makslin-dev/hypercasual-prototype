using UnityEngine;
using Zenject;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] private BlockController _block;
    [SerializeField] private Transform _leftBlockSpawn;
    [SerializeField] private Transform _rightBlockSpawn;
    private float _currentY = 0.5f;
    private float _Yoffset = 1f;
    public MoveAxis CurrentAxis { get; private set; } = MoveAxis.Z;
    private ColorManager _colorManager;
    [Inject]
    private void Construct(ColorManager colorManager)
    {
        _colorManager = colorManager;
    }
    private void Awake()
    {
        EventBus.OnNextBlockStart += ChangeAxis;
    }

    public BlockController SpawnBlock(BlockController lastBlock, float scaleX, float scaleZ, MoveAxis axis, float distance, float duration)
    {
        Vector3 newPos = lastBlock.transform.position;
        _currentY += _Yoffset;
        newPos.y = _currentY;

        float target;

        if (axis == MoveAxis.X)
        {
            newPos.x = lastBlock.transform.position.x - distance;
            newPos.z = lastBlock.transform.position.z;
            target = lastBlock.transform.position.x + distance;
        }
        else
        {
            newPos.z = lastBlock.transform.position.z - distance;
            newPos.x = lastBlock.transform.position.x;
            target = lastBlock.transform.position.z + distance;
        }

        var block = Instantiate(_block, newPos, Quaternion.identity);
        block.SetScale(scaleX, scaleZ);

        block.SetColor(_colorManager.GetNextColor());

        block.ConfigureMovement(duration);

        if (axis == MoveAxis.X)
        {
            block.MoveBlockX(target);
        }
        else
        {
            block.MoveBlockZ(target);
        }

        return block;
    }

    private void ChangeAxis()
    {
        if (CurrentAxis == MoveAxis.X)
        {
            CurrentAxis = MoveAxis.Z;
        }
        else
        {
            CurrentAxis = MoveAxis.X;
        }
    }
}