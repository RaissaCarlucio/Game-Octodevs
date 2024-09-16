using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coelho : MonoBehaviour
{
    private Rigidbody2D rig; // Use Rigidbody2D para objetos 2D
    private Animator anim;

    public float speed = -2f; // Velocidade do Coelho, começando para a esquerda

    public Transform rightCol; // Ponto de detecção à direita
    public Transform leftCol;  // Ponto de detecção à esquerda

    public bool colliding; // Verificação de colisão

    public LayerMask layer; // Layer para verificar colisões

    public int lifes = 2; // Coelho começa com 2 vidas

    public HeartSystem heart;

    public Player player;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>(); // Inicialização do Rigidbody2D
        anim = GetComponent<Animator>();   // Inicialização do Animator

        // Certifica-se de que o Slime começa virado para a direita
        transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
    }

    void Update()
    {
        // Movimento do Slime
        rig.velocity = new Vector2(speed, rig.velocity.y);

        // Verifica se há colisão entre os pontos `rightCol` e `leftCol`
        colliding = Physics2D.Linecast(rightCol.position, leftCol.position, layer);

        if (colliding)
        {
            // Inverte a direção do Slime
            transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
            speed = -speed;
        }
    }

    //Método chamado ao coelho receber dano
    public void TakeDamage()
    {
        lifes--; // Reduz uma vida

        if (lifes <= 0)
        {
            Die(); // Se as vidas chegarem a 0, o Coelho "morre"
        }
    }

    // Método chamado quando o Coelho morre
    void Die()
    {
        Destroy(gameObject); // Destroi o Coelho
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.KBCount = player.KBTime;
            if (collision.transform.position.x <= transform.position.x)
            {
                player.isKnockRight = true;
            }
            if (collision.transform.position.x > transform.position.x)
            {
                player.isKnockRight = false;
            }
            heart.vida--;
            player.anim.SetTrigger("TakeDamage");
        }
    }
}
