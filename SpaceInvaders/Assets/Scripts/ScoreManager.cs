using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int scoreValue = 0;

    public int baseValue = 50;
    public int multiplierValue = 1;

    public int killCount = 0;
    public int totalKillCount = 0;
    public int targetKillCount = 50;

    public Canvas canvasWorld;
    public GameObject scoreGainObject;
    public Text multiplierText;
    public Vector3 multiplierTextOffset;

    private Text score;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        else
        {
            Instance = this;
        }

        score = GetComponent<Text>();
    }

    private void Update()
    {
        // Update score text
        score.text = "Score - " + scoreValue;
    }

    public void IncreaseScore(Vector3 pos, Quaternion rot)
    {
        scoreValue += baseValue * multiplierValue;

        GameObject tempText = Instantiate(scoreGainObject, pos, rot);
        tempText.transform.SetParent(canvasWorld.transform, false);
        tempText.GetComponent<Text>().text = baseValue * multiplierValue + "";
    }

    public void IncreaseKillCount(Vector3 pos, Quaternion rot)
    {
        killCount++;
        totalKillCount++;

        // Update multiplier
        while (killCount >= targetKillCount)
        {
            multiplierValue++;
            killCount -= targetKillCount;

            GameObject tempMultiplierText = Instantiate(multiplierText.gameObject, pos + multiplierTextOffset, rot);
            tempMultiplierText.transform.SetParent(canvasWorld.transform, false);
            tempMultiplierText.GetComponent<Text>().text = "x" + multiplierValue;
        }
    }
}
