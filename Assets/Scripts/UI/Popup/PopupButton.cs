using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupButton : MonoBehaviour
{
    [SerializeField] SlidePopup slidePopup;

    private void Start()
    {
        transform.GetComponent<Button>().onClick.AddListener(onButtonClick);
    }

    public void onButtonClick()
    {
        slidePopup.TogglePopup();
    }
}
