
using UnityEngine;
using System.Collections; // allows IEnumerator, allow piece of code to be paused
using UnityEngine.UI; // allows reference to text

public class WaveSpawner : MonoBehaviour
{

    public Transform enemyPrefab;

    public Transform spawnpoint;

    public float timeBetweenWaves = 5f;
    private float countdown = 2f;

    private int waveIndex = 0;

    public Text waveCountDownText;

    void Update() {
        if (countdown <= 0f) {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime; // reduce countdown by 1 every second.

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity); // clamp time as a positive number

        waveCountDownText.text = string.Format("{0:00.00}", countdown); // fomat countdown in that specific format
    }

    IEnumerator SpawnWave() {//allow us to pause the code, when using an IEnumerator, we need to StartCoroutin somewhere.
        waveIndex++;
        for (int i = 0; i < waveIndex; i++) {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f); // wait for 0.5 seconds before enemy is spawned again.
        }
    }

    void SpawnEnemy() {
        Instantiate(enemyPrefab, spawnpoint.position, spawnpoint.rotation);
    }
}
