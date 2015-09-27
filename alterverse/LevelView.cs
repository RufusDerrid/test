using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer2.LevelObjects
{
    public class LevelView
    {
        public List<View> _loViews;

        public LevelView(Level level)
        {
            _loViews = new List<View>();

            foreach(BaseLevelObject lo in level.LevelObjects)
            {
                View view = new View(lo);
                _loViews.Add(view);
            }

            foreach(InteractiveLevelObject lo in level.InteractiveLevelObjects)
            {
                View view = new InteractiveLevelObjectView(lo);
                _loViews.Add(view);
            }
        }

        public void ChangeAlternative(Level level)
        {
            int counter = 0;

            foreach (BaseLevelObject lo in level.LevelObjects)
            {
                _loViews[counter].ChangeModel(lo);
                counter++;
            }

            foreach (InteractiveLevelObject lo in level.InteractiveLevelObjects)
            {
                _loViews[counter].ChangeModel(lo);
                counter++;
            }
        }

        public void LoadContent()
        {
            foreach(View view in _loViews)
            {
                view.LoadContent();
            }
        }

        public void UnlladContent()
        {
            foreach (View view in _loViews)
            {
                view.UnloadContent();
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (View view in _loViews)
            {
                view.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (View view in _loViews)
            {
                view.Draw(spriteBatch);
            }
        }
    }
}
