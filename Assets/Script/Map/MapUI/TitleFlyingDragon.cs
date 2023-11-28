using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleFlyingDragon : MonoBehaviour
{
    public Transform[] track = new Transform[4];
    public float dragonSpeed;
    Transform targetTransform;
    int trackNum = 1;

    void Start()
    {
        targetTransform = track[0];
    }

    
    void Update()
    {
        Vector3 nextTilePosition = targetTransform.transform.position;

        transform.rotation = Quaternion.LookRotation(nextTilePosition - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, nextTilePosition, dragonSpeed);

        if (Vector3.Distance(targetTransform.transform.position, transform.position) <= 1f)
        {
            if(trackNum <= track.Length)
            {
                trackNum += 1;
                targetTransform = track[trackNum];
            }
            else
            {
                trackNum = 0;
                targetTransform = track[0];
            }
        }
    }  
}
