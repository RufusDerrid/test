using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Platformer2.LevelObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer2.LevelObjects
{
    public class Alterverse
    {
        public Level CurrentLevel
        {
            get
            {
                return _alternatives[_currentAlternative];
            }
        }

        public int CurrentAlternative
        {
            get { return _currentAlternative; }
        }

        private List<Level> _alternatives;
        private int _currentAlternative;
        private Action<Level> _onAlternativeChange;
        private int _maxAlters = 2;

        public Alterverse(Action<Level> onAlternativeChange)
        {
            _alternatives = new List<Level>();
            _currentAlternative = 0;
            _onAlternativeChange = onAlternativeChange;
        }

        public void LoadContent()
        {
            XmlManager<Level> levelLoader = new XmlManager<Level>();
            Level level = levelLoader.Load("Content/Data/Gameplay/Level.xml");
            level.Initialization();

            _alternatives.Add(level);
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime, ref Entity e)
        {
            if (InputManager.Instance.KeyPressed(Keys.LeftShift))
                CreateAlternative();

            if (InputManager.Instance.KeyPressed(Keys.RightShift))
                ChangeAlternative();

            foreach(Level level in _alternatives)
                level.Update(gameTime);
            
            _alternatives[_currentAlternative].CheckCollision(ref e);
            _alternatives[_currentAlternative].SetAvailableInteractiveObject(ref e);
        }

        private void CreateAlternative()
        {
            if (_alternatives.Count < _maxAlters)
            {
                Level newLevel = _alternatives[_currentAlternative].Clone() as Level;
                _currentAlternative++;
                _alternatives.Add(newLevel);
                if (_onAlternativeChange != null)
                    _onAlternativeChange(newLevel);
            }
        }

        private void ChangeAlternative()
        {
            _currentAlternative++;
            if(_currentAlternative == _alternatives.Count)
            {
                _currentAlternative = 0;
            }

            if (_onAlternativeChange != null)
                _onAlternativeChange(_alternatives[_currentAlternative]);
        }
    }
}