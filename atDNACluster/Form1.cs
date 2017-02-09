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
        CommonMatch[] CommonMatches;
        Match[] Matches;

        string DataReceivedMessage = "Данные успешно получены с сервера!";
        string DataReceivedCaption = "Успех!";
        DialogResult DataReceivedResult;

        string LoginErrorMessage = "Неправильный логин и/или пароль!";
        string LoginErrorCaption = "Ошибка!";
        DialogResult LoginErrorResult;

        string ServerOfflineMessage = "Сервер с данными FTDNA временно недоступен!";
        string ServerOfflineCaption = "Ошибка!";
        DialogResult ServerOfflineResult;

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

        double[,] MatrixOfDistances;
        string[] KitNumbers;
        string[] KitNames;
        double[,] matrixOfCoordinates;
        double[][] mixture;
        int[] classificationsOur;

        int LastPCANumberOfClusters;
        int LastClusteringNumberOfClusters;
        int numberOfClusters = 2;

        bool FTDNA = false;

        public Form1()
        {
            InitializeComponent();

            zedGraph = new ZedGraphControl();
            zedGraph.Location = new Point(0, 24);
            zedGraph.Name = "zedGraph";
            zedGraph.Size = new Size(1366, 768 - 24 - 54 - 22);
            zedGraph.GraphPane.XAxis.IsVisible = false;
            zedGraph.GraphPane.YAxis.IsVisible = false;
            zedGraph.GraphPane.Title.IsVisible = false;
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
                    kit = KitNumbers[i];
                    name = KitNames[i];
                    distance = MatrixOfDistances[0, i].ToString();
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
            for (int i = 0; i < MatrixOfDistances.GetLength(0); i++)
            {
                for (int j = 0; j < MatrixOfDistances.GetLength(0); j++)
                {
                    MatrixOfDistances[i, j] = 99;
                }
            }
        }

        void fillDiagonalByZeros()
        {
            for (int i = 0; i < MatrixOfDistances.GetLength(0); i++)
            {
                MatrixOfDistances[i, i] = 0;
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

        private void processToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FTDNA == true)
            {
                MatrixOfDistances = null;
                KitNames = null;
                KitNumbers = null;

                MatrixOfDistances = new double[Matches.Length + 1, Matches.Length + 1];
                KitNames = new string[Matches.Length + 1];
                KitNumbers = new string[Matches.Length + 1];

                replaceZeros();
                fillDiagonalByZeros();

                //-----------------------------------------------------

                for (int i = 1; i < MatrixOfDistances.GetLength(0); i++)
                {
                    MatrixOfDistances[0, i] = convertTotalCMToTMRCA(Matches[i - 1].totalCM);
                    MatrixOfDistances[i, 0] = convertTotalCMToTMRCA(Matches[i - 1].totalCM);
                    KitNames[i] = Matches[i - 1].firstName + " " + Matches[i - 1].middleName + " " + Matches[i - 1].lastName;
                    KitNumbers[i] = Matches[i - 1].eKitNum;
                }

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
                                        MatrixOfDistances[i + 1, m + 1] = convertTotalCMToTMRCA(CommonMatches[j].commonMatches[n].totalCM);
                                    }
                                }
                            }
                        }
                    }
                }

                //-----------------------------------------------------

                int[] counter = new int[MatrixOfDistances.GetLength(0)];
                int[] orphan = new int[1];
                int d = 0;

                for (int i = 0; i < MatrixOfDistances.GetLength(0); i++)
                {
                    for (int j = 0; j < MatrixOfDistances.GetLength(0); j++)
                    {
                        if (MatrixOfDistances[i, j] == 99)
                        {
                            counter[i]++;
                        }
                    }

                    if (counter[i] == MatrixOfDistances.GetLength(0) - 2)
                    {
                        orphan[d] = i;

                        d++;

                        Array.Resize(ref orphan, orphan.Length + 1);
                    }
                }

                counter = null;

                Array.Resize(ref orphan, orphan.Length - 1);

                //-----------------------------------------------------

                int deleted = 0;
                string[] TempKitNamesMatrix = KitNames;
                string[] TempKitNumbersMatrix = KitNumbers;
                double[,] TempDistancesMatrix = MatrixOfDistances;

                for (int i = 0; i < orphan.Length; i++)
                {
                    TempKitNamesMatrix = CutArrayString(orphan[i] - deleted, TempKitNamesMatrix);
                    TempKitNumbersMatrix = CutArrayString(orphan[i] - deleted, TempKitNumbersMatrix);
                    TempDistancesMatrix = CutArrayDouble(orphan[i] - deleted, orphan[i] - deleted, TempDistancesMatrix);

                    deleted++;
                }

                orphan = null;

                KitNames = null;
                KitNames = TempKitNamesMatrix;
                TempKitNamesMatrix = null;

                KitNumbers = null;
                KitNumbers = TempKitNumbersMatrix;
                TempKitNumbersMatrix = null;

                MatrixOfDistances = null;
                MatrixOfDistances = TempDistancesMatrix;
                TempDistancesMatrix = null;

                classificationsOur = null;
                classificationsOur = new int[KitNumbers.Length];
            }

            toolStripStatusLabel1.Text = "Число совпаденцев: " + KitNumbers.Length;

            //-----------------------------------------------------

            if ((MatrixOfDistances != null) && (KitNumbers != null))
            {
                sda = null;

                sda = new DescriptiveAnalysis(MatrixOfDistances);
                sda.Compute();

                AnalysisPCA = new AnalysisMethod();

                if (centerToolStripMenuItem.CheckState == CheckState.Checked)
                {
                    AnalysisPCA = AnalysisMethod.Center;
                }
                else if (standartizeToolStripMenuItem.CheckState == CheckState.Checked)
                {
                    AnalysisPCA = AnalysisMethod.Standardize;
                }

                pca = new PrincipalComponentAnalysis(sda.Source, AnalysisPCA);
                pca.Compute();

                matrixOfCoordinates = pca.Transform(MatrixOfDistances, 2);
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

        void paintDots(int ColorNumber)
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
                                for (int j = 1; j < KitNumbers.Length; j++)
                                {
                                    if (KitNumbers[j] == kitsForPaint[i])
                                    {
                                        if (ColorNumber == 1)
                                        {
                                            classificationsOur[j - 1] = 1;
                                        }
                                        else if (ColorNumber == 2)
                                        {
                                            classificationsOur[j - 1] = 2;
                                        }
                                        else if (ColorNumber == 3)
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
        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            paintDots(1);
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            paintDots(2);
        }

        private void blackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            paintDots(3);
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

            ServerOfflineMessage = "FTDNA's data-server is temporarily unavailable!";
            ServerOfflineCaption = "Error!";

            NumberOfClusteringClustersErrorMessage = "First, you need to run K-means clustering when the number of clusters is equal to 4!";
            NumberOfClusteringClustersErrorCaption = "Wrong number of clusters!";

            PCAMmessage = "At first, you must use PCA-processing.";
            PCACaption = "Not enough data!";

            fileToolStripMenuItem.Text = "File";
            openToolStripMenuItem1.Text = "Load";
            numberOfClustersToolStripMenuItem.Text = "Number of clusters";
            processingToolStripMenuItem.Text = "Processing";
            outputTypeToolStripMenuItem.Text = "Output type";
            standartizeToolStripMenuItem.Text = "Standartize";
            centerToolStripMenuItem.Text = "Center";
            processToolStripMenuItem.Text = "Process (PCA)";
            clusterizationToolStripMenuItem.Text = "Clusterization";
            processToolStripMenuItem1.Text = "Process (K-means)";
            colorHighlightningToolStripMenuItem.Text = "Color highlighting";
            redToolStripMenuItem.Text = "1 - Red";
            greenToolStripMenuItem.Text = "2 - Green";
            blackToolStripMenuItem.Text = "3 - Black";
            processToolStripMenuItem2.Text = "Process";
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

        public static string[] CutArrayString(int rowToRemove, string[] originalArray)
        {
            string[] result = new string[originalArray.GetLength(0) - 1];

            for (int i = 0, j = 0; i < originalArray.GetLength(0); i++)
            {
                if (i == rowToRemove)
                {
                    continue;
                }

                result[j] = originalArray[i];

                j++;
            }

            return result;
        }

        public static double[,] CutArrayDouble(int rowToRemove, int columnToRemove, double[,] originalArray)
        {
            double[,] result = new double[originalArray.GetLength(0) - 1, originalArray.GetLength(1) - 1];

            for (int i = 0, j = 0; i < originalArray.GetLength(0); i++)
            {
                if (i == rowToRemove)
                    continue;

                for (int k = 0, u = 0; k < originalArray.GetLength(1); k++)
                {
                    if (k == columnToRemove)
                        continue;

                    result[j, u] = originalArray[i, k];
                    u++;
                }
                j++;
            }

            return result;
        }

        private void numberOfClustersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClustersRegulator ClustersRegulatorWindow = new ClustersRegulator(numberOfClusters);
            ClustersRegulatorWindow.ShowDialog();
            numberOfClusters = ClustersRegulatorWindow.numberOfClusters;
        }

        private void saveKitsOfMatchesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveKitNumbersDialog = new SaveFileDialog();
            SaveKitNumbersDialog.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            SaveKitNumbersDialog.FilterIndex = 1;
            SaveKitNumbersDialog.RestoreDirectory = true;

            if (SaveKitNumbersDialog.ShowDialog() == DialogResult.OK)
            {
                if (SaveKitNumbersDialog.FileName != null)
                {
                    StreamWriter str = new StreamWriter(SaveKitNumbersDialog.FileName);

                    for (int i = 0; i < KitNumbers.GetLength(0); i++)
                    {
                        str.WriteLine(KitNumbers[i]);
                    }
                    str.Close();
                }
            }
        }

        private void openGedmatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MatrixOfDistances = null;
            KitNumbers = null;
            KitNames = null;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Csv files (*.csv)|*.csv|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] allLinesDistances = File.ReadAllLines(openFileDialog.FileName);

                MatrixOfDistances = new double[allLinesDistances.Length - 1, allLinesDistances.Length - 1];

                replaceZeros();
                fillDiagonalByZeros();

                for (int i = 1; i < allLinesDistances.Length; i++)
                {
                    string[] rowDistances = allLinesDistances[i].Split(new[] { ';' });

                    for (int j = 2; j < allLinesDistances.Length + 1; j++)
                    {
                        if (double.TryParse(rowDistances[j], out MatrixOfDistances[i - 1, j - 2]))
                        {

                        }
                    }
                }

                string[] allLinesKits = File.ReadAllLines(openFileDialog.FileName);

                KitNumbers = new string[allLinesKits.Length - 1];
                KitNames = new string[allLinesKits.Length - 1];

                for (int i = 1; i < allLinesKits.Length; i++)
                {
                    string[] rowKits = allLinesKits[i].Split(new[] { ';' });

                    for (int j = 0; j < 0 + 1; j++)
                    {
                        KitNumbers[i - 1] = rowKits[j];
                    }

                    for (int j = 1; j < 1 + 1; j++)
                    {
                        KitNames[i - 1] = rowKits[j];
                    }
                }

                FTDNA = false;
            }
        }

        private void openFTDNAToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            string KitNumber;
            string PassWord;

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

                    Matches = null;
                    CommonMatches = null;

                    string url2 = "https://www.familytreedna.com/api/family-finder/matches-common";
                    var jsonCommonMatchesRaw = client.DownloadString(url2);
                    JavaScriptSerializer serializerCommonMatches = new JavaScriptSerializer();
                    serializerCommonMatches.MaxJsonLength = int.MaxValue;
                    CommonMatches = serializerCommonMatches.Deserialize<CommonMatch[]>(jsonCommonMatchesRaw);
                    jsonCommonMatchesRaw = null;

                    string url = "https://www.familytreedna.com/api/family-finder/matches";
                    var jsonMatchesRaw = client.DownloadString(url);
                    JavaScriptSerializer serializerMatches = new JavaScriptSerializer();
                    serializerMatches.MaxJsonLength = int.MaxValue;
                    Matches = serializerMatches.Deserialize<Match[]>(jsonMatchesRaw);
                    jsonMatchesRaw = null;

                    FTDNA = true;
                }
                catch (WebException ex)
                {
                    LoginErrorResult = MessageBox.Show(LoginErrorMessage, LoginErrorCaption, MessageBoxButtons.OK);
                }
                catch (ArgumentException ex)
                {
                    ServerOfflineResult = MessageBox.Show(ServerOfflineMessage, ServerOfflineCaption, MessageBoxButtons.OK);
                }
            }
        }

        private void SumOfSegmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LongestSegmentToolStripMenuItem.CheckState == CheckState.Checked)
            {
                LongestSegmentToolStripMenuItem.CheckState = CheckState.Unchecked;
            }

            SumOfSegmentsToolStripMenuItem.CheckState = CheckState.Checked;
        }

        private void LongestSegmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SumOfSegmentsToolStripMenuItem.CheckState == CheckState.Checked)
            {
                SumOfSegmentsToolStripMenuItem.CheckState = CheckState.Unchecked;
            }

            LongestSegmentToolStripMenuItem.CheckState = CheckState.Checked;
        }
    }
}
