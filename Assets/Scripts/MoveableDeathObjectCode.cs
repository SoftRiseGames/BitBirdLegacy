using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveableDeathObjectCode : MonoBehaviour
{
    [SerializeField] GameObject dotweengidisPoint;
    [SerializeField] float gidis; // s�re
    [SerializeField] float gelis; // isteniyorsa ayr� s�re tan�mlanabilir ama kullan�lmam��
    [SerializeField] float StoppingAmount;

    private Vector3 startPos;

    void Start()
    {
        Vector3 startPos = transform.position;
        bool isFlipped = false;

        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOMove(dotweengidisPoint.transform.position, gidis)
                .SetEase(Ease.InOutSine))
           .AppendCallback(() =>
           {
       // 180 derece d�nd�r
       //transform.rotation = Quaternion.Euler(0, 0, 180f);
               isFlipped = true;
           })
           .AppendInterval(StoppingAmount)
           .Append(transform.DOMove(startPos, gidis)
                .SetEase(Ease.InOutSine))
           .AppendCallback(() =>
           {
       // Orijinal y�ne geri d�nd�r
       //transform.rotation = Quaternion.Euler(0, 0f, 0);
               isFlipped = false;
           })
           .AppendInterval(StoppingAmount)
           .SetLoops(-1);

    }
}
