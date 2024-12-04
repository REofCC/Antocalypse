using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Specialization", menuName = "Research/Specialization")]
public class Specialization : ScriptableObject
{
    [SerializeField] string antType;
    [SerializeField] string specializationName;
    [TextArea]
    [SerializeField] string description;
    [SerializeField] bool isAdopted;
    
    public string AntType => antType;
    public string SpecializationName => specializationName;
    public string Description => description;
    public bool IsAdopted => isAdopted;
   
    public void SetAdopted(bool value)
    {
        isAdopted = value;
    }
}
