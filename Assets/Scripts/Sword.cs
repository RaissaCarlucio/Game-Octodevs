using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private SpriteRenderer playerSpriteRenderer;
    private BoxCollider2D collider2D;

    public Animator animator;
    public GameObject swordParent;

    public Vector3 swordOffsetRight; // Posição da espada quando o personagem está virado para a direita
    public Vector3 swordOffsetLeft;  // Posição da espada quando o personagem está virado para a esquerda

    void Start()
    {
        playerSpriteRenderer = transform.root.GetComponent<SpriteRenderer>();
        collider2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        if (playerSpriteRenderer.flipX)
        {
            swordParent.transform.localPosition = swordOffsetLeft; // Move a espada para a posição da esquerda
        }
        else
        {
            swordParent.transform.localPosition = swordOffsetRight; // Move a espada para a posição da direita
        }
    }

    public void Attack()
    {
        animator.Play("Swordattack");
        collider2D.enabled = true;
        Invoke("DisableAttack", 0.5f);
    }

    private void DisableAttack()
    {
        collider2D.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // Supondo que o Slime tenha a tag "Enemy"
        {
            Slime slime = collision.GetComponent<Slime>();

            if (slime != null)
            {
                slime.TakeDamage(); // Reduz a vida do Slime ao acertar
            }
        }
    }
}
