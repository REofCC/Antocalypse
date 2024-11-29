using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CloseType
{
    IMMEDIATELY,
    AFTER_ANIMATION,
}
public class PanelPopupManager : MonoBehaviour
{
    SlidePopup currentPopup;
    [SerializeField] CloseType closeType = CloseType.IMMEDIATELY;

    public void TogglePopup(SlidePopup newPopup)
    {
        if (currentPopup == newPopup && newPopup.IsPopupOpen())
        {
            newPopup.ClosePopup();
            currentPopup = null;
            return;
        }

        if (currentPopup != null && currentPopup != newPopup)
        {
            if (closeType == CloseType.IMMEDIATELY)
            {
                currentPopup.ClosePopup();
                currentPopup = newPopup;
                newPopup.OpenPopup();
            }
            else if (closeType == CloseType.AFTER_ANIMATION)
            {
                StartCoroutine(CloseAndOpenPopup(currentPopup, newPopup));
            }
        }
        else
        {
            currentPopup = newPopup;
            newPopup.OpenPopup();
        }
    }

    private IEnumerator CloseAndOpenPopup(SlidePopup closePopup, SlidePopup openPopup)
    {
        closePopup.ClosePopup();        
        yield return new WaitUntil(() => !closePopup.IsPopupOpen());
        currentPopup = openPopup;
        openPopup.OpenPopup();
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
