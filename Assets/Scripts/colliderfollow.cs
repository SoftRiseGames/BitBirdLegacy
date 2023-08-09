using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderfollow : MonoBehaviour
{
    [SerializeField] Transform charactertransform;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = charactertransform.gameObject.transform.position;
    }
}
