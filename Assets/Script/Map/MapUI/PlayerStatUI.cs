using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatUI : MonoBehaviour
{
    public Character character;
    public Character_type character_Type;

    public TMP_Text nickName;
    public TMP_Text level;
    public TMP_Text strength;
    public TMP_Text intel;
    public TMP_Text luck;
    public TMP_Text speed;
    public TMP_Text guard;
    public TMP_Text magicGuard;

    public Slider hpbar;
    public Slider expbar;


    void Start()
    {
        nickName.text = character_Type.nickname;   
    }


    void Update()
    {
        level.text = character.level.ToString();
        strength.text = character.strength.ToString();
        intel.text = character.intelligence.ToString();
        luck.text = character.luck.ToString();
        speed.text = character.speed.ToString();
    }
}
