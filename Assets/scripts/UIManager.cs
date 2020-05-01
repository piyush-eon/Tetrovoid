using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public int scoreToScaleGroundIncrease = 15;
    public int scoreToScaleCameraIncrease = 15;

    public int scoreToScaleGround = 15;
    public int scoreToScaleCamera = 15;

    private bool enableCheck = true;

    public Text credit;
    public GameObject replayBtn;

    public Text scoreText, bestScoreText, goldText;

    void Start()
    {
        credit.enabled = false;
        replayBtn.SetActive(false);
        StartCoroutine(CountScore());
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = ScoreManager.Instance.score.ToString();
        bestScoreText.text = PlayerPrefs.GetInt("BestScore").ToString();
        goldText.text = PlayerPrefs.GetInt("Coin", 0).ToString();

        if (FindObjectOfType<CameraController>().startToRotateCamera && enableCheck)
        {
            enableCheck = false;
            scoreToScaleCamera += scoreToScaleCameraIncrease;
            scoreToScaleGround += scoreToScaleGroundIncrease;
            StartCoroutine(WaitAndEnableCheck());
        }

        if (FindObjectOfType<playerController>().gameOver)
            Invoke("EnableButton", 1.5f);
    }

    IEnumerator CountScore()
    {
        while (true)
        {
            if (FindObjectOfType<GroundManager>().finishMoveGround && !FindObjectOfType<playerController>().gameOver)
                ScoreManager.Instance.AddScore(1);

            yield return new WaitForSeconds(1);
        }
    }

    void EnableButton()
    {
        replayBtn.SetActive(true);
        credit.enabled = true;
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator WaitAndEnableCheck()
    {
        yield return new WaitForSeconds(2);
        enableCheck = true;
    }
}