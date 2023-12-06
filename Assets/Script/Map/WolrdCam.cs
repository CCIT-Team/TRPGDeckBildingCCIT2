using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolrdCam : MonoBehaviour
{
    public Transform player;
    public float smooth_speed = 0.125f;
    public float scroll_speed = 0.8f;
    public float cameraMoveing_Speed = 8f;
    public Vector3 offset;
    public float rotate_speed = 5;

    Vector3 originpos;
    Vector3 mouseCurserpos;

    public Vector3 minimum;
    public Vector3 maximum;

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
            smooth_speed = 0.25f;
        }
        if (player != null && !Map.instance.isOutofUI)
        {
            if (Input.GetKey(KeyCode.A) && transform.position.x > -7) { offset.x -= cameraMoveing_Speed * Time.deltaTime; if (Map.instance.isPlayerMoving) { offset = originpos; } } 
            else { if (Map.instance.isPlayerMoving) { offset = originpos; } }
            if (Input.GetKey(KeyCode.D) && transform.position.x < 45) { offset.x += cameraMoveing_Speed * Time.deltaTime; if (Map.instance.isPlayerMoving) { offset = originpos; } }
            else { if (Map.instance.isPlayerMoving) { offset = originpos; } }
            if (Input.GetKey(KeyCode.W) && transform.position.z < 40) { offset.z += cameraMoveing_Speed * Time.deltaTime; if (Map.instance.isPlayerMoving) { offset = originpos; } }
            else { if (Map.instance.isPlayerMoving) { offset = originpos; } }
            if (Input.GetKey(KeyCode.S) && transform.position.z > -10) { offset.z -= cameraMoveing_Speed * Time.deltaTime; if (Map.instance.isPlayerMoving) { offset = originpos; } }
            else { if (Map.instance.isPlayerMoving) { offset = originpos; } }

            Vector3 desired_position = player.transform.position + offset;
            Vector3 smoothed_position = Vector3.Lerp(transform.position, desired_position, smooth_speed);
            transform.position = smoothed_position;
        }
        //if (player != null )
        //{
        //    Vector3 desired_position = player.transform.position + offset;
        //    Vector3 smoothed_position = Vector3.Lerp(transform.position, desired_position, smooth_speed);
        //    transform.position = smoothed_position;
        //}

        float scroollWheel = Input.GetAxis("Mouse ScrollWheel");
       if(scroollWheel > 0)//¡‹ ¿Œ
        {
            if (offset.y > minimum.y)
            {
                Vector3 smoothed_position = Vector3.Lerp(offset, minimum, scroll_speed);
                offset = smoothed_position;
            }
            else
            {
                offset = minimum;
            }
        }

       if(Input.GetMouseButtonDown(2))//¡‹ √ ±‚»≠
        {
            offset = originpos;
        }

        if(scroollWheel < 0)//¡‹ æ∆øÙ
        {
            if(offset.y < maximum.y)
            {
                Vector3 smoothed_position = Vector3.Lerp(offset, maximum, scroll_speed);
                offset = smoothed_position;
            }
            else
            {
                offset = maximum;
            }
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
