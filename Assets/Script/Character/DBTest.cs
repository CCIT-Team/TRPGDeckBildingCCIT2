using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBTest : MonoBehaviour
{
    void Start()
    {
        //GameManager.instance.GetLobbyAvatar(Vector3.zero);
        GameManager.instance.GetLoadAvatar(Vector3.zero);
    }
}
