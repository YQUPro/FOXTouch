using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOXTouch_WPF.GeometryClasses
{
    public class Geometry_3DGenericCollection
    {
        protected ObservableCollection<Geometry_3DPoint> pointsList = new ObservableCollection<Geometry_3DPoint>();

        public Geometry_3DGenericCollection(){}

        public Geometry_3DGenericCollection(List<Geometry_3DPoint> points)
        {
            pointsList = new ObservableCollection<Geometry_3DPoint>(points);
        }

        public List<Geometry_3DPoint> GetPointsList()
        {
            return pointsList.ToList();
        }

        public void AddPointToList(Geometry_3DPoint point)
        {
            pointsList.Add(point);
        }

        public void RemovePointFromList(Geometry_3DPoint point)
        {
            pointsList.Remove(point);
        }

        public void ClearPointsList()
        {
            pointsList.Clear();
        }

        public Geometry_3DPoint GetStartPoint()
        {

            return new Geometry_3DPoint(0, 0, 0);
        }

        public Geometry_3DPoint GetEndPoint()
        {

            return new Geometry_3DPoint(0, 0, 0);
        }

        public Geometry_3DPoint GetMiddlePoint()
        {

            return new Geometry_3DPoint(0, 0, 0);
        }

    }
}
