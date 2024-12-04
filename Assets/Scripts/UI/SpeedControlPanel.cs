using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedControlPanel : MonoBehaviour
{
    [SerializeField] Button pauseButton;
    [SerializeField] Button playButton;
    [SerializeField] Button fastForwardButton;
    [SerializeField] Image pauseImage;

    private void Start()
    {
        pauseButton.onClick.AddListener(() => SetGameSpeed(0));
        playButton.onClick.AddListener(() => SetGameSpeed(1));
        fastForwardButton.onClick.AddListener(() => SetGameSpeed(2));
    }

    void SetGameSpeed(float speed)
    {
        Time.timeScale = speed;
        if (speed == 0)
        {
            pauseImage.gameObject.SetActive(true);
        }
        else
        {
            pauseImage.gameObject.SetActive(false);
        }
    }
}
