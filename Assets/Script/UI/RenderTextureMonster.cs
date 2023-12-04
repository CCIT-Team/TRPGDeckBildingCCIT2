using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTextureMonster : MonoBehaviour
{
    public GameObject[] monsterList;
    public int no;


    public void SetMonster(MonsterData stat)
    {
        no = stat.no;
        monsterList[int.Parse(no.ToString().Substring(no.ToString().Length - 3)) - 1].SetActive(true);
    }

    public void SetUnitPosition(Vector3 position, Vector3 rotation)
    {
        gameObject.transform.localPosition = position;
        gameObject.transform.localRotation = Quaternion.Euler(rotation);
    }
}
