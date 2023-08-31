using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Unit
{
    public enum AvatarType
    {
        None,
        Human,
        Elf,
        DarkElf,
        HalfOrc
    }

    public enum Major
    {
        None,
        Fighter,
        Magician,
        Cleric
    }

    [SerializeField]
    private AvatarType type;
    [SerializeField]
    private Major major;
}
