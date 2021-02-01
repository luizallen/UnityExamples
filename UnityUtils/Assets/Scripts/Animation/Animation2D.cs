using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Animation2D
{
    public string Name;
    public float FrameRate;
    public float DelayStart;
    public List<Sprite> Frames;
}
