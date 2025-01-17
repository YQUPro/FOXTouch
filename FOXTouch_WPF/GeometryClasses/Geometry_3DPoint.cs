using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOXTouch_WPF.GeometryClasses
{
    public class Geometry_3DPoint
    {
        private readonly float _x;
        private readonly float _y;
        private readonly float _z;

        public float X => _x;
        public float Y => _y;
        public float Z => _z;

        public Geometry_3DPoint(float x, float y, float z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        /// <summary>
        /// Get a copy from the current Geometry_3DPoint object.
        /// </summary>
        /// <returns></returns>
        public Geometry_3DPoint Get_3DPoint()
        {
            return new Geometry_3DPoint(_x, _y, _z);
        }
    }
}
