using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class UnitAnimationControl : MonoBehaviour
{
    public delegate void CardAnimationEvent();
    public CardAnimationEvent ATEvent;
    Animator animator;
    public UnitAnimationControl targetControler;
    public void SetAnimator()
    {
        animator = GetComponent<Animator>();
        if (animator.runtimeAnimatorController == null)
        {
            switch(GetComponent<Character_type>().major)    //나중에 무기 구분으로 변경
            {
                case PlayerType.Major.Fighter:
                    animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/In Battle/OneHanded");
                    break;
                case PlayerType.Major.Wizard:
                    animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/In Battle/WandStaff");
                    break;
                case PlayerType.Major.Cleric:
                    animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/In Battle/Shield");
                    break;
            }
        }
    }

    public void HitAnimation()
    {
        animator.SetTrigger("Hit");
    }

    public void DeathAnimation()
    {
        animator.SetTrigger("Die");
        
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

    public void FakeAttackEvent()
    {
        targetControler.GetDamage();
    }

    public void DeathEvent()
    {
        animator.SetBool("Death", true);
        N_BattleManager.instance.ExitBattle(GetComponent<Unit>());
    }

    public void AnnounceEndAction()
    {
        N_BattleManager.instance.IsAction = false;
    }

    public void AnnounceStartAction()
    {
        N_BattleManager.instance.IsAction = true;
    }
}
