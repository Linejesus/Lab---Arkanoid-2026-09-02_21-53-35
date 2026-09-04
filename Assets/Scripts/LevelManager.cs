using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private bool levelCompleted = false;

    void Update()
    {
        if (levelCompleted)
            return;

        GameObject[] bricks =
            GameObject.FindGameObjectsWithTag("Brick");

        if (bricks.Length == 0)
        {
            levelCompleted = true;

            Debug.Log("Todos os blocos foram destruídos!");

            if (GameManager.Instance != null)
            {
                GameManager.Instance.LevelComplete();
            }
        }
    }
}