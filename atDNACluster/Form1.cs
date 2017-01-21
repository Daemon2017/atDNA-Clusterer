using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Net;
using System.Text;

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

        string DataReceivedMessage = "Данные успешно получены с сервера!";
        string DataReceivedCaption = "Успех!";
        DialogResult DataReceivedResult;

        string LoginErrorMessage = "Неправильный логин и/или пароль!";
        string LoginErrorCaption = "Ошибка!";
        DialogResult LoginErrorResult;

        string PCAMmessage = "Сперва Вы должны провести обработку с помощью МГК!";
        string PCACaption = "Нехватка данных!";

        string NumberOfClustersErrorMessage = "Для работы этой функции число кластеров должно быть равным 4!";
        string NumberOfClustersErrorCaption = "Неправильное количество кластеров!";
        DialogResult NumberOfClustersError;

        string NumberOfPCAClustersErrorMessage = "Сперва нужно запустить МГК при числе кластеров, равном 4!";
        string NumberOfPCAClustersErrorCaption = "Неправильное количество кластеров!";
        DialogResult NumberOfPCAClustersError;

        string NumberOfClusteringClustersErrorMessage = "Сперва нужно запустить кластеризацию К-средних при числе кластеров, равном 4!";
        string NumberOfClusteringClustersErrorCaption = "Неправильное количество кластеров!";
        DialogResult NumberOfClusteringClustersError;

        double[,] matrixOfDistances;
        string[] kitNumbers;
        string[] kitNames;
        double[,] matrixOfCoordinates;
        double[][] mixture;
        int[] classificationsOur;

        int LastPCANumberOfClusters;
        int LastClusteringNumberOfClusters;
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
            string distance = 0.ToString();

            for (int i = 0; i < matrixOfCoordinates.GetLength(0); i++)
            {
                if ((point.X == matrixOfCoordinates[i, 0]) && (point.Y == matrixOfCoordinates[i, 1]))
                {
                    kit = kitNumbers[i];
                    name = kitNames[i];
                    distance = matrixOfDistances[0, i].ToString();
                }
            }

            string result = string.Format("X: {0:F3}\nY: {1:F3}\nKit: {2:F3}\nName: {3:F3}\nTMRCA: {4:F3}", point.X, point.Y, kit, name, distance);

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
                    matrixOfDistances[i, j] = 99;
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

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void processToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((matrixOfDistances != null) && (kitNumbers != null))
            {
                sda = null;

                sda = new DescriptiveAnalysis(matrixOfDistances);
                sda.Compute();

                AnalysisPCA = new AnalysisMethod();

                if (centerToolStripMenuItem.CheckState == CheckState.Checked)
                {
                    AnalysisPCA = AnalysisMethod.Center;
                }

                if (standartizeToolStripMenuItem.CheckState == CheckState.Checked)
                {
                    AnalysisPCA = AnalysisMethod.Standardize;
                }

                pca = new PrincipalComponentAnalysis(sda.Source, AnalysisPCA);
                pca.Compute();

                matrixOfCoordinates = pca.Transform(matrixOfDistances, 2);
                LastPCANumberOfClusters = numberOfClusters;

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

        private void processToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (matrixOfCoordinates != null)
            {
                transformOfMatrix();

                kmeans = new KMeans(numberOfClusters);
                kmeans.Compute(mixture);

                int[] classifications = kmeans.Clusters.Nearest(mixture);
                LastClusteringNumberOfClusters = numberOfClusters;

                updateGraph(classifications);
            }
            else
            {
                MessageBoxButtons PCAButtons = MessageBoxButtons.OK;
                DialogResult PCAResult;

                PCAResult = MessageBox.Show(PCAMmessage, PCACaption, PCAButtons);
            }
        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (numberOfClusters != 4)
            {
                NumberOfClustersError = MessageBox.Show(NumberOfClustersErrorMessage, NumberOfClustersErrorCaption, MessageBoxButtons.OK);
            }
            else
            {
                if (LastPCANumberOfClusters != 4)
                {
                    NumberOfPCAClustersError = MessageBox.Show(NumberOfPCAClustersErrorMessage, NumberOfPCAClustersErrorCaption, MessageBoxButtons.OK);
                }
                else
                {
                    if (LastClusteringNumberOfClusters != 4)
                    {
                        NumberOfClusteringClustersError = MessageBox.Show(NumberOfClusteringClustersErrorMessage, NumberOfClusteringClustersErrorCaption, MessageBoxButtons.OK);
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
            }
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (numberOfClusters != 4)
            {
                NumberOfClustersError = MessageBox.Show(NumberOfClustersErrorMessage, NumberOfClustersErrorCaption, MessageBoxButtons.OK);
            }
            else
            {
                if (LastPCANumberOfClusters != 4)
                {
                    NumberOfPCAClustersError = MessageBox.Show(NumberOfPCAClustersErrorMessage, NumberOfPCAClustersErrorCaption, MessageBoxButtons.OK);
                }
                else
                {
                    if (LastClusteringNumberOfClusters != 4)
                    {
                        NumberOfClusteringClustersError = MessageBox.Show(NumberOfClusteringClustersErrorMessage, NumberOfClusteringClustersErrorCaption, MessageBoxButtons.OK);
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
            }
        }

        private void blackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (numberOfClusters != 4)
            {
                NumberOfClustersError = MessageBox.Show(NumberOfClustersErrorMessage, NumberOfClustersErrorCaption, MessageBoxButtons.OK);
            }
            else
            {
                if (LastPCANumberOfClusters != 4)
                {
                    NumberOfPCAClustersError = MessageBox.Show(NumberOfPCAClustersErrorMessage, NumberOfPCAClustersErrorCaption, MessageBoxButtons.OK);
                }
                else
                {
                    if (LastClusteringNumberOfClusters != 4)
                    {
                        NumberOfClusteringClustersError = MessageBox.Show(NumberOfClusteringClustersErrorMessage, NumberOfClusteringClustersErrorCaption, MessageBoxButtons.OK);
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
            }
        }

        private void processToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            updateGraph(classificationsOur);
        }

        private void centerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (standartizeToolStripMenuItem.CheckState == CheckState.Checked)
            {
                standartizeToolStripMenuItem.CheckState = CheckState.Unchecked;
            }

            centerToolStripMenuItem.CheckState = CheckState.Checked;
        }

        private void standartizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (centerToolStripMenuItem.CheckState == CheckState.Checked)
            {
                centerToolStripMenuItem.CheckState = CheckState.Unchecked;
            }

            standartizeToolStripMenuItem.CheckState = CheckState.Checked;
        }

        private void eNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataReceivedMessage = "The data was successfully received from the server!";
            DataReceivedCaption = "Success!";

            LoginErrorMessage = "Wrong username and / or password!";
            LoginErrorCaption = "Error!";

            NumberOfClustersErrorMessage = "To use this feature, number of clusters must be equal to 4!";
            NumberOfClustersErrorCaption = "Wrong number of clusters!";

            NumberOfPCAClustersErrorMessage = "First, you need to run PCA when the number of clusters is equal to 4!";
            NumberOfPCAClustersErrorCaption = "Wrong number of clusters!";

            NumberOfClusteringClustersErrorMessage = "First, you need to run K-means clustering when the number of clusters is equal to 4!";
            NumberOfClusteringClustersErrorCaption = "Wrong number of clusters!";

            PCAMmessage = "At first, you must use PCA-processing.";
            PCACaption = "Not enough data!";

            fileToolStripMenuItem.Text = "File";
            openToolStripMenuItem.Text = "Open";
            clustersToolStripMenuItem.Text = "Clusters";
            clustersNumberToolStripMenuItem.Text = "Number of clusters";
            processingToolStripMenuItem.Text = "Processing (PCA)";
            outputTypeToolStripMenuItem.Text = "Output type";
            standartizeToolStripMenuItem.Text = "Standartize";
            centerToolStripMenuItem.Text = "Center";
            processToolStripMenuItem.Text = "Process";
            clusterizationToolStripMenuItem.Text = "Clusterization (K-means)";
            processToolStripMenuItem1.Text = "Process";
            colorHighlightningToolStripMenuItem.Text = "Color highlighting";
            redToolStripMenuItem.Text = "1 - Red";
            greenToolStripMenuItem.Text = "2 - Green";
            blackToolStripMenuItem.Text = "3 - Black";
            processToolStripMenuItem2.Text = "Process";
        }

        private void clustersNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClustersRegulator ClustersRegulatorWindow = new ClustersRegulator();
            ClustersRegulatorWindow.ShowDialog();
            numberOfClusters = ClustersRegulatorWindow.numberOfClusters;
        }

        double convertTotalCMToTMRCA(double TotalCM)
        {
            double TMRCA = -0.722 * Math.Log(TotalCM) + 6.8657;

            return TMRCA;
        }

        public class Match
        {
            public int matchResultId { get; set; }
            public string eKitNum { get; set; }
            public string firstName { get; set; }
            public string middleName { get; set; }
            public string lastName { get; set; }
            public double totalCM { get; set; }
            public double longestCM { get; set; }
        }

        public class Common
        {
            public int resultId2 { get; set; }
            public double totalCM { get; set; }
        }

        public class CommonMatch
        {
            public int matchResultId { get; set; }
            public List<Common> commonMatches { get; set; }
        }

        private void openFTDNAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String KitNumber;
            String PassWord;

            Authorization AuthorizationWindow = new Authorization();
            AuthorizationWindow.ShowDialog();
            KitNumber = AuthorizationWindow.KitNumber;
            PassWord = AuthorizationWindow.PassWord;

            if (KitNumber != null & PassWord != null)
            {
                try
                {
                    WebClient client = new WebClient();
                    string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(KitNumber + ":" + PassWord));
                    client.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;

                    string url = "https://www.familytreedna.com/api/family-finder/matches";
                    var jsonMatchesRaw = client.DownloadString(url);

                    JavaScriptSerializer serializerMatches = new JavaScriptSerializer();
                    serializerMatches.MaxJsonLength = int.MaxValue;
                    Match[] Matches = serializerMatches.Deserialize<Match[]>(jsonMatchesRaw);

                    matrixOfDistances = null;
                    kitNames = null;

                    matrixOfDistances = new double[Matches.Length + 1, Matches.Length + 1];
                    kitNames = new string[Matches.Length + 1];
                    kitNumbers = new string[Matches.Length + 1];

                    replaceZeros();
                    fillDiagonalByZeros();

                    for (int i = 1; i < matrixOfDistances.GetLength(0); i++)
                    {
                        matrixOfDistances[0, i] = convertTotalCMToTMRCA(Matches[i - 1].totalCM);
                        matrixOfDistances[i, 0] = convertTotalCMToTMRCA(Matches[i - 1].totalCM);
                        kitNames[i] = Matches[i - 1].firstName + " " + Matches[i - 1].middleName + " " + Matches[i - 1].lastName;
                        kitNumbers[i] = Matches[i - 1].eKitNum;
                    }

                    //--------------------------------------------------------------------------------------------------

                    string url2 = "https://www.familytreedna.com/api/family-finder/matches-common";
                    var jsonCommonMatchesRaw = client.DownloadString(url2);

                    JavaScriptSerializer serializerCommonMatches = new JavaScriptSerializer();
                    serializerCommonMatches.MaxJsonLength = int.MaxValue;
                    CommonMatch[] CommonMatches = serializerCommonMatches.Deserialize<CommonMatch[]>(jsonCommonMatchesRaw);

                    for (int i = 0; i < Matches.Length; i++)
                    {
                        for (int j = 0; j < CommonMatches.Length; j++)
                        {
                            if (Matches[i].matchResultId == CommonMatches[j].matchResultId)
                            {
                                for (int n = 0; n < CommonMatches[j].commonMatches.Count; n++)
                                {
                                    for (int m = 0; m < Matches.Length; m++)
                                    {
                                        if (Matches[m].matchResultId == CommonMatches[j].commonMatches[n].resultId2)
                                        {
                                            matrixOfDistances[i + 1, m + 1] = convertTotalCMToTMRCA(CommonMatches[j].commonMatches[n].totalCM);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (jsonMatchesRaw != null & jsonCommonMatchesRaw != null)
                    {
                        DataReceivedResult = MessageBox.Show(DataReceivedMessage, DataReceivedCaption, MessageBoxButtons.OK);
                    }
                }
                catch (WebException ex)
                {
                    LoginErrorResult = MessageBox.Show(LoginErrorMessage, LoginErrorCaption, MessageBoxButtons.OK);
                }
            }
        }
    }
}