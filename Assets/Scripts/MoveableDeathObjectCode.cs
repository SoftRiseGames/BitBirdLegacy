using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MoveableDeathObjectCode : MonoBehaviour
{
    [SerializeField] GameObject dotweengidisPoint;
    [SerializeField] float gidis;
    [SerializeField] float gelis;
    [SerializeField] float StoppingAmount;

    void Start()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(this.gameObject.transform.DOMove(dotweengidisPoint.transform.position, gidis)
                .SetEase(Ease.Linear))
           .AppendInterval(StoppingAmount) 
           .Append(this.gameObject.transform.DOMove(this.transform.position, gidis)
                .SetEase(Ease.Linear))
           .AppendInterval(StoppingAmount)
           .SetLoops(-1); 
    }

   

}
