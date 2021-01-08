using UnityEngine;

public class MapLoader : MonoBehaviour
{
    public Unit unitPrefab;

    public static MapLoader instance;
   
    void Awake()
    {
        instance = this;   
    }

    public void LoadUnits()
    {
        var holder = new GameObject("Units Holder");
        holder.transform.parent = Board.instance.transform;

        var unity = Instantiate(unitPrefab,
            Board.GetTile(new Vector3Int(-2, -6, 0)).worldPos,
            Quaternion.identity, 
            holder.transform);

    }
}
