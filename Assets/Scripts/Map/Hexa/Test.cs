using UnityEngine;
using UnityEngine.Tilemaps;

public class Test : MonoBehaviour
{
    public Tilemap tilemap; // Inspector���� �Ҵ�
    public HexaGrid grid;
    public CellPositionCalc calc;
    private void Start()
    {
        tilemap = GameObject.Find("Grid").transform.GetChild(0).GetComponent<Tilemap>();
        grid = GameObject.Find("MapTool").GetComponent<HexaGrid>();
        calc = grid.GetCellPosCalc();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 Ŭ�� ��
        {
            //Debug.Log($"Grid Offset : {calc.GetOffset()}");
            // 1. ���콺 ��ũ�� ��ǥ�� ���� ��ǥ�� ��ȯ
            Vector3 mouseScreenPos = Input.mousePosition;
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, Camera.main.transform.position.z * -1));

            // 2. ���� ��ǥ�� Tilemap�� �� ��ǥ�� ��ȯ
            Vector3Int tilePos = tilemap.WorldToCell(mouseWorldPos);
            Vector2Int gridPos = calc.CalcGridPos(mouseWorldPos);
            // 3. Ÿ�� ��ǥ ���
            Debug.Log("Tilemap Ÿ�� ��ǥ: " + tilePos);
            HexaMapNode node = grid.GetNode(gridPos.x, gridPos.y);
            Debug.Log($"GridPos :{node.GetGridPos()}");
            //Debug.Log($"WorldPos :{node.GetWorldPos()}");
            //Debug.Log($"CellPos :{node.GetCellPos()}");
            //Debug.Log(tilemap.GetTile(tilePos).name);
        }
    }
}