using UnityEngine;

public class BallControl : MonoBehaviour
{
    public float launchSpeed = 8.0f;
    public float maxSpeed = 15.0f;

    public Transform player;
    public float playerOffsetY = 0.5f;

    public float fasterBallDuration = 10.0f;
    public float fasterBallMultiplier = 1.5f;

    private float normalSpeed;
    private bool isFaster = false;

    private Rigidbody2D rb2d;
    private bool isLaunched = false;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        normalSpeed = launchSpeed;

        rb2d.linearVelocity = Vector2.zero;

        ResetBallPosition();
    }

    void Update()
    {
        if (!isLaunched)
        {
            FollowPlayer();
        }

        LimitBallSpeed();

        if (isLaunched && transform.position.y < -6.0f)
        {
            BallLost();
        }
    }

    // =========================================================
    // ACOMPANHAR NAVE
    // =========================================================

    void FollowPlayer()
    {
        if (player == null)
            return;

        transform.position = new Vector2(
            player.position.x,
            player.position.y + playerOffsetY
        );
    }

    // =========================================================
    // LANÇAMENTO
    // =========================================================

    public void LaunchBall()
    {
        if (isLaunched)
            return;

        if (GameManager.Instance != null &&
            GameManager.Instance.IsGameOver())
        {
            return;
        }

        isLaunched = true;

        float randomX = Random.Range(-0.7f, 0.7f);

        Vector2 direction = new Vector2(
            randomX,
            1.0f
        ).normalized;

        rb2d.linearVelocity = direction * launchSpeed;
    }

    // =========================================================
    // COLISÕES
    // =========================================================

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            BounceFromPlayer(collision);
        }

        Brick brick =
            collision.gameObject.GetComponent<Brick>();

        if (brick != null)
        {
            brick.TakeDamage();
        }
    }

    // =========================================================
    // REBATER NA NAVE
    // =========================================================

    void BounceFromPlayer(Collision2D collision)
    {
        if (player == null)
            return;

        Collider2D playerCollider =
            player.GetComponent<Collider2D>();

        float hitPoint =
            (transform.position.x - player.position.x) /
            (playerCollider.bounds.size.x / 2);

        hitPoint = Mathf.Clamp(hitPoint, -1.0f, 1.0f);

        Vector2 direction = new Vector2(
            hitPoint,
            1.0f
        ).normalized;

        float currentSpeed =
            rb2d.linearVelocity.magnitude;

        rb2d.linearVelocity =
            direction * currentSpeed;
    }

    // =========================================================
    // BOLA PERDIDA
    // =========================================================

    void BallLost()
    {
        isLaunched = false;

        rb2d.linearVelocity = Vector2.zero;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoseLife();

            if (!GameManager.Instance.IsGameOver())
            {
                ResetBallPosition();
            }
        }
    }

    // =========================================================
    // RESET
    // =========================================================

    public void ResetBall()
    {
        isLaunched = false;

        rb2d.linearVelocity = Vector2.zero;

        ResetBallPosition();
    }

    void ResetBallPosition()
    {
        if (player != null)
        {
            transform.position = new Vector2(
                player.position.x,
                player.position.y + playerOffsetY
            );
        }
        else
        {
            transform.position = Vector2.zero;
        }
    }

    // =========================================================
    // LIMITE DE VELOCIDADE
    // =========================================================

    void LimitBallSpeed()
    {
        if (rb2d.linearVelocity.magnitude > maxSpeed)
        {
            rb2d.linearVelocity =
                rb2d.linearVelocity.normalized *
                maxSpeed;
        }
    }

    // =========================================================
    // POWER-UP: BOLA RÁPIDA
    // =========================================================

    public void IncreaseSpeed(float multiplier)
    {
        StopCoroutine(nameof(FasterBallRoutine));

        StartCoroutine(
            FasterBallRoutine(multiplier)
        );
    }

    private System.Collections.IEnumerator FasterBallRoutine(
        float multiplier)
    {
        isFaster = true;

        float fasterSpeed =
            normalSpeed * multiplier;

        Debug.Log(
            "PowerUp: Bola acelerada por 10 segundos!"
        );

        if (rb2d.linearVelocity.magnitude > 0)
        {
            rb2d.linearVelocity =
                rb2d.linearVelocity.normalized *
                fasterSpeed;
        }

        yield return new WaitForSeconds(
            fasterBallDuration
        );

        if (rb2d.linearVelocity.magnitude > 0)
        {
            rb2d.linearVelocity =
                rb2d.linearVelocity.normalized *
                normalSpeed;
        }

        isFaster = false;

        Debug.Log(
            "PowerUp: Bola voltou à velocidade normal."
        );
    }
}