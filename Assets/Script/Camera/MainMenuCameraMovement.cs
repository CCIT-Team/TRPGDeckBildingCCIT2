using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraMovement : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10f;
    
    private Vector3 rotationVector = new Vector3(0, 0, 0);

    private void Update()
    {
        rotationVector.y = Input.GetAxis("Mouse X");
        rotationVector.x = -(Input.GetAxis("Mouse Y"));
        transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;
    }
}
