using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class CombatLog
{
    public static List<string> CombatLogs = new List<string>();

    public static void CheckActive()
    {
        foreach (var unit in StateMachineController.Instance.Units)
        {
            unit.Active = unit.GetStat(StatEnum.HP) <= 0 ? false : true;
        }
    }

    public static bool IsOver()
    {
        var activeAlliances = 0;

        foreach (var alliances in MapLoader.Instance.Alliances)
        {
            activeAlliances += CheckAnyActive(alliances);
        }

        return activeAlliances > 1 ? false : true;
    }

    public static void Append(string value)
        => CombatLogs.Add(DateTime.Now.ToString("MM/dd HH:mm") + ":" + value);

    public static void ShowLog()
    {
        foreach (var logItem in CombatLogs)
        {
            Debug.Log(logItem);
        }
    } 

    static int CheckAnyActive(Alliance alliance) => alliance.Units.Any(x => x.Active) ? 1 : 0;
}
