using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Platformer2.Physics
{
    public class PointMass
    {
        public Vector2 Position
        {
            get { return _position; }
        }

        private float _mass;
        private Vector2 _position;
        private Vector2 _linearVelocity;
        private Vector2 _linearAcceleration;
        private Vector2 _sumForces;

        public PointMass(float mass, Vector2 position)
        {
            _mass = mass;
            _position = position;
        }

        public void ApplyForce(Vector2 sumForce)
        {
            _sumForces = sumForce;
        }

        public void Update(GameTime gameTime)
        {
            Debug.Assert(_mass != 0);

            _linearAcceleration = _sumForces / _mass;
            _linearVelocity += _linearAcceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
            _position += _linearVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
