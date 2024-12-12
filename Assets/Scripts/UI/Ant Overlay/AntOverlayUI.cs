using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AntOverlayUI : MonoBehaviour
{
    [SerializeField] RectTransform overlayPanel;

    public abstract void OnClickAnt();
    public abstract void CheckAntType();
    public abstract void ShowAntOverlay();
}
