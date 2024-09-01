using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float fallingTime;
    private TargetJoint2D target;
    private BoxCollider2D boxColl;
    void Start()
    {
        target = GetComponent<TargetJoint2D>();
        boxColl = GetComponent<BoxCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision) // Método para detectar toda vez que o personagem tocar em alguma coisa
    {
        if (collision.gameObject.tag == "Player")
        {
            Invoke("Falling", fallingTime); // Se o personagem tocar, a plataforma cai depois de ... segundos
        }

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 9)
        {
            Destroy(gameObject); // Se ele bater em um objeto com a layer 9 ele é destruido
        }
    }



    void Falling()
    {
        target.enabled = false;
        boxColl.isTrigger = true;
    }

}
