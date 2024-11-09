using OxyPlot;
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


namespace trpo
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void btnPaint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int count = int.Parse(txtCount.Text);
                if (count <= 0) 
                {
                    throw new Exception();
                }
                MainViewModel mvm = new MainViewModel(count);
                PlotModel pm = mvm.plotModel;
                Graph.Model = pm;
            }
            catch 
            {
                MessageBox.Show("Ошибка ввода количества элементов массива"); 
            }
        }

        private async void btnSort_Click(object sender, RoutedEventArgs e)
        {
            List<int> array = new List<int>();
            string[] numbers = txtArray.Text.Split(' ');
            
            if (numbers.Length > 10)
            {
                MessageBox.Show("Слишком большой массив");
                return;
            }

            foreach (string numberStr in numbers)
            {
                if (int.TryParse(numberStr, out int number))
                {
                    array.Add(number);
                    ListBoxNumbers.Items.Clear();
                    ListBoxNumbers.Items.Add(number);
                }
                else
                {
                    MessageBox.Show("Ошибка ввода массива");
                    return;
                }
            }

            if (rbSimpleSort.IsChecked == true)
            {
                await InsertionSort(array);
            }
            else if (rbBinariSort.IsChecked == true)
            {
                await BinaryInsertionSort(array);
            }
        }

        private async Task InsertionSort(List<int> numbers)
        {
            //ListBoxId.Visibility = Visibility.Collapsed;
            List<int> id = new List<int>();
            for (int i = 1; i <= numbers.Count; i++)
                id.Add(i);
            UpdateListBox(ListBoxId, id);
            ListBoxId.Items.Insert(0, "№");

            int n = numbers.Count;
            for (int i = 1; i < n; i++)
            {
                int key = numbers[i];
                txtCurrentElement.Text = key.ToString();
                int j = i - 1;

                while (j >= 0 && numbers[j] > key)
                {
                    numbers[j + 1] = numbers[j];
                    numbers[j] = int.MinValue;
                    j--;

                    // Обновляем отображение в ListBox
                    UpdateListBox(ListBoxNumbers, numbers);
                    ListBoxNumbers.SelectedIndex = j + 1;
                    ListBoxNumbers.ScrollIntoView(ListBoxNumbers.SelectedItem);
                    await Task.Delay(1000); // Задержка для визуализации
                }
                numbers[j + 1] = key;

                // Обновляем отображение в ListBox
                UpdateListBox(ListBoxNumbers, numbers);
                ListBoxNumbers.SelectedIndex = j + 1;
                ListBoxNumbers.ScrollIntoView(ListBoxNumbers.SelectedItem);
                await Task.Delay(1000); // Задержка для визуализации
            }
        }

        private async Task BinaryInsertionSort(List<int> numbers)
        {
            //ListBoxId.Visibility = Visibility.Visible;
            List<int> id = new List<int>();
            for (int i = 1; i<= numbers.Count; i++)
                id.Add(i);
            UpdateListBox(ListBoxId, id);
            ListBoxId.Items.Insert(0, "№");

            for (int i = 1; i < numbers.Count; i++)
            {
                int key = numbers[i];
                txtCurrentElement.Text = key.ToString();
                int left = 0;
                int right = i - 1;

                while (left <= right)
                {
                    int mid = left + (right - left) / 2;
                    PaintForeground(mid + 1, Brushes.LightCoral);
                    if (key < numbers[mid])
                    {
                        right = mid - 1;
                    }
                    else
                    {
                        left = mid + 1;
                    }
                    await Task.Delay(1000);
                    PaintForeground(mid + 1, Brushes.White);
                }

                for (int j = i - 1; j >= left; j--)
                {
                    numbers[j + 1] = numbers[j];

                    numbers[j] = int.MinValue;
                    // Обновляем отображение в ListBox
                    UpdateListBox(ListBoxNumbers, numbers);
                    ListBoxNumbers.SelectedIndex = j + 1;
                    ListBoxNumbers.ScrollIntoView(ListBoxNumbers.SelectedItem);
                    await Task.Delay(1000); // Задержка для визуализации
                }

                numbers[left] = key;

                // Обновляем отображение в ListBox
                UpdateListBox(ListBoxNumbers, numbers);
                ListBoxNumbers.SelectedIndex = left;
                ListBoxNumbers.ScrollIntoView(ListBoxNumbers.SelectedItem);
                await Task.Delay(1000); // Задержка для визуализации
            }
        }

        private void PaintForeground(int index, Brush color)
        {
            ListBoxItem item = (ListBoxItem)ListBoxId.ItemContainerGenerator.ContainerFromIndex(index);
            if (item != null)
            {
                item.Background = color;
            }
        }

        private void UpdateListBox(ListBox lb, List<int> numbers)
        {
            lb.Items.Clear();
            foreach (var number in numbers)
            {
                if (number == int.MinValue)
                    lb.Items.Add(" ");
                else
                    lb.Items.Add(number);
            }
        }
    }
}
