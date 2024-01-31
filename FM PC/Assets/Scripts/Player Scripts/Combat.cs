using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public GameObject weaponHitBox;
    public Animator animator;

    private Player player;
    internal bool attackState;

    internal static int mana;

    private void Start()
    {
        mana = 3;
        attackState = false;
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (attackState)
            return;

        weaponHitBox.transform.localPosition = new Vector3(0.55f * player.lastDirection.x, 0.55f * player.lastDirection.y, 0);

        if (Input.GetMouseButtonDown(0))
        {
            attackState = true;
            animator.SetTrigger("swordAttack");

            animator.SetFloat("Horizontal", player.lastDirection.x);
            StartCoroutine(ResetAttackState());
        }
    }

    private IEnumerator ResetAttackState()
    {
        yield return new WaitForSeconds(0.85f);
        attackState = false;
    }
}
