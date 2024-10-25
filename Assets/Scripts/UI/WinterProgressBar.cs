using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinterProgressBar : MonoBehaviour
{
    [SerializeField] Image antIcon;
    [SerializeField] Image progressBar;

    public void UpdateProgress(float progress)
    {        
        progressBar.fillAmount = Mathf.Clamp01(progress);
        
        UpdateAntIconPosition();
    }

    private void UpdateAntIconPosition()
    {        
        RectTransform progressBarRect = progressBar.GetComponent<RectTransform>();
        
        float barWidth = progressBarRect.rect.width;
        float antIconXPosition = barWidth * progressBar.fillAmount;
        
        antIcon.rectTransform.anchoredPosition = new Vector2(antIconXPosition, antIcon.rectTransform.anchoredPosition.y);
    }
}
