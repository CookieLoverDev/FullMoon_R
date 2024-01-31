using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolySpirit : MonoBehaviour, IDamagable
{
    private float speed = 2f;
    private float directionTime = 3f;

    public Rigidbody2D spiritRb;
    private Vector2 floatDirection;
    private bool changeDirection;
    public Shop shop;
    public DetectionZone range;
    public GameObject bulletPre;
    internal static Vector2 target;
    private bool canShoot;

    private int health = 5;
    private bool isAlive;

    private void Start()
    {
        isAlive = true;
        changeDirection = true;
        canShoot = true;
        if (shop == null)
            shop = FindObjectOfType<Shop>();
    }

    private void Update()
    {
        if (changeDirection)
        {
            RandomDir();
        }
    }

    private void FixedUpdate()
    {
        if (range.detectedObjcs.Count > 0 && canShoot)
        {
            canShoot = false;
            spiritRb.velocity = Vector2.zero;

            target = range.detectedObjcs[0].transform.position - this.transform.position;
            Instantiate(bulletPre, transform.position, transform.rotation);

            StartCoroutine(AttackReset());
        }

        if (range.detectedObjcs.Count == 0)
        {
            Move();
        }
    }

    private void RandomDir()
    {
        changeDirection = false;

        float RandomX = Random.Range(-1f, 1f);
        float RandomY = Random.Range(-1f, 1f);
        floatDirection = new Vector2(RandomX, RandomY).normalized;

        StartCoroutine(DirReset());
    }

    private void Move()
    {
        spiritRb.velocity = floatDirection * speed;
    }

    public void OnHit(int damage, Vector2 knockBack)
    {
        health -= damage;

        if (health <= 0)
        {
            isAlive = false;
            shop.playerMoney += 2;
            Destroy(gameObject);
        }

        if (health > 1)
        {
            spiritRb.AddForce(knockBack);
        }

        Debug.Log($"Slime current have {health} health");
    }

    private IEnumerator DirReset()
    {
        yield return new WaitForSeconds(directionTime);
        changeDirection = true;
    }

    private IEnumerator AttackReset()
    {
        yield return new WaitForSeconds(2);
        canShoot = true;
    }
}
