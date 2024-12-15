using UnityEngine;

[CreateAssetMenu(fileName = "Evo Data", menuName = "Scriptable Object/Evo Data")]
public class EvoData : ScriptableObject
{
    [SerializeField]
    AntType antType;
    [SerializeField]
    [TextArea]
    string BasicDescript;

    [SerializeField]
    BuffType headBuffType1;
    [SerializeField]
    float headBuffValue1;
    [SerializeField]
    [TextArea]
    string headDescript1;
    [SerializeField]
    BuffType headBuffType2;
    [SerializeField]
    float headBuffValue2;
    [SerializeField]
    [TextArea]
    string headDescript2;

    [SerializeField]
    BuffType chestBuffType1;
    [SerializeField]
    float chestBuffValue1;
    [SerializeField]
    [TextArea]
    string chestDescript1;
    [SerializeField]
    BuffType chestBuffType2;
    [SerializeField]
    float chestBuffValue2;
    [SerializeField]
    [TextArea]
    string chestDescript2;

    [SerializeField]
    BuffType bellyBuffType1;
    [SerializeField]
    float bellyBuffValue1;
    [SerializeField]
    [TextArea]
    string bellyDescript1;
    [SerializeField]
    BuffType bellyBuffType2;
    [SerializeField]
    float bellyBuffValue2;
    [SerializeField]
    [TextArea]
    string bellyDescript2;

    public AntType GetAntType()
    {
        return antType;
    }    
    public string GetBasicDescription(AntType antType)
    {
        return BasicDescript;
    }
    public string GetBuffDescription(BuffPart part, int branch)
    {
        switch (part)
        {
            case BuffPart.Head:
                if (branch ==1)
                    return headDescript1;
                else if(branch ==2) 
                    return headDescript2;
                break;
            case BuffPart.Chest:
                if (branch == 1)
                    return chestDescript1;
                else if (branch == 2)
                    return chestDescript2;
                break;
            case BuffPart.Belly:
                if (branch == 1)
                    return bellyDescript1;
                else if (branch == 2)
                    return bellyDescript2;
                break;
        }
        return null;
    }
    public BuffType GetBuffType(BuffPart part, int branch)
    {
        switch (part)
        {
            case BuffPart.Head:
                if (branch == 1)
                    return headBuffType1;
                else if (branch == 2)
                    return headBuffType2;
                break;
            case BuffPart.Chest:
                if (branch == 1)
                    return chestBuffType1;
                else if (branch == 2)
                    return chestBuffType2;
                break;
            case BuffPart.Belly:
                if (branch == 1)
                    return bellyBuffType1;
                else if (branch == 2)
                    return bellyBuffType2;
                break;
        }
        return BuffType.EndOfFloatBuff;
    }
    public float GetBuffValue(BuffPart part, int branch)
    {
        switch (part)
        {
            case BuffPart.Head:
                if (branch == 1)
                    return headBuffValue1;
                else if (branch == 2)
                    return headBuffValue2;
                break;
            case BuffPart.Chest:
                if (branch == 1)
                    return chestBuffValue1;
                else if (branch == 2)
                    return chestBuffValue2;
                break;
            case BuffPart.Belly:
                if (branch == 1)
                    return bellyBuffValue1;
                else if (branch == 2)
                    return bellyBuffValue2;
                break;
        }
        return -1;
    }

}
