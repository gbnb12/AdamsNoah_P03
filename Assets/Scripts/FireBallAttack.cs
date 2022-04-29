using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallAttack : MonoBehaviour
{
    private Animator animator;
    private Player player;
    [SerializeField] private float attackCooldown;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown )
        {
            Attack();
            cooldownTimer += Time.deltaTime; 
        }
    }

    private void Attack()
    {
        animator.SetTrigger("attack");
        cooldownTimer = 0;
    }
}
