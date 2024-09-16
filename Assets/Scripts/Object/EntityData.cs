using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "Scriptable Object/EntityData", order = int.MaxValue)]
public class EntityData : ScriptableObject
{

    [SerializeField]
    string _entityType;
    [SerializeField]
    int _maxHp;
    [SerializeField]
    int _hp;
    [SerializeField]
    float _speed;
    [SerializeField]
    int _kcal;
    [SerializeField]
    int _gatherValue; //�ڿ� ��ݷ�

    bool _isHolding;  //�ڿ� �����
    int _holdValue;  //�ڿ� ������

    public string entityType { get { return _entityType; } set { _entityType = value; } }
    public int maxHp { get { return _maxHp; } set { _maxHp = value; } }
    public int hp { get { return _hp; } set { _hp = value; } }
    public float speed { get { return _speed; } set { _speed = value; } }
    public int kcal { get { return _kcal; } set { _kcal = value; } }

    public int gatherValue { get { return _gatherValue; } set { _gatherValue = value; } }

    public bool isHolding { get { return _isHolding; } set { _isHolding = value; } }
    public int holdValue { get { return _holdValue; } set { _holdValue = value; } }
}
