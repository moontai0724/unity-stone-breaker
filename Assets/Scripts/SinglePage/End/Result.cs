using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Result : MonoBehaviour
{
    public Text result;
    public Text grade;
    public AudioSource winAudio;
    public AudioSource loseAudio;

    void Start()
    {
        if (SceneController.status > 0)
        {
            winAudio.Play(0);
            Color color = new Color((float)144 / (float)255, (float)238 / (float)255, (float)144 / (float)255, 1.0f);
            result.text = "你贏了！";
            result.color = color;
            grade.color = color;
        }
        else if (SceneController.status < 0)
        {
            loseAudio.Play(0);
            Color color = new Color((float)255 / (float)255, (float)182 / (float)255, (float)193 / (float)255, 1.0f);
            result.text = "你輸了...";
            result.color = color;
            grade.color = color;
        }
        else
        {
            result.text = "發生未知的錯誤";
            result.color = Color.red;
            grade.color = Color.red;
        }
        grade.text = SceneController.grade.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
            SceneController.restart();
    }
}
