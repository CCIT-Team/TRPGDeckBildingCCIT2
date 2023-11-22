using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCameraMove : MonoBehaviour
{
    Camera cam;
    public Transform playerSight;
    public Transform monsterSight;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }
    
    public void MovePosition(GameObject gameObject)
    {
        if(gameObject.CompareTag("Player"))
        {
            transform.position = playerSight.position;
            transform.rotation = playerSight.rotation;
        }
        else
        {
            transform.position = monsterSight.position;
            transform.rotation = monsterSight.rotation;
        }
    }
}
