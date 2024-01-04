using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastDragonCameraMove : MonoBehaviour
{
    public Transform targetpos;
    public float moveSpeed;
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetpos.position, moveSpeed);
    }
}
