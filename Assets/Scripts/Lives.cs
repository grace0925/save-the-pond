using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Lives : MonoBehaviour
{

    public Text lives;

    void Update() {
        lives.text = Stats.lives.ToString() + "❤︎";
    }

}
