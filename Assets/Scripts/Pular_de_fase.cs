using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Adicionei o ponto e vírgula ao final

public class Pular_de_fase : MonoBehaviour
{
    public string lvlName; // Isso aparecerá no Inspector

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(lvlName);
        }
    }
}
