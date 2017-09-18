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
        private string pythonExePath = "python.exe";
        private string pythonScriptPath = "python.py";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 設定ファイルをロード
            List<string> pointItems = new List<string>();
            using (StreamReader sr = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\div22.json"))
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
            comboBoxTimeSelect.SelectedIndex = 0;

            // 予想日コンボボックスの初期設定
            comboBoxExpectedDateSelect.Items.AddRange(new string[] {
                "翌日",
                "今日"
            });
            comboBoxExpectedDateSelect.SelectedIndex = 0;
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            // 正解データを Weather Hacks から取得して表示
            string uriWeatherHacks = UriGenerator.GenerateWeatherHacksUri("東京");
            JObject jsonWeatherHacks = JObject.Parse(new HttpClient().GetStringAsync(uriWeatherHacks).Result);
            pictureBoxActual.Image = GetWeatherBitmap((string)(jsonWeatherHacks["forecasts"][0]["telop"] as JValue).Value);

            // サーバに問い合わせて、jsonデータを取得
            string uriGetSensorData = UriGenerator.GenerateGetSensorDataUri(
                dateTimePicker.Value.Year,
                dateTimePicker.Value.Month, 
                dateTimePicker.Value.Day,
                comboBoxTimeSelect.SelectedIndex * 3);
            JArray jsonGetSensorData = JArray.Parse(new HttpClient().GetStringAsync(uriGetSensorData).Result);

            // 一旦、ファイルに保存
            File.WriteAllText(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "tokyo_weather.txt"),
                jsonGetSensorData.ToString());

            // DataGridViewにjsonデータを表示
            //TODO: 

            // Python 実行して、終了まで待機
            Process ps = new Process();
            ps.StartInfo.FileName = pythonExePath;
            ps.StartInfo.Arguments = pythonScriptPath;
            ps.Start();
            ps.WaitForExit();

            // 終了したら、画面に結果を表示
            pictureBoxExpect.Image = GetWeatherBitmap(ps.ExitCode);
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
