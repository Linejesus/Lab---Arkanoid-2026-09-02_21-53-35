using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int startingLives = 3;

    private int lives;
    private int score;

    private bool gameOver = false;

    public TMP_Text scoreText;
    public TMP_Text livesText;
    public TMP_Text levelText;

    private bool playerWon = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        lives = startingLives;
        score = 0;
        gameOver = false;
        playerWon = false;

        UpdateUI();
    }

    // =========================================================
    // PONTUAÇÃO
    // =========================================================

    public void AddScore(int points)
    {
        score += points;

        UpdateUI();
    }

    // =========================================================
    // VIDAS
    // =========================================================

    public void LoseLife()
    {
        lives--;

        UpdateUI();

        Debug.Log(
            "Vida perdida! Vidas restantes: " +
            lives
        );

        if (lives <= 0)
        {
            GameOver();
        }
    }

    public void AddLife()
    {
        lives++;

        UpdateUI();

        Debug.Log(
            "Vida extra! Vidas: " +
            lives
        );
    }

    // =========================================================
    // GAME OVER
    // =========================================================

    void GameOver()
    {
        gameOver = true;

        Debug.Log("GAME OVER!");

        SceneManager.LoadScene("Cena_Final");
    }

    // =========================================================
    // FASE
    // =========================================================

    public void LevelComplete()
    {
        string currentScene =
            SceneManager.GetActiveScene().name;

        Debug.Log(
            "Fase concluída: " +
            currentScene
        );

        if (currentScene == "Cena_1")
        {
            SceneManager.LoadScene("Cena_2");
        }
        else if (currentScene == "Cena_2")
        {
            SceneManager.LoadScene("Cena_3");
        }
        else if (currentScene == "Cena_3")
        {
            playerWon = true;

            SceneManager.LoadScene("Cena_Final");
        }
    }

    public bool HasWon()
    {
        return playerWon;
    }

    // =========================================================
    // UI
    // =========================================================

    public void UpdateUI()
    {
        GameObject scoreObject =
            GameObject.Find("ScoreText");

        GameObject livesObject =
            GameObject.Find("LivesText");

        GameObject levelObject =
            GameObject.Find("LevelText");

        if (scoreObject != null)
        {
            scoreText =
                scoreObject.GetComponent<TMP_Text>();
        }

        if (livesObject != null)
        {
            livesText =
                livesObject.GetComponent<TMP_Text>();
        }

        if (levelObject != null)
        {
            levelText =
                levelObject.GetComponent<TMP_Text>();
        }

        if (scoreText != null)
        {
            scoreText.text =
                "PONTOS: " + score;
        }

        if (livesText != null)
        {
            livesText.text =
                "VIDAS: " + lives;
        }

        if (levelText != null)
        {
            string sceneName =
                SceneManager.GetActiveScene().name;

            if (sceneName == "Cena_1")
                levelText.text = "NÍVEL: 1";

            else if (sceneName == "Cena_2")
                levelText.text = "NÍVEL: 2";

            else if (sceneName == "Cena_3")
                levelText.text = "NÍVEL: 3";
        }
    }

    // =========================================================
    // GETTERS
    // =========================================================

    public int GetLives()
    {
        return lives;
    }

    public int GetScore()
    {
        return score;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    // =========================================================
    // REINICIAR
    // =========================================================

    public void RestartGame()
    {
        lives = startingLives;
        score = 0;
        gameOver = false;
        playerWon = false;

        SceneManager.LoadScene("Cena_1");
    }
}