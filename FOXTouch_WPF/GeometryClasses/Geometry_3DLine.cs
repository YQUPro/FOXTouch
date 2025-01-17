using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOXTouch_WPF.GeometryClasses
{
    public class Geometry_3DLine : Geometry_3DGenericCollection
    {
        public Geometry_3DLine() : base() { }

        public Geometry_3DLine(List<Geometry_3DPoint> points) : base(points) { }

        public Geometry_3DPoint StartPoint
        {
            get { return pointsList.FirstOrDefault(); }
        }

        public Geometry_3DPoint EndPoint
        {
            get { return pointsList.LastOrDefault(); }
        }
    }
}
