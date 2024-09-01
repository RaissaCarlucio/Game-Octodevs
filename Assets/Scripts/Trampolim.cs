using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolim : MonoBehaviour
{

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public float jumpForce;

    void OnCollisionEnter2D(Collision2D collision) // Quando meu personagem bater no trampolim
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.SetTrigger("Jump");
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }
}
