using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class Money : MonoBehaviour
{

    public Text moneyText;

    void Update()
    {
        moneyText.text = "$" + Stats.Money.ToString();    
    }
}
