using System.Collections.Generic;
using UnityEngine;

public class ResourceFactory : MonoBehaviour
{
    #region Attribute
    [SerializeField]
    List<ResourceData> datas;
    bool[,] enables;
    GameObject resources;
    HexaGrid grid;

    int[][] oddDirX;
    int[][] evenDirX;
    int[][] DirY;

    int mapSize;
    int ratio = 75;
    #endregion

    #region Function
    private void SetEnable(HexaMapNode node, int phase)
    {
        Vector2Int pos = node.GetGridPos();

        if (pos.y % 2 == 0)
        {
            for(int i = 0; i < phase; i++)
            {
                for(int x= 0; x < evenDirX[i].Length; x++)
                {
                    for(int y= 0;y<DirY[i].Length; y++)
                    {
                        int posx = evenDirX[i][x] + pos.x;
                        int posy = DirY[i][y] + pos.y;
                        if (posx >= 0 && posy >= 0 && posx < mapSize && posy < mapSize)
                            enables[posx, posy] = false;
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < phase; i++)
            {
                for (int x = 0; x < oddDirX[i].Length; x++)
                {
                    for (int y = 0; y < DirY[i].Length; y++)
                    {
                        int posx = oddDirX[i][x] + pos.x;
                        int posy = DirY[i][y] + pos.y;
                        if (posx >= 0 && posy >= 0 && posx < mapSize && posy < mapSize)
                            enables[posx, posy] = false;
                    }
                }
            }
        }
    }
    private bool CheckEnable(HexaMapNode node)
    {
        Vector2Int pos = node.GetGridPos();
        if (enables[pos.x, pos.y])
            return true;
        else
            return false;
    }
    private bool CheckResource(HexaMapNode node, float distance,float cellSize)
    {
        if (node.GetTileType() != TileType.Wall)
            return false;
        if (CheckEnable(node))
            return true;
        return false;
    }
    private ResourceData ResourceSelect(HexaMapNode node, float distance, float cellSize)
    {
        if(distance < cellSize * 5)
            return datas[UnityEngine.Random.Range(0, 3)];
        else if (distance < cellSize * 10)
            return datas[UnityEngine.Random.Range(3, 6)];
        else 
            return datas[UnityEngine.Random.Range(6, 9)];    
    }
    private GameObject InstantiateResource(string name)
    {
        GameObject resource = Resources.Load<GameObject>($"Prefabs/Resource/{name}");
        if (resource == null)
        {
            Debug.Log($"Error : Prefabs/Resource/{name} is not exist");
            return null;
        }
        return Instantiate(resource, resources.transform);
    }
    private void SetResource(BaseResource resource, HexaMapNode node, ResourceData data)
    {
        resource.SetResourceData(data);
        resource.SetNode(node);
        node.SetBuildable(data.Buildable);
        node.SetResource(resource);
        Managers.Resource.resGroup.AddResource(resource);
    }
    public void SetResource(HexaMapNode node, ResourceNode2 resourceNode)
    {
        BaseResource resource = node.GetResource();
        resourceNode.SetResource(resource);
        resource.SetNode(resourceNode);
        resourceNode.SetBuildable(node.GetBuildable());
    }
    public void MakeResource(HexaMapNode node, float distance, float cellSize)
    {
        int phase;
        if (distance < cellSize * 5)
            phase = 2;
        else
            phase = 3;

        if (!CheckResource(node, distance, cellSize))
            return;

        int rand = UnityEngine.Random.Range(0, 100);
        if(rand < ratio)
        {
            return;
        }

        ResourceData info = ResourceSelect(node, distance, cellSize);

        GameObject obj = InstantiateResource(info.ResourceName);
        if(obj == null)
        {
            return;
        }
        SetResource(obj.GetComponent<BaseResource>(), node, info);
        obj.transform.position = node.GetWorldPos();
        SetEnable(node, phase);
    }
    public void MakeResource(HexaMapNode node, int idx)
    {
        ResourceData info = datas[idx];
        GameObject obj = InstantiateResource(info.ResourceName);
        if (obj == null)
            return;
        SetResource(obj.GetComponent<BaseResource>(), node, info);
        obj.transform.position = node.GetWorldPos();
    }
    #endregion
    public void OnAwake(int mapSize)
    {
        resources = GameObject.Find("Resources");
        grid = MapManager.Map.UnderGrid;
        this.mapSize = mapSize;

        enables = new bool[mapSize,mapSize];
        for(int i = 0; i < mapSize; i++)
        {
            for(int j =0 ; j <mapSize; j++)
            {
                enables[i,j] = true;
            }
        }
        oddDirX = grid.GetDirectionOddX();
        evenDirX = grid.GetDirectionEvenX();
        DirY = grid.GetDirectionY();
    }
}