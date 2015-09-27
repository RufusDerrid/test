using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer2.LevelObjects
{
    public class InteractiveLevelObject : Model
    {
        public int StatesNumber;
        public bool IsCollided;
        public int CurrentState;

        private List<BaseLevelObject> _lo;

        public InteractiveLevelObject()
        {
            StatesNumber = 1;
            CurrentState = 0;
            _lo = new List<BaseLevelObject>();
        }

        private InteractiveLevelObject(int statesNumber, int currentState, bool isCollided, Vector2 position,
            string texturePath, bool isVisible, Rectangle collisionRect)
            : base(position, texturePath, isVisible, collisionRect)
        {
            StatesNumber = statesNumber;
            CurrentState = currentState;
            IsCollided = isCollided;

            _lo = new List<BaseLevelObject>();
        }

        public void AddLevelObject(BaseLevelObject lo)
        {
            if(!_lo.Contains(lo))
                _lo.Add(lo);
        }

        public void CheckAvailable(ref Entity e)
        {
            if (IsVisible)
            {
                Rectangle eRect = new Rectangle((int)e.Image.Position.X, (int)e.Image.Position.Y,
                    e.Image.SourceRect.Width, e.Image.SourceRect.Height);
                Rectangle loRect = new Rectangle((int)Position.X, (int)Position.Y,
                    CollisionRect.Width, CollisionRect.Height);

                if (eRect.Intersects(loRect))
                {
                    e.InteractiveLevelObject = this;
                } else if(e.InteractiveLevelObject == this)
                {
                    e.InteractiveLevelObject = null;
                }
            }
        }

        public void Use()
        {
            CurrentState++;
            if (CurrentState == StatesNumber)
                CurrentState = 0;

            foreach(var lo in _lo)
            {
                lo.IsVisible = !lo.IsVisible;
            }
        }

        public override object Clone()
        {
            Vector2 position = new Vector2(Position.X, Position.Y);
            Rectangle collisionRect = new Rectangle(CollisionRect.X, CollisionRect.Y, CollisionRect.Width, CollisionRect.Height);
            InteractiveLevelObject clone = new InteractiveLevelObject(StatesNumber, CurrentState, IsCollided, position, TexturePath, IsVisible, collisionRect);

            return clone;
        }
    }
}
