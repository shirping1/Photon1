using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpItem : MonoBehaviourPun
{
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponentInParent<PlayerController>().HpUpdate(50f);

            if (photonView.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}
