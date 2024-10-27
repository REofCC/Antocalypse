using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffDebuffTooltip : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] List<Image> buffImage = new List<Image>();
    [SerializeField] List<Image> debuffImage = new List<Image>();

    public void Setup()
    {

    }
}
