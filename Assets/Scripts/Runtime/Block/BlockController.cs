using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;

public class BlockController : MonoBehaviour
{
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
        _moveTween = transform.DOMoveX(_distance, _duration).From(-_distance).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }
    public void StopMoving()
    {
        _moveTween?.Kill();
    }
    public void SetScaleX(float scaleX)
    {
        var newScale = transform.localScale;
        newScale.x = scaleX;
        transform.localScale = newScale;
    }
    public bool CutBlock(BlockController previousBlock)
    {
        float offset = transform.position.x - previousBlock.transform.position.x;

        float tolerance = 0.3f;

        if (Mathf.Abs(offset) < tolerance)
        {
            transform.position = new Vector3(
                previousBlock.transform.position.x,
                transform.position.y,
                transform.position.z);
            print("perfect");
            return true;
        }
        float prevLeft = previousBlock.transform.position.x - previousBlock.transform.localScale.x / 2f;
        float prevRight = previousBlock.transform.position.x + previousBlock.transform.localScale.x / 2f;

        float myLeft = transform.position.x - transform.localScale.x / 2f;
        float myRight = transform.position.x + transform.localScale.x / 2f;

        float overlapLeft = Mathf.Max(prevLeft, myLeft);
        float overlapRight = Mathf.Min(prevRight, myRight);

        if (overlapRight <= overlapLeft)
        {
            Debug.LogWarning("you lost");
            return false;
        }
        float newSizeX = overlapRight - overlapLeft;
        float newCentreX = overlapLeft + (newSizeX / 2);
        if (myLeft < overlapLeft) //need to spawn on the left 
        {
            float pieceSize = overlapLeft - myLeft;
            float pieceCenterX = (overlapLeft - (pieceSize / 2));
            SpawnFallingPiece(pieceCenterX,pieceSize);
        }
        if (myRight > overlapRight) //need to spawn on the right
        {
            float pieceSize = myRight - overlapRight;
            float pieceCentre = overlapRight + (pieceSize / 2);
            SpawnFallingPiece(pieceCentre, pieceSize);
        }
        var newScale = transform.localScale;
        newScale.x = newSizeX;
        transform.localScale = newScale;

        var newPosX = newCentreX;
        transform.position = new Vector3(newPosX, transform.position.y,transform.position.z);

        return true;
    }

    private void SpawnFallingPiece(float posX, float sizeX)
    {
        GameObject fallingPiece = GameObject.CreatePrimitive(PrimitiveType.Cube);
        fallingPiece.transform.position = new Vector3(posX, transform.position.y, transform.position.z);
        fallingPiece.transform.localScale = new Vector3(sizeX, transform.localScale.y, transform.localScale.z);
        fallingPiece.AddComponent<Rigidbody>();

        if (TryGetComponent<Renderer>(out var myRenderer))
        {
            fallingPiece.GetComponent<Renderer>().material = myRenderer.material;
        }
        Destroy(fallingPiece, 3f);
    }
}
