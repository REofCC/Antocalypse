using UnityEngine;
using UnityEngine.Tilemaps;

public class Test : MonoBehaviour
{
    public Tilemap tilemap; // Inspector에서 할당

    private void Start()
    {
        tilemap = GameObject.Find("Grid").transform.GetChild(0).GetComponent<Tilemap>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 클릭 시
        {
            // 1. 마우스 스크린 좌표를 월드 좌표로 변환
            Vector3 mouseScreenPos = Input.mousePosition;
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, Camera.main.transform.position.z * -1));

            // 2. 월드 좌표를 Tilemap의 셀 좌표로 변환
            Vector3Int tilePos = tilemap.WorldToCell(mouseWorldPos);

            // 3. 타일 좌표 출력
            Debug.Log("Tilemap 타일 좌표: " + tilePos);

            Debug.Log(tilemap.GetTile(tilePos).name);
        }
    }
}