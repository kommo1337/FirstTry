using System;
using System.Windows;

namespace FirstTry.CLassFolder
{
    internal class MBClass
    {
        public static void ErrorMB(string text)
        {
            MessageBox.Show(text, "Ahtung!!!", MessageBoxButton.OK,
                MessageBoxImage.Error);
        }

        public static void ErrorMB(Exception ex)
        {
            MessageBox.Show(ex.Message, "Ahtung!!!", MessageBoxButton.OK,
                MessageBoxImage.Error);
        }

        public static void InfoMB(string text)
        {
            MessageBox.Show(text, "Information", MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        public static bool QuestionMB(string text)
        {
            return MessageBoxResult.Yes == MessageBox.Show(text, "Вопрос", MessageBoxButton.YesNo,
                MessageBoxImage.Question);
        }

        public static void ExitMB()
        {
            if (QuestionMB("Выйти?"))
            {
                App.Current.Shutdown();
            }
        }

    }
}
