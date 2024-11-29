using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace myGame.UI
{
    public abstract class UIScreen
{
    protected List<UIButton> buttons;
    protected SpriteBatch spriteBatch;
    protected SpriteFont font;

    public UIScreen(SpriteBatch spriteBatch, SpriteFont font)
    {
        this.spriteBatch = spriteBatch;
        this.font = font;
        buttons = new List<UIButton>();
    }

    public virtual void Update(GameTime gameTime)
    {
        var mouseState = Mouse.GetState();
        foreach (var button in buttons)
        {
            button.Update(mouseState);
        }
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        foreach (var button in buttons)
        {
            button.Draw(spriteBatch);
        }
    }
} 

} 