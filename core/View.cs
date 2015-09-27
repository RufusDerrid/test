using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer2
{
    public class View
    {
        private Texture2D _texture;
        private Vector2 _origin;
        private ContentManager _content;

        protected Model _model;
        protected Rectangle _sourceRect;

        public View(Model model)
        {
            _model = model;
        }

        public virtual void ChangeModel(Model model)
        {
            _model = model;
        }

        public virtual void LoadContent()
        {
            _content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
            _texture = _content.Load<Texture2D>(_model.TexturePath);
            _origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
            _sourceRect = new Rectangle(0, 0, _texture.Width, _texture.Height);
        }

        public void UnloadContent()
        {
            _content.Unload();
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(_model.IsVisible)
                spriteBatch.Draw(_texture, _model.Position + _origin, _sourceRect, Color.White,
                    0.0f, _origin, 1.0f, SpriteEffects.None, 0.0f);
        }
    }
}
