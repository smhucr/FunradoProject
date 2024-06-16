using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectSpinner : MonoBehaviour
{
    [Header("Rotation")]
    public Vector3 rotationDegree;
    public float rotationTime;
    [Header("Move")]
    public float moveDirection;// Y direction
    public float moveSpeed;
    private Tween tweenRotation;
    private Tween tweenMove;
    private void Start()
    {
        // 360 Degree spin object with DoTween
        if (rotationDegree != Vector3.zero)
            tweenRotation = transform.DORotate(rotationDegree, rotationTime, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
        tweenMove = transform.DOMove(new Vector3(transform.position.x, moveDirection, transform.position.z), moveSpeed).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

    private void OnDisable()
    {
        tweenRotation.Kill();
        tweenMove.Kill();
    }
}
