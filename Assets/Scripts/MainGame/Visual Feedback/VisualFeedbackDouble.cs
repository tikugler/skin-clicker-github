using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualFeedbackDouble : MonoBehaviour
{



    public void ChangeCreditLabelAfterBuyingMultipleDoubles(int amount)
    {
        Color[] colors = { new Color(0, 1, 0, 1), new Color(1, 0, 0, 1), new Color(0, 0, 1, 1) };
        int fontSize;
        Text ScoreLabel = GameObject.Find("CreditUiText").GetComponent<Text>();

        if ((amount % 5) == 0)
        {
            
            ScoreLabel.color = colors[(amount / 5) % 3];
            ScoreLabel.fontSize = fontSize = 40;
            ScoreLabel.fontStyle = FontStyle.Bold;
        }
        else {
            
            ScoreLabel.fontSize = fontSize = 30;
            ScoreLabel.fontStyle = FontStyle.Normal;
        
        }
    }
    }



