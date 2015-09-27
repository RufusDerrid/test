using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer2.LevelObjects
{
    public class AlternativeIcon
    {
        public bool IsActive;

        public Vector2 Position
        {
            get { return _position; }
        }

        private Texture2D _texture;
        private Vector2 _position;
        private Vector2 _origin;
        private Rectangle _sourceRect;
        private string _text;
        private SpriteFont _font;
        private ContentManager _content;

        public AlternativeIcon(Vector2 position, string text)
        {
            _position = position;
            _text = text;
        }

        public void LoadContent()
        {
            _content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
            _texture = _content.Load<Texture2D>("Gameplay/alterIcon");
            _sourceRect = new Rectangle(0, 0, _texture.Width, _texture.Height);
            _origin = new Vector2(_sourceRect.Width / 2, _sourceRect.Height / 2);
            _font = _content.Load<SpriteFont>("Fonts/Font");
        }

        public void UnloadContent()
        {
            _content.Unload();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            float scale = 1.0f;
            if(IsActive)
                scale = 1.2f;

            spriteBatch.Draw(_texture, _position, _sourceRect, Color.White, 0.0f, _origin, scale, SpriteEffects.None, 1.0f);
            if(!string.IsNullOrEmpty(_text))
                spriteBatch.DrawString(_font, _text, _position + _origin, Color.Black, 0.0f, _origin, scale, SpriteEffects.None, 0.0f);
        }
    }
}