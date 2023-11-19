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
        originpos = offset;
    }
    void FixedUpdate()
    {
        if (!Map.instance.dragonScript.isdragonTurn)
        {
            player = Map.instance.wolrdTurn.currentPlayer.transform;
            smooth_speed = 0.125f;
        }
        else
        {
            player = Map.instance.instantiateDragon.transform;
            smooth_speed = 0.35f;
        }

        if (player != null && inputMode == false)
        {
            Vector3 desired_position = player.transform.position + offset;
            Vector3 smoothed_position = Vector3.Lerp(transform.position, desired_position, smooth_speed);
            transform.position = smoothed_position;
        }
    }

    //private void Update()
    //{
    //    if (!Map.instance.dragonScript.isdragonTurn)
    //    {
    //        player = Map.instance.wolrdTurn.currentPlayer.transform;
    //    }
    //    else
    //    {
    //        player = Map.instance.instantiateDragon.transform;
    //    }

    //    if (player != null && inputMode == false)
    //    {
    //        Vector3 desired_position = player.transform.position + offset;
    //        Vector3 smoothed_position = Vector3.Lerp(transform.position, desired_position, smooth_speed);
    //        transform.position = smoothed_position;
    //    }
    //}
}
