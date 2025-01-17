using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FOXTouch_WPF
{
    /// <summary>
    /// Logique d'interaction pour SplashScreenWindow.xaml
    /// </summary>
    public partial class SplashScreenWindow : Window
    {
        public SplashScreenWindow()
        {
            InitializeComponent();
            SetRandomBackgroundImage();
        }

        private void SetRandomBackgroundImage()
        {
            string directoryPath = @"C:\Users\YoanQUEDVILLE\Downloads\Splash screen FOXTouch";
            var imageFiles = Directory.GetFiles(directoryPath, "*.png");

            if (imageFiles.Length == 0)
            {
                MessageBox.Show("Aucune image trouvée dans le répertoire.");
                return;
            }

            Random random = new Random();
            int randomIndex = random.Next(imageFiles.Length);
            string randomImagePath = imageFiles[randomIndex];

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(randomImagePath, UriKind.Absolute);
            bitmap.EndInit();

            ImageBrush brush = new ImageBrush
            {
                Stretch = Stretch.UniformToFill,
                ImageSource = bitmap
            };
            this.Background = brush;
        }
    }
}
