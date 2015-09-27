using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer2
{
    public class MenuManager
    {
        private Menu _menu;
        private bool _isTransitioning;

        public MenuManager()
        {
            _menu = new Menu();
            _menu.OnMenuChange = _menu_OnMenuChange;
        }

        public void LoadContent(string menuPath)
        {
            if (!string.IsNullOrEmpty(menuPath))
                _menu.ID = menuPath;
        }

        public void UnloadContent()
        {
            _menu.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            if(!_isTransitioning)
                _menu.Update(gameTime);

            if(InputManager.Instance.KeyPressed(Keys.Enter) && !_isTransitioning)
            {
                if (_menu.Items[_menu.ItemNumber].LinkType == "Screen")
                    ScreenManager.Instance.ChangeScreens(_menu.Items[_menu.ItemNumber].LinkID);
                else
                {
                    _isTransitioning = true;
                    _menu.Transition(1.0f);
                    //foreach (MenuItem item in _menu.Items)
                    //{
                    //    item.Image.StoreEffects();
                    //    item.Image.ActivateEffect("FadeEffect");
                    //}
                }
            }

            Transition(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _menu.Draw(spriteBatch);
        }

        private void _menu_OnMenuChange()
        {
            XmlManager<Menu> xmlMenuManager = new XmlManager<Menu>();
            _menu.OnMenuChange = null;
            _menu.UnloadContent();
            _menu = xmlMenuManager.Load(_menu.ID);
            _menu.LoadContent();
            _menu.OnMenuChange = _menu_OnMenuChange;
            _menu.Transition(0.0f);

            //foreach (MenuItem item in _menu.Items)
            //{
            //    item.Image.StoreEffects();
            //    item.Image.ActivateEffect("FadeEffect");
            //}
        }

        private void Transition(GameTime gameTime)
        {
            if(_isTransitioning)
            {
                for(int i=0; i < _menu.Items.Count; i++)
                {
                    _menu.Items[i].Image.Update(gameTime);
                    float first = _menu.Items[0].Image.Alpha;
                    float last = _menu.Items[_menu.Items.Count - 1].Image.Alpha;
                    if (first == 0.0f && last == 0.0f)
                        _menu.ID = _menu.Items[_menu.ItemNumber].LinkID;
                    else if (first == 1.0f && last == 1.0f)
                    {
                        _isTransitioning = false;
                        //foreach (var item in _menu.Items)
                        //    item.Image.RestoreEffects();
                    }
                }
            }
        }
    }
}
