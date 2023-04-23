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
              .Append(this.gameObject.transform.DOMoveX(3.2f, gidis).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo));
    }

    // Update is called once per frame
    void Update()
    {
        


    }
    
    
}
