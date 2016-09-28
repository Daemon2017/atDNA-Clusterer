using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ZedGraph;
using Accord.Statistics.Analysis;
using Accord.MachineLearning;
using Accord.Controls;

namespace atDNACluster
{
    public partial class Form1 : Form
    {
        ColorSequenceCollection colors = new ColorSequenceCollection();

        ZedGraphControl zedGraph;
        KMeans kmeans;
        PrincipalComponentAnalysis pca;
        DescriptiveAnalysis sda;

        double[,] matrixOfDistances;
        string[] kitNumbers;
        string[] kitNames;
        double[,] matrixOfCoordinates;
        double[][] mixture;
        int[] classificationsOur;

        int numberOfClusters = 4;

        public Form1()
        {
            InitializeComponent();

            zedGraph = new ZedGraphControl();
            zedGraph.Location = new Point(124, 12);
            zedGraph.Name = "zedGraph";
            zedGraph.Size = new Size(1240, 700);
            zedGraph.PointValueEvent += new ZedGraphControl.PointValueHandler(zedGraph_PointValueEvent);
            Controls.Add(zedGraph);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            matrixOfDistances = null;
            kitNumbers = null;
            kitNames = null;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Csv files (*.csv)|*.csv|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] allLinesDistances = File.ReadAllLines(openFileDialog.FileName);

                matrixOfDistances = new double[allLinesDistances.Length - 1, allLinesDistances.Length - 1];

                for (int i = 1; i < allLinesDistances.Length; i++)
                {
                    string[] rowDistances = allLinesDistances[i].Split(new[] { ';' });

                    for (int j = 2; j < allLinesDistances.Length + 1; j++)
                    {
                        if (double.TryParse(rowDistances[j], out matrixOfDistances[i - 1, j - 2])) { }
                    }
                }

                replaceZeros();

                fillDiagonalByZeros();

                string[] allLinesKits = File.ReadAllLines(openFileDialog.FileName);

                kitNumbers = new string[allLinesKits.Length - 1];
                kitNames = new string[allLinesKits.Length - 1];

                for (int i = 1; i < allLinesKits.Length; i++)
                {
                    string[] rowKits = allLinesKits[i].Split(new[] { ';' });

                    for (int j = 0; j < 0 + 1; j++)
                    {
                        kitNumbers[i - 1] = rowKits[j];
                    }

                    for (int j = 1; j < 1 + 1; j++)
                    {
                        kitNames[i - 1] = rowKits[j];
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if ((matrixOfDistances != null) && (kitNumbers != null))
            {
                sda = null;

                sda = new DescriptiveAnalysis(matrixOfDistances);
                sda.Compute();

                AnalysisMethod AnalysisPCA = new AnalysisMethod();

                if(radioButton1.Checked==true)
                {
                    AnalysisPCA = AnalysisMethod.Center;
                }
                else if(radioButton2.Checked==true)
                {
                    AnalysisPCA = AnalysisMethod.Standardize;
                }

                pca = new PrincipalComponentAnalysis(sda.Source, AnalysisPCA);
                pca.Compute();

                matrixOfCoordinates = pca.Transform(matrixOfDistances, 2);
                numberOfClusters = (int)numericUpDown1.Value;

                CreateGraph(zedGraph);
            }
            else
            {
                string message = "Вы не загрузили матрицы с данными!";
                string caption = "Нехватка данных!";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                result = MessageBox.Show(message, caption, buttons);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (matrixOfCoordinates != null)
            {
                transformOfMatrix();

                kmeans = new KMeans(numberOfClusters);
                kmeans.Compute(mixture);

                int[] classifications = kmeans.Clusters.Nearest(mixture);

                updateGraph(classifications);
            }
            else
            {
                string message = "Вы не произвели обработку через PCA!";
                string caption = "Нехватка данных!";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                result = MessageBox.Show(message, caption, buttons);
            }
        }

        void CreateGraph(ZedGraphControl zgc)
        {
            GraphPane myPane = zgc.GraphPane;
            myPane.CurveList.Clear();

            myPane.XAxis.Cross = 0.0;
            myPane.YAxis.Cross = 0.0;
            myPane.XAxis.Title.IsVisible = false;
            myPane.YAxis.Title.IsVisible = false;
            myPane.Title.IsVisible = false;
            myPane.Legend.IsVisible = false;

            int start;
            if (checkBox1.Checked == true)
            {
                start = 0;
            }
            else
            {
                start = 1;
            }

            PointPairList list = new PointPairList();
            for (int i = start; i < matrixOfCoordinates.GetLength(0); i++)
            {
                list.Add(matrixOfCoordinates[i, 0],
                         matrixOfCoordinates[i, 1]);
            }

            LineItem myCurve = myPane.AddCurve("Совпаденец", list, Color.Gray, SymbolType.Diamond);
            myCurve.Line.IsVisible = false;
            myCurve.Symbol.Border.IsVisible = false;
            myCurve.Symbol.Fill = new Fill(Color.Gray);

            for (int i = 0; i < numberOfClusters; i++)
            {
                Color color = colors[i];
                myCurve = myPane.AddCurve("D" + (i + 1), new PointPairList(), color, SymbolType.Diamond);
                myCurve.Line.IsVisible = false;
                myCurve.Symbol.Border.IsVisible = false;
                myCurve.Symbol.Fill = new Fill(color);
            }

            myPane.Fill = new Fill(Color.WhiteSmoke);

            zgc.IsShowPointValues = true;
            zgc.AxisChange();
            zgc.Refresh();
        }

        void updateGraph(int[] classifications)
        {
            for (int i = 0; i < numberOfClusters + 1; i++)
            {
                zedGraph.GraphPane.CurveList[i].Clear();
            }

            for (int j = 0; j < mixture.Length; j++)
            {
                int c = classifications[j];

                var curveList = zedGraph.GraphPane.CurveList[c + 1];
                double[] point = mixture[j];
                curveList.AddPoint(point[0], point[1]);
            }

            zedGraph.Invalidate();
        }

        string zedGraph_PointValueEvent(ZedGraphControl sender,
                                        GraphPane pane,
                                        CurveItem curve,
                                        int iPt)
        {
            PointPair point = curve[iPt];

            string kit = 0.ToString();
            string name = 0.ToString();

            for (int i = 0; i < matrixOfCoordinates.GetLength(0); i++)
            {
                if ((point.X == matrixOfCoordinates[i, 0]) && (point.Y == matrixOfCoordinates[i, 1]))
                {
                    kit = kitNumbers[i];
                    name = kitNames[i];
                }
            }

            string result = string.Format("X: {0:F3}\nY: {1:F3}\nKit: {2:F3}\nName: {3:F3}", point.X, point.Y, kit, name);

            return result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (numberOfClusters < 4)
            {
                string message = "Сперва нужно выставить число кластеров равным 4 или более, а затем нажать PCA и К-Средних!";
                string caption = "Слишком мало кластеров!";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                result = MessageBox.Show(message, caption, buttons);
            }
            else
            {
                if (checkBox1.Checked == true)
                {
                    classificationsOur = new int[kitNumbers.Length - 1];
                }
                else
                {
                    classificationsOur = new int[kitNumbers.Length];
                }

                string[] kitsForPaint;

                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Csv files (*.csv)|*.csv|All files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    kitsForPaint = File.ReadAllLines(openFileDialog.FileName);

                    for (int i = 0; i < kitsForPaint.Length; i++)
                    {
                        for (int j = 1; j < kitNumbers.Length; j++)
                        {
                            if (kitNumbers[j] == kitsForPaint[i])
                            {
                                classificationsOur[j - 1] = 1;
                            }
                        }
                    }
                }

                button6.Enabled = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string[] kitsForPaint;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Csv files (*.csv)|*.csv|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                kitsForPaint = File.ReadAllLines(openFileDialog.FileName);

                for (int i = 0; i < kitsForPaint.Length; i++)
                {
                    for (int j = 1; j < kitNumbers.Length; j++)
                    {
                        if (kitNumbers[j] == kitsForPaint[i])
                        {
                            classificationsOur[j - 1] = 2;
                        }
                    }
                }
            }

            button7.Enabled = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string[] kitsForPaint;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Csv files (*.csv)|*.csv|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                kitsForPaint = File.ReadAllLines(openFileDialog.FileName);

                for (int i = 0; i < kitsForPaint.Length; i++)
                {
                    for (int j = 1; j < kitNumbers.Length; j++)
                    {
                        if (kitNumbers[j] == kitsForPaint[i])
                        {
                            classificationsOur[j - 1] = 3;
                        }
                    }
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            updateGraph(classificationsOur);

            button6.Enabled = false;
            button7.Enabled = false;
        }

        void replaceZeros()
        {
            for (int i = 0; i < matrixOfDistances.GetLength(0); i++)
            {
                for (int j = 0; j < matrixOfDistances.GetLength(0); j++)
                {
                    if (matrixOfDistances[i, j] == 0)
                    {
                        matrixOfDistances[i, j] = 99;
                    }
                }
            }
        }

        void fillDiagonalByZeros()
        {
            for (int i = 0; i < matrixOfDistances.GetLength(0); i++)
            {
                matrixOfDistances[i, i] = 0;
            }
        }

        void transformOfMatrix()
        {
            if (checkBox1.Checked == true)
            {
                mixture = new double[matrixOfCoordinates.GetLength(0)][];

                for (int i = 0; i < matrixOfCoordinates.GetLength(0); i++)
                {
                    mixture[i] = new double[] { matrixOfCoordinates[i, 0], matrixOfCoordinates[i, 1] };
                }
            }
            else
            {
                mixture = new double[matrixOfCoordinates.GetLength(0) - 1][];

                for (int i = 1; i < matrixOfCoordinates.GetLength(0); i++)
                {
                    mixture[i - 1] = new double[] { matrixOfCoordinates[i, 0], matrixOfCoordinates[i, 1] };
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.Enabled = false;

            groupBox1.Text = "Load";
            groupBox2.Text = "Processing";
            groupBox4.Text = "Clustering";
            groupBox5.Text = "Highlighting";

            button1.Text = "Data";
            button3.Text = "PCA";
            button5.Text = "K-means";

            button2.Text = "1 - Red";
            button6.Text = "2 - Green";
            button7.Text = "3 - Black";
            button8.Text = "Start";

            label9.Text = "Number of clusters:";

            checkBox1.Text = "Show yourself";

            radioButton1.Text = "Center";
            radioButton2.Text = "Standartize";
        }
    }
}