using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Specialization", menuName = "Research/Specialization")]
public class Specialization : ScriptableObject
{
    [SerializeField] string specializationName;
    [SerializeField] string Description;
    [SerializeField] bool isAdopted;


    public string GetSpecializationName()
    {
        return specializationName;
    }

    public string GetDescription()
    {
        return Description;
    }

    public bool IsAdopted() 
    {
        return isAdopted;
    }

    public void SetAdopted(bool value)
    {
        isAdopted = value;
    }
}
