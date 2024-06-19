using FirstTry.DataFolder;
using Microsoft.Win32;
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
using Image = FirstTry.DataFolder.Image;


namespace FirstTry.WindowFolder
{
    /// <summary>
    /// Логика взаимодействия для AddImage.xaml
    /// </summary>
    public partial class AddImage : Window
    {

        byte[] imageData;

        public AddImage()
        {
            InitializeComponent();
        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            SaveImageToDatabase(imageData);
        }

        private void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files|*.jpg;*.jpeg;*.png;*.bmp"
            };


            if (openFileDialog.ShowDialog() == true)
            {
                
                LoadedImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));

                imageData = File.ReadAllBytes(openFileDialog.FileName);
            }
        }


        private void SaveImageToDatabase(byte[] imageData)
        {
            Image image = DBEntities.GetContext().Image.Add(new Image 
            { 
                Image1 = imageData 
            });

            DBEntities.GetContext().SaveChanges();
        }
    }
}
