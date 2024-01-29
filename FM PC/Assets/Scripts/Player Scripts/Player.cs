using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float walkSpeed;
    public float sprintSpeed;
    private float currentSpeed;
    float directionX, directionY;

    public float dashSpeed = 14;
    public float dashTime = 0.5f;
    public float dashWaitTime = 1.5f;
    private bool isDashing;
    private bool canDash;
    private Vector3 mousePos;
    private Vector2 dashDir;
    internal Vector2 lastDirection = new Vector2();

    public Rigidbody2D playerRb;
    private TrailRenderer trailRenderer;

    private bool sprintInput;
    private bool dashInput;

    public PlayerHealthSystem playerStatus;
    public Animator animator;
    private Combat combat;

    private void Start()
    {
        currentSpeed = walkSpeed;
        canDash = true;

        combat = GetComponent<Combat>();
        trailRenderer = gameObject.GetComponent<TrailRenderer>();

        if (lastDirection == Vector2.zero)
        {
            lastDirection.x = 0;
            lastDirection.y = -1;
        }

        if (trailRenderer != null)
        {
            trailRenderer.emitting = false;
        }
    }

    private void Update()
    {
        if (combat.attackState)
            return;

        if (!playerStatus.isAlive)
            return;

        directionX = Input.GetAxisRaw("Horizontal");
        directionY = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(directionX, directionY);
        
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        if (direction.sqrMagnitude > 0.01 && direction != Vector2.zero)
        {
            animator.SetFloat("speed", direction.sqrMagnitude);
            animator.SetTrigger("inMovement");

            animator.SetFloat("Horizontal", directionX);
            animator.SetFloat("Vertical", directionY);

            lastDirection = direction;
        }
        else
        {
            animator.SetFloat("speed", 0);

            animator.SetFloat("Horizontal", lastDirection.x);
            animator.SetFloat("Vertical", lastDirection.y);
        }

        sprintInput = Input.GetKey(KeyCode.LeftShift);
        dashInput = Input.GetKeyDown(KeyCode.Space);
    }

    private void FixedUpdate()
    {
        if (playerStatus.isAlive)
        {
            Vector2 delta = new Vector2(directionX * currentSpeed, directionY * currentSpeed);
            playerRb.velocity = delta;

            Sprint();
            Dash();
        }
    }

    private void Sprint()
    {
        if (sprintInput)
        {
            currentSpeed = sprintSpeed;
        }
        else
        {
            currentSpeed = walkSpeed;
        }
    }

    private void Dash()
    {
        if (dashInput && canDash)
        {
            canDash = false;
            isDashing = true;
            trailRenderer.emitting = true;
            dashDir = new Vector2(directionX, directionY);
            Debug.Log(dashDir);

            if (dashDir == Vector2.zero)
            {
                dashDir = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
            }

            StartCoroutine(StopDashing());
            StartCoroutine(AllowDash());
        }

        if (isDashing)
        {
            playerRb.velocity = dashDir * dashSpeed;
            return;
        }
    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashTime);
        trailRenderer.emitting = false;
        isDashing = false;
    }

    private IEnumerator AllowDash()
    {
        yield return new WaitForSeconds(dashWaitTime);
        canDash = true;
    }
}
