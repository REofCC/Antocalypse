using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayerController : MonoBehaviour
{
    [SerializeField] Button layerChangeButton;

    private void Start()
    {
        layerChangeButton.onClick.AddListener(OnLayerChangeButtonClick);
    }

    private void OnLayerChangeButtonClick()
    {
        MapManager.Map.State.ChangeMod();
    }
}
