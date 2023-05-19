using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class LookAt : MonoBehaviour
{
    CinemachineVirtualCamera CMVCam;

    // Start is called before the first frame update
    void OnEnable()
    {
        CMVCam = GetComponent<CinemachineVirtualCamera>();
        LookAtMe.OnStart += SetLookAt;
    }

    void OnDisable()
    {
        LookAtMe.OnStart -= SetLookAt;
    }

    void SetLookAt(Transform obj) => CMVCam.Follow = obj;

}
