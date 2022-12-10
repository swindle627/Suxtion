using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ManageScores : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    void Start()
    {
        SaveData data = LoadData.Load();

        if(data == null)
        {
            for(int i = 0; i < DataList.scores.Length; i++)
            {
                DataList.scores[i] = 0;
            }
        }
        else
        {
            DataList.scores = data.highScores;
        }

        if(PlayerPrefs.HasKey("score"))
        {
            float matchScore = PlayerPrefs.GetFloat("score");

            if (matchScore > DataList.scores[DataList.scores.Length - 1])
            {
                DataList.scores[DataList.scores.Length - 1] = matchScore;
                Array.Sort(DataList.scores);
                Array.Reverse(DataList.scores);
                LoadData.Save();
            }
        }

        string rank1 = "01. " + DataList.scores[0] + "\n";
        string rank2 = "02. " + DataList.scores[1] + "\n";
        string rank3 = "03. " + DataList.scores[2] + "\n";
        string rank4 = "04. " + DataList.scores[3] + "\n";
        string rank5 = "05. " + DataList.scores[4] + "\n";
        string rank6 = "06. " + DataList.scores[5] + "\n";
        string rank7 = "07. " + DataList.scores[6] + "\n";
        string rank8 = "08. " + DataList.scores[7] + "\n";
        string rank9 = "09. " + DataList.scores[8] + "\n";
        string rank10 = "10. " + DataList.scores[9];

        scoreText.text = rank1 + rank2 + rank3 + rank4 + rank5 + rank6 + rank7 + rank8 + rank9 + rank10;

    }
}
