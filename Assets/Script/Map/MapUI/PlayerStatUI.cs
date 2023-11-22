using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatUI : MonoBehaviour
{
    //작업 순서

    //selectPlayerUI 조립

    //Find Tag => 캐릭터타입에 플레이어 넘버 1,2,3
    //찾아서 연결후 데이터 표시
    //포션 아이템 소지 확인, 갯수 판단

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
