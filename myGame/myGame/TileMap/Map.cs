using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace myGame.TileMap
{
    public class Map
    {
        private List<CollisionTiles> tiles;
        private int[,] tileData;
        private int tileSize;

        public int Width { get; private set; }
        public int Height { get; private set; }
        public List<CollisionTiles> CollisionTiles => tiles;

        public Map()
        {
            tiles = new List<CollisionTiles>();
        }

        public void LoadMap(int[,] mapData, int tileSize)
        {
            this.tileData = mapData;
            this.tileSize = tileSize;

            Width = mapData.GetLength(1) * tileSize;
            Height = mapData.GetLength(0) * (tileSize / 2);

            GenerateTiles();
        }

        private void GenerateTiles()
        {
            tiles.Clear();

            for (int y = 0; y < tileData.GetLength(0); y++)
            {
                for (int x = 0; x < tileData.GetLength(1); x++)
                {
                    int tileType = tileData[y, x];
                    if (tileType > 0)
                    {
                        tiles.Add(new CollisionTiles(
                            tileType,
                            new Rectangle(
                                x * tileSize,
                                y * (tileSize / 2),
                                tileSize,
                                tileSize / 2
                            )
                        ));
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var tile in tiles)
            {
                tile.Draw(spriteBatch);
            }
        }
    }
}
