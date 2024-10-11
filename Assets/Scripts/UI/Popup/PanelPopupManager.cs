using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelPopupManager : MonoBehaviour
{    
    SlidePopup currentPopup;   

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

    public void PopupClosed(SlidePopup closedPopup)
    {
        if (currentPopup == closedPopup)
        {
            currentPopup = null;
        }
    }
}
