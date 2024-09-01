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

    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * Speed;

        if (Input.GetAxis("Horizontal") > 0f)
        {
            anim.SetBool("walk", true); // andando para direita
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
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
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
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

    void OnCollisionEnter2D(Collision2D collision) // Método para detectar toda vez que o personagem tocar em lguma coisa
    {
        if (collision.gameObject.layer == 8)
        {
            isJumping = false; // Quando meu personagem está tocando o chão, ele não esta pulando.
        }

        if (collision.gameObject.tag == "Spike")
        {
            //Debug.Log("Tocou o espinho!");
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Serra")
        {
            //Debug.Log("Tocou na serra!");
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }
    }

    void OnCollisionExit2D(Collision2D collision)// Método chamado quando o personagem para de tocar alguma coisa
    {
        if (collision.gameObject.layer == 8)
        {
            isJumping = true;
        }
    }

    void KnockLogic()
    {
        if (KBCount < 0)
        {
            Move();
        }
        else
        {
            if (isKnockRight == true)
            {
                rig.velocity = new Vector2(-KBForce, KBForce);
            }
            if (isKnockRight == false)
            {
                rig.velocity = new Vector2(KBForce, KBForce);
            }
        }
        KBCount -= Time.deltaTime;
    }


}
