using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer2
{
    public class Camera
    {
        public Matrix ViewMatrix;

        private static Camera _instance;

        private Vector2 _position;

        public static Camera Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Camera();
                return _instance;
            }
        }

        public void SetFocalPoint(Vector2 focalPosition)
        {
            _position = new Vector2(focalPosition.X - ScreenManager.Instance.Dimensions.X / 2,
                focalPosition.Y - ScreenManager.Instance.Dimensions.Y / 2);

            if (_position.X < 0)
                _position.X = 0;
            if (_position.Y < 0)
                _position.Y = 0;
        }

        public void Update()
        {
            ViewMatrix = Matrix.CreateTranslation(new Vector3(-_position, 0));
        }
    }
}
