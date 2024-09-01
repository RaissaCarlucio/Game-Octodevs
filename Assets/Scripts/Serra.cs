using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serra : MonoBehaviour
{
    public float speed; // Velocidade da serra

    private bool dirRight = true; // Quando a serra vai pro lado direito ou esquerdo. True = serra começa indo para direita
    private float timer;

    public float moveTime; // Tempo de movimento da serra

    // Update is called once per frame
    void Update()
    {
        if (dirRight)
        {
            // Serra vai para a direita
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            // Serra vai para a esquerda
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        // Incrementa o timer
        timer += Time.deltaTime; // Time.deltaTime retorna o tempo real do jogo

        // Se o timer atingir o tempo de movimento, a serra muda de direção
        if (timer >= moveTime)
        {
            dirRight = !dirRight; // Inverte a direção
            timer = 0f; // Reseta o timer
        }
    }
}
