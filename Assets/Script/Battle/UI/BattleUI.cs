using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUI : MonoBehaviour
{
    public static BattleUI instance;
    public GameObject inputBlocker;

    public GameObject targetIndicator;
    public GameObject currentTargetIndicator;

    [Header("TurnDisplay")]

    [SerializeField]
    GameObject turnDisplay;

    [SerializeField]
    Slider slider;
    List<Slider> turnSlider = new List<Slider>();

    public GameObject TurnAnnounce;
    public RawImage announceImage;
    public TMP_Text announceText;

    [Header("Player")]
    public PlayerBattleUI[] playerUI = new PlayerBattleUI[3];
    List<Unit> boundUnits = new List<Unit>();

    [Header("Token")]
    public GameObject tokenPosition;
    public Token tokenPrefab;
    public List<Token> tokens;
    [HideInInspector]
    public int faildTokens = 0;
    public List<AudioClip> tokenSounds;

    [Header("Log")]
    public Text logText;
    public RectTransform logSize;
    public TMP_Text costWarning;
    public Damage damagePrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
            Destroy(this);
    }

    private void Update()
    {
        announceImage.color = new Color(1,1,1,announceText.color.a);
    }

    public void BindPlayer(GameObject[] playerarray)
    {
        for(int i = 0; i < playerarray.Length; i++)
        {
            playerUI[i].BindCharacter(playerarray[i].GetComponent<Character>());
        }
    }

    public IEnumerator RollToken(StatusType statusType, int mainStatus, int tokenAmount)
    {
        faildTokens = 0;
        for (int i = 0; i < tokenAmount; i++)
            tokens.Add(Instantiate(tokenPrefab, tokenPosition.transform));

        for (int i = 0; i < tokenAmount; i++)
        {
            tokens[i].transform.localPosition = new Vector2((-tokenAmount / 2 + i + (tokenAmount + 1) % 2 / 2f) * 160, 0);
            tokens[i].SetToken(statusType);
        }

        for (int i = 0; i < tokenAmount; i++)
        {
            yield return new WaitForSeconds(0.2f);
            int x = Random.Range(0, 100);
            if (x <= mainStatus)
            {
                tokens[i].CheckToken(statusType, true);
                N_BattleManager.instance.audioSource.PlayOneShot(tokenSounds[0]);
            }
            else
            {
                tokens[i].CheckToken(statusType, false);
                N_BattleManager.instance.audioSource.PlayOneShot(tokenSounds[1]);
                faildTokens++;
            }
            yield return new WaitForSeconds(0.2f);
        }

        if(faildTokens == 0)
            N_BattleManager.instance.audioSource.PlayOneShot(tokenSounds[2]);
        else if(faildTokens == tokenAmount)
            N_BattleManager.instance.audioSource.PlayOneShot(tokenSounds[4]);
        else
            N_BattleManager.instance.audioSource.PlayOneShot(tokenSounds[3]);

        for (int i = 0; i < tokenAmount; i++)
            Destroy(tokens[i].gameObject);
        tokens.Clear();
    }

    public void AddLog(string log)
    {
        logText.text += "\n" + log;
        logSize.sizeDelta += new Vector2(0, 60);
    }

    #region 턴
    public void SetTurnSlider(List<Unit> units)
    {
        int playerindex = 0;
        int monsterindex = 0;
        for(int i = 0; i < units.Count; i++)
        {
            boundUnits.Add(units[i]);
            TurnSlider icon = Instantiate(slider, turnDisplay.transform).GetComponent<TurnSlider>();
            turnSlider.Add(icon.GetComponent<Slider>());
            icon.BindingUnit(units[i], units.Count - 1);
            if (units[i].CompareTag("Player"))
            {
                playerindex++;
                icon.renderImage.texture = (Texture)Resources.Load("Prefabs/UI/Player/Player" + playerindex + "Render");
                if (Resources.Load("Prefabs/UI/Player/Player" + playerindex + "Render") == null)
                    icon.renderImage.texture = (Texture)Resources.Load("Prefabs/UI/Player/Player" + playerindex + "RenderTexture");
            }
            else
            {
                monsterindex++;
                icon.renderImage.texture = (Texture)Resources.Load("Prefabs/UI/Monster/Monster" + monsterindex);
            }
            icon.StartCoroutine(icon.DisplayTurn());
        }
    }

    public void AnnounceUnitDead(TurnSlider unitIcon)
    {
        //턴테이블에서 아이콘 제거 후 남은 턴 조정
        turnSlider.Remove(unitIcon.slider);
        unitIcon.gameObject.SetActive(false);
        foreach(Slider slider in turnSlider)
        {
            slider.maxValue = turnSlider.Count - 1;
        }

        foreach(PlayerBattleUI PUI in playerUI)
        {
            if(PUI.boundCharacter == unitIcon.boundUnit)
            {
                PUI.UnBindCharacter();
                PUI.gameObject.SetActive(false);
            }
        }
    }

    #endregion
}
