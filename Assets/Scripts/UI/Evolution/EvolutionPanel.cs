using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvolutionPanel : MonoBehaviour, ISrollViewControl
{
    [Header("Top Button")]
    [SerializeField] Button queenAntPanelButton;
    [SerializeField] Button specializePanelButton;
    [SerializeField] Button exitButton;
    [SerializeField] Scrollbar scrollbar;
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
        GetComponent<SlidePopup>().ClosePopup();
    }

    public IEnumerator SlideScrollBar(bool _isQueenAnt)
    {
        while (true)
        {
            yield return null;

            if (_isQueenAnt)
            {
                scrollbar.value = Mathf.Lerp(scrollbar.value, 0f, Time.unscaledDeltaTime * scrollSpeed);

                if (Mathf.Approximately(scrollbar.value, 0f))
                {
                    scrollbar.value = 0f;
                    yield break;
                }
            }
            else
            {
                scrollbar.value = Mathf.Lerp(scrollbar.value, 1f, Time.unscaledDeltaTime * 10f);

                if (Mathf.Approximately(scrollbar.value, 1f))
                {
                    scrollbar.value = 1f;
                    yield break;
                }
            }
        }
    }
}
