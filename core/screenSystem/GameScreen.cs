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
    public class GameScreen
    {
        [XmlIgnore]
        public Type Type;
        public string XmlPath;

        protected ContentManager _content;

        public GameScreen()
        {
            Type = GetType();
            XmlPath = "Load/" + Type.ToString().Replace("Platformer2.", "") + ".xml";
        }

        public virtual void LoadContent()
        {
            _content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
        }

        public virtual void UnloadContent()
        {
            _content.Unload();
        }

        public virtual void Update(GameTime gameTime)
        {
            InputManager.Instance.Update();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
