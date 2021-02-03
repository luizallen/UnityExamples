using System;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public List<Stat> StatsList;

    public Stat this[StatEnum s]
    {
        get { return StatsList[(int)s]; }
    }

    void Awake()
    {
        StatsList = new List<Stat>();

        foreach (StatEnum item in Enum.GetValues(typeof(StatEnum)))
        {
            StatsList.Add(new Stat { Type = item });
        }
    }
}
