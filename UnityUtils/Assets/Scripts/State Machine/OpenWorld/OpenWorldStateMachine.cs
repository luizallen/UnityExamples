public class OpenWorldStateMachine : BaseStateMachine
{
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
