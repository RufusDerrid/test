using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Platformer2
{
    public class Map
    {
        [XmlElement("Layer")]
        public List<Layer> Layer;
        public Vector2 TileDimensions;

        private Layer _currentLayer;
        private int _layerCounter;

        public Map()
        {
            Layer = new List<Layer>();
            TileDimensions = Vector2.Zero;
            _layerCounter = 0;
        }

        public void LoadContent()
        {
            foreach (Layer l in Layer)
                l.LoadContent(TileDimensions);

            if (Layer.Count > 0)
                _currentLayer = Layer[_layerCounter];
        }

        public void UnloadContent()
        {
            foreach (Layer l in Layer)
                l.UnloadContent();
        }

        public void Update(GameTime gameTime, ref Player player)
        {
            foreach (Layer l in Layer)
                l.Update(gameTime, ref player);

            if(InputManager.Instance.KeyPressed(Keys.RightShift))
            {
                _layerCounter++;
                if(_layerCounter >= Layer.Count)
                    _layerCounter = 0;
                _currentLayer = Layer[_layerCounter];
                player.ActivateGravity = true;
            }
        }

        public void UpdateCollision(ref Entity e)
        {
            _currentLayer.UpdateCollision(ref e);
        }

        public void Draw(SpriteBatch spriteBatch, string drawType, ref List<Tile> _accessedTiles, bool drawText = false)
        {
            _currentLayer.Draw(spriteBatch, drawType, ref _accessedTiles, drawText);
        }
    }
}
