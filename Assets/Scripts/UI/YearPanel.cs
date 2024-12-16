using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class YearPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI yearText;
    [SerializeField] Image winterProgressFieldBar;
    [SerializeField] RectTransform followImage;

    int currentYear = 1;

    private void Start()
    {
        UpdateYear();   // 권희준 - 초기 연도 설정
        Managers.YearManager.OnWinterEvent += UpdateYear;
        Managers.YearManager.OnTimeEvent += UpdateProgressBar;
    }

    private void OnDisable()
    {
        Managers.YearManager.OnWinterEvent -= UpdateYear;
        Managers.YearManager.OnTimeEvent -= UpdateProgressBar;
    }

    public void UpdateYear()
    {
        currentYear = Managers.YearManager.GetCurrentYear();
        yearText.text = $"{currentYear}";
    }

    void UpdateProgressBar()
    {
        winterProgressFieldBar.fillAmount = Managers.YearManager.GetFillAmount();
        UpdateFollowImagePosition();
    }

    void UpdateFollowImagePosition()
    {
        float fillAmount = winterProgressFieldBar.fillAmount;
        RectTransform barRectTransform = winterProgressFieldBar.rectTransform;

        float newX = barRectTransform.rect.width * fillAmount - barRectTransform.rect.width / 2;
        followImage.anchoredPosition = new Vector2(newX, followImage.anchoredPosition.y);
    }
}
