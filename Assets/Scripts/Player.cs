using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour

{
    // Start is called before the first frame update

    public float Speed;
    public float JumpForce;

    public bool isJumping;

    //public bool doubleJump;

    private Rigidbody2D rig;
    public Animator anim;

    public float KBForce;
    public float KBCount;
    public float KBTime;
    public bool isKnockRight;


    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); // Animação
    }

    // Update is called once per frame
    void Update()
    {
        KnockLogic();
        Jump();
    }

    // Função para movimentar o jogador
    void Move()
    {
        // Cria um vetor de movimento com base no input horizontal
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        // Move o jogador na posição usando o vetor de movimento
        transform.position += movement * Time.deltaTime * Speed;

        // Verifica a direção do movimento para alterar a animação e a rotação
        if (Input.GetAxis("Horizontal") > 0f)
        {
            anim.SetBool("walk", true); // andando para direita
            transform.eulerAngles = new Vector3(0f, 0f, 0f); // personagem olha para direita
        }

        if (Input.GetAxis("Horizontal") < 0f)
        {
            anim.SetBool("walk", true); // andando para esquerda
            transform.eulerAngles = new Vector3(0f, 180f, 0f); // personagem olha para esquerda
        }

        if (Input.GetAxis("Horizontal") == 0f)
        {
            anim.SetBool("walk", false); // Estou parado
        }

    }

    void Jump()
    {
        // Verifica se o botão de pulo foi pressionado e o jogador não está pulando
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            // Adiciona uma força para cima para pular
            rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
        }
        if (isJumping)
        {
            anim.SetBool("jump", true); // pulando
        }
        else
        {
            anim.SetBool("jump", false); // Não pulando
        }

    }

    void OnCollisionEnter2D(Collision2D collision) // Método para detectar toda vez que o personagem tocar em alguma coisa
    {
        if (collision.gameObject.layer == 8)
        {
            isJumping = false; // Quando meu personagem está tocando o chão, ele não esta pulando.
        }

        if (collision.gameObject.tag == "Spike") // Se o personagem encostar nos espinhos, ele morre
        {
            //Debug.Log("Tocou o espinho!");
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Serra") // Se o personagem encostar na serra, ele morre
        {
            //Debug.Log("Tocou na serra!");
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }
        
    }

    void OnCollisionExit2D(Collision2D collision)// Método chamado quando o personagem para de tocar alguma coisa
    {
        // Verifica se o jogador saiu da colisão com um objeto na camada 8 (chão)
        if (collision.gameObject.layer == 8)
        {
            isJumping = true;
        }
    }

    // Método quando o personagem é acertado pelo inimigo
    void KnockLogic()
    {
        // Se o tempo de knockback acabou, o jogador pode se mover normalmente
        if (KBCount < 0)
        {
            Move();
        }
        else
        {
            // Caso contrário, aplica a força de knockback
            if (isKnockRight == true)
            {
                rig.velocity = new Vector2(-KBForce, KBForce); // Empurra para a esquerda
            }
            if (isKnockRight == false)
            {
                rig.velocity = new Vector2(KBForce, KBForce); // Empurra para a direita
            }
        }
        // Diminui o tempo de knockback
        KBCount -= Time.deltaTime;
    }


}
