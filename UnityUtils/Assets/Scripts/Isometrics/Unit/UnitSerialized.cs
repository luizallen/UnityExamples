using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitSerialized 
{
    public string CharacterName;
    public PlayerType Playertype;
    public string Job;
    public Vector3Int Position;
    public int Faction;
    public int Level;
    public List<Item> Items;
}
