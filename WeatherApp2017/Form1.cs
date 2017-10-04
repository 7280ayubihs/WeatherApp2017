using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace WeatherApp2017
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 初期化済みフラグ
        /// </summary>
        private bool isInitialized = false;

        /// <summary>
        /// pyhton.exeのパスです。
        /// 設定ファイルの内容で上書きされます。
        /// </summary>
        private string pythonExePath = "python.exe";

        /// <summary>
        /// 実行されるpython scriptのパスです。
        /// 設定ファイルの内容で上書きされます。
        /// </summary>
        private string pythonScriptPath = "python.py";

        /// <summary>
        /// アルゴリズムの表示名（DisplayName）と略称（Abbreviation）を紐付けるオブジェクトです。
        /// 表示名が key で、略称を value としています。。
        /// </summary>
        private Dictionary<string, string> algorithmMap = new Dictionary<string, string>();

        /// <summary>
        /// 設定ファイルから、正解率ロードする領域です。
        /// </summary>
        private double[,] accuracyRateArray;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 設定ファイルをロード
            List<string> pointItems = new List<string>();
            using (StreamReader sr = new StreamReader(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "div22.json")))
            {
                JObject jobj = JObject.Parse(sr.ReadToEnd()); ;

                // pyhton.exeのパス
                pythonExePath = jobj["Python"]["Exe"].ToString();

                // 実行するpythonスクリプトのパス
                pythonScriptPath = jobj["Python"]["Scripy"].ToString();

                // データ蓄積サーバの get_sensor_data のベースUri
                UriGenerator.SetBaseUriGetSensorData(jobj["Uri"]["GetSensorData"].ToString());

                // 地点コンボボックスの初期設定
                for (int i = 0; i < jobj["Point"]["Items"].Count(); i++)
                {
                    comboBoxPointSelect.Items.Add(jobj["Point"]["Items"][i].ToString());
                }
                comboBoxPointSelect.SelectedItem = jobj["Point"]["Initial"].ToString();

                // アルゴリズムコンボボックス、および、正解率配列の初期設定
                accuracyRateArray = new double[jobj["Algorithm"]["Items"]["DisplayName"].Count(), 2];
                for (int i = 0; i < jobj["Algorithm"]["Items"]["DisplayName"].Count(); i++)
                {
                    string key = jobj["Algorithm"]["Items"]["DisplayName"][i].ToString();
                    string value = jobj["Algorithm"]["Items"]["Abbreviation"][i].ToString();
                    double accuracyRate = double.Parse(jobj["Algorithm"]["Items"]["AccuracyRate"][i].ToString());
                    double accuracyRateNextDay = double.Parse(jobj["Algorithm"]["Items"]["AccuracyRateNextDay"][i].ToString());

                    comboBoxAlgorithmSelect.Items.Add(key);
                    algorithmMap.Add(key, value);

                    // 当日の正解率
                    accuracyRateArray[i, 0] = accuracyRate;

                    // 翌日の正解率
                    accuracyRateArray[i, 1] = accuracyRateNextDay;
                }
                comboBoxAlgorithmSelect.SelectedItem = jobj["Algorithm"]["Initial"].ToString();

                // 正解率配列の初期設定
            }

            // コンボボックスの初期設定
            comboBoxTimeSelect.Items.AddRange(new string[] {
                " 0時",
                " 3時",
                " 6時",
                " 9時",
                "12時",
                "15時",
                "18時",
                "21時" }
            );
            comboBoxTimeSelect.SelectedIndex = 4;

            // 予想日コンボボックスの初期設定
            comboBoxExpectedDateSelect.Items.AddRange(new string[] {
                "当日",
                "翌日"
            });
            comboBoxExpectedDateSelect.SelectedIndex = 0;

            // 初期化済みフラグを更新
            isInitialized = true;

            // 正解率ラベルの初期化
            updateAccuracyRateLabel();
        }

        private void comboBoxAlgorithmSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 正解率ラベルの更新
            if (isInitialized)
            {
                updateAccuracyRateLabel();
            }
        }

        private void comboBoxExpectedDateSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 正解率ラベルの更新
            if (isInitialized)
            {
                updateAccuracyRateLabel();
            }
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            // サーバに問い合わせて、jsonデータを取得
            string uriGetSensorData = UriGenerator.GenerateGetSensorDataUri(
                dateTimePicker.Value.Year,
                dateTimePicker.Value.Month,
                dateTimePicker.Value.Day,
                comboBoxTimeSelect.SelectedIndex * 3);
            JArray jsonGetSensorData = JArray.Parse(new HttpClient().GetStringAsync(uriGetSensorData).Result);

            // jsonデータが0件の場合、エラーダイアログを表示してなにもしない。
            if (jsonGetSensorData.Count < 1)
            {
                MessageBox.Show("サーバとの通信に失敗したか、指定した日時のデータが存在しません。",
                    "ERROR",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            // サーバから取得したjsonデータを
            File.WriteAllText(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "get_sensor_data.txt"),
                jsonGetSensorData.ToString());

            // DataGridViewにjsonデータを表示
            dataGridView1.Rows.Clear();
            for (int i = 0; i < jsonGetSensorData.Count; i++)
            {
                dataGridView1.Rows.Add(new string[] {
                    jsonGetSensorData[i]["id"].ToString(),
                    jsonGetSensorData[i]["year"].ToString(),
                    jsonGetSensorData[i]["month"].ToString(),
                    jsonGetSensorData[i]["day"].ToString(),
                    jsonGetSensorData[i]["hour"].ToString(),
                    double.Parse(jsonGetSensorData[i]["temperture"].ToString()).ToString("F1"),
                    double.Parse(jsonGetSensorData[i]["humidity"].ToString()).ToString("F1"),
                    double.Parse(jsonGetSensorData[i]["pressure"].ToString()).ToString("F1")
                });
            }

            // 機械学習ツールへ渡すパラメータを作成
            string csvWeatherStr = json2csv(jsonGetSensorData, comboBoxPointSelect.SelectedIndex + 1);
            if (csvWeatherStr == null)
            {
                MessageBox.Show(string.Format("指定した地点(ID: {0})のデータが存在しません。", comboBoxPointSelect.SelectedIndex + 1),
                    "ERROR",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            string pythonArguments = String.Format(" {0} \"{1}\" {2} {3} {4}",
                (csvWeatherStr.Length - csvWeatherStr.Replace("|", "").Length + 1),
                csvWeatherStr,
                comboBoxExpectedDateSelect.SelectedItem.ToString(),
                algorithmMap[comboBoxAlgorithmSelect.SelectedItem.ToString()],
                comboBoxPointSelect.SelectedItem.ToString());

            // 機械学習ツールへ渡すパラメータを作成
            // ※これは、機械学習ツールとの連携確認用
            /*
            string csvWeatherStr = "2011,12,31,3,3.5,37,1018.1|2011,12,31,6,3.2,41,1019.1|2011,12,31,9,3,48,1020.3|2011,12,31,12,8.6,29,1018.2|2011,12,31,15,9.5,24,1017.8|2011,12,31,18,8.2,30,1019.1|2011,12,31,21,5.4,39,1019.5|2012,1,1,3,4.4,41,1018.4";
            string pythonArguments = String.Format(" {0} {1} {2} {3} {4}",
                (csvWeatherStr.Length - csvWeatherStr.Replace("|", "").Length + 1),
                csvWeatherStr,
                "当日",
                "DT",
                "東京");
            */

            // Python 実行して、終了まで待機
            Process ps = new Process();
            ps.StartInfo.FileName = pythonExePath;
            ps.StartInfo.Arguments = pythonScriptPath + pythonArguments;
            ps.Start();
            ps.WaitForExit();

            // 終了したら、画面に結果を表示
            pictureBoxExpect.Image = GetWeatherBitmap(ps.ExitCode);

            // 正解データを Weather Hacks から取得して表示
            string uriWeatherHacks = UriGenerator.GenerateWeatherHacksUri(comboBoxPointSelect.SelectedItem.ToString());
            JObject jsonWeatherHacks = JObject.Parse(new HttpClient().GetStringAsync(uriWeatherHacks).Result);
            pictureBoxActual.Image = GetWeatherBitmap((string)(jsonWeatherHacks["forecasts"][0]["telop"] as JValue).Value);
        }

        private void updateAccuracyRateLabel()
        {
            accuracyRateLabel.Text = string.Format("正解率 {0:0.00}%",
                accuracyRateArray[comboBoxAlgorithmSelect.SelectedIndex, comboBoxExpectedDateSelect.SelectedIndex]);
        }

        /// <summary>
        /// サーバから取得したjson配列を機械学習ツールに渡せる形式に変換する。
        /// </summary>
        /// <param name="jarray"></param>
        /// <param name="id">今のところ、1始まり？</param>
        /// <returns></returns>
        private string json2csv(JArray jarray, int id)
        {
            // jarrayをIDで絞り込み、jarray2に格納する。
            JArray jarray2 = new JArray();
            for (int i = 0; i < jarray.Count; i++)
            {
                if (int.Parse(jarray[i]["id"].ToString()) == id)
                {
                    jarray2.Add(jarray[i]);
                }
            }

            // 指定されたIDのデータが存在しない場合、nullを返す。
            if (jarray2.Count < 1) return null;

            // CSV文字列化する変数
            bool[] flag = new bool[9];
            int[] year = new int[9];
            int[] month = new int[9];
            int[] day = new int[9];
            int[] hour = new int[9];
            double[] temperture = new double[9];
            double[] humidity = new double[9];
            double[] pressure = new double[9];

            // json配列の先頭データを基に初期化
            DateTime dateTime = new DateTime(
                int.Parse(jarray2[0]["year"].ToString()), int.Parse(jarray2[0]["month"].ToString()), int.Parse(jarray2[0]["day"].ToString()),
                int.Parse(jarray2[0]["hour"].ToString()), 0, 0);
            for (int i = 0; i < 9; i++)
            {
                flag[i] = false;
                year[i] = dateTime.Year;
                month[i] = dateTime.Month;
                day[i] = dateTime.Day;
                hour[i] = dateTime.Hour;
                temperture[i] = 0.0;
                humidity[i] = 0.0;
                pressure[i] = 0.0;
                dateTime = dateTime.AddHours(3);
            }

            // データを格納しながら、複数項目があった場合は、2つの値の平均を取得する。
            for (int i = 0; i < jarray2.Count; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (year[j] != int.Parse(jarray2[i]["year"].ToString())) continue;
                    if (month[j] != int.Parse(jarray2[i]["month"].ToString())) continue;
                    if (day[j] != int.Parse(jarray2[i]["day"].ToString())) continue;
                    if (hour[j] != int.Parse(jarray2[i]["hour"].ToString())) continue;

                    // 既に、データが格納されているかによって処理分岐
                    if (flag[j])
                    {
                        // 既にデータが存在する
                        temperture[j] = (temperture[j] + double.Parse(jarray2[i]["temperture"].ToString())) / 2.0;
                        humidity[j] = (humidity[j] + double.Parse(jarray2[i]["humidity"].ToString())) / 2.0;
                        pressure[j] = (pressure[j] + double.Parse(jarray2[i]["pressure"].ToString())) / 2.0;
                    }
                    else
                    {
                        // 新規のデータ
                        temperture[j] = double.Parse(jarray2[i]["temperture"].ToString());
                        humidity[j] = double.Parse(jarray2[i]["humidity"].ToString());
                        pressure[j] = double.Parse(jarray2[i]["pressure"].ToString());
                    }

                    // フラグを有効かして、次のjsonデータを処理
                    flag[j] = true;
                    break;
                }
            }

            List<string> str = new List<string>();
            for (int i = 0; i < 9; i++)
            {
                if (flag[i])
                {
                    str.Add(String.Format("{0},{1},{2},{3},{4},{5},{6}",
                        year[i], month[i], day[i], hour[i],
                        temperture[i], humidity[i], pressure[i]));
                }
            }

            if (1 <= str.Count)
            {
                return String.Join("|", str.ToArray());
            }
            return null;
        }

        /// <summary>
        /// 天気を示す文字列から画面に表示する画像を取得します。
        /// </summary>
        /// <param name="weatherStr">天気を示す文字列</param>
        /// <returns>対応する画像</returns>
        private Bitmap GetWeatherBitmap(string weatherStr)
        {
            Bitmap bitmap; ;
            switch (weatherStr.Substring(0, 1))
            {
                case "晴":
                    bitmap = Properties.Resources.sunny;
                    break;
                case "曇":
                    bitmap = Properties.Resources.cloudy;
                    break;
                case "雨":
                    bitmap = Properties.Resources.raining;
                    break;
                case "雪":
                    bitmap = Properties.Resources.snowy;
                    break;
                default:
                    bitmap = Properties.Resources.cloudy;
                    break;
            }
            return bitmap;
        }

        /// <summary>
        /// 天気を示す文字列から画面に表示する画像を取得します。
        /// </summary>
        /// <param name="weatherStr">天気を示す文字列</param>
        /// <returns>対応する画像</returns>
        private Bitmap GetWeatherBitmap(int weatherCode)
        {
            Bitmap bitmap; ;
            switch (weatherCode)
            {
                case 0:
                    bitmap = Properties.Resources.sunny;
                    break;
                case 1:
                    bitmap = Properties.Resources.cloudy;
                    break;
                case 2:
                    bitmap = Properties.Resources.raining;
                    break;
                case 3:
                    bitmap = Properties.Resources.snowy;
                    break;
                default:
                    bitmap = Properties.Resources.cloudy;
                    break;
            }
            return bitmap;
        }
    }
}
