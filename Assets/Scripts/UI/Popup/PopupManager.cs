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

    public void TogglePopup(SlidePopup newPopup)
    {
        if (currentPopup == newPopup && newPopup.IsPopupOpen())
        {
            newPopup.ClosePopup();
            currentPopup = null;
            return;
        }

        if(currentPopup != null && currentPopup != newPopup)
        {
            currentPopup.ClosePopup();
        }

        currentPopup = newPopup;
        newPopup.OpenPopup();
    }    

    public void ClosePopup(SlidePopup closePopup)
    {
        if (currentPopup == closePopup)
        {
            currentPopup = null;
        }
    }
}
