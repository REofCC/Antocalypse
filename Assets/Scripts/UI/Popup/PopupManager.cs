using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance;

    SlidePopup currentPopup;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OpenPopup(SlidePopup newPopup)
    {
        if (currentPopup != null)
        {
            currentPopup.ClosePopup();
        }

        currentPopup = newPopup;        
    }

    public void ClosePopup(SlidePopup popup)
    {
        if (currentPopup == popup)
        {
            currentPopup = null;
        }
    }
}
