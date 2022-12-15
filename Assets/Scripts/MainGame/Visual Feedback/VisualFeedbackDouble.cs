using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualFeedbackDouble : MonoBehaviour
{
 


    public void ChangeColorOfCreditLabelAfterBuyingMultipleDoubles(int amount)
    {
        Color[] colors = { new Color(0, 1, 0, 1), new Color(1, 0, 0, 1), new Color(1, 1, 1, 1), new Color(0, 0, 1, 1), new Color(1, 1, 0, 1), new Color(0, 0, 0, 1) };

        if ((amount % 5) == 0)
            {
                Text ScoreLabel = GameObject.Find("CreditUiText").GetComponent<Text>();
                ScoreLabel.color = colors[(amount / 5) % 3];
            }
        Debug.Log("****************************");
        Debug.Log(colors.Length);
        Debug.Log(amount);
        }
    }



