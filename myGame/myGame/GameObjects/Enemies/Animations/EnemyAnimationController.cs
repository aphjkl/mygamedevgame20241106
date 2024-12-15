public class EnemyAnimationController
{
    private Dictionary<EnemyAnimationState, Animatie> animations;
    private EnemyAnimationState currentState;
    private Animatie currentAnimation;

    public EnemyAnimationController(EnemyAnimationData data)
    {
        animations = new Dictionary<EnemyAnimationState, Animatie>();
        InitializeAnimations(data);
        SetState(EnemyAnimationState.Idle);
    }

    private void InitializeAnimations(EnemyAnimationData data)
    {
        foreach (var kvp in data.Frames)
        {
            var animation = new Animatie();
            foreach (var frame in kvp.Value)
            {
                animation.AddFrame(new AnimationFrame(frame));
            }
            animations[kvp.Key] = animation;
        }
    }

    public void SetState(EnemyAnimationState state)
    {
        if (currentState != state && animations.ContainsKey(state))
        {
            currentState = state;
            currentAnimation = animations[state];
        }
    }

    public void Update(GameTime gameTime)
    {
        currentAnimation?.Update(gameTime);
    }

    public AnimationFrame CurrentFrame => currentAnimation?.CurrentFrame;
    public bool IsAnimationComplete() => currentAnimation?.IsAnimationComplete() ?? false;
} 