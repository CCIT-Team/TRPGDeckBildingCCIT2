using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterUIManager : MonoBehaviour
{
    private GameObject[] monster;

    public GameObject selectUI;
    private void Awake()
    {
        N_BattleManager.instance.monsterUI = this;
    }

    public void SetMonster(GameObject[] monster)
    {
        //player = GameObject.FindGameObjectsWithTag("Player");
        GameObject childUI;
        for (int i = 0; i < monster.Length; i++)
        {
            childUI = transform.GetChild(i + 1).gameObject;
            childUI.SetActive(true);
            childUI.GetComponent<MonsterStatUI>().LinkingMonster(monster[i]);
            childUI.GetComponent<MonsterStatUI>().InitUI();
        }
    }
}
