using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] private BlockController _block;

    private float _currentY = 0.5f;
    private float _Yoffset = 1f;

    public BlockController SpawnBlock()
    {
        _currentY += _Yoffset;
        var newPos =  new Vector3(0, _currentY, 4);
        var block = Instantiate(_block,newPos, Quaternion.identity);
        return block;
    }
}
