using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveableDeathObjectCode : MonoBehaviour
{
    [SerializeField] GameObject dotweengidisPoint;
    [SerializeField] float gidis; // süre
    [SerializeField] float gelis; // isteniyorsa ayrý süre tanýmlanabilir ama kullanýlmamýþ
    [SerializeField] float StoppingAmount;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;

        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOMove(dotweengidisPoint.transform.position, gidis)
                .SetEase(Ease.InOutSine)) // ivmelenerek baþlar ve yavaþlayarak biter
           .AppendInterval(StoppingAmount)
           .Append(transform.DOMove(startPos, gidis)
                .SetEase(Ease.InOutSine))
           .AppendInterval(StoppingAmount)
           .SetLoops(-1);
    }
}
