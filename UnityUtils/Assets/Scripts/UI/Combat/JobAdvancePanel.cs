using System.Collections.Generic;
using UnityEngine;

public class JobAdvancePanel : MonoBehaviour
{
    PanelPositioner _positioner;
    int _index;

    public List<JobPanelInfo> JobPanelInfos;
    public RectTransform Selector;

    private void Awake()
    {
        _positioner = GetComponent<PanelPositioner>();
    }

    public void Show()
    {
        _index = 0;
        ChangeSelected();
        UpdatePanelInfo();
        _positioner.MoveTo("Show");
    }

    public void Hide()
    {
        _index = 0;
        _positioner.MoveTo("Hide");
    }

    public void SelectNext()
    {
        _index++;
        if (_index >= JobPanelInfos.Count)
            _index = 0;

        ChangeSelected();
    }

    public void SelectPrevious()
    {
        _index--;
        if (_index < 0)
            _index = JobPanelInfos.Count - 1;

        ChangeSelected();
    }

    public void DuplicatePanel()
    {
        var newPos = JobPanelInfos[0].Panel.transform.position;
        newPos.x += 260 * JobPanelInfos.Count;

        var instantiated = Instantiate(JobPanelInfos[0].Panel, newPos, Quaternion.identity, transform);

        var newInfo = instantiated.GetComponent<JobPanelInfo>();
        JobPanelInfos.Add(newInfo);

    }

    public void JobChange() => Job.Employ(Turn.Unit,
            Turn.Unit.Job.AdvancesTo[_index],
            Turn.Unit.GetStat(StatEnum.LVL));

    void ChangeSelected()
    {
        Selector.position = JobPanelInfos[_index].Panel.transform.position;
    }

    void UpdatePanelInfo()
    {
        Clean();

        var count = 0;
        foreach (var job in Turn.Unit.Job.AdvancesTo)
        {
            var jobPanel = JobPanelInfos[count];

            jobPanel.JobName.text = job.name;
            jobPanel.Description.text = job.Description;
            jobPanel.Portrait.sprite = job.Portrait;

            if (Turn.Unit.Job.AdvancesTo.Count > JobPanelInfos.Count)
                DuplicatePanel();

            count++;
        }
    }

    void Clean()
    {
        for (int i = JobPanelInfos.Count - 1; i > 0; i--)
        {
            var info = JobPanelInfos[i];
            JobPanelInfos.Remove(info);
            Destroy(info.Panel);
        }
    }
}
