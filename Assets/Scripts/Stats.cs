
using UnityEngine;

public class Stats : MonoBehaviour
{

    public static int Money; // static so that Money is accessible with only player stats type without requiring any instacnes or references
    public int startMoney = 400;

    public static int lives;
    public int startLife = 20;

    void Start() {
        Money = startMoney;
        lives = startLife;
    }

}
