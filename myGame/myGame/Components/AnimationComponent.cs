using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using myGame.Animations;
using System.Collections.Generic;
namespace myGame.Components{
public class AnimationComponent
{
    private Texture2D texture;
    private Animatie currentAnimation;
    private Dictionary<string, Animatie> animations;
    private bool isFacingRight;
    private bool isJumping;
    private bool isLanding;
    
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
        var idleAnimation = new Animatie();
        idleAnimation.AddFrame(new AnimationFrame(new Rectangle(1, 1, 68, 56)));
        animations["idle"] = idleAnimation;

        var walkAnimation = new Animatie();
        walkAnimation.AddFrame(new AnimationFrame(new Rectangle(1, 1, 68, 56)));
        walkAnimation.AddFrame(new AnimationFrame(new Rectangle(70, 1, 68, 56)));
        walkAnimation.AddFrame(new AnimationFrame(new Rectangle(1, 1, 68, 56)));
        walkAnimation.AddFrame(new AnimationFrame(new Rectangle(139, 1, 68, 56)));
        animations["walk"] = walkAnimation;

        var jumpAnimation = new Animatie();
        jumpAnimation.AddFrame(new AnimationFrame(new Rectangle(70, 115, 68, 56)));

        animations["jump"] = jumpAnimation;

        var landAnimation = new Animatie();
        landAnimation.AddFrame(new AnimationFrame(new Rectangle(139, 58, 68, 56))); 
        landAnimation.AddFrame(new AnimationFrame(new Rectangle(1, 115, 68, 56))); 
        animations["land"] = landAnimation;

        currentAnimation = animations["idle"];
    }

    public void PlayAnimation(string animationName)
    {
        if (animations.ContainsKey(animationName) && currentAnimation != animations[animationName])
        {
            currentAnimation = animations[animationName];
        }
    }

    public void Update(GameTime gameTime, bool isMoving, bool isInAir, bool wasInAir)
    {
        // Handle jump/land animations
        if (isInAir)
        {
            PlayAnimation("jump");
        }
        else if (wasInAir) // Just landed
        {
            PlayAnimation("land");
            if (currentAnimation.IsAnimationComplete())
            {
                PlayAnimation(isMoving ? "walk" : "idle");
            }
        }
        else // Normal ground animations
        {
            PlayAnimation(isMoving ? "walk" : "idle");
        }

        currentAnimation.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color)
    {
        SpriteEffects effect = isFacingRight ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        spriteBatch.Draw(
            texture, 
            position, 
            currentAnimation.CurrentFrame.SourceRectangle,
            color,
            0,
            Vector2.Zero,
            1.0f,
            effect,
            0
        );
    }
} }