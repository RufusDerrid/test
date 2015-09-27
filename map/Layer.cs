using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Platformer2
{
    public class Layer
    {
        public class TileMap
        {
            [XmlElement("Row")]
            public List<string> Row;

            public TileMap()
            {
                Row = new List<string>();
            }
        }

        [XmlElement("TileMap")]
        public TileMap Map;

        public Image Image;
        public string SolidTiles, OverlayTiles;

        private List<Tile> _underlayTiles, _overlayTiles;

        public List<Tile> UnderlayTilesList
        {
            get { return _underlayTiles; }
        }

        public List<Tile> OverlayTilesList
        {
            get { return _overlayTiles; }
        }

        private string _state;

        public Layer()
        {
            Image = new Image();
            _underlayTiles = new List<Tile>();
            _overlayTiles = new List<Tile>();
            SolidTiles = OverlayTiles = string.Empty;
        }

        public void UpdateCollision(ref Entity e)
        {
            for (int i = 0; i < _underlayTiles.Count; i++)
                _underlayTiles[i].UpdateCollision(ref e);

            for (int i = 0; i < _overlayTiles.Count; i++)
                _overlayTiles[i].UpdateCollision(ref e);
        }

        public void LoadContent(Vector2 tileDimensions)
        {
            Image.LoadContent();

            Vector2 position = -tileDimensions;

            foreach (string row in Map.Row)
            {
                string[] split = row.Split(']');
                position.X = -tileDimensions.X;
                position.Y += tileDimensions.Y;
                foreach (string s in split)
                {
                    if (s != String.Empty)
                    {
                        position.X += tileDimensions.X;
                        if (!s.Contains("---"))
                        {
                            _state = "Passive";

                            Tile tile = new Tile();

                            string str = s.Replace("[", String.Empty);
                            int value1 = int.Parse(str.Substring(0, str.IndexOf(':')));
                            int value2 = int.Parse(str.Substring(str.IndexOf(':') + 1));

                            if (SolidTiles.Contains(s))
                            {
                                _state = "Solid";
                            }

                            int chanse = 0;
                            if (s.Contains("1:"))
                                chanse = 10;

                            tile.LoadContent(position, new Rectangle(
                                value1 * (int)tileDimensions.X, value2 * (int)tileDimensions.Y,
                                (int)tileDimensions.X, (int)tileDimensions.Y), _state, OnDestroy, chanse);

                            if (OverlayTiles.Contains(s))
                                _overlayTiles.Add(tile);
                            else
                                _underlayTiles.Add(tile);
                        }
                    }
                }
            }
        }

        private void OnDestroy(Tile tile)
        {
            tile.UnloadContent();
            _underlayTiles.Remove(tile);
            _overlayTiles.Remove(tile);
        }

        public void UnloadContent()
        {
            Image.UnloadContent();
        }

        public void Update(GameTime gameTime, ref Player player)
        {
            for (int i=0; i < _underlayTiles.Count; i++)
                _underlayTiles[i].Update(gameTime, ref player);

            for (int i = 0; i < _overlayTiles.Count; i++)
                _overlayTiles[i].Update(gameTime, ref player);
        }

        public void Draw(SpriteBatch spriteBatch, string drawType, ref List<Tile> _accessedTiles, bool drawText = false)
        {
            List<Tile> tiles;
            if (drawType == "Underlay")
                tiles = _underlayTiles;
            else
                tiles = _overlayTiles;

            foreach(Tile tile in tiles)
            {
                if (_accessedTiles.Contains(tile))
                {
                    Image.DrawText = drawText;
                    Image.Text = tile.Chanse.ToString();
                }
                else
                {
                    Image.DrawText = false;
                    Image.Text = String.Empty;
                }

                if (tile.Selected)
                    Image.Color = Color.Red;
                else
                    Image.Color = Color.White;
                Image.Position = tile.Position;
                Image.SourceRect = tile.SourceRect;
                Image.Draw(spriteBatch);
            }
        }
    }
}
