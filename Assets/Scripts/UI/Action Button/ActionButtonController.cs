using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ActionButtonController : MonoBehaviour
{
    [SerializeField] List<Button> buttons = new List<Button>();

    private void Start()
    {
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => OnClickedButton(button));
        }
    }

    protected abstract void OnClickedButton(Button button);
    protected List<Button> GetButtons()
    {
        return buttons;
    }
    protected void InitButtons()
    {
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }
    }
}
