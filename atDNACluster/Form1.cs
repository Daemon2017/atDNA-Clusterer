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

        bool getDistanceMode = false;
        double x1 = 0,
               y1 = 0,
               x2 = 0,
               y2 = 0;
        int numberOfClick = 0;
        int numberOfClusters = 4;

        public Form1()
        {
            InitializeComponent();

            zedGraph = new ZedGraphControl();
            zedGraph.Location = new Point(124, 12);
            zedGraph.Name = "zedGraph";
            zedGraph.Size = new Size(1240, 700);
            zedGraph.PointValueEvent += new ZedGraphControl.PointValueHandler(zedGraph_PointValueEvent);
            zedGraph.MouseClick += new MouseEventHandler(zedGraph_MouseClick);
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

                pca = new PrincipalComponentAnalysis(sda.Source, AnalysisMethod.Standardize);
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

        private void button4_Click(object sender, EventArgs e)
        {
            getDistanceMode = true;

            label7.Text = "Выберите точку 1";
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

            PointPairList list = new PointPairList();
            for (int i = 1; i < matrixOfCoordinates.GetLength(0); i++)
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

        private void zedGraph_MouseClick(object sender, MouseEventArgs e)
        {
            if (getDistanceMode == true)
            {
                if (numberOfClick == 0)
                {
                    zedGraph.GraphPane.ReverseTransform(e.Location, out x1, out y1);

                    label7.Text = "Выберите точку 2";
                    label3.Text = x1.ToString();
                    label4.Text = y1.ToString();

                    numberOfClick++;
                }
                else if (numberOfClick == 1)
                {
                    zedGraph.GraphPane.ReverseTransform(e.Location, out x2, out y2);

                    double pointsDistance = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
                    label7.Text = "Дистанция:";
                    label8.Text = pointsDistance.ToString();
                    label5.Text = x2.ToString();
                    label2.Text = y2.ToString();

                    numberOfClick = 0;

                    getDistanceMode = false;
                }
            }
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
                classificationsOur = new int[kitNumbers.Length - 1];

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
            mixture = new double[matrixOfCoordinates.GetLength(0) - 1][];

            for (int i = 1; i < matrixOfCoordinates.GetLength(0); i++)
            {
                mixture[i - 1] = new double[] { matrixOfCoordinates[i, 0], matrixOfCoordinates[i, 1] };
            }
        }
    }
}