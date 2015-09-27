using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platformer2.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer2
{
    public class PhysicsTestScreen : GameScreen
    {
        private Texture2D _texture;
        private PointMass _pointMass;
        private Rectangle _sourceRect;

        public PhysicsTestScreen()
        {
            _pointMass = new PointMass(10, Vector2.Zero);
            _pointMass.ApplyForce(new Vector2(30, 0));
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _texture = _content.Load<Texture2D>("Gameplay/player");
            _sourceRect = new Rectangle(0, 0, _texture.Width / 4, _texture.Height / 4);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            _pointMass.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _pointMass.Position, _sourceRect, Color.White);
        }
    }
}
