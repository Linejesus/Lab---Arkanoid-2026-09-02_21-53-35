using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public KeyCode moveLeft = KeyCode.LeftArrow;
    public KeyCode moveRight = KeyCode.RightArrow;
    public float speed = 10.0f;

    public float boundX = 7.5f;

    public float biggerSize = 2.5f;
    public float biggerSizeDuration = 10.0f;

    private Rigidbody2D rb2d;
    private Vector3 originalScale;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
    }

    void Update()
    {
        MovePlayer();
        LimitPlayerPosition();
    }

    void MovePlayer()
    {
        float movement = 0;

        if (Input.GetKey(moveRight))
            movement = 1;

        else if (Input.GetKey(moveLeft))
            movement = -1;

        rb2d.linearVelocity = new Vector2(movement * speed, 0);
    }

    void LimitPlayerPosition()
    {
        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, -boundX, boundX);

        transform.position = pos;
    }

    public void IncreaseSize()
    {
        StopCoroutine(nameof(BiggerPlayerRoutine));
        StartCoroutine(BiggerPlayerRoutine());
    }

    private System.Collections.IEnumerator BiggerPlayerRoutine()
    {
        transform.localScale = new Vector3(
            originalScale.x * biggerSize,
            originalScale.y,
            originalScale.z
        );

        Debug.Log("PowerUp: Paddle aumentado!");

        yield return new WaitForSeconds(biggerSizeDuration);

        transform.localScale = originalScale;

        Debug.Log("PowerUp: Paddle voltou ao tamanho normal.");
    }
}