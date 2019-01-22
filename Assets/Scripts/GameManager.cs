using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool gameOver = false;
    // Update is called once per frame
    void Update()
    {
        if (gameOver)
            return;

        if (Stats.lives <= 0) {
            EndGame();
        }
    }

    private void EndGame() {
        gameOver = true;
        Debug.Log("GameOver!");
    }
}
