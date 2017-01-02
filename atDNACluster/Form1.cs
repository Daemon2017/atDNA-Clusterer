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
        AnalysisMethod AnalysisPCA;

        string PCAMmessage = "Сперва Вы должны провести обработку с помощью МГК!";
        string PCACaption = "Нехватка данных!";

        string ClusterNumberMessage = "Для работы этой функции число кластеров должно быть равным 4!";
        string ClustersNumberCaption = "Слишком мало кластеров!";
        MessageBoxButtons ClustersNumberButtons = MessageBoxButtons.OK;
        DialogResult ClustersNumberResult;

        double[,] matrixOfDistances;
        string[] kitNumbers;
        string[] kitNames;
        double[,] matrixOfCoordinates;
        double[][] mixture;
        int[] classificationsOur;

        int numberOfClusters = 2;

        public Form1()
        {
            InitializeComponent();

            zedGraph = new ZedGraphControl();
            zedGraph.Location = new Point(0, 24);
            zedGraph.Name = "zedGraph";
            zedGraph.Size = new Size(1366, 768 - 24 - 54);
            zedGraph.PointValueEvent += new ZedGraphControl.PointValueHandler(zedGraph_PointValueEvent);
            Controls.Add(zedGraph);
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

            int start = 1;

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

        private void button8_Click(object sender, EventArgs e)
        {
            updateGraph(classificationsOur);
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

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void обработатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((matrixOfDistances != null) && (kitNumbers != null))
            {
                sda = null;

                sda = new DescriptiveAnalysis(matrixOfDistances);
                sda.Compute();

                AnalysisPCA = new AnalysisMethod();

                if (центровкаToolStripMenuItem.CheckState == CheckState.Checked)
                {
                    AnalysisPCA = AnalysisMethod.Center;
                }

                if (стандартизацияToolStripMenuItem.CheckState == CheckState.Checked)
                {
                    AnalysisPCA = AnalysisMethod.Standardize;
                }

                pca = new PrincipalComponentAnalysis(sda.Source, AnalysisPCA);
                pca.Compute();

                matrixOfCoordinates = pca.Transform(matrixOfDistances, 2);
                numberOfClusters = 2;

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

        private void обработатьToolStripMenuItem1_Click(object sender, EventArgs e)
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
                MessageBoxButtons PCAButtons = MessageBoxButtons.OK;
                DialogResult PCAResult;

                PCAResult = MessageBox.Show(PCAMmessage, PCACaption, PCAButtons);
            }
        }

        private void красныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (numberOfClusters < 4)
            {
                ClustersNumberResult = MessageBox.Show(ClusterNumberMessage, ClustersNumberCaption, ClustersNumberButtons);
            }
            else
            {
                classificationsOur = new int[kitNumbers.Length];

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
            }
        }

        private void зеленыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (numberOfClusters < 4)
            {
                ClustersNumberResult = MessageBox.Show(ClusterNumberMessage, ClustersNumberCaption, ClustersNumberButtons);
            }
            else
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
            }
        }

        private void черныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (numberOfClusters < 4)
            {
                ClustersNumberResult = MessageBox.Show(ClusterNumberMessage, ClustersNumberCaption, ClustersNumberButtons);
            }
            else
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
        }

        private void обработатьToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            updateGraph(classificationsOur);
        }

        private void центровкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (стандартизацияToolStripMenuItem.CheckState == CheckState.Checked)
            {
                стандартизацияToolStripMenuItem.CheckState = CheckState.Unchecked;
            }

            центровкаToolStripMenuItem.CheckState = CheckState.Checked;
        }

        private void стандартизацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (центровкаToolStripMenuItem.CheckState == CheckState.Checked)
            {
                центровкаToolStripMenuItem.CheckState = CheckState.Unchecked;
            }

            стандартизацияToolStripMenuItem.CheckState = CheckState.Checked;
        }

        private void eNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClusterNumberMessage = "To use this feature, number of clusters must be equal to 4!";
            ClustersNumberCaption = "Too few clusters!";

            PCAMmessage = "At first, you must use PCA-processing.";
            PCACaption = "Not enough data!";

            файлToolStripMenuItem.Text = "File";
            открытьToolStripMenuItem.Text = "Open";
            кластерыToolStripMenuItem.Text = "Clusters";
            количествоКластеровToolStripMenuItem.Text = "Number of clusters";
            обработкаToolStripMenuItem.Text = "PCA processing";
            типВыводаToolStripMenuItem.Text = "Output type";
            стандартизацияToolStripMenuItem.Text = "Standartize";
            центровкаToolStripMenuItem.Text = "Center";
            обработатьToolStripMenuItem.Text = "Process";
            кластеризацияToolStripMenuItem.Text = "Clusterization";
            обработатьToolStripMenuItem1.Text = "Process";
            выделениеЦветомToolStripMenuItem.Text = "Color highlighting";
            красныйToolStripMenuItem.Text = "1 - Red";
            зеленыйToolStripMenuItem.Text = "2 - Green";
            черныйToolStripMenuItem.Text = "3 - Black";
            обработатьToolStripMenuItem2.Text = "Process";
        }
    }
}