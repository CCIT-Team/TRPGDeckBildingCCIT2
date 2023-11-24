using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private bool invert;

    private Transform cameraTransform;

    private void Awake()
    {
        cameraTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        if (invert)
        {
            Vector3 dirToCamera = (cameraTransform.position - transform.position).normalized;
            transform.LookAt(transform.position + dirToCamera * -1);
            transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.y, 0));
        }
        else
        {
            transform.LookAt(cameraTransform);
            transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.y, 0));
        }
    }
}
