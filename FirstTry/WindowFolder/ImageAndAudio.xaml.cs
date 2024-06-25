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
    /// Логика взаимодействия для ImageAndAudio.xaml
    /// </summary>
    public partial class ImageAndAudio : Window
    {
        private string audioPath;
        private string photoPath;

        public ImageAndAudio()
        {
            InitializeComponent();
        }

        private void SelectAudio_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Audio files (*.mp3;*.wav)|*.mp3;*.wav"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                audioPath = openFileDialog.FileName;
                AudioFileName.Text = System.IO.Path.GetFileName(audioPath);
                AudioPlayer.Source = new Uri(audioPath);
            }
        }

        private void SelectPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                photoPath = openFileDialog.FileName;
                PhotoFileName.Text = System.IO.Path.GetFileName(photoPath);
                PhotoViewer.Source = new BitmapImage(new Uri(photoPath));
            }
        }

        private void AddAudioToDB_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(audioPath))
            {
                try
                {
                    byte[] audioData = File.ReadAllBytes(audioPath);

                    var context = DBEntities.GetContext();
                    var newImage = new Image
                    {
                        Audio = audioData
                    };

                    context.Image.Add(newImage);
                    context.SaveChanges();

                    MessageBox.Show("Аудио успешно добавлено в БД");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при добавлении аудио в БД: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Сначала выберите аудио файл");
            }
        }

        private void AddPhotoToDB_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(photoPath))
            {
                try
                {
                    byte[] imageData = File.ReadAllBytes(photoPath);

                    var context = DBEntities.GetContext();
                    var newImage = new Image
                    {
                        Image1 = imageData
                    };

                    context.Image.Add(newImage);
                    context.SaveChanges();

                    MessageBox.Show("Фото успешно добавлено в БД");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при добавлении фото в БД: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Сначала выберите фото файл");
            }
        }

        private void LoadAudioFromDB_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var context = DBEntities.GetContext();
                var lastAudio = context.Image.Where(i => i.Audio != null).OrderByDescending(i => i.IdImage).FirstOrDefault();

                if (lastAudio != null && lastAudio.Audio != null)
                {
                    string tempFilePath = System.IO.Path.GetTempFileName() + ".mp3";
                    File.WriteAllBytes(tempFilePath, lastAudio.Audio);

                    AudioPlayer.Source = new Uri(tempFilePath);
                    AudioFileName.Text = "Аудио загружено из БД";
                    MessageBox.Show("Аудио успешно загружено из БД");
                }
                else
                {
                    MessageBox.Show("В базе данных нет аудио файлов");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке аудио из БД: {ex.Message}");
            }
        }

        private void LoadPhotoFromDB_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var context = DBEntities.GetContext();
                var lastPhoto = context.Image.Where(i => i.Image1 != null).OrderByDescending(i => i.IdImage).FirstOrDefault();

                if (lastPhoto != null && lastPhoto.Image1 != null)
                {
                    BitmapImage bitmap = new BitmapImage();
                    using (MemoryStream stream = new MemoryStream(lastPhoto.Image1))
                    {
                        bitmap.BeginInit();
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.StreamSource = stream;
                        bitmap.EndInit();
                    }

                    PhotoViewer.Source = bitmap;
                    PhotoFileName.Text = "Фото загружено из БД";
                    MessageBox.Show("Фото успешно загружено из БД");
                }
                else
                {
                    MessageBox.Show("В базе данных нет фото");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке фото из БД: {ex.Message}");
            }
        }
    

        private void PlayAudio_Click(object sender, RoutedEventArgs e)
        {
            AudioPlayer.Play();
        }

        private void PauseAudio_Click(object sender, RoutedEventArgs e)
        {
            AudioPlayer.Pause();
        }

        private void StopAudio_Click(object sender, RoutedEventArgs e)
        {
            AudioPlayer.Stop();
        }
    }
}
