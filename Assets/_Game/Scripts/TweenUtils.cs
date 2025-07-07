using DG.Tweening;
using UnityEngine;

public static class TweenUtils
{
    public static void DoSpawnTween(this Transform transform)
    {
        Vector3 startPos = transform.position + Vector3.up * 0.5f;
        transform.position = startPos;

        transform.DOMoveY(transform.position.y - 0.5f, 0.4f)
            .SetEase(Ease.OutBounce);

        transform.DOScale(Vector3.one * 1.1f, 0.1f)
            .SetLoops(2, LoopType.Yoyo);

        transform.DORotate(Vector3.zero, 0.3f)
            .SetEase(Ease.InOutSine);
    }
}