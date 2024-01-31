using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealthSystem : MonoBehaviour
{
    public static float maxHealth = 6;
    internal static float currentHealth;
    internal static float armor;

    private bool isInvincible;
    public bool isAlive;

    public Rigidbody2D playerRb;

    public Text armorValue;

    public float timer = 0f;
    public float timerinterval = 10f;

    private void Start()
    {
        isAlive = true;
        isInvincible = false;
        currentHealth = maxHealth;

        armor = 0;
    }

    private void Update()
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (!isAlive)
        {
            if(Input.GetKeyDown(KeyCode.R)) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        timer += Time.deltaTime;
        if(timer >= timerinterval) {
            Regen();
            timer = 0f;
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


        if (armorValue.text == "1") {
            maxHealth = 7;
        }
        else if (armorValue.text == "2")
        {
            maxHealth = 8;
        }
        else if (armorValue.text == "3")
        {
            maxHealth = 9;

        }
        else if (armorValue.text == "4") {
            maxHealth = 10;
        }
        else if (armorValue.text == "0")
        {
        }
    }

    public void Regen()
    {
        if (isAlive)
        {
            if (currentHealth != maxHealth)
            {
                currentHealth += 1;
            }
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
