using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Platformer2.LevelObjects
{
    public class Level : ICloneable
    {
        [XmlElement("LevelObjects")]
        public List<BaseLevelObject> LevelObjects;
        [XmlElement("InteractiveLevelObjects")]
        public List<InteractiveLevelObject> InteractiveLevelObjects;

        public Level() { }

        private Level(List<BaseLevelObject> levelObjects, List<InteractiveLevelObject> interactiveLevelObjects)
        {
            LevelObjects = levelObjects;
            InteractiveLevelObjects = interactiveLevelObjects;
        }

        public void Initialization()
        {
            InteractiveLevelObjects[0].AddLevelObject(LevelObjects[2]);
            InteractiveLevelObjects[0].AddLevelObject(LevelObjects[3]);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var levelObj in LevelObjects)
            {
                levelObj.Update(gameTime);
            }

            foreach (var levelObj in InteractiveLevelObjects)
            {
                levelObj.Update(gameTime);
            }
        }

        public void CheckCollision(ref Entity e)
        {
            foreach (var levelObj in LevelObjects)
            {
                levelObj.CheckCollision(ref e);
            }
        }

        public void SetAvailableInteractiveObject(ref Entity e)
        {
            foreach (var levelObj in InteractiveLevelObjects)
            {
                levelObj.CheckAvailable(ref e);
            }
        }

        public object Clone()
        {
            List<BaseLevelObject> lObjects = new List<BaseLevelObject>();
            foreach(BaseLevelObject lo in LevelObjects)
            {
                lObjects.Add(lo.Clone() as BaseLevelObject);
            }

            List<InteractiveLevelObject> ilObjets = new List<InteractiveLevelObject>();
            foreach(InteractiveLevelObject lo in InteractiveLevelObjects)
            {
                ilObjets.Add(lo.Clone() as InteractiveLevelObject);
            }

            ilObjets[0].AddLevelObject(lObjects[2]);
            ilObjets[0].AddLevelObject(lObjects[3]);

            Level clone = new Level(lObjects, ilObjets);

            return clone;
        }
    }
}
