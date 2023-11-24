using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBTest : MonoBehaviour
{
    void Start()
    {
        GameManager.instance.GetLoadAvatar(Vector3.zero);
        //GameManager.instance.MonsterMapInstance(30000001, Vector3.zero);
    }
}
