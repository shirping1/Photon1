using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviourPun
{
    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * 7);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponentInParent<PlayerController>().HpUpdate(-10f);

            if (photonView.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }

    }
}
