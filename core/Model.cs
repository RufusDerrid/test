using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer2
{
    public class Model : ICloneable
    {
        public Vector2 Position;
        public string TexturePath;
        public bool IsVisible;
        public Rectangle CollisionRect;

        public Model()
        {
            Position = Vector2.Zero;
            TexturePath = string.Empty;
            IsVisible = true;
            CollisionRect = Rectangle.Empty;
        }

        protected Model(Vector2 position, string texturePath, bool isVisible, Rectangle collisionRect)
        {
            Position = position;
            TexturePath = texturePath;
            IsVisible = isVisible;
            CollisionRect = collisionRect;
        }

        public virtual void Update(GameTime gameTime)
        {
            CollisionRect.X = CollisionRect.X + (int)Position.X;
            CollisionRect.Y = CollisionRect.Y + (int)Position.Y;
        }

        public virtual object Clone()
        {
            Vector2 position = new Vector2(Position.X, Position.Y);
            Rectangle collisionRect = new Rectangle(CollisionRect.X, CollisionRect.Y, CollisionRect.Width, CollisionRect.Height);
            Model clone = new Model(position, TexturePath, IsVisible, collisionRect);

            return clone;
        }
    }
}
