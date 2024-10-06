using UnityEngine;
using UnityEngine.Tilemaps;

public class Test : MonoBehaviour
{
    public Tilemap tilemap; // Inspector에서 할당
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
        if (Input.GetMouseButtonDown(0)) // 마우스 클릭 시
        {
            //Debug.Log($"Grid Offset : {calc.GetOffset()}");
            // 1. 마우스 스크린 좌표를 월드 좌표로 변환
            Vector3 mouseScreenPos = Input.mousePosition;
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, Camera.main.transform.position.z * -1));

            // 2. 월드 좌표를 Tilemap의 셀 좌표로 변환
            Vector3Int tilePos = tilemap.WorldToCell(mouseWorldPos);
            Vector2Int gridPos = calc.CalcGridPos(mouseWorldPos);
            // 3. 타일 좌표 출력
            Debug.Log("Tilemap 타일 좌표: " + tilePos);
            HexaMapNode node = grid.GetNode(gridPos.x, gridPos.y);
            Debug.Log($"GridPos :{node.GetGridPos()}");
            //Debug.Log($"WorldPos :{node.GetWorldPos()}");
            //Debug.Log($"CellPos :{node.GetCellPos()}");
            //Debug.Log(tilemap.GetTile(tilePos).name);
        }
    }
}