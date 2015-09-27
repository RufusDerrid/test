using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer2.LevelObjects
{
    public class InteractiveLevelObjectView : View
    {
        public InteractiveLevelObjectView(InteractiveLevelObject iObj)
            : base(iObj)
        {

        }

        public void ChangeModel(InteractiveLevelObject iObj)
        {
            base.ChangeModel(iObj);
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _sourceRect.Width = _sourceRect.Width / (_model as InteractiveLevelObject).StatesNumber;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            InteractiveLevelObject model = _model as InteractiveLevelObject;
            _sourceRect.X = model.CurrentState * model.CollisionRect.Width;
        }
    }
}
