using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolrdCam : MonoBehaviour
{
    public Transform player;
    public float smooth_speed = 0.125f;
    public Vector3 offset;
    public float rotate_speed = 5;

    Vector3 originpos;
    Vector3 mouseCurserpos;
    float v;

    bool inputMode = false;
    private void Start()
    {
        player = Map.instance.wolrdTurn.currentPlayer.transform;
        originpos = offset;
    }
    void FixedUpdate()
    {
        player = Map.instance.wolrdTurn.currentPlayer.transform;
        if (player != null && inputMode == false)
        {
            Vector3 desired_position = Map.instance.wolrdTurn.currentPlayer.transform.position + offset;
            Vector3 smoothed_position = Vector3.Lerp(transform.position, desired_position, smooth_speed);
            transform.position = smoothed_position;
        }
    }

    private void Update()
    {

    }
}
