using DG.Tweening;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    [SerializeField] private float _duration = 1f;
    [SerializeField] private Renderer _renderer;

    private Tween _moveTween;

    private void OnDisable()
    {
        _moveTween?.Kill();
    }
    public void MoveBlockX(float targetX)
    {
        _moveTween?.Kill();
        _moveTween = transform.DOMoveX(targetX, _duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear);
    }
    public void MoveBlockZ(float targetZ)
    {
        _moveTween?.Kill();
        _moveTween = transform.DOMoveZ(targetZ, _duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear);
    }
    public void ConfigureMovement(float duration)
    {
        _duration = duration;
    }
    public void SetColor(Color color)
    {
        _renderer.material.color = color;
    }
    public void StopMoving()
    {
        _moveTween?.Kill();
    }
    public void SetScale(float scaleX, float scaleZ)
    {
        var newScale = transform.localScale;
        newScale.x = scaleX;
        newScale.z = scaleZ;
        transform.localScale = newScale;
    }
    public bool CutBlock(BlockController previousBlock, MoveAxis moveAxis)
    {
        float tolerance = 0.3f;
        bool isX = moveAxis == MoveAxis.X;

        float myPos = isX ? transform.position.x : transform.position.z;
        float mySize = isX ? transform.localScale.x : transform.localScale.z;

        float prevPos = isX ? previousBlock.transform.position.x : previousBlock.transform.position.z;
        float prevSize = isX ? previousBlock.transform.localScale.x : previousBlock.transform.localScale.z;

        float offset = myPos - prevPos;
        if (Mathf.Abs(offset) < tolerance)
        {
            Vector3 perfectPos = transform.position;
            if (isX) perfectPos.x = prevPos;
            else perfectPos.z = prevPos;

            transform.position = perfectPos;
            print("perfect");
            return true;
        }
        float prevMin = prevPos - prevSize / 2f;
        float prevMax = prevPos + prevSize / 2f;
        float myMin = myPos - mySize / 2f;
        float myMax = myPos + mySize / 2f;

        float overlapMin = Mathf.Max(prevMin, myMin);
        float overlapMax = Mathf.Min(prevMax, myMax);
        if (overlapMax <= overlapMin)
        {
            SpawnPieceHelper(myPos, mySize, isX);
            Destroy(this.gameObject);
            return false;
        }
        float newSize = overlapMax - overlapMin;
        float newCenter = overlapMin + (newSize / 2f);
        if (myMin < overlapMin) 
        {
            float pieceSize = overlapMin - myMin;
            float pieceCenter = overlapMin - (pieceSize / 2f);
            SpawnPieceHelper(pieceCenter, pieceSize, isX);
        }
        if (myMax > overlapMax) 
        {
            float pieceSize = myMax - overlapMax;
            float pieceCenter = overlapMax + (pieceSize / 2f);
            SpawnPieceHelper(pieceCenter, pieceSize, isX);
        }
        Vector3 newScaleVec = transform.localScale;
        Vector3 newPosVec = transform.position;

        if (isX)
        {
            newScaleVec.x = newSize;
            newPosVec.x = newCenter;
        }
        else
        {
            newScaleVec.z = newSize;
            newPosVec.z = newCenter;
        }

        transform.localScale = newScaleVec;
        transform.position = newPosVec;

        return true;
    }
    private void SpawnPieceHelper(float center, float size, bool isX)
    {
        Vector3 piecePos = transform.position;
        Vector3 pieceScale = transform.localScale;

        if (isX)
        {
            piecePos.x = center;
            pieceScale.x = size;
        }
        else
        {
            piecePos.z = center;
            pieceScale.z = size;
        }

        SpawnFallingPiece(piecePos, pieceScale);
    }
    private void SpawnFallingPiece(Vector3 pos, Vector3 scale)
    {
        GameObject fallingPiece = GameObject.CreatePrimitive(PrimitiveType.Cube);
        fallingPiece.transform.position = pos;
        fallingPiece.transform.localScale = scale;
        fallingPiece.AddComponent<Rigidbody>();

        if (TryGetComponent<Renderer>(out var myRenderer))
        {
            fallingPiece.GetComponent<Renderer>().material = myRenderer.material;
        }
        Destroy(fallingPiece, 3f);
    }
}
