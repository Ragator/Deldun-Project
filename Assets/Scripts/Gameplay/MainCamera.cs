using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera myVirtualCamera;

    void Start()
    {
        myVirtualCamera.Follow = GameObject.FindWithTag(DeldunProject.Tags.player).transform;
    }
}
