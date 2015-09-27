using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer2
{
    public class ScaleEffect : ImageEffect
    {
        public float Speed;

        public override void LoadContent(ref Image image)
        {
            base.LoadContent(ref image);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (_image.Scale.X > 0)
            {
                _image.Scale.X -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                _image.Scale.Y -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}