using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : MonoBehaviour
{

    private SpriteRenderer sr;
    private CircleCollider2D circle;

    public GameObject collected;

    public int Score;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        circle = GetComponent<CircleCollider2D>();
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player") // Checando se a minha maçã bateu em alguma coisa
        {
            sr.enabled = false;
            circle.enabled = false;
            collected.SetActive(true); // Ativando a minha fumaça quando a Cherry for destruida
            // Ela tem que sumir e mostrar que coletou

            GameController.instance.totalScore += Score; //Chamando a função da outra classe para essa.
            GameController.instance.UpdateScoreText(); // Chamando a função do GameController

            Destroy(gameObject, 0.3f); // Cherry vai ser destruida depois de 1s

        }
    }

}
