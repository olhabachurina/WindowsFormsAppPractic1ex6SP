using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace WindowsFormsAppPractic1ex6SP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private async void StartButton_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            int startRange = (int)numericUpDownStart.Value;
            int endRange = (int)numericUpDownEnd.Value;
            int numberOfThreads = (int)numericUpDownThreads.Value;
            int range = endRange - startRange + 1;
            int rangePerThread = range / numberOfThreads;
            var tasks = new List<Task>();
            for (int i = 0; i < numberOfThreads; i++)
            {
                int start = startRange + i * rangePerThread;
                int end = (i == numberOfThreads - 1) ? endRange : start + rangePerThread - 1;
                tasks.Add(Task.Run(() => DisplayNumbers(start, end)));
            }
            await Task.WhenAll(tasks);
            MessageBox.Show("Все потоки завершили выполнение");
        }
        private void DisplayNumbers(int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                int number = i; 
                textBox1.Invoke((MethodInvoker)delegate
                {
                    textBox1.AppendText(number + Environment.NewLine);
                });
                Task.Delay(5).Wait(); 
            }
        }
    }
}


