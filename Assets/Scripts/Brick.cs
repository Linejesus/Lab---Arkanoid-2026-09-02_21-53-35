using UnityEngine;

public class Brick : MonoBehaviour
{
    public int hitPoints = 1;
    public int scoreValue = 10;

    public GameObject[] powerUpPrefabs;

    [Range(0f, 1f)]
    public float powerUpChance = 0.15f;

    // =========================================================
    // RECEBER DANO
    // =========================================================

    public void TakeDamage()
    {
        hitPoints--;

        Debug.Log(
            "Bloco atingido! Vida restante: " +
            hitPoints
        );

        if (hitPoints <= 0)
        {
            DestroyBrick();
        }
    }

    // =========================================================
    // DESTRUIR BLOCO
    // =========================================================

    void DestroyBrick()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.AddScore(scoreValue);

        SpawnPowerUp();

        Destroy(gameObject);
    }

    // =========================================================
    // GERAR POWER-UP
    // =========================================================

    void SpawnPowerUp()
    {
        if (powerUpPrefabs == null ||
            powerUpPrefabs.Length == 0)
        {
            return;
        }

        float randomValue = Random.value;

        if (randomValue > powerUpChance)
        {
            return;
        }

        int randomIndex =
            Random.Range(
                0,
                powerUpPrefabs.Length
            );

        GameObject selectedPowerUp =
            powerUpPrefabs[randomIndex];

        Instantiate(
            selectedPowerUp,
            transform.position,
            Quaternion.identity
        );
    }
}
