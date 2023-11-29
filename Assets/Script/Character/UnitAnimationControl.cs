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


    public BattleParticleAndSound particleAndSound;
    AudioSource audioSource;
    public int particleindex;
    public int soundindex;

    int attackType;
    public int AttackType
    {
        set
        {
            attackType = value;
            animator.SetFloat("AttackType", attackType);
        }
    }

    public void SetAnimator()
    {
        animator = GetComponent<Animator>();
        if (animator.runtimeAnimatorController == null)
        {
            switch(GetComponent<Character_type>().major)    //나중에 무기 구분으로 변경
            {
                case PlayerType.Major.Fighter:
                    animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/In Battle/OneHanded");
                    particleAndSound = Resources.Load<BattleParticleAndSound>("SFX & BGM/SFX/Battle/Combat_Fighter");
                    break;
                case PlayerType.Major.Wizard:
                    animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/In Battle/WandStaff");
                    particleAndSound = Resources.Load<BattleParticleAndSound>("SFX & BGM/SFX/Battle/Combat_Wizard");
                    break;
                case PlayerType.Major.Cleric:
                    animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/In Battle/Shield");
                    particleAndSound = Resources.Load<BattleParticleAndSound>("SFX & BGM/SFX/Battle/Combat_Cleric");
                    break;
            }
        }
        if (TryGetComponent<AudioSource>(out audioSource))
            return;
        else
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.volume = 0.8f;
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
            if(TryGetComponent<PlayerType>(out PlayerType playerType))
            {
                if (playerType.gender == PlayerType.Gender.Male)
                    soundindex = 0;
                else
                    soundindex = 1;
            }
            else
                soundindex = 0;
            DeathAnimation();
        }
        else
        {
            HitAnimation();
        }
            
    }

    public void AttackEvent()
    {
        ATEvent();
    }

    public void FakeAttackEvent()
    {
        targetControler.GetDamage();
    }

    public void ParticleEvent()
    {
        particleAndSound.PlayParticle(particleindex);
    }

    public void SoundEvent(int index)
    {
        if (index == -1)
            index = soundindex;
        particleAndSound.PlaySound(audioSource,index);
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
