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
    Regeneration
}

public class EffectTurnChecker : MonoBehaviour
{
    public bool[] isBuffRun = new bool[16];
    public EffectType effect;
    public Character boundCharacter;
    public Monster boundMonster;

    public void StartEffect(EffectType effectType, float effectvalue, int turn)
    {
        if (isBuffRun[(int)effectType + 8])
        {

        }
        else
        {
            isBuffRun[8] = true;
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
            }
        }
    }

    public void StopEffect(EffectType effectType)
    {
        isBuffRun[(int)effectType + 8] = false;
    }


    IEnumerator Faint(EffectType effectType, float effectvalue, int turn)
    {
        while (turn >= 0)
        {
            yield return new WaitUntil(() => boundCharacter.isMyturn && isBuffRun[(int)effectType + 8] && isBuffRun[8]);
            yield return new WaitUntil(() => !boundCharacter.isMyturn && isBuffRun[(int)effectType + 8] && isBuffRun[8]);
            turn--;
        }
        isBuffRun[(int)effectType + 8] = false;
    }

    IEnumerator Burn(EffectType effectType, float effectvalue, int turn)
    {
        while (turn >= 0 && isBuffRun[(int)effectType + 8] && isBuffRun[8])
        {
            yield return new WaitUntil(() => boundCharacter.isMyturn && isBuffRun[(int)effectType + 8] && isBuffRun[8]);
            boundCharacter.Damaged(effectvalue);
            yield return new WaitUntil(() => !boundCharacter.isMyturn && isBuffRun[(int)effectType + 8] && isBuffRun[8]);
            turn--;
        }
        isBuffRun[(int)effectType + 8] = false;
    }

    IEnumerator Confusion(EffectType effectType, float effectvalue, int turn)
    {
        while (turn >= 0 && isBuffRun[(int)effectType + 8] && isBuffRun[8])
        {   
            yield return new WaitUntil(() => boundCharacter.isMyturn && isBuffRun[(int)effectType + 8] && isBuffRun[8]);
            boundCharacter.isMyturn = false;
            yield return new WaitUntil(() => !boundCharacter.isMyturn && isBuffRun[(int)effectType + 8] && isBuffRun[8]);
            turn--;
        }
        isBuffRun[(int)effectType + 8] = false;
    }

    IEnumerator Weak(EffectType effectType, float effectvalue, int turn)
    {
        while (turn >= 0 && isBuffRun[(int)effectType + 8] && isBuffRun[8])
        {
            yield return new WaitUntil(() => boundCharacter.isMyturn && isBuffRun[(int)effectType + 8] && isBuffRun[8]);
            yield return new WaitUntil(() => !boundCharacter.isMyturn && isBuffRun[(int)effectType + 8] && isBuffRun[8]);
            turn--;
        }
        isBuffRun[(int)effectType + 8] = false;
    }

    IEnumerator Speed_Decrease(EffectType effectType, float effectvalue, int turn)
    {
        boundCharacter.speed -= (int)effectvalue;
        while (turn >= 0 && isBuffRun[(int)effectType + 8] && isBuffRun[8])
        {
            yield return new WaitUntil(() => boundCharacter.isMyturn && isBuffRun[(int)effectType + 8] && isBuffRun[8]);
            yield return new WaitUntil(() => !boundCharacter.isMyturn && isBuffRun[(int)effectType + 8] && isBuffRun[8]);
            turn--;
        }
        boundCharacter.speed += (int)effectvalue;
        isBuffRun[(int)effectType + 8] = false;
    }

    IEnumerator Luck_Decrease(EffectType effectType, float effectvalue, int turn)
    {
        boundCharacter.luck -= (int)effectvalue;
        while (turn >= 0 && isBuffRun[(int)effectType + 8] && isBuffRun[8])
        {
            yield return new WaitUntil(() => boundCharacter.isMyturn && isBuffRun[(int)effectType + 8] && isBuffRun[8]);
            yield return new WaitUntil(() => !boundCharacter.isMyturn && isBuffRun[(int)effectType + 8] && isBuffRun[8]);
            turn--;
        }
        boundCharacter.luck += (int)effectvalue;
        isBuffRun[(int)effectType + 8] = false;
    }

    IEnumerator Intelliegence_Decrease(EffectType effectType, float effectvalue, int turn)
    {
        boundCharacter.intelligence -= (int)effectvalue;
        while (turn >= 0 && isBuffRun[(int)effectType + 8] && isBuffRun[8])
        {
            yield return new WaitUntil(() => boundCharacter.isMyturn && isBuffRun[(int)effectType + 8] && isBuffRun[8]);
            yield return new WaitUntil(() => !boundCharacter.isMyturn && isBuffRun[(int)effectType + 8] && isBuffRun[8]);
            turn--;
        }
        boundCharacter.intelligence += (int)effectvalue;
        isBuffRun[(int)effectType + 8] = false;
    }

    IEnumerator Strength_Decrease(EffectType effectType, float effectvalue, int turn)
    {
        boundCharacter.strength -= (int)effectvalue;
        while (turn >= 0 && isBuffRun[(int)effectType + 8] && isBuffRun[8])
        {
            
            yield return new WaitUntil(() => boundCharacter.isMyturn && isBuffRun[(int)effectType + 8] && isBuffRun[8]);
            yield return new WaitUntil(() => !boundCharacter.isMyturn && isBuffRun[(int)effectType + 8] && isBuffRun[8]);
            turn--;
        }
        boundCharacter.strength += (int)effectvalue;
        isBuffRun[(int)effectType + 8] = false;
    }

    IEnumerator Strength_Increase(EffectType effectType, float effectvalue, int turn)
    {
        boundCharacter.strength += (int)effectvalue;
        while (turn >= 0 && isBuffRun[(int)effectType + 8] && isBuffRun[8])
        {
            
            yield return new WaitUntil(() => boundCharacter.isMyturn && isBuffRun[(int)effectType + 8] && isBuffRun[8]);
            yield return new WaitUntil(() => !boundCharacter.isMyturn && isBuffRun[(int)effectType + 8] && isBuffRun[8]);
            turn--;
        }
        boundCharacter.strength -= (int)effectvalue;
        isBuffRun[(int)effectType + 8] = false;
    }

    IEnumerator Intelliegence_Increase(EffectType effectType, float effectvalue, int turn)
    {
        boundCharacter.intelligence += (int)effectvalue;
        while (turn >= 0 && isBuffRun[(int)effectType + 8] && isBuffRun[8])
        {
            
            yield return new WaitUntil(() => boundCharacter.isMyturn && isBuffRun[(int)effectType + 8] && isBuffRun[8]);
            yield return new WaitUntil(() => !boundCharacter.isMyturn && isBuffRun[(int)effectType + 8] && isBuffRun[8]);
            turn--;
        }
        boundCharacter.intelligence -= (int)effectvalue;
        isBuffRun[(int)effectType + 8] = false;
    }

    IEnumerator Luck_Increase(EffectType effectType, float effectvalue, int turn)
    {
        boundCharacter.luck += (int)effectvalue;
        while (turn >= 0 && isBuffRun[(int)effectType + 8] && isBuffRun[8])
        {
            
            yield return new WaitUntil(() => boundCharacter.isMyturn && isBuffRun[(int)effectType + 8] && isBuffRun[8]);
            yield return new WaitUntil(() => !boundCharacter.isMyturn && isBuffRun[(int)effectType + 8] && isBuffRun[8]);
            turn--;
        }
        boundCharacter.luck -= (int)effectvalue;
        isBuffRun[(int)effectType + 8] = false;
    }

    IEnumerator Speed_Increase(EffectType effectType, float effectvalue, int turn)
    {
        boundCharacter.speed += (int)effectvalue;
        while (turn >= 0 && isBuffRun[(int)effectType + 8] && isBuffRun[8])
        {
            
            yield return new WaitUntil(() => boundCharacter.isMyturn && isBuffRun[(int)effectType + 8] && isBuffRun[8]);
            yield return new WaitUntil(() => !boundCharacter.isMyturn && isBuffRun[(int)effectType + 8] && isBuffRun[8]);
            turn--;
        }
        boundCharacter.speed -= (int)effectvalue;
        isBuffRun[(int)effectType + 8] = false;
    }
    IEnumerator Thorn(EffectType effectType, float effectvalue, int turn)
    {
        while (turn >= 0 && isBuffRun[(int)effectType + 8] && isBuffRun[8])
        {
            
            yield return new WaitUntil(() => boundCharacter.isMyturn && isBuffRun[(int)effectType + 8] && isBuffRun[8]);
            yield return new WaitUntil(() => !boundCharacter.isMyturn && isBuffRun[(int)effectType + 8] && isBuffRun[8]);
            turn--;
        }
        isBuffRun[(int)effectType + 8] = false;
    }
    IEnumerator MagicMirror(EffectType effectType, float effectvalue, int turn)
    {
        while (turn >= 0 && isBuffRun[(int)effectType + 8] && isBuffRun[8])
        {
            
            yield return new WaitUntil(() => boundCharacter.isMyturn && isBuffRun[(int)effectType + 8] && isBuffRun[8]);
            yield return new WaitUntil(() => !boundCharacter.isMyturn && isBuffRun[(int)effectType + 8] && isBuffRun[8]);
            turn--;
        }
        isBuffRun[(int)effectType + 8] = false;
    }

    IEnumerator Regeneration(EffectType effectType, float effectvalue, int turn)
    {
        while (turn >= 0 && isBuffRun[(int)effectType + 8] && isBuffRun[8])
        {
            
            yield return new WaitUntil(() => boundCharacter.isMyturn && isBuffRun[(int)effectType + 8] && isBuffRun[8]);
            boundCharacter.Hp += effectvalue;
            yield return new WaitUntil(() => !boundCharacter.isMyturn && isBuffRun[(int)effectType + 8] && isBuffRun[8]);
            turn--;
        }
        isBuffRun[(int)effectType + 8] = false;
    }
}
