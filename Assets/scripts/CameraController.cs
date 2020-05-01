using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public UIManager UIManager;
    [HideInInspector]
    public bool startToRotateCamera = false;

    private bool isCameraRotateFinish = true;
    private float rotateSpeed = 90;
    private float rotateAngle = 90;

    public Color[] colors;

    void Start()
    {
        startToRotateCamera = false;
        StartCoroutine(RotateCamera());

        GetComponent<Camera>().backgroundColor = colors[Random.Range(0, colors.Length)];
    }

    IEnumerator RotateCamera()
    {
        while (true)
        {
            if (ScoreManager.Instance.score != 0 && (ScoreManager.Instance.score % UIManager.scoreToScaleCamera == 0) && isCameraRotateFinish && !FindObjectOfType<playerController>().gameOver)
            {
                isCameraRotateFinish = false;
                startToRotateCamera = true;
                FindObjectOfType<playerController>().touchDisable = true;

                float currentAngle = 0;
                while (currentAngle < rotateAngle)
                {
                    transform.RotateAround(Vector3.zero, Vector3.up, rotateSpeed * Time.deltaTime);
                    currentAngle += rotateSpeed * Time.deltaTime;
                    yield return null;
                }
                startToRotateCamera = false;
                isCameraRotateFinish = true;
                FindObjectOfType<playerController>().touchDisable = false;
            }
            yield return null;
        }
    }
}
