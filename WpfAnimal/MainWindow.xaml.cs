using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAnimal.Classes;

namespace WpfAnimal
{
    public partial class MainWindow : Window
    {
        int Counter = 0;
        public MainWindow()
        {
            InitializeComponent();

            AnimalList.SelectionChanged += MList_SelectionChanged;

            AnimalList.ItemsSource = DataLayer.SampleItems;

            imagePanel.Visibility = Visibility.Hidden;

            background.Visibility = Visibility.Visible;

        }

        private void StandByTimer_HasCompleted(object sender, StandByTimer.TimerEventArgs e)
        {
            if (e.IsCompleted)
            {
                imagePanel.Visibility = Visibility.Hidden;

                background.Visibility = Visibility.Visible;
            }
        }

        private void MList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var selectedItem = AnimalList.SelectedItem as DataLayer.Animals;

            if (selectedItem == null) return;

            animalImage.Source = selectedItem.Gallery[0].BImage;

            txt_description.Text = selectedItem.Description;

            imagePanel.Visibility = Visibility.Visible;

            background.Visibility = Visibility.Hidden;

            Counter = 0;
            pageCount.Content = (Counter + 1) + "/" + selectedItem.Gallery.Count;

            StandByTimer.HasCompleted += StandByTimer_HasCompleted;
            StandByTimer.Start();
        }
        private void Rect1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void btn_prev_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_next_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_prev_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var sItem = AnimalList.SelectedItem as DataLayer.Animals;
            Counter--;
            if (Counter < 0)
            {
                animalImage.Source = sItem.Gallery[0].BImage;
                Counter = 0;
            }
            else
            {
                animalImage.Source = sItem.Gallery[Counter].BImage;
            }
            pageCount.Content = (Counter + 1) + "/" + sItem.Gallery.Count;
        }
        private void btn_next_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var sItem = AnimalList.SelectedItem as DataLayer.Animals;
            Counter++;
            if (Counter > sItem.Gallery.Count)
            {
                Counter = sItem.Gallery.Count;
                animalImage.Source = sItem.Gallery[(Counter - 1)].BImage;
            }
            else
            {
                animalImage.Source = sItem.Gallery[(Counter-1)].BImage;
            }
            pageCount.Content = (Counter) + "/" + sItem.Gallery.Count;
        }
        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StandByTimer.Start();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            imagePanel.Visibility = Visibility.Hidden;

            background.Visibility = Visibility.Visible;

        }
        private void AnimalList_MouseEnter(object sender, MouseEventArgs e)
        {

        }
    }
}