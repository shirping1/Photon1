using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public GameObject item;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);

        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(SpawnItem(item, 5f));
        }
    }

    IEnumerator SpawnItem(GameObject item, float delay)
    {
        float x;
        float y;
        while (true)
        {
            yield return new WaitForSeconds(delay);
            x = Random.Range(-10, 10);
            y = Random.Range(-10, 10);
            PhotonNetwork.Instantiate(item.name, new Vector3(x, y, 0), Quaternion.identity);
        }
    }
}
