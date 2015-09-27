using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer2
{
    public class FadeEffect : ImageEffect
    {
        public float FadeSpeed;
        public bool Increase;

        public FadeEffect()
        {
            FadeSpeed = 1;
            Increase = false;
        }

        public override void LoadContent(ref Image image)
        {
            base.LoadContent(ref image);
            Increase = false;
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (_image.IsActive)
            {
                if (!Increase)
                    _image.Alpha -= FadeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                else
                    _image.Alpha += FadeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_image.Alpha < 0.0f)
                {
                    Increase = true;
                    _image.Alpha = 0.0f;
                }
                else if (_image.Alpha > 1.0f)
                {
                    Increase = false;
                    _image.Alpha = 1.0f;
                }
            }
            else
                _image.Alpha = 1.0f;
        }
    }
}