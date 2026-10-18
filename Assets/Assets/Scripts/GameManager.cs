using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField] private GameObject gameOverPanel;

    [Header("Pontuação")]
    [SerializeField] private TMP_Text scoreText;

    [Header("Vitória")]
    [SerializeField] private GameObject victoryPanel;

    [Header("Pausa")]
    [SerializeField] private GameObject pausePanel;

    private int score = 0;
    private bool isGameOver = false;
    private bool hasWon = false;
    private bool isPaused = false;

    #region PAUSE
    private void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TogglePause();
        }
    }
    public void TogglePause()
    {
        // Não permite pausar ou retomar após o fim do jogo.
        if (isGameOver || hasWon)
            return;

        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }
    public void PauseGame()
    {
        if (isGameOver || hasWon || isPaused)
            return;

        isPaused = true;
        Time.timeScale = 0f;

        if (pausePanel != null)
            pausePanel.SetActive(true);
    }
    public void ResumeGame()
    {
        if (!isPaused)
            return;

        isPaused = false;
        Time.timeScale = 1f;

        if (pausePanel != null)
            pausePanel.SetActive(false);
    }
    #endregion
    void Start()
    {
        Time.timeScale = 1f;
        score = 0;
        isGameOver = false;
        hasWon = false;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (victoryPanel != null)
            victoryPanel.SetActive(false);

        UpdateScoreUI();
    }
    #region SCORE
    public void AddPoint()
    {
        if (isGameOver || hasWon)
            return;

        score++;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Pontos: " + score;
    }

    #endregion

    #region GAMEOVER//WIN
    public void GameOver()
    {
        if (isGameOver || hasWon)
            return;

        isGameOver = true;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void WinGame()
    {
        if (isGameOver || hasWon)
            return;

        hasWon = true;

        if (victoryPanel != null)
            victoryPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
    #endregion
}