using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType
    {
        BiggerPlayer,
        FasterBall,
        ExtraLife
    }

    public PowerUpType type;

    public float fallSpeed = 3.0f;

    public float ballSpeedMultiplier = 1.5f;

    void Update()
    {
        transform.Translate(
            Vector2.down *
            fallSpeed *
            Time.deltaTime
        );

        if (transform.position.y < -7.0f)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        PlayerControls player =
            collision.GetComponent<PlayerControls>();

        if (player == null)
            return;

        ApplyPowerUp(player);

        Destroy(gameObject);
    }

    void ApplyPowerUp(PlayerControls player)
    {
        switch (type)
        {
            case PowerUpType.BiggerPlayer:

                player.IncreaseSize();

                Debug.Log("PowerUp: Paddle aumentado por 10 segundos!");

                break;


            case PowerUpType.FasterBall:

                BallControl ball =
                    FindObjectOfType<BallControl>();

                if (ball != null)
                {
                    ball.IncreaseSpeed(ballSpeedMultiplier);
                }

                Debug.Log("PowerUp: Bola rápida por 10 segundos!");

                break;


            case PowerUpType.ExtraLife:

                if (GameManager.Instance != null)
                {
                    GameManager.Instance.AddLife();
                }

                Debug.Log("PowerUp: Vida extra!");

                break;
        }
    }
}