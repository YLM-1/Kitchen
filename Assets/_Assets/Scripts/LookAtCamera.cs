using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private enum E_Mode
    {
        LookAt,
        LookAtInvert,
        LookAtCamera,
        LookAtCameraInvert,
    }
    [SerializeField] private E_Mode mode;
    
    private void LateUpdate()
    {
        switch (mode) 
        {
            case E_Mode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;
            case E_Mode.LookAtInvert:
                Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position+dirFromCamera);
                break;
            case E_Mode.LookAtCamera:
                transform.forward = Camera.main.transform.forward;
                break;
            case E_Mode.LookAtCameraInvert:
                transform.forward = -Camera.main.transform.forward;
                break;
        }

        
    }
}
