using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCameraMove : MonoBehaviour
{
    Camera cam;
    public Transform playerSight;
    public Transform monsterSight;

    public GameObject cameratarget;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        MovePosition(cameratarget);
    }
    public void MovePosition(GameObject gameObject)
    {
        if(gameObject.CompareTag("Player"))
        {
            transform.position = Vector3.Lerp(transform.position, playerSight.position, 0.5f);
            transform.rotation = Quaternion.Euler(Vector3.Lerp(transform.rotation.eulerAngles, playerSight.rotation.eulerAngles, 0.05f));
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, monsterSight.position, 0.5f);
            transform.rotation = Quaternion.Euler(Vector3.Lerp(transform.rotation.eulerAngles, monsterSight.rotation.eulerAngles, 0.05f));
        }
    }
}
