using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallAttack : MonoBehaviour
{
    
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;

    private float cooldownTimer = Mathf.Infinity;
    private Animator animator;
    private Player player;
    public AudioSource fireballThrowingSound;
    public AudioSource playerHitsEnemySound;
    public AudioSource powerupSound;

    static public bool canFireball = false;

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
            
            
        }
        cooldownTimer += Time.deltaTime;
      
    }
   
    public void Attack()
    {
        if (canFireball == true)
        {
            animator.SetTrigger("attack");
            cooldownTimer = 0;

            fireballs[FindFireball()].transform.position = firePoint.position;
            fireballs[FindFireball()].GetComponent<FireBall>().SetDirection(Mathf.Sign(transform.localScale.x));
            fireballThrowingSound.Play();
        }
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                return i;
            }
            
        }
        return 0;
    }

    // player interacts with fire power up
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PowerUp")
        {
            Destroy(collision.gameObject);
            GetComponent<SpriteRenderer>().color = Color.red;
            FireBallAttack.canFireball = true;
            powerupSound.Play();
        }
        if (collision.tag == "Enemy")
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            FireBallAttack.canFireball = false;
            playerHitsEnemySound.Play();
        }
    }
}
