using UnityEngine;
using Zenject;

public class TowerBase : MonoBehaviour
{
    [SerializeField] private BlockController _topBlock;
    [SerializeField] private BlockController _middleBlock;
    [SerializeField] private BlockController _bottomBlock;
    private ColorManager _colorManager;
    [Inject]
    private void Construct(ColorManager colorManager)
    {
        _colorManager = colorManager;
    }
    private void Start()
    {    
        _bottomBlock.SetColor(_colorManager.GetNextColor());
        _middleBlock.SetColor(_colorManager.GetNextColor());
        _topBlock.SetColor(_colorManager.GetNextColor());
    }
}
