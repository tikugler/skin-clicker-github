using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardEntryTemplate : MonoBehaviour
{
    public Text PosText, NameText, ScoreText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTexts(string posText, string nameText, string scoreText)
    {
        PosText.text = posText;
        if (nameText == null)
            nameText = "Unknown";
        NameText.text = nameText;
        ScoreText.text = scoreText;
    }

    public void HighlightEntry()
    {
        PosText.color = Color.cyan;
        NameText.color = Color.cyan;
        ScoreText.color = Color.cyan;


    }

    public void RemoveHighlighting()
    {
        PosText.color = Color.white;
        NameText.color = Color.white;
        ScoreText.color = new Color(255, 255, 0);


    }




}
