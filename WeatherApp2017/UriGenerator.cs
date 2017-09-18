using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp2017
{
    class UriGenerator
    {
        private static string mBaseUriGetSensorData = null;

        public static void SetBaseUriGetSensorData(string baseUri)
        {
            mBaseUriGetSensorData = baseUri;
        }

        public static string GenerateGetSensorDataUri(int year, int month, int day, int hour)
        {
            return String.Format(mBaseUriGetSensorData, year, month, day, hour);
        }

        public static string GenerateWeatherHacksUri(string city)
        {
            return String.Format(
                "http://weather.livedoor.com/forecast/webservice/json/v1?city={0}",
                GetWeatherHacksCityCode(city));
        }

        private static int GetWeatherHacksCityCode(string city)
        {
            switch (city)
            {
                // 関東地方のみ
                case "水戸": return 80010;
                case "土浦": return 80020;
                case "宇都宮": return 90010;
                case "大田原": return 90020;
                case "前橋": return 100010;
                case "みなかみ": return 100020;
                case "さいたま": return 110010;
                case "熊谷": return 110020;
                case "秩父": return 110030;
                case "千葉": return 120010;
                case "銚子": return 120020;
                case "館山": return 120030;
                case "東京": return 130010;
                case "大島": return 130020;
                case "八丈島": return 130030;
                case "父島": return 130040;
                case "横浜": return 140010;
                case "小田原": return 140020;
            }
            // デフォルトは東京
            return 130010;
        }
    }
}
