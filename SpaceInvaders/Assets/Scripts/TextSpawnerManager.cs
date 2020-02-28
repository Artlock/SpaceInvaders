using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSpawnerManager : MonoBehaviour
{
    public GameObject startText;
    public GameObject endText;

    private void Awake()
    {
        startText.SetActive(false);
        endText.SetActive(false);

        GameManager.OnEndOfGame += EndText;

        startText.SetActive(true);
        startText.GetComponent<InOutFade>().StartInOut();
        startText.GetComponent<Text>().text = "ALERT - SPACE INVADERS !";
    }

    private void EndText()
    {
        endText.SetActive(true);
        endText.GetComponent<InOutFade>().StartInOut();
        endText.GetComponent<Text>().text = ScoreManager.Instance.totalKillCount + " ALIENS WERE DESTROYED";
    }

    private void OnDestroy()
    {
        GameManager.OnEndOfGame -= EndText;
    }
}
