using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour, IDamagable
{
    public Rigidbody2D slimeRb;
    public DetectionZone detectionZone;
    public Animator animator;
    public GameObject deadBody;

    private float moveSpeed = 1.0f;
    public float damage = 1.0f;
    public int resistance = 0;
    public float knockBackForce = 500f;
    private int health = 5;

    private bool isAlive;

    private void Start()
    {
        isAlive = true;
    }

    private void FixedUpdate()
    {
        if (detectionZone.detectedObjcs.Count > 0)
        {
            transform.position = Vector2.MoveTowards(
                this.transform.position, 
                detectionZone.detectedObjcs[0].transform.position, 
                moveSpeed * Time.deltaTime
                );
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealthSystem playerStatus = collision.gameObject.GetComponent<PlayerHealthSystem>();
        Player player = collision.gameObject.GetComponent<Player>();

        if (!isAlive)
            return;

        if (playerStatus != null)
        {
            Vector3 enemyPos = this.transform.position;
            Vector2 knockBackDirection = (Vector2)(player.transform.position - enemyPos).normalized;
            Vector2 knockBack = knockBackDirection * knockBackForce;

            playerStatus.RecieveDamage(damage, knockBack);
        }
    }


    public void OnHit(int damage, Vector2 knockBack)
    {
        health -= damage;

        if (health <= 0)
        {
            isAlive = false;
            PlayerMoney.playerMoney += 1;
            animator.SetTrigger("Dead");
            StartCoroutine(Die());
        }

        if (health > 1)
        {
            slimeRb.AddForce(knockBack);
        }

        Debug.Log($"Slime current have {health} health");
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        Instantiate(deadBody, transform.position, Quaternion.identity);
    }
}