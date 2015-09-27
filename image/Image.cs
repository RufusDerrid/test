using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Platformer2
{
    public class Image
    {
        public float Alpha;
        public string Text, FontName, Path;
        public Vector2 Position, Scale;
        public Rectangle SourceRect;
        public Rectangle CollisionRect;
        public string Effects;
        public bool IsActive;
        public FadeEffect FadeEffect;
        public SpriteSheetEffect SpriteSheetEffect;
        public ScaleEffect ScaleEffect;
        public bool DrawText;
        public Color Color;

        [XmlIgnore]
        public Texture2D Texture;
        
        private Vector2 _origin;
        private ContentManager _content;
        private RenderTarget2D _renderTarget;
        private SpriteFont _font;
        private Dictionary<string, ImageEffect> _effects;

        public Image()
        {
            Path = Text = Effects = String.Empty;
            FontName = "Fonts/Font";
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Alpha = 1.0f;
            SourceRect = Rectangle.Empty;
            CollisionRect = Rectangle.Empty;
            _effects = new Dictionary<string, ImageEffect>();
            Color = Color.White;
        }

        public void LoadContent()
        {
            _content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");

            if (!string.IsNullOrEmpty(Path))
                Texture = _content.Load<Texture2D>(Path);

            _font = _content.Load<SpriteFont>(FontName);

            Vector2 dimensions = Vector2.Zero;

            if (Texture != null)
                dimensions.X += Texture.Width;
            dimensions.X += _font.MeasureString(Text).X;

            if (Texture != null)
                dimensions.Y = Math.Max(Texture.Height, _font.MeasureString(Text).Y);
            else
                dimensions.Y = _font.MeasureString(Text).Y;

            if (SourceRect == Rectangle.Empty)
                SourceRect = new Rectangle(0, 0, (int)dimensions.X, (int)dimensions.Y);

            if (CollisionRect == Rectangle.Empty)
                CollisionRect = new Rectangle(0, 0, (int)dimensions.X, (int)dimensions.Y);

            _renderTarget = new RenderTarget2D(ScreenManager.Instance.GraphicsDevice,
                (int)dimensions.X, (int)dimensions.Y);

            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(_renderTarget);
            ScreenManager.Instance.GraphicsDevice.Clear(Color.Transparent);
            ScreenManager.Instance.SpriteBatch.Begin();
            if(Texture != null)
                ScreenManager.Instance.SpriteBatch.Draw(Texture, Vector2.Zero, Color.White);
            ScreenManager.Instance.SpriteBatch.DrawString(_font, Text, Vector2.Zero, Color.White);
            ScreenManager.Instance.SpriteBatch.End();

            Texture = _renderTarget;

            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(null);

            SetEffect<FadeEffect>(ref FadeEffect);
            SetEffect<SpriteSheetEffect>(ref SpriteSheetEffect);
            SetEffect<ScaleEffect>(ref ScaleEffect);

            if(!string.IsNullOrEmpty(Effects))
            {
                string[] split = Effects.Split(':');
                foreach(string item in split)
                {
                    ActivateEffect(item);
                }
            }
        }

        public void UnloadContent()
        {
            _content.Unload();
            foreach (var effect in _effects)
                DeactivateEffect(effect.Key);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var effect in _effects)
            {
                if(effect.Value.IsActive)
                    effect.Value.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _origin = new Vector2(SourceRect.Width / 2, SourceRect.Height / 2);
            spriteBatch.Draw(Texture, Position + _origin, SourceRect, Color * Alpha,
                0.0f, _origin, Scale, SpriteEffects.None, 0.0f);

            if(!string.IsNullOrEmpty(Text) && DrawText)
                spriteBatch.DrawString(_font, Text, Position + _origin, Color.Black * Alpha, 0.0f, _origin, Scale, SpriteEffects.None, 0.0f);
        }

        public void ActivateEffect(string effect)
        {
            if (_effects.ContainsKey(effect))
            {
                _effects[effect].IsActive = true;
                var obj = this;
                _effects[effect].LoadContent(ref obj);
            }
        }

        public void DeactivateEffect(string effect)
        {
            if (_effects.ContainsKey(effect))
            {
                _effects[effect].IsActive = false;
                _effects[effect].UnloadContent();
            }
        }

        public void StoreEffects()
        {
            Effects = String.Empty;
            foreach(var effect in _effects)
            {
                if (effect.Value.IsActive)
                    Effects += effect.Key + ":";
            }

            if(Effects != String.Empty)
                Effects = Effects.Remove(Effects.Length - 1);
        }

        public void RestoreEffects()
        {
            foreach (var effect in _effects)
                DeactivateEffect(effect.Key);

            string[] split = Effects.Split(':');
            foreach(string e in split)
                ActivateEffect(e);
        }

        #region private methods

        private void SetEffect<T>(ref T effect) where T : ImageEffect
        {
            if (effect == null)
                effect = (T)Activator.CreateInstance(typeof(T));
            else
            {
                //effect.IsActive = true;
                var obj = this;
                effect.LoadContent(ref obj);
            }

            _effects.Add(effect.GetType().ToString().Replace("Platformer2.", ""), effect);
        }

        #endregion
    }
}