using System.Collections.Generic;
using UnityEngine;

public class AISkillBehavior : MonoBehaviour
{
    int _index;

    public List<SkillPicker> Pickers;

    void Awake()
    {
        Pickers = new List<SkillPicker>();
        Pickers.AddRange(
            GetComponent<Unit>()
            .Job
            .Behavior
            .GetComponents<SkillPicker>());
    }

    public void Pick(AIPlan plan)
    {
        Pickers[_index].Pick(plan);
        _index++;

        if (_index >= Pickers.Count)
            _index = 0;
    }
}
