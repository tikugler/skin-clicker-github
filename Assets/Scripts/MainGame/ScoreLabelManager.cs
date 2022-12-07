using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreLabelManager : MonoBehaviour
{
    public Text CreditUiText;
    public static int Credits;

    // Start is called before the first frame update
    void Start()
    {
        Credits = Account.credits;
        CreditUiText = GameObject.Find("CreditUiText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        RefreshCredits();
    }
    
    private void RefreshCredits()
    {
        CreditUiText.text = "$ " + Account.credits;
    }
}
