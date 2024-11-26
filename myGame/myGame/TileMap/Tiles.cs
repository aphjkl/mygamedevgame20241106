﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace myGame.TileMap
{
    public class Tiles
    {
        protected Texture2D texture;
        private Rectangle rectangle;
        private static ContentManager content;

        
        public static ContentManager Content 
        {
            protected get { return content; }
            set { content = value; } 
        }

        public Rectangle Rectangle
        {
             get { return rectangle; }
            protected set { rectangle = value; } 
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rectangle, Color.White);
        }


    }
    class CollisionTiles : Tiles
    {
        public CollisionTiles(int i, Rectangle newRectangle)
        {
            texture = Content.Load<Texture2D>("Tile" + i);
            this.Rectangle = newRectangle;
        }

    }
}