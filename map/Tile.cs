using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer2
{
    public class Tile
    {
        public bool Selected;

        private Vector2 _position;
        private Rectangle _sourceRect;
        private string _state;
        private double _time;
        private int _chanse;
        private Action<Tile> _onDestroy;

        public int Chanse
        {
            get { return _chanse; }
            set { _chanse = value; }
        }

        public Vector2 Position
        {
            get { return _position; }
        }

        public Rectangle SourceRect
        {
            get { return _sourceRect; }
        }

        public Tile()
        {
            _chanse = 1;
        }

        public void LoadContent(Vector2 position, Rectangle sourceRect, string state, Action<Tile> onDestroy, int chanse)
        {
            _position = position;
            _sourceRect = sourceRect;
            _state = state;

            _onDestroy = onDestroy;
            _chanse = chanse;
        }

        public void UnloadContent()
        { }

        public void UpdateCollision(ref Entity e)
        {
            Rectangle rect = new Rectangle((int)_position.X, (int)_position.Y, _sourceRect.Width, _sourceRect.Height);

            if (e.Image.CollisionRect.Intersects(rect))
            {
                e.ActivateGravity = false;
            }
        }

        public void Update(GameTime gameTime, ref Player player)
        {
            //_time += gameTime.ElapsedGameTime.TotalSeconds;
            //if(_time >= 5)
            //{
            //    _time = 0;

            //    int r = ScreenManager.Instance.Random.Next(1, 100);
            //    if(r >= 1 && r <= _chanse)
            //    {
            //        if (_onDestroy != null)
            //            _onDestroy(this);
            //    }
            //}

            //Rectangle tileRect = new Rectangle((int)Position.X, (int)Position.Y,
            //        _sourceRect.Width, _sourceRect.Height);
            ////Rectangle playerRect = new Rectangle((int)player.Image.Position.X, (int)player.Image.Position.Y,
            ////    player.Image.CollisionRect.Width, player.Image.CollisionRect.Height);

            //Rectangle playerRect = player.Image.CollisionRect;

            //if (playerRect.Intersects(tileRect))
            //{
            //    if(_state == "Solid")
            //    {
                
            //        if (player.Velocity.X < 0)
            //            player.Image.Position.X = tileRect.Right;
            //        else if (player.Velocity.X > 0)
            //            player.Image.Position.X = tileRect.Left - player.Image.CollisionRect.Width;

            //        if (player.Velocity.Y < 0)
            //            player.Image.Position.Y = tileRect.Bottom;
            //        else if (player.Velocity.Y > 0)
            //            player.Image.Position.Y = tileRect.Top - player.Image.CollisionRect.Height;

            //        player.Velocity = Vector2.Zero;
            //    }

            //    _chanse *= 20;

            //    int r = ScreenManager.Instance.Random.Next(1, 100);
            //    if (r >= 1 && r <= _chanse)
            //    {
            //        if (_onDestroy != null)
            //            _onDestroy(this);
            //    }
            //}
        }
    }
}
