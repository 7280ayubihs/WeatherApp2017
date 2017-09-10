using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace WeatherApp2017
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            // 地点A（Tokyo）
            JObject jObjectA = JObject.Parse(new HttpClient().GetStringAsync("http://weather.livedoor.com/forecast/webservice/json/v1?city=130010").Result);
            pictureBoxPointA.Image = getWeatherBitmap((string)(jObjectA["forecasts"][0]["telop"] as JValue).Value);

            // 地点B（Yokohama）
            JObject jObjectB = JObject.Parse(new HttpClient().GetStringAsync("http://weather.livedoor.com/forecast/webservice/json/v1?city=140010").Result);
            pictureBoxPointB.Image = getWeatherBitmap((string)(jObjectA["forecasts"][0]["telop"] as JValue).Value);

            // 地点C（Mito）
            JObject jObjectC = JObject.Parse(new HttpClient().GetStringAsync("http://weather.livedoor.com/forecast/webservice/json/v1?city=080010").Result);
            pictureBoxPointC.Image = getWeatherBitmap((string)(jObjectA["forecasts"][0]["telop"] as JValue).Value);
        }

        /// <summary>
        /// 天気を示す文字列から画面に表示する画像を取得します。
        /// </summary>
        /// <param name="weatherStr">天気を示す文字列</param>
        /// <returns>対応する画像</returns>
        private Bitmap getWeatherBitmap(string weatherStr)
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
