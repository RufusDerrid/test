using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer2
{
    class FloatRect
    {
        float _top, _bottom, _left, _right;

        public float Top
        {
            get { return _top; }
        }

        public float Bottom
        {
            get { return _bottom; }
        }

        public float Left
        {
            get { return _left; }
        }

        public float Right
        {
            get { return _right; }
        }

        public FloatRect(float x, float y, float width, float height)
        {
            _left = x;
            _right = x + width;
            _top = y;
            _bottom = y + height;
        }

        public bool Intersects(FloatRect f)
        {
            if (_right <= f.Left || _left >= f.Right || _top >= f.Bottom || _bottom <= f.Top)
                return false;

            return true;
        }
    }
}
