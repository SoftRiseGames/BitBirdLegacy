using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class platform : MonoBehaviour
{
    [SerializeField] GameObject dotweengidisPoint;
    [SerializeField] float gidis;
    [SerializeField] float gelis;

    
    void Start()
    {
        DOTween.Sequence()
              .Append(this.gameObject.transform.DOMove(dotweengidisPoint.transform.position, gidis).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo));
    }

}
