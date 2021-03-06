﻿public class OpenWorldStateMachine : BaseStateMachine
{
    public Unit Player;

    public static OpenWorldStateMachine Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ChangeTo<LoadOpenWorldState>();
    }
}
