using Platformer2.LevelObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer2
{
    public class Factory
    {
        private static Dictionary<string, Type> _factories;

        public static void Initialization()
        {
            _factories = new Dictionary<string, Type>();
            _factories.Add("base", typeof(BaseLevelObject));
            _factories.Add("interactive", typeof(InteractiveLevelObject));
        }

        public static BaseLevelObject Build(string type)
        {
            return (BaseLevelObject)Activator.CreateInstance(_factories[type]);
        }
    }
}
