using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer2
{
    public class TitleScreen : GameScreen
    {
        private MenuManager _menuManager;

        public TitleScreen()
        {
            _menuManager = new MenuManager();
        }

        public override void LoadContent()
        {
            base.LoadContent();
            _menuManager.LoadContent("Content/Data/Menus/TitleMenu.xml");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            _menuManager.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _menuManager.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            _menuManager.Draw(spriteBatch);
        }
    }
}
