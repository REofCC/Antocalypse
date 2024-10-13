using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class Test : MonoBehaviour
{
    public Tilemap tilemap; // Inspector에서 할당
    public HexaGrid grid;
    public HexaMapNode current;
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
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                //Debug.Log($"Grid Offset : {calc.GetOffset()}");
                // 1. 마우스 스크린 좌표를 월드 좌표로 변환
                Vector3 mouseScreenPos = Input.mousePosition;
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, Camera.main.transform.position.z * -1));

                // 2. 월드 좌표를 Tilemap의 셀 좌표로 변환
                Vector3Int tilePos = tilemap.WorldToCell(mouseWorldPos);
                Vector2Int gridPos = calc.CalcGridPos(mouseWorldPos);
                // 3. 타일 좌표 출력
                HexaMapNode node = grid.GetNode(gridPos.x, gridPos.y);
                current = node;
                //Debug.Log($"WorldPos :{node.GetWorldPos()}");
                //Debug.Log($"CellPos :{node.GetCellPos()}");
                //Debug.Log(tilemap.GetTile(tilePos).name);
            }
        }

    }
}