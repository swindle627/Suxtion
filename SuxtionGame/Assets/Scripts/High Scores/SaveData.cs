using System;

[Serializable]
public class SaveData
{
    public float[] highScores;

    public SaveData()
    {
        highScores = DataList.scores;
    }
}
