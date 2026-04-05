using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] private BlockController _block;
    [SerializeField] private Transform _leftBlockSpawn;
    [SerializeField] private Transform _rightBlockSpawn;
    private float _currentY = 0.5f;
    private float _Yoffset = 1f;
    private float _colorHue;
    public MoveAxis CurrentAxis { get; private set; } = MoveAxis.X;
    private void Awake()
    {
        EventBus.OnNextBlockStart += ChangeAxis;
    }
    public BlockController SpawnBlock(BlockController lastBlock, float scaleX, float scaleZ, MoveAxis axis)
    {
        Vector3 newPos = Vector3.zero;
        if (CurrentAxis == MoveAxis.X)
        {
            newPos = _leftBlockSpawn.position; 
        }
        else
        {
            newPos = _rightBlockSpawn.position;
        }
        _currentY += _Yoffset;
        newPos.y = _currentY;
        if (axis == MoveAxis.X)
        {
            newPos.z = lastBlock.transform.position.z;
        }
        else
        {
            newPos.x = lastBlock.transform.position.x;
        }
        var block = Instantiate(_block, newPos, Quaternion.identity);
        block.SetScale(scaleX,scaleZ);
        _colorHue += 0.05f;
        Color color = Color.HSVToRGB(_colorHue % 1f, 0.8f, 0.9f);
        block.SetColor(color);
        if (CurrentAxis == MoveAxis.X)
        {
            block.MoveBlockX();
        }
        else
        {
            block.MoveBlockZ();
        }
        return block;
    }
    private void ChangeAxis()
    {
        print("changed");
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
