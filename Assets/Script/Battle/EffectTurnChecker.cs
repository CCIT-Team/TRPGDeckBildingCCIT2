using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    Burn = -8,
    Faint = -7,
    Confusion = -6,
    Weak = -5,
    Speed_Decrease = -4,
    Luck_Decrease = -3,
    Intelliegence_Decrease = -2,
    Strength_Decrease = -1,
    None = 0,
    Strength_Increase,
    Intelliegence_Increase,
    Luck_Increase,
    Speed_Increase,
    Thorn,
    MagicMirror,
    Regeneration,
    AttackGuard,
    MagicGuard,
    Parry,
    BashEffect,
    Ignition,
    DoubleCharm
}

public class EffectTurnChecker : MonoBehaviour
{
    public bool[] isBuffRun = new bool[22];
    public EffectType effect;
    public Character boundCharacter = null;
    public Monster boundMonster = null;

    public void StartEffect(EffectType effectType, float effectvalue, int turn)
    {
        if (isBuffRun[(int)effectType + 8])
        {
            isBuffRun[(int)effectType + 8] = false;
            StartEffect(effectType, effectvalue, turn);
        }
        else
        {
            N_BattleManager.instance.isBuffRun_All = true;
            isBuffRun[(int)effectType + 8] = true;
            switch (effectType)
            {
                case EffectType.Burn:
                    StartCoroutine(Burn(effectType, effectvalue, turn));
                    break;
                case EffectType.Faint:
                    StartCoroutine(Faint(effectType, effectvalue, turn));
                    break;
                case EffectType.Confusion:
                    StartCoroutine(Confusion(effectType, effectvalue, turn));
                    break;
                case EffectType.Weak:
                    StartCoroutine(Weak(effectType, effectvalue, turn));
                    break;
                case EffectType.Speed_Decrease:
                    StartCoroutine(Speed_Decrease(effectType, effectvalue, turn));
                    break;
                case EffectType.Luck_Decrease:
                    StartCoroutine(Luck_Decrease(effectType, effectvalue, turn));
                    break;
                case EffectType.Intelliegence_Decrease:
                    StartCoroutine(Intelliegence_Decrease(effectType, effectvalue, turn));
                    break;
                case EffectType.Strength_Decrease:
                    StartCoroutine(Strength_Decrease(effectType, effectvalue, turn));
                    break;
                case EffectType.None:
                    break;
                case EffectType.Strength_Increase:
                    StartCoroutine(Strength_Increase(effectType, effectvalue, turn));
                    break;
                case EffectType.Intelliegence_Increase:
                    StartCoroutine(Intelliegence_Increase(effectType, effectvalue, turn));
                    break;
                case EffectType.Luck_Increase:
                    StartCoroutine(Luck_Increase(effectType, effectvalue, turn));
                    break;
                case EffectType.Speed_Increase:
                    StartCoroutine(Speed_Increase(effectType, effectvalue, turn));
                    break;
                case EffectType.Thorn:
                    StartCoroutine(Thorn(effectType, effectvalue, turn));
                    break;
                case EffectType.MagicMirror:
                    StartCoroutine(MagicMirror(effectType, effectvalue, turn));
                    break;
                case EffectType.Regeneration:
                    StartCoroutine(Regeneration(effectType, effectvalue, turn));
                    break;
                case EffectType.AttackGuard:
                    StartCoroutine(AttackGuard(effectType, effectvalue, turn));
                    break;
                case EffectType.MagicGuard:
                    StartCoroutine(MagicGuard(effectType, effectvalue, turn));
                    break;
                case EffectType.Parry:
                    StartCoroutine(Parry(effectType, effectvalue, turn));
                    break;
                case EffectType.Ignition:
                    StartCoroutine(Ignition(effectType, effectvalue, turn));
                    break;
                case EffectType.DoubleCharm:
                    StartCoroutine(DoubleCharm(effectType, effectvalue, turn));
                    break;
            }
        }
    }

    public void StopEffect(EffectType effectType)
    {
        isBuffRun[(int)effectType + 8] = false;
    }


    IEnumerator Faint(EffectType effectType, float effectvalue, int turn)
    {
        if(boundCharacter != null)
            while (turn >= -1)
            {
                yield return new WaitUntil(() => (boundCharacter.isMyturn && isBuffRun[(int)effectType + 8]) || !N_BattleManager.instance.isBuffRun_All);
                boundCharacter.isMyturn = false;
                yield return new WaitUntil(() => (!boundCharacter.isMyturn && isBuffRun[(int)effectType + 8]) || !N_BattleManager.instance.isBuffRun_All);
                turn--;
            }
        else
            while (turn >= -1)
            {
                yield return new WaitUntil(() => boundMonster.IsMyturn && isBuffRun[(int)effectType + 8] && !N_BattleManager.instance.isBuffRun_All);
                boundMonster.IsMyturn = false;
                yield return new WaitUntil(() => !boundMonster.IsMyturn && isBuffRun[(int)effectType + 8] && !N_BattleManager.instance.isBuffRun_All);
                turn--;
            }
        isBuffRun[(int)effectType + 8] = false;
    }

    IEnumerator Burn(EffectType effectType, float effectvalue, int turn)
    {
        if (boundCharacter != null)
            while (turn >= -1)
            {
                yield return new WaitUntil(() => (boundCharacter.isMyturn && isBuffRun[(int)effectType + 8]) || !N_BattleManager.instance.isBuffRun_All);
                boundCharacter.Damaged(boundCharacter.maxHp * effectvalue);
                yield return new WaitUntil(() => (!boundCharacter.isMyturn && isBuffRun[(int)effectType + 8]) || !N_BattleManager.instance.isBuffRun_All);
                turn--;
            }
        else
            while (turn >= -1)
            {
                yield return new WaitUntil(() => boundMonster.IsMyturn && isBuffRun[(int)effectType + 8] && !N_BattleManager.instance.isBuffRun_All);
                boundMonster.Damaged(boundMonster.maxHp * effectvalue);
                yield return new WaitUntil(() => !boundMonster.IsMyturn && isBuffRun[(int)effectType + 8] && !N_BattleManager.instance.isBuffRun_All);
                turn--;
            }
        isBuffRun[(int)effectType + 8] = false;
    }

    IEnumerator Confusion(EffectType effectType, float effectvalue, int turn)
    {
        if (boundCharacter != null)
            while (turn >= -1)
            {
                yield return new WaitUntil(() => (boundCharacter.isMyturn && isBuffRun[(int)effectType + 8]) || !N_BattleManager.instance.isBuffRun_All);
                yield return new WaitUntil(() => (!boundCharacter.isMyturn && isBuffRun[(int)effectType + 8]) || !N_BattleManager.instance.isBuffRun_All);
                turn--;
            }
        else
            while (turn >= -1)
            {
                yield return new WaitUntil(() => boundMonster.IsMyturn && isBuffRun[(int)effectType + 8] && !N_BattleManager.instance.isBuffRun_All);
                yield return new WaitUntil(() => !boundMonster.IsMyturn && isBuffRun[(int)effectType + 8] && !N_BattleManager.instance.isBuffRun_All);
                turn--;
            }
        isBuffRun[(int)effectType + 8] = false;
    }

    IEnumerator Weak(EffectType effectType, float effectvalue, int turn)
    {
        if (boundCharacter != null)
            while (turn >= -1)
            {
                yield return new WaitUntil(() => (boundCharacter.isMyturn && isBuffRun[(int)effectType + 8]) || !N_BattleManager.instance.isBuffRun_All);
                yield return new WaitUntil(() => (!boundCharacter.isMyturn && isBuffRun[(int)effectType + 8]) || !N_BattleManager.instance.isBuffRun_All);
                turn--;
            }
        else
            while (turn >= -1)
            {
                yield return new WaitUntil(() => boundMonster.IsMyturn && isBuffRun[(int)effectType + 8] && !N_BattleManager.instance.isBuffRun_All);
                yield return new WaitUntil(() => !boundMonster.IsMyturn && isBuffRun[(int)effectType + 8] && !N_BattleManager.instance.isBuffRun_All);
                turn--;
            }
        isBuffRun[(int)effectType + 8] = false;
    }

    IEnumerator Speed_Decrease(EffectType effectType, float effectvalue, int turn)
    {
       
        if (boundCharacter != null)
        {
            boundCharacter.speed -= (int)effectvalue;
            while (turn >= -1)
            {
                yield return new WaitUntil(() => (boundCharacter.isMyturn && isBuffRun[(int)effectType + 8]) || !N_BattleManager.instance.isBuffRun_All);
                yield return new WaitUntil(() => (!boundCharacter.isMyturn && isBuffRun[(int)effectType + 8]) || !N_BattleManager.instance.isBuffRun_All);
                turn--;
            }
            boundCharacter.speed += (int)effectvalue;
        }
        else
        {
            boundMonster.speed -= (int)effectvalue;
            while (turn >= -1)
            {
                yield return new WaitUntil(() => boundMonster.IsMyturn && isBuffRun[(int)effectType + 8] && !N_BattleManager.instance.isBuffRun_All);
                yield return new WaitUntil(() => !boundMonster.IsMyturn && isBuffRun[(int)effectType + 8] && !N_BattleManager.instance.isBuffRun_All);
                turn--;
            }
            boundMonster.speed += (int)effectvalue;
        }
        isBuffRun[(int)effectType + 8] = false;
    }

    IEnumerator Luck_Decrease(EffectType effectType, float effectvalue, int turn)
    {
        if (boundCharacter != null)
        {
            boundCharacter.luck -= (int)effectvalue;
            while (turn >= -1)
            {
                yield return new WaitUntil(() => (boundCharacter.isMyturn && isBuffRun[(int)effectType + 8]) || !N_BattleManager.instance.isBuffRun_All);
                yield return new WaitUntil(() => (!boundCharacter.isMyturn && isBuffRun[(int)effectType + 8]) || !N_BattleManager.instance.isBuffRun_All);
                turn--;
            }
            boundCharacter.luck += (int)effectvalue;
        }
        else
        {
            boundMonster.luck -= (int)effectvalue;
            while (turn >= -1)
            {
                yield return new WaitUntil(() => boundMonster.IsMyturn && isBuffRun[(int)effectType + 8] && !N_BattleManager.instance.isBuffRun_All);
                yield return new WaitUntil(() => !boundMonster.IsMyturn && isBuffRun[(int)effectType + 8] && !N_BattleManager.instance.isBuffRun_All);
                turn--;
            }
            boundMonster.luck += (int)effectvalue;
        }
        isBuffRun[(int)effectType + 8] = false;
    }

    IEnumerator Intelliegence_Decrease(EffectType effectType, float effectvalue, int turn)
    {
        if (boundCharacter != null)
        {
            boundCharacter.intelligence -= (int)effectvalue;
            while (turn >= -1)
            {
                yield return new WaitUntil(() => (boundCharacter.isMyturn && isBuffRun[(int)effectType + 8]) || !N_BattleManager.instance.isBuffRun_All);
                yield return new WaitUntil(() => (!boundCharacter.isMyturn && isBuffRun[(int)effectType + 8]) || !N_BattleManager.instance.isBuffRun_All);
                turn--;
            }
            boundCharacter.intelligence += (int)effectvalue;
        }
        else
        {
            boundMonster.intelligence -= (int)effectvalue;
            while (turn >= -1)
            {
                yield return new WaitUntil(() => boundMonster.IsMyturn && isBuffRun[(int)effectType + 8] && !N_BattleManager.instance.isBuffRun_All);
                yield return new WaitUntil(() => !boundMonster.IsMyturn && isBuffRun[(int)effectType + 8] && !N_BattleManager.instance.isBuffRun_All);
                turn--;
            }
            boundMonster.intelligence += (int)effectvalue;
        }
        isBuffRun[(int)effectType + 8] = false;
    }

    IEnumerator Strength_Decrease(EffectType effectType, float effectvalue, int turn)
    {
        if (boundCharacter != null)
        {
            boundCharacter.strength -= (int)effectvalue;
            while (turn >= -1)
            {
                yield return new WaitUntil(() => (boundCharacter.isMyturn && isBuffRun[(int)effectType + 8]) || !N_BattleManager.instance.isBuffRun_All);
                yield return new WaitUntil(() => (!boundCharacter.isMyturn && isBuffRun[(int)effectType + 8]) || !N_BattleManager.instance.isBuffRun_All);
                turn--;
            }
            boundCharacter.strength += (int)effectvalue;
        }
        else
        {
            boundMonster.strength -= (int)effectvalue;
            while (turn >= -1)
            {
                yield return new WaitUntil(() => boundMonster.IsMyturn && isBuffRun[(int)effectType + 8] && !N_BattleManager.instance.isBuffRun_All);
                yield return new WaitUntil(() => !boundMonster.IsMyturn && isBuffRun[(int)effectType + 8] && !N_BattleManager.instance.isBuffRun_All);
                turn--;
            }
            boundMonster.strength += (int)effectvalue;
        }
        isBuffRun[(int)effectType + 8] = false;
    }

    IEnumerator Strength_Increase(EffectType effectType, float effectvalue, int turn)
    {
        if (boundCharacter != null)
        {
            boundCharacter.strength += (int)effectvalue;
            while (turn >= -1)
            {
                yield return new WaitUntil(() => (boundCharacter.isMyturn && isBuffRun[(int)effectType + 8]) || !N_BattleManager.instance.isBuffRun_All);
                yield return new WaitUntil(() => (!boundCharacter.isMyturn && isBuffRun[(int)effectType + 8]) || !N_BattleManager.instance.isBuffRun_All);
                turn--;
            }
            boundCharacter.strength -= (int)effectvalue;
        }
        else
        {
            boundMonster.strength += (int)effectvalue;
            while (turn >= -1)
            {
                yield return new WaitUntil(() => boundMonster.IsMyturn && isBuffRun[(int)effectType + 8] && !N_BattleManager.instance.isBuffRun_All);
                yield return new WaitUntil(() => !boundMonster.IsMyturn && isBuffRun[(int)effectType + 8] && !N_BattleManager.instance.isBuffRun_All);
                turn--;
            }
            boundMonster.strength -= (int)effectvalue;
        }
        isBuffRun[(int)effectType + 8] = false;
    }

    IEnumerator Intelliegence_Increase(EffectType effectType, float effectvalue, int turn)
    {
        if (boundCharacter != null)
        {
            boundCharacter.intelligence += (int)effectvalue;
            while (turn >= -1)
            {
                yield return new WaitUntil(() => (boundCharacter.isMyturn && isBuffRun[(int)effectType + 8]) || !N_BattleManager.instance.isBuffRun_All);
                yield return new WaitUntil(() => (!boundCharacter.isMyturn && isBuffRun[(int)effectType + 8]) || !N_BattleManager.instance.isBuffRun_All);
                turn--;
            }
            boundCharacter.intelligence -= (int)effectvalue;
        }
        else
        {
            boundMonster.intelligence += (int)effectvalue;
            while (turn >= -1)
            {
                yield return new WaitUntil(() => boundMonster.IsMyturn && isBuffRun[(int)effectType + 8] && !N_BattleManager.instance.isBuffRun_All);
                yield return new WaitUntil(() => !boundMonster.IsMyturn && isBuffRun[(int)effectType + 8] && !N_BattleManager.instance.isBuffRun_All);
                turn--;
            }
            boundMonster.intelligence -= (int)effectvalue;
        }
        isBuffRun[(int)effectType + 8] = false;
    }

    IEnumerator Luck_Increase(EffectType effectType, float effectvalue, int turn)
    {
        if (boundCharacter != null)
        {
            boundCharacter.luck += (int)effectvalue;
            while (turn >= -1)
            {
                yield return new WaitUntil(() => (boundCharacter.isMyturn && isBuffRun[(int)effectType + 8]) || !N_BattleManager.instance.isBuffRun_All);
                yield return new WaitUntil(() => (!boundCharacter.isMyturn && isBuffRun[(int)effectType + 8]) || !N_BattleManager.instance.isBuffRun_All);
                turn--;
            }
            boundCharacter.luck -= (int)effectvalue;
        }
        else
        {
            boundMonster.luck += (int)effectvalue;
            while (turn >= -1)
            {
                yield return new WaitUntil(() => boundMonster.IsMyturn && isBuffRun[(int)effectType + 8] && !N_BattleManager.instance.isBuffRun_All);
                yield return new WaitUntil(() => !boundMonster.IsMyturn && isBuffRun[(int)effectType + 8] && !N_BattleManager.instance.isBuffRun_All);
                turn--;
            }
            boundMonster.luck -= (int)effectvalue;
        }
        isBuffRun[(int)effectType + 8] = false;
    }

    IEnumerator Speed_Increase(EffectType effectType, float effectvalue, int turn)
    {
        if (boundCharacter != null)
        {
            boundCharacter.speed += (int)effectvalue;
            while (turn >= -1)
            {
                yield return new WaitUntil(() => (boundCharacter.isMyturn && isBuffRun[(int)effectType + 8]) || !N_BattleManager.instance.isBuffRun_All);
                yield return new WaitUntil(() => (!boundCharacter.isMyturn && isBuffRun[(int)effectType + 8]) || !N_BattleManager.instance.isBuffRun_All);
                turn--;
            }
            boundCharacter.speed -= (int)effectvalue;
        }
        else
        {
            boundMonster.speed += (int)effectvalue;
            while (turn >= -1)
            {
                yield return new WaitUntil(() => boundMonster.IsMyturn && isBuffRun[(int)effectType + 8] && !N_BattleManager.instance.isBuffRun_All);
                yield return new WaitUntil(() => !boundMonster.IsMyturn && isBuffRun[(int)effectType + 8] && !N_BattleManager.instance.isBuffRun_All);
                turn--;
            }
            boundMonster.speed -= (int)effectvalue;
        }
        isBuffRun[(int)effectType + 8] = false;
    }
    IEnumerator Thorn(EffectType effectType, float effectvalue, int turn)
    {
        if (boundCharacter != null)
            while (turn >= -1)
            {
                yield return new WaitUntil(() => (boundCharacter.isMyturn && isBuffRun[(int)effectType + 8]) || !N_BattleManager.instance.isBuffRun_All);
                yield return new WaitUntil(() => (!boundCharacter.isMyturn && isBuffRun[(int)effectType + 8]) || !N_BattleManager.instance.isBuffRun_All);
                turn--;
            }
        else
            while (turn >= -1)
            {
                yield return new WaitUntil(() => boundMonster.IsMyturn && isBuffRun[(int)effectType + 8] && !N_BattleManager.instance.isBuffRun_All);
                yield return new WaitUntil(() => !boundMonster.IsMyturn && isBuffRun[(int)effectType + 8] && !N_BattleManager.instance.isBuffRun_All);
                turn--;
            }
        isBuffRun[(int)effectType + 8] = false;
    }
    IEnumerator MagicMirror(EffectType effectType, float effectvalue, int turn)
    {
        if (boundCharacter != null)
            while (turn >= -1)
            {
                yield return new WaitUntil(() => (boundCharacter.isMyturn && isBuffRun[(int)effectType + 8]) || !N_BattleManager.instance.isBuffRun_All);
                yield return new WaitUntil(() => (!boundCharacter.isMyturn && isBuffRun[(int)effectType + 8]) || !N_BattleManager.instance.isBuffRun_All);
                turn--;
            }
        else
            while (turn >= -1)
            {
                yield return new WaitUntil(() => boundMonster.IsMyturn && isBuffRun[(int)effectType + 8] && !N_BattleManager.instance.isBuffRun_All);
                yield return new WaitUntil(() => !boundMonster.IsMyturn && isBuffRun[(int)effectType + 8] && !N_BattleManager.instance.isBuffRun_All);
                turn--;
            }
        isBuffRun[(int)effectType + 8] = false;
    }

    IEnumerator Regeneration(EffectType effectType, float effectvalue, int turn)
    {
        if (boundCharacter != null)
            while (turn >= -1)
            {
                yield return new WaitUntil(() => (boundCharacter.isMyturn && isBuffRun[(int)effectType + 8]) || !N_BattleManager.instance.isBuffRun_All);
                boundCharacter.Hp += effectvalue;
                yield return new WaitUntil(() => (!boundCharacter.isMyturn && isBuffRun[(int)effectType + 8]) || !N_BattleManager.instance.isBuffRun_All);
                turn--;
            }
        else
            while (turn >= -1)
            {
                yield return new WaitUntil(() => boundMonster.IsMyturn && isBuffRun[(int)effectType + 8] && !N_BattleManager.instance.isBuffRun_All);
                boundMonster.Hp += effectvalue;
                yield return new WaitUntil(() => !boundMonster.IsMyturn && isBuffRun[(int)effectType + 8] && !N_BattleManager.instance.isBuffRun_All);
                turn--;
            }
        isBuffRun[(int)effectType + 8] = false;
    }

    IEnumerator Parry(EffectType effectType, float effectvalue, int turn)
    {
        if (boundCharacter != null)
            while (turn >= -1)
            {
                yield return new WaitUntil(() => (boundCharacter.isMyturn && isBuffRun[(int)effectType + 8]) || !N_BattleManager.instance.isBuffRun_All);
                yield return new WaitUntil(() => (!boundCharacter.isMyturn && isBuffRun[(int)effectType + 8]) || !N_BattleManager.instance.isBuffRun_All);
                turn--;
            }
        else
            while (turn >= -1)
            {
                yield return new WaitUntil(() => boundMonster.IsMyturn && isBuffRun[(int)effectType + 8] && !N_BattleManager.instance.isBuffRun_All);
                yield return new WaitUntil(() => !boundMonster.IsMyturn && isBuffRun[(int)effectType + 8] && !N_BattleManager.instance.isBuffRun_All);
                turn--;
            }
        isBuffRun[(int)effectType + 8] = false;
    }

    IEnumerator AttackGuard(EffectType effectType, float effectvalue, int turn)
    {
        if (boundCharacter != null)
        {
            boundCharacter.attackGuard += (int)effectvalue;
            while (turn >= -1)
            {
                yield return new WaitUntil(() => (boundCharacter.isMyturn && isBuffRun[(int)effectType + 8]) || !N_BattleManager.instance.isBuffRun_All);
                yield return new WaitUntil(() => (!boundCharacter.isMyturn && isBuffRun[(int)effectType + 8]) || !N_BattleManager.instance.isBuffRun_All);
                turn--;
            }
            boundCharacter.attackGuard = 0;
        }
        else
        {
            boundMonster.attackGuard += (int)effectvalue;
            while (turn >= -1)
            {
                yield return new WaitUntil(() => boundMonster.IsMyturn && isBuffRun[(int)effectType + 8] && !N_BattleManager.instance.isBuffRun_All);
                yield return new WaitUntil(() => !boundMonster.IsMyturn && isBuffRun[(int)effectType + 8] && !N_BattleManager.instance.isBuffRun_All);
                turn--;
            }
            boundMonster.attackGuard = 0;
        }
        isBuffRun[(int)effectType + 8] = false;
    }

    IEnumerator MagicGuard(EffectType effectType, float effectvalue, int turn)
    {
        if (boundCharacter != null)
        {
            boundCharacter.magicGuard += (int)effectvalue;
            while (turn >= -1)
            {
                yield return new WaitUntil(() => (boundCharacter.isMyturn && isBuffRun[(int)effectType + 8]) || !N_BattleManager.instance.isBuffRun_All);
                yield return new WaitUntil(() => (!boundCharacter.isMyturn && isBuffRun[(int)effectType + 8]) || !N_BattleManager.instance.isBuffRun_All);
                turn--;
            }
            boundCharacter.magicGuard = 0;
        }
        else
        {
            boundMonster.magicGuard += (int)effectvalue;
            while (turn >= -1)
            {
                yield return new WaitUntil(() => boundMonster.IsMyturn && isBuffRun[(int)effectType + 8] && !N_BattleManager.instance.isBuffRun_All);
                yield return new WaitUntil(() => !boundMonster.IsMyturn && isBuffRun[(int)effectType + 8] && !N_BattleManager.instance.isBuffRun_All);
                turn--;
            }
            boundMonster.magicGuard = 0;
        }
        isBuffRun[(int)effectType + 8] = false;
    }

    IEnumerator Ignition(EffectType effectType, float effectvalue, int turn)
    {
        if (boundCharacter != null)
        {
            while (turn >= -1)
            {
                yield return new WaitUntil(() => (boundCharacter.isMyturn && isBuffRun[(int)effectType + 8]) || !N_BattleManager.instance.isBuffRun_All);
                yield return new WaitUntil(() => (!boundCharacter.isMyturn && isBuffRun[(int)effectType + 8]) || !N_BattleManager.instance.isBuffRun_All);
                turn--;
            }
        }
        else
        {
            while (turn >= -1)
            {
                yield return new WaitUntil(() => boundMonster.IsMyturn && isBuffRun[(int)effectType + 8] && !N_BattleManager.instance.isBuffRun_All);
                yield return new WaitUntil(() => !boundMonster.IsMyturn && isBuffRun[(int)effectType + 8] && !N_BattleManager.instance.isBuffRun_All);
                turn--;
            }
        }
        isBuffRun[(int)effectType + 8] = false;
    }

    IEnumerator DoubleCharm(EffectType effectType, float effectvalue, int turn)
    {
        if (boundCharacter != null)
        {
            while (turn >= -1)
            {
                yield return new WaitUntil(() => (boundCharacter.isMyturn && isBuffRun[(int)effectType + 8]) || !N_BattleManager.instance.isBuffRun_All);
                yield return new WaitUntil(() => (!boundCharacter.isMyturn && isBuffRun[(int)effectType + 8]) || !N_BattleManager.instance.isBuffRun_All);
                turn--;
            }
        }
        else
        {
            while (turn >= -1)
            {
                yield return new WaitUntil(() => boundMonster.IsMyturn && isBuffRun[(int)effectType + 8] && !N_BattleManager.instance.isBuffRun_All);
                yield return new WaitUntil(() => !boundMonster.IsMyturn && isBuffRun[(int)effectType + 8] && !N_BattleManager.instance.isBuffRun_All);
                turn--;
            }
        }
        isBuffRun[(int)effectType + 8] = false;
    }

}
