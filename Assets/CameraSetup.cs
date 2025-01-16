using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraSetup : MonoBehaviourPun
{

    Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        }
    }
}
