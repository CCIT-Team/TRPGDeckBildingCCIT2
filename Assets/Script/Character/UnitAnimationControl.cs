using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class UnitAnimationControl : MonoBehaviour
{
    public delegate void CardAnimationEvent();
    public CardAnimationEvent ATEvent;
    Animator animator;
    public void SetAnimator()
    {
        animator = GetComponent<Animator>();
        if (animator.runtimeAnimatorController == null)
        {
            animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Test_Assets/Animations/Unit Animator Controler");
        }
    }

    public void HitAnimation()
    {
        animator.SetTrigger("Hit");
    }

    public void DeathAnimation()
    {
        animator.SetTrigger("Die");
        animator.SetBool("Death",true);
    }

    public void AttackAnimation()
    {
        animator.SetTrigger("Attack");
    }

    public void SwitchMoveAnimation()
    {
        animator.SetBool("",!animator.GetBool(""));
    }

    public void GetDamage()
    {
        if (GetComponent<Unit>().Hp <= 0)
        {
            DeathAnimation();
        }
        else
            HitAnimation();
    }

    public void AttackEvent()
    {
        ATEvent();
    }
}
