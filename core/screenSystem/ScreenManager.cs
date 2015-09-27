using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Platformer2
{
    public class ScreenManager
    {
        private static ScreenManager _instance;

        public Image Image;
        public Vector2 Dimensions;

        [XmlIgnore]
        public bool IsTransitioning { get; private set; }
        [XmlIgnore]
        public ContentManager Content { get; private set; }
        [XmlIgnore]
        public GraphicsDevice GraphicsDevice;
        [XmlIgnore]
        public SpriteBatch SpriteBatch;
        [XmlIgnore]
        public Random Random;

        private GameScreen _currentScreen, _newScreen;
        private XmlManager<GameScreen> _xmlGameScreenManager;

        public static ScreenManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    XmlManager<ScreenManager> xml = new XmlManager<ScreenManager>();
                    _instance = xml.Load("Content/Data/ScreenManager.xml");
                }

                return _instance;
            }
        }

        private ScreenManager() 
        {
            Dimensions = new Vector2(640, 480);
            _xmlGameScreenManager = new XmlManager<GameScreen>();
            _xmlGameScreenManager.Type = typeof(SplashScreen);
            _currentScreen = _xmlGameScreenManager.Load("Content/Data/SplashScreen.xml");
            Random = new Random();
        }

        public void ChangeScreens(string screenName)
        {
            _newScreen = (GameScreen)Activator.CreateInstance(Type.GetType("Platformer2." + screenName));
            Image.IsActive = true;
            Image.FadeEffect.Increase = true;
            Image.Alpha = 1.0f;
            IsTransitioning = true;
        }

        public void LoadContent(ContentManager content)
        {
            Content = new ContentManager(content.ServiceProvider, "Content");
            _currentScreen.LoadContent();
            Image.LoadContent();
        }

        public void UnloadContent()
        {
            _currentScreen.UnloadContent();
            Image.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            _currentScreen.Update(gameTime);
            Transition(gameTime);

            Camera.Instance.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _currentScreen.Draw(spriteBatch);
            if (IsTransitioning)
                Image.Draw(spriteBatch);
        }


        #region private methods

        private void Transition(GameTime gameTime)
        {
            if(IsTransitioning)
            {
                Image.Update(gameTime);
                if(Image.Alpha == 1.0f)
                {
                    _currentScreen.UnloadContent();
                    _currentScreen = _newScreen;
                    _xmlGameScreenManager.Type = _currentScreen.Type;
                    if (File.Exists(_currentScreen.XmlPath))
                        _currentScreen = _xmlGameScreenManager.Load(_currentScreen.XmlPath);
                    _currentScreen.LoadContent();
                } else if(Image.Alpha == 0.0f)
                {
                    Image.IsActive = false;
                    IsTransitioning = false;
                }
            }
        }

        #endregion
    }
}
