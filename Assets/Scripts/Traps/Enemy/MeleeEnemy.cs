using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header ("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range; // Declarado a variável range
    [SerializeField] private int damage;

    [Header ("Collider Parameters")]
    [SerializeField] private float colliderDistance; 
    [SerializeField] private BoxCollider2D boxCollider; // Corrigido o nome para BoxCollider2D

    [Header ("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;
    private Animator anim;

    public float deathAnimationDuration = 1.0f; // Duração da animação de morte em segundos

    private EnemyPatrol enemyPatrol;

    [Header ("Heart System")]
    private HeartSystem playerHeartSystem;

    public int lifes = 2;

    public Heart2 heart2;

    public void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime; // Corrigido o uso do Time.deltaTime

        if (PlayerInsight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                // Attack logic here
                cooldownTimer = 0; // Reset the cooldown timer after attacking
                anim.SetTrigger("meleeAttack");
                DamagePlayer();
            }
        }

        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInsight(); // Corrigido o erro de sintaxe aqui
        }
    }

    private bool PlayerInsight()
    {
        RaycastHit2D hit = 
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
        {
            playerHeartSystem = hit.transform.GetComponent<HeartSystem>();
        }
            
        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if (playerHeartSystem != null)
        {
            playerHeartSystem.vida -= damage; // Subtrai o dano da vida do jogador
        }
    }

    // Inimigo leva dano
    public void TakeDamage(int damage)
   {
        lifes -= damage; // Reduz uma vida

        if (lifes <= 0)
        {
            Die(); // Se as vidas chegarem a 0, o boss morre
        }
   }

    protected virtual void Die()
    {
        if (anim != null)
        {
            anim.SetTrigger("die"); // Inicia a animação de morte
            StartCoroutine(WaitForAnimationAndDestroy()); // Espera a animação terminar antes de destruir o objeto
        }
        else
        {
            Destroy(gameObject); // Se não houver Animator, destrói o objeto imediatamente
        }
    }

    private IEnumerator WaitForAnimationAndDestroy()
    {
        // Espera a duração da animação de morte antes de destruir o objeto
        yield return new WaitForSeconds(deathAnimationDuration);
        Destroy(gameObject); // Destrói o objeto
    }

}
