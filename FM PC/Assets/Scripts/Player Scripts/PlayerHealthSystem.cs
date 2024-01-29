using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    internal float maxHealth = 6;
    internal float currentHealth;
    private bool isInvincible;
    public bool isAlive;

    public Rigidbody2D playerRb;

    private void Start()
    {
        isAlive = true;
        isInvincible = false;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (currentHealth <= 0)
        {
            isAlive = false;
        }

        if (isInvincible)
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            RecieveDamage(1, Vector2.zero);
        }
    }

    public void RecieveDamage(float damage, Vector2 knockBack)
    {
        if (!isInvincible && isAlive)
        {
            currentHealth -= damage;
            isInvincible = true;

            playerRb.AddForce(knockBack);

            StartCoroutine(Invincible());
        }
    }

    private IEnumerator Invincible()
    {
        yield return new WaitForSeconds(1f);
        isInvincible = false;
    }
}
