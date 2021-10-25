using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FovCamera1 : MonoBehaviour
{
    public Space SpaceScript;
    private CinemachineVirtualCamera vcam;
    private float zoomSpeed = 1;
    private float zoomMin = 90 ;
    private float zoomMax = 115;
    public GameObject[] Rockets;

    private float zoom;
    // Start is called before the first frame update
     void Start() 
     {
         vcam = GetComponent<CinemachineVirtualCamera>();
         zoom = vcam.m_Lens.FieldOfView;
         vcam.LookAt = Rockets[1].transform;
         vcam.Follow = Rockets[1].transform;
     }
      
     void Update() 
     {
         zoom -= Input.GetAxis("Mouse ScrollWheel") * 21;
         zoom = Mathf.Clamp(zoom, zoomMin, zoomMax);
     }
         
     void LateUpdate() 
     {
        vcam.m_Lens.FieldOfView = Mathf.Lerp (vcam.m_Lens.FieldOfView, zoom, Time.deltaTime * zoomSpeed);
     }
}
    
