using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TransitionEvent : MonoBehaviour
{
    public Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        TransitionOpen();
    }
    private void OnEnable()
    {
        CharacterManager.isDeathEvent += TransitionClose;

    }
    private void OnDisable()
    {
        CharacterManager.isDeathEvent -= TransitionClose;
    }
    void TransitionOpen()
    {
        animator.SetBool("isTransitionOpen", true);
    }
    void TransitionClose()
    {
        animator.SetBool("isTransitionClose", true);
    }
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
