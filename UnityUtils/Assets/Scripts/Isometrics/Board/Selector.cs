using UnityEngine;

public class Selector : MonoBehaviour
{
    public static Selector Instance;
    public Vector3Int Position { get { return Tile.Pos;  } }
    public TileLogic Tile;
    [HideInInspector]
    public SpriteRenderer SpriteRenderer;

    void Awake() 
    {
        Instance = this;
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
}
