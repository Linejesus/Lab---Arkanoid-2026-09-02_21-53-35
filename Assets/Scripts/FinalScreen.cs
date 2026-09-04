using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinalScreen : MonoBehaviour
{
    public TMP_Text resultText;
    public TMP_Text finalScoreText;
    public TMP_Text messageText;

    void Start()
    {
        if (GameManager.Instance == null)
            return;

        int finalScore =
            GameManager.Instance.GetScore();

        // Mostra a pontuação
        finalScoreText.text =
            "PONTUAÇÃO FINAL: " + finalScore;

        // Verifica vitória ou derrota
        if (!GameManager.Instance.HasWon())
        {
            resultText.text = "GAME OVER";

            messageText.text =
                "Suas vidas acabaram.\n" +
                "Tente novamente e melhore sua pontuação!";
        }
        else
        {
            resultText.text = "PARABÉNS!";

            messageText.text =
                "Você destruiu todos os blocos\n" +
                "e completou todos os níveis!";
        }
    }

    public void RestartGame()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RestartGame();
        }
        else
        {
            SceneManager.LoadScene("Cena_1");
        }
    }
}