﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using SharpDX.Direct2D1;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace game_monogame1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;

        private Texture2D texture;
        Hero hero;
       
        //int frameWidth;
        //int frameHeight;
        //int frameIndex = 0;
        //double timeElapsed;
        //double timeToUpdate = 0.1; // Time (in seconds) between frames
        //private int numberOfFrames = 7; // Replace 4 with the actual number of frames in your sprite sheet

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            //_spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // Load your texture here (replace "yourSprite" with the actual file name)
            texture = Content.Load<Texture2D>("CharacterSheet");

            // Set frame dimensions (assuming a grid of frames)
            //frameWidth = spriteTexture.Width / numberOfFrames; // e.g., if you have 4 frames, divide by 4
            //frameHeight = spriteTexture.Height; // Assuming a single row of frames
        }

        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            // TODO: Add your update logic heretimeElapsed += gameTime.ElapsedGameTime.TotalSeconds;

          
            base.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            
            spriteBatch.End();


            //deelRectangle = new Rectangle(moveOn_x, 0, 190, 225);
            
            base.Draw(gameTime);

        }
    }
}
