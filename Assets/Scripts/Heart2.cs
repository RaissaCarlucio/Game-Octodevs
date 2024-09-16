using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart2 : MonoBehaviour
{
    public int vida;
    public int vidaMaxima;

    public Image[] coracao;
    public Sprite cheio;
    public Sprite vazio;

    public Player player;


    void Update()
    {
        HealthLogic();
        DeadState();
    }

    void HealthLogic()
    {
        if (vida > vidaMaxima)
        {
            vida = vidaMaxima;
        }
        for (int i = 0; i < coracao.Length; i++)
        {

            if (i < vida)
            {
                coracao[i].sprite = cheio;
            }
            else
            {
                coracao[i].sprite = vazio;
            }
            if (i < vidaMaxima)
            {
                coracao[i].enabled = true;
            }
            else
            {
                coracao[i].enabled = false;
            }
        }
    }

    void DeadState()
    {
        if (vida <= 0)
        {
            if (player != null)
            {
                player.enabled = false; // Desativa o script Player
            }

            // Adicione outros comportamentos desejados aqui
            // Exemplo: Ativar uma tela de Game Over
            GameController.instance.ShowGameOver();

            Destroy(gameObject, 1.0f); // Destrói o GameObject após 1 segundo
        }
    }

}
