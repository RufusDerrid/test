using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer2
{
    public class InputManager
    {
        private KeyboardState _currentKeyState, _prevKeyState;

        private static InputManager _instance;
        public static InputManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new InputManager();

                return _instance;
            }
        }

        private InputManager()
        {

        }

        public void Update()
        {
            _prevKeyState = _currentKeyState;
            if (!ScreenManager.Instance.IsTransitioning)
                _currentKeyState = Keyboard.GetState();
        }

        public bool KeyPressed(params Keys[] keys)
        {
            foreach(var key in keys)
            {
                if (_currentKeyState.IsKeyDown(key) && _prevKeyState.IsKeyUp(key))
                    return true;
            }

            return false;
        }

        public bool KeyReleased(params Keys[] keys)
        {
            foreach (var key in keys)
            {
                if (_currentKeyState.IsKeyUp(key) && _prevKeyState.IsKeyDown(key))
                    return true;
            }

            return false;
        }

        public bool KeyDown(params Keys[] keys)
        {
            foreach (var key in keys)
            {
                if (_currentKeyState.IsKeyDown(key))
                    return true;
            }

            return false;
        }
    }
}
