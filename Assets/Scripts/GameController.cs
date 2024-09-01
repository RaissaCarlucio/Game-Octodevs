using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Importando o namespace para TextMeshPro
using UnityEngine.SceneManagement; // Biblioteca para o restart do game

public class GameController : MonoBehaviour
{
    public int totalScore; // Criar um método Estático para poder utilizar o que está aqui em outro Script
    public TextMeshProUGUI scoreText; // Variável para editar o texto de score usando TextMeshPro
    public static GameController instance;

    public GameObject gameOver;

    // Start is called antes do primeiro frame update
    void Start()
    {
        instance = this;
        gameOver.SetActive(false);
        totalScore = PlayerPrefs.GetInt("TotalScore", 0); // Carrega o valor salvo
        UpdateScoreText();

    }

    public void UpdateScoreText()
    {
        scoreText.text = totalScore.ToString(); // Transformando o valor em um texto
    }

    public void ShowGameOver()
    {
        gameOver.SetActive(true); // Torna o objeto ativo
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("lvl_1");
    }

    void OnDestroy()
    {
        PlayerPrefs.SetInt("TotalScore", totalScore); // Salva o valor antes de destruir o GameController
    }
}
