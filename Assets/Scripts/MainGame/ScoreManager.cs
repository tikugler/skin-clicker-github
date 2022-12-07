using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
   
    public Text creditUIText;
    public static int credit = Account.credits;


    // Start is called before the first frame update
    void Start()
    {
        RefreshCredits();
    }

    // Update is called once per frame
    void Update()
    {
        RefreshCredits();
    }

    private void RefreshCredits()
    {
        credit = Account.credits;
        creditUIText.text = "$ " + credit.ToString();
    }

}
