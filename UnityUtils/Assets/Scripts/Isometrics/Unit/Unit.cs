using UnityEngine;

public class Unit : MonoBehaviour
{
    [HideInInspector]
    public Stats stats;
   
    void Awake()
    {
        stats = GetComponentInChildren<Stats>();
    }
}
