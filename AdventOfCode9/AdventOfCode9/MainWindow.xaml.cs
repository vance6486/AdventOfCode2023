using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdventOfCode9
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Sequence> sequences = new List<Sequence>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            ParseInput();
            txtPart1.Text = CalculatePart1();
            txtPart2.Text = CalculatePart2();
        }

        private void ParseInput()
        {
            sequences = new List<Sequence>();
            List<string> lines = new List<string>();

            if(/*chbReadFromFile.IsChecked ==*/ true)
            {
                String data;
                try
                {
                    //Pass the file path and file name to the StreamReader constructor
                    StreamReader sr = new StreamReader(@"..\..\..\MyData.txt");
                    //Read the first line of text
                    data = sr.ReadLine();

                    //Continue to read until you reach end of file
                    while (data != null)
                    {
                        lines.Add(data);
                        //write the line to console window
                        Console.WriteLine(data);
                        //Read the next line
                        data = sr.ReadLine();
                    }
                    //close the file
                    sr.Close();
                    Console.ReadLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
                finally
                {
                    Console.WriteLine("Executing finally block.");
                }
            }
            else
            {
                // lineCount may be -1 if TextBox layout info is not up-to-date.
                int lineCount = txtInput.LineCount;

                for (int line = 0; line < lineCount; line++)
                    // GetLineText takes a zero-based line index.
                    lines.Add(txtInput.GetLineText(line));
            }


            foreach (string line in lines)
            {
                Sequence thisLineSeq = new Sequence();
                string[] numbers = line.Split(" ");

                // capture the sequence
                foreach (string number in numbers)
                {
                    if(int.TryParse(number, out int value))
                    {
                        thisLineSeq.Add(value);
                    }
                }

                thisLineSeq.calculateBaseSequence();
                // add it to the sequence list
                sequences.Add(thisLineSeq);

            }

            int breakpoint = 0;
        }

        public string CalculatePart1()
        {
            long sumOfNext = 0;
            int i = 0;
            //txtInput.Text = "";
            foreach (Sequence seq in sequences)
            {
                i++;
                long seqLastNum = seq.getLastNumber();
                long seqNextNum = seq.getNextNumber();

                sumOfNext += seqNextNum;
                //txtInput.Text += $"{seqNextNum}\n";
            }

            return sumOfNext.ToString();
        }

        public string CalculatePart2()
        {
            long sumOfNewFirst = 0;
            int i = 0;
            txtInput.Text = "";
            foreach (Sequence seq in sequences)
            {
                i++;
                long seqNewFirstNum = seq.getNewFirstNumber();

                sumOfNewFirst += seqNewFirstNum;
                txtInput.Text += $"{seqNewFirstNum}\n";
            }

            return sumOfNewFirst.ToString();
        }
    }
}
