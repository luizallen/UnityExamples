using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public List<Stat> StatsList;

    public Stat this[StatEnum s] => StatsList[(int)s];
}
