using DG.Tweening;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private float _distance = 3f;
    [SerializeField] private float _duration = 1f;

    private Tween _moveTween;

    private void Start()
    {
        MoveBlock();
    }
    private void OnDisable()
    {
        _moveTween?.Kill();
    }
    private void MoveBlock()
    {
        _moveTween = _rigidBody.DOMoveX(_distance, _duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }
    public void StopMoving()
    {
        _moveTween?.Kill();
    }
}
