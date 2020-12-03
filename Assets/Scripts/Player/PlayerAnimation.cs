using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator _baseAnimator;

    [SerializeField]
    private Animator _frontAnimator;

    public void Walk(float moveSpeed)
    {
        _baseAnimator.SetFloat("Speed", Mathf.Abs(moveSpeed));
        _frontAnimator.SetFloat("Speed", Mathf.Abs(moveSpeed));
    }

    public void Hurt()
    {
        _baseAnimator.SetTrigger("Hurt");
        _frontAnimator.SetTrigger("Hurt");
    }

    public void Jump()
    {
        _baseAnimator.SetTrigger("Jump");
        _frontAnimator.SetTrigger("Jump");
    }

    public void Throw()
    {
        _baseAnimator.SetTrigger("Throw");
        _frontAnimator.SetTrigger("Throw");
    }
}
