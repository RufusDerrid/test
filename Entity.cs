using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platformer2.LevelObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer2
{
    public class Entity
    {
        //XML
        public float JumpSpeed;
        public float MoveSpeed;
        public float Gravity;
        public Image Image;

        public Vector2 Velocity;
        public Vector2 PrevPosition;

        public bool ActivateGravity;
        public bool OnLevelObject;

        public BaseLevelObject LevelObject;
        public InteractiveLevelObject InteractiveLevelObject;

        public Entity()
        {
            Velocity = Vector2.Zero;
            ActivateGravity = true;
        }

        public virtual void LoadContent()
        {
            Image.LoadContent();
        }

        public virtual void UnloadContent()
        {
            Image.UnloadContent();
        }

        public virtual void Update(GameTime gameTime)
        {
            Image.Update(gameTime);
        }

        public void BackToPrevPosition()
        {
            Image.Position = PrevPosition;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Image.Draw(spriteBatch);
        }
    }
}