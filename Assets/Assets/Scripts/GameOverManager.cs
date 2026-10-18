using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField] private GameObject gameOverPanel;

    [Header("Pontuação")]
    [SerializeField] private TMP_Text scoreText;

    private int score = 0;
    private bool isGameOver = false;

    void Start()
    {
        Time.timeScale = 1f;
        score = 0;
        isGameOver = false;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        UpdateScoreUI();
    }

    public void AddPoint()
    {
        if (isGameOver)
            return;

        score++;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Pontos: " + score;
    }


    public void GameOver()
    {
        if (isGameOver)
            return;

        isGameOver = true;

        // Exibe a tela de Game Over
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        // Pausa o jogo
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}