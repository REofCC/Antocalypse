using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPanel : MonoBehaviour
{
    [SerializeField] Button centerCameraButton;
    [SerializeField] Button helpButton;
    [SerializeField] Button settingsButton;

    private void Start()
    {
        centerCameraButton.onClick.AddListener(MoveCameraToQueen);
        helpButton.onClick.AddListener(OpenHelpMenu);
        settingsButton.onClick.AddListener(OpenSettingsMenu);
    }

    //[LSH:TODO] [Option Panel]
    void MoveCameraToQueen()
    {
        //Camera.main.transform.position = new Vector3(QueenAnt.Instance.transform.position.x, QueenAnt.Instance.transform.position.y, -10f);
    }
    
    private void OpenHelpMenu()
    {        
        //Open Help Panel & Pause
        Time.timeScale = 0;
        //Activate Help Panel Logic
    }

    private void OpenSettingsMenu()
    {
        //Open Settings Panel & Pause
        Time.timeScale = 0;
        //Activate Settings Panel Logic
    }
}
