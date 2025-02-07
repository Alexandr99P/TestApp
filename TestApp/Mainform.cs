using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestApp
{
    public partial class MainForm : Form
    {
        private CancellationTokenSource _cancellationTokenSource;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Добавляем тесты в ComboBox
            comboBoxTests.Items.AddRange(new string[] { "Test 1", "Test 2", "Test 3" });
            comboBoxTests.SelectedIndex = 0;
            UpdateStatus("Ожидание ввода...");
        }

        private async void buttonStart_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxProductId.Text))
            {
                MessageBox.Show("Введите идентификатор изделия.");
                return;
            }

            _cancellationTokenSource = new CancellationTokenSource();
            buttonStart.Enabled = false;
            buttonStop.Enabled = true;
            UpdateStatus("Выполнение теста...");

            try
            {
                var result = await RunTestAsync(comboBoxTests.SelectedIndex, _cancellationTokenSource.Token);
                SaveResult(textBoxProductId.Text, result);
                UpdateStatus("Тест завершен.");
            }
            catch (OperationCanceledException)
            {
                UpdateStatus("Тест отменен.");
            }
            finally
            {
                buttonStart.Enabled = true;
                buttonStop.Enabled = false;
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            _cancellationTokenSource?.Cancel();
        }

        private async Task<TestResult> RunTestAsync(int testIndex, CancellationToken cancellationToken)
        {
            Random random = new Random();
            await Task.Delay(random.Next(10000, 30000), cancellationToken); // Имитация длительной операции

            if (random.Next(0, 2) == 0) // Случайный выбор: успех или ошибка
            {
                return new TestResult
                {
                    IsSuccess = true,
                    Data = new object[] { random.Next(100), random.NextDouble(), Guid.NewGuid().ToString() }
                };
            }
            else
            {
                return new TestResult
                {
                    IsSuccess = false,
                    ErrorMessage = "Ошибка теста",
                    Data = new object[] { random.Next(100), random.NextDouble(), Guid.NewGuid().ToString() }
                };
            }
        }

        private void SaveResult(string productId, TestResult result)
        {
            string fileName = $"{productId}.txt";
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine($"Успех: {result.IsSuccess}");
                if (!result.IsSuccess)
                {
                    writer.WriteLine($"Ошибка: {result.ErrorMessage}");
                }
                writer.WriteLine("Данные:");
                foreach (var data in result.Data)
                {
                    writer.WriteLine(data);
                }
            }
        }

        private void UpdateStatus(string status)
        {
            labelStatus.Text = status;
        }

        private void textBoxProductId_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelStatus_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxTests_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

    public class TestResult
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public object[] Data { get; set; }
    }
}