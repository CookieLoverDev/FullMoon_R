using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    internal float bulletSpeed = 1;
    internal Rigidbody2D bulletRb;
    private int damage = 1;

    void Start()
    {
        bulletRb = GetComponent<Rigidbody2D>();
        bulletRb.velocity = HolySpirit.target * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerHealthSystem playerStatus = collision.gameObject.GetComponent<PlayerHealthSystem>();
            playerStatus.RecieveDamage(damage, Vector2.zero);
            Destroy(gameObject);
        }
    }
}
