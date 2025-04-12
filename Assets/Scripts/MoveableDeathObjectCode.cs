using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MoveableDeathObjectCode : MonoBehaviour
{
    [SerializeField] GameObject dotweengidisPoint;
    [SerializeField] float gidis;
    [SerializeField] float gelis;

    void Start()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(this.gameObject.transform.DOMove(dotweengidisPoint.transform.position, gidis)
                .SetEase(Ease.Linear))
           .AppendInterval(0.5f) 
           .Append(this.gameObject.transform.DOMove(this.transform.position, gidis)
                .SetEase(Ease.Linear))
           .AppendInterval(0.5f)
           .SetLoops(-1); 
    }

   

}
