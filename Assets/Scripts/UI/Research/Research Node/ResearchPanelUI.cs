using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchPanelUI : MonoBehaviour, ISrollViewControl
{
    [Header("Top Button")]
    [SerializeField] Button queenAntPanelButton;
    [SerializeField] Button specializePanelButton;
    [SerializeField] Button exitButton;
    [SerializeField] Scrollbar researchScrollbar;
    [SerializeField] Scrollbar informationScrollbar;

    Coroutine currentCoroutine;
    float scrollSpeed = 10f;
    
    private void Start()
    {
        queenAntPanelButton.onClick.AddListener(() => TopButtonClick(true));
        specializePanelButton.onClick.AddListener(() => TopButtonClick(false));
        exitButton.onClick.AddListener(ExitButtonClick);
    }

    void TopButtonClick(bool _isQueenAnt)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(SlideScrollBar(_isQueenAnt));
    }

    void ExitButtonClick()
    {
        GetComponentInParent<SlidePopup>().ClosePopup();
    }

    public IEnumerator SlideScrollBar(bool _isQueenAnt)
    {
        while (true)
        {
            yield return null;

            if (_isQueenAnt)
            {
                researchScrollbar.value = Mathf.Lerp(researchScrollbar.value, 0f, Time.unscaledDeltaTime * scrollSpeed);
                informationScrollbar.value = Mathf.Lerp(informationScrollbar.value, 0f, Time.unscaledDeltaTime * scrollSpeed);

                if (Mathf.Approximately(researchScrollbar.value, 0f) && Mathf.Approximately(informationScrollbar.value, 0f))
                {
                    researchScrollbar.value = 0f;
                    informationScrollbar.value = 0f;
                    yield break;
                }
            }
            else
            {
                researchScrollbar.value = Mathf.Lerp(researchScrollbar.value, 1f, Time.unscaledDeltaTime * scrollSpeed);
                informationScrollbar.value = Mathf.Lerp(informationScrollbar.value, 1f, Time.unscaledDeltaTime * scrollSpeed);

                if (Mathf.Approximately(researchScrollbar.value, 1f) && Mathf.Approximately(informationScrollbar.value, 1f))
                {
                    researchScrollbar.value = 1f;
                    informationScrollbar.value = 1f;
                    yield break;
                }
            }
        }
    }
}
