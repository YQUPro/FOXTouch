using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CalibrationMapComponent
{
    public class TwoAxesCalibrationMapping
    {
        // Matrice de correspondance pour les axes X et Y
        private Dictionary<(double, double), (double, double)> calibrationMap = new Dictionary<(double, double), (double, double)>();

        // Méthode pour ajouter des points d'étalonnage
        public void AddCalibrationPoint(double positionX, double positionY, double calibratedValueX, double calibratedValueY)
        {
            calibrationMap[(positionX, positionY)] = (calibratedValueX, calibratedValueY);
        }

        // Méthode pour obtenir une valeur interpolée ou extrapolée pour une position X/Y
        public (double, double) GetInterpolatedOrExtrapolatedValue(double positionX, double positionY)
        {
            var keys = calibrationMap.Keys.OrderBy(k => k.Item1).ThenBy(k => k.Item2).ToList();
            if (positionX < keys.First().Item1 || positionX > keys.Last().Item1 ||
                positionY < keys.First().Item2 || positionY > keys.Last().Item2)
            {
                // Extrapolation linéaire
                var (x0, y0) = keys.First();
                var (x1, y1) = keys.Last();
                var (z0X, z0Y) = calibrationMap[(x0, y0)];
                var (z1X, z1Y) = calibrationMap[(x1, y1)];

                double extrapolatedX = z0X + ((positionX - x0) * (z1X - z0X) / (x1 - x0)) + ((positionY - y0) * (z1X - z0X) / (y1 - y0));
                double extrapolatedY = z0Y + ((positionX - x0) * (z1Y - z0Y) / (x1 - x0)) + ((positionY - y0) * (z1Y - z0Y) / (y1 - y0));

                return (extrapolatedX, extrapolatedY);
            }
            else
            {
                // Interpolation bilinéaire
                var (x0, y0) = keys.First(k => k.Item1 <= positionX && k.Item2 <= positionY);
                var (x1, y1) = keys.Last(k => k.Item1 >= positionX && k.Item2 >= positionY);
                var (z00X, z00Y) = calibrationMap[(x0, y0)];
                var (z01X, z01Y) = calibrationMap[(x0, y1)];
                var (z10X, z10Y) = calibrationMap[(x1, y0)];
                var (z11X, z11Y) = calibrationMap[(x1, y1)];

                double z0X = z00X + (positionX - x0) * (z10X - z00X) / (x1 - x0);
                double z1X = z01X + (positionX - x0) * (z11X - z01X) / (x1 - x0);
                double interpolatedX = z0X + (positionY - y0) * (z1X - z0X) / (y1 - y0);

                double z0Y = z00Y + (positionX - x0) * (z10Y - z00Y) / (x1 - x0);
                double z1Y = z01Y + (positionX - x0) * (z11Y - z01Y) / (x1 - x0);
                double interpolatedY = z0Y + (positionY - y0) * (z1Y - z0Y) / (y1 - y0);

                return (interpolatedX, interpolatedY);
            }
        }

        // Méthode pour sérialiser la matrice de correspondance en JSON
        public void SerializeCalibrationMap(string filePath)
        {
            var json = JsonConvert.SerializeObject(calibrationMap, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        // Méthode pour désérialiser la matrice de correspondance à partir d'un fichier JSON
        public void DeserializeCalibrationMap(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    var json = File.ReadAllText(filePath);
                    calibrationMap = JsonConvert.DeserializeObject<Dictionary<(double, double), (double, double)>>(json);
                }
                catch (IOException ex)
                {
                    throw new IOException("Erreur lors de l'accès au fichier. Il est possible qu'un autre programme utilise ce fichier.", ex);
                }
                catch (UnauthorizedAccessException ex)
                {
                    throw new UnauthorizedAccessException("Accès non autorisé au fichier.", ex);
                }
            }
            else
            {
                throw new FileNotFoundException("Le fichier spécifié n'existe pas.");
            }
        }
    }
}
