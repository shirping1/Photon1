using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviourPun
{
    float horizontal;
    float vertical;

    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(horizontal, vertical, 0).normalized;
        transform.position += movement * 5 * Time.deltaTime;
    }
}
