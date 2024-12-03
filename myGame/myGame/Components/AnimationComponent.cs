using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using myGame.Animation;
using System.Collections.Generic;
namespace myGame.Components{
public class AnimationComponent
{
    private Texture2D texture;
    private Animatie currentAnimation;
    private Dictionary<string, Animatie> animations;
    private bool isFacingRight;
    
    public bool IsFacingRight
    {
        get => isFacingRight;
        set => isFacingRight = value;
    }

    public AnimationComponent(Texture2D texture)
    {
        this.texture = texture;
        animations = new Dictionary<string, Animatie>();
        InitializeAnimations();
    }

    private void InitializeAnimations()
    {
        // Create idle animation
        var idleAnimation = new Animatie();
        idleAnimation.AddFrame(new AnimationFrame(new Rectangle(1, 1, 68, 56)));
        animations["idle"] = idleAnimation;

        // Create walking animation
        var walkAnimation = new Animatie();
        walkAnimation.AddFrame(new AnimationFrame(new Rectangle(1, 1, 68, 56)));
        walkAnimation.AddFrame(new AnimationFrame(new Rectangle(70, 1, 68, 56)));
        walkAnimation.AddFrame(new AnimationFrame(new Rectangle(1, 1, 68, 56)));
        walkAnimation.AddFrame(new AnimationFrame(new Rectangle(139, 1, 68, 56)));
        animations["walk"] = walkAnimation;

        currentAnimation = animations["idle"];
    }

    public void PlayAnimation(string animationName)
    {
        if (animations.ContainsKey(animationName) && currentAnimation != animations[animationName])
        {
            currentAnimation = animations[animationName];
        }
    }

    public void Update(GameTime gameTime, bool isMoving)
    {
        PlayAnimation(isMoving ? "walk" : "idle");
        if (isMoving)
        {
            currentAnimation.Update(gameTime);
        }
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
        SpriteEffects effect = isFacingRight ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        spriteBatch.Draw(
            texture, 
            position, 
            currentAnimation.CurrentFrame.SourceRectangle,
            Color.White,
            0,
            Vector2.Zero,
            1.0f,
            effect,
            0
        );
    }
} }