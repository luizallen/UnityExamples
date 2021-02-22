using System.Collections.Generic;
using UnityEngine;

public delegate void MultiplicativeMods(object args);

public class Stats : MonoBehaviour
{
    public List<Stat> StatsList;
    public MultiplicativeMods MultiplicativeModifier;

    public Stat this[StatEnum s] => StatsList[(int)s];
}
