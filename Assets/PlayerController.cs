using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerController : MonoBehaviourPun
{
    public GameObject bulletPrefab;
    public Transform firePos;
    public Image hpBar;

    float horizontal;
    float vertical;

    Rigidbody2D rb;

    [SerializeField]
    private float hp;

    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        hp = 100f;
        hpBar.fillAmount = hp / 100f;
    }

    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(horizontal, vertical, 0).normalized;
        transform.position += movement * 3 * Time.deltaTime;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector2 direction = (mousePosition - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;

        if (Input.GetMouseButtonDown(0))
        {
            GameObject newBullet = PhotonNetwork.Instantiate(bulletPrefab.name, firePos.position, firePos.rotation);
            StartCoroutine(DestoryObject(newBullet, 2f));
        }
    }

    IEnumerator DestoryObject(GameObject target, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (tag != null)
            PhotonNetwork.Destroy(target);
    }

    [PunRPC]
    public void GetDamage(float damage)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (hp >= 0)
            {
                hp -= damage;
                photonView.RPC("UpdateHp", RpcTarget.All, hp);
            }
        }
    }

    [PunRPC]
    public void UpdateHp(float newHp)
    {
        hp = newHp;
        hpBar.fillAmount = hp / 100f;
    }
}
