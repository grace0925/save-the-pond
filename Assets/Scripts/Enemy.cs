
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    [HideInInspector] // hide speed in inspector bc we don't need to modify it
    public float speed = 10f;

    public float health = 100;

    public int moneyGain = 30;

    void Start() {
        speed = startSpeed;
    }

    public void TakeDamage(float damage) {
        health -=damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Slow(float pert) {
        speed = startSpeed * (1f - pert);
    }

    void Die() {
        Stats.Money += moneyGain;
        Destroy(gameObject);
    }

}
