using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{

    public static int scoreValue = 0;
    public static int multiplierValue = 1;
    public static int killCount = 0;
    public static int targetKillCount = 50;

    Text score;
    public Text multiplierText;
    public Canvas canvasWorld;
    public Vector3 multiplierTextOffset;
    
    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text> ();
    }

    // Update is called once per frame
    void Update()
    {
        if (killCount >= targetKillCount) {
            multiplierValue++;
            killCount = 0;
        }

        score.text = "Score: " + scoreValue;
    }
}
