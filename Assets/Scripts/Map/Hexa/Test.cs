using UnityEngine;
using UnityEngine.Tilemaps;

public class Test : MonoBehaviour
{
    public Tilemap tilemap; // Inspector���� �Ҵ�

    private void Start()
    {
        tilemap = GameObject.Find("Grid").transform.GetChild(0).GetComponent<Tilemap>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 Ŭ�� ��
        {
            // 1. ���콺 ��ũ�� ��ǥ�� ���� ��ǥ�� ��ȯ
            Vector3 mouseScreenPos = Input.mousePosition;
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, Camera.main.transform.position.z * -1));

            // 2. ���� ��ǥ�� Tilemap�� �� ��ǥ�� ��ȯ
            Vector3Int tilePos = tilemap.WorldToCell(mouseWorldPos);

            // 3. Ÿ�� ��ǥ ���
            Debug.Log("Tilemap Ÿ�� ��ǥ: " + tilePos);

            Debug.Log(tilemap.GetTile(tilePos).name);
        }
    }
}