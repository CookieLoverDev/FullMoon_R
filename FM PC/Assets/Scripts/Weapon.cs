using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Collider2D weaponCollider;
    private float knockBackForce = 1500f;

    internal int weaponLevel;
    internal static int weaponDamage = 5;

    private void Start()
    {
        if (weaponCollider != null)
            Debug.Log("Weapon is ready to use!");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            IDamagable enemy = collision.gameObject.GetComponent<IDamagable>();
            if (enemy != null) { 
                Vector3 parentPos = gameObject.GetComponentInParent<Transform>().position;

                Vector2 knockBackDirection = (Vector2)(collision.gameObject.transform.position - parentPos).normalized;
                Vector2 knockBack = knockBackDirection * knockBackForce;

                enemy.OnHit(weaponDamage, knockBack);
            }
            else
            {
                Debug.Log("Collider does not implement interface");
            }
        }
    }
}
