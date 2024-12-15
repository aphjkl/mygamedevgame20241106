public class EnemyAnimationData
{
    public Dictionary<EnemyAnimationState, Rectangle[]> Frames { get; }

    public EnemyAnimationData()
    {
        Frames = new Dictionary<EnemyAnimationState, Rectangle[]>();
    }

    public void AddFrames(EnemyAnimationState state, Rectangle[] frames)
    {
        Frames[state] = frames;
    }
} 