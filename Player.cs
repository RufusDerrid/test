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
    public class Player : Entity
    {
        public Rectangle ForceField;

        //private Body _body;
        private Vector2 _origin;

        public Player()
        {
            ForceField = new Rectangle();
        }

        public override void LoadContent()
        {
            base.LoadContent();

            //Vector2 position = ConvertUnits.ToSimUnits(new Vector2(ScreenManager.Instance.Dimensions.X/2f,
            //    ScreenManager.Instance.Dimensions.Y/2f)) + new Vector2(0, -1.5f);

            ////_body = BodyFactory.CreateRectangle(ScreenManager.Instance.World,
            ////    ConvertUnits.ToSimUnits(Image.Texture.Width), ConvertUnits.ToSimUnits(Image.Texture.Height),
            ////    1f, position);

            //_body = BodyFactory.CreateCircle(ScreenManager.Instance.World, ConvertUnits.ToSimUnits(Image.Texture.Height), 1f, position);

            //_body.BodyType = BodyType.Dynamic;
            //_body.FixedRotation = false;
            //_body.Restitution = 0.3f;
            //_body.Friction = 1f;

            //_origin = new Vector2(Image.Texture.Width / 2f, Image.Texture.Height / 2f);
        }

        public override void Update(GameTime gameTime)
        {
            Image.IsActive = true;

            //if (InputManager.Instance.KeyDown(Keys.Left, Keys.A))
            //    _body.ApplyForce(new Vector2(-400, 0));

            //if (InputManager.Instance.KeyDown(Keys.Right, Keys.D))
            //    _body.ApplyForce(new Vector2(400, 0));

            //ActivateGravity = false;

            if (InputManager.Instance.KeyDown(Keys.Right, Keys.D) && Image.Position.X + Image.CollisionRect.Width < 1024)
            {
                Velocity.X = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Image.SpriteSheetEffect.CurrentFrame.Y = 2;
            }
            else if (InputManager.Instance.KeyDown(Keys.Left, Keys.A) && Image.Position.X > 0)
            {
                Velocity.X = -MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Image.SpriteSheetEffect.CurrentFrame.Y = 1;
            }
            else
            {
                Velocity.X = 0;
                Image.IsActive = false;
            }

            if (InputManager.Instance.KeyPressed(Keys.Space) && !ActivateGravity)
            {
                Velocity.Y = -JumpSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                ActivateGravity = true;
            }

            if (ActivateGravity)
                Velocity.Y += Gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else
                Velocity.Y = 0;

            PrevPosition = Image.Position;

            Image.Position += Velocity;

            //Image.Position = ConvertUnits.ToDisplayUnits(_body.Position);

            Image.CollisionRect.X = (int)Image.Position.X + 20;
            Image.CollisionRect.Y = (int)Image.Position.Y + 68;
            Image.CollisionRect.Width = 26;
            Image.CollisionRect.Height = 16;

            ForceField.X = (int)Image.Position.X - 64;
            ForceField.Y = (int)Image.Position.Y - 64;
            ForceField.Width = 192;
            ForceField.Height = 192;

            if (InputManager.Instance.KeyPressed(Keys.Enter) && InteractiveLevelObject != null)
                InteractiveLevelObject.Use();

            base.Update(gameTime);

            //Camera.Instance.SetFocalPoint(new Vector2(Image.Position.X, ScreenManager.Instance.Dimensions.Y / 2));
        }

        //public override void Draw(SpriteBatch spriteBatch)
        //{
        //    spriteBatch.Draw(Image.Texture, ConvertUnits.ToDisplayUnits(_body.Position), null, Color.White, 0f, _origin, 1f, SpriteEffects.None, 0f);
        //}
    }
}