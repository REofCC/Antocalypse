using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildData", menuName = "Scriptable Object/Saver")]
public class SaveData : ScriptableObject
{
    [SerializeField]
    List<int> maxSave;
    [SerializeField]
    List<Resourcetype> saveResources;

    public List<int> MaxSave { get { return maxSave; } }
    public List<Resourcetype> SaveResources { get { return saveResources; } }
}
