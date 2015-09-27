using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Platformer2.LevelObjects
{
    public class BaseLevelObject : Model
    {
        public bool IsCollided;

        public BaseLevelObject()
        {
            IsCollided = true;
        }

        private BaseLevelObject(bool isCollided, Vector2 position, string texturePath, bool isVisible, Rectangle collisionRect) 
            : base(position, texturePath, isVisible, collisionRect)
        {
            IsCollided = isCollided;
        }

        public void CheckCollision(ref Entity e)
        {
            if (IsVisible && IsCollided)
            {
                Rectangle eRect = new Rectangle((int)e.Image.Position.X, (int)e.Image.Position.Y,
                    e.Image.SourceRect.Width, e.Image.SourceRect.Height);
                Rectangle loRect = new Rectangle((int)Position.X, (int)Position.Y,
                    CollisionRect.Width, CollisionRect.Height);

                if (eRect.Intersects(loRect))
                {
                    //e.BackToPrevPosition();

                    //if ((int)e.Image.Position.Y + e.Image.SourceRect.Height <= Image.Position.Y)
                    //{
                    e.Image.Position.Y = Position.Y - e.Image.SourceRect.Height + 2;
                        //e.Velocity.Y = 0;
                        e.ActivateGravity = false;
                        e.LevelObject = this;
                    //}
                } else if(e.LevelObject == this)
                {
                    e.ActivateGravity = true;
                }
            }
        }

        public override object Clone()
        {
            Vector2 position = new Vector2(Position.X, Position.Y);
            Rectangle collisionRect = new Rectangle(CollisionRect.X, CollisionRect.Y, CollisionRect.Width, CollisionRect.Height);
            BaseLevelObject clone = new BaseLevelObject(IsCollided, position, TexturePath, IsVisible, collisionRect);

            return clone;
        }
    }
}
