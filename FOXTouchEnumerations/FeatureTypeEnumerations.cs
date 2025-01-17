using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOXTouchEnumerations.FeaturesType
{
    public static class FeatureTypeEnumerations
    {
        public enum EResultFeatureType : int
        {
            Undefined = 0,
            Type01,
            Type02,
            Type03
        }

        /// <summary>
        /// Eléments géométrique tirés de l'expérience MV. A ajuster en fonction du contenu de FOXTouch
        /// </summary>
        public enum EProgrammedObjectType : int
        {
            Undefined = 0,
            Point,
            Line,
            Circle,
            Plane,
            Cylinder,
            Surface, 
            Cloud,
            Spline,
            Oblong,
            ReferenceMark = 50, // Référentiel
            CameraContext = 90, // Permet de coller aux définitions de FOXTouch
            ConfocalContext // Permet de coller aux définitions de FOXTouch
        }
    }
}
