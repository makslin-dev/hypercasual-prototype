using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;
using UnityEngine;
using Zenject;

public class TowerBase : MonoBehaviour
{
    [SerializeField] private BlockController _topBlock;
    [SerializeField] private BlockController _middleBlock;
    [SerializeField] private BlockController _bottomBlock;
    private ColorManager _colorManager;

    public void Init(ColorManager colorManager)
    {
        _colorManager = colorManager;
        _bottomBlock.SetColor(_colorManager.GetNextColor());
        _middleBlock.SetColor(_colorManager.GetNextColor());
        _topBlock.SetColor(_colorManager.GetNextColor());
    }
    public BlockController GetTopBlock()
    {
        return _topBlock;
    }
    public void ClearFromScene()
    {
        _topBlock.ClearFromScene();
       _middleBlock.ClearFromScene();
       _bottomBlock.ClearFromScene();
    }
    public void MoveToBasePosition()
    {
        Vector3 targetPos = new Vector3(transform.position.x, 0.5f, transform.position.z);

        transform.DOMove(targetPos, 0.2f)
            .SetEase(Ease.Linear);       
    }
}
