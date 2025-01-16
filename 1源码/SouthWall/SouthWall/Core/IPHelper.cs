using IPGeolocation;
using Org.BouncyCastle.Bcpg.OpenPgp;

namespace SouthWall
{
    public class IPHelper
    {
        public static async Task<IPInfo> GetIPInfo(string ip)
        {
            //return null;
            string url = $"http://ip-api.com/json/{ip}";
            string res = await HttpHelper.GetAsync(url);
            if (!string.IsNullOrEmpty(res))
            {
                IPInfo info = res.ToObject<IPInfo>();
                if (info.Status == "fail")
                {
                    return null;
                }
                return info;
            }
            return new IPInfo();
        }
        public static async Task<IPInfoByBaidu> GetIPInfoByBaidu(string ip)
        {
            //return null;
            string url = $"https://qifu-api.baidubce.com/ip/geo/v1/district?ip={ip}";
            string res = await HttpHelper.GetAsync(url);
            if (!string.IsNullOrEmpty(res))
            {
                IPInfoByBaiduResponse info = res.ToObject<IPInfoByBaiduResponse>();
                if (info.Code != "Success")
                {
                    return null;
                }
                return info.Data;
            }
            return new IPInfoByBaidu();
        }
        public static IPInfo GetIPInfoByipgeolocation(string ip)
        {
            // Get geolocation for IP address (1.1.1.1) and fields (geo, time_zone and currency)
            IPGeolocationAPI api = new IPGeolocationAPI(Config.IPGeolocationAPIKey);
            // Get geolocation in Russian** for IP address (1.1.1.1) and all fields
            GeolocationParams geoParams = new GeolocationParams();
            geoParams.SetIPAddress(ip);
            geoParams.SetLang("cn");
            try
            {
                var res = api.GetGeolocation(geoParams);
                if (res.ContainsKey("status") && res["status"].ToString() == "200" && res.ContainsKey("response"))
                {
                    Geolocation geolocation = res["response"] as Geolocation;
                    IPInfo ipInfo = new IPInfo
                    {
                        Country = geolocation.GetCountryName(),
                        RegionName = geolocation.GetStateProvince(),
                        City = geolocation.GetCity(),
                        Lat = geolocation.GetLatitude(),
                        Lon = geolocation.GetLongitude(),
                        District = geolocation.GetDistrict()
                    };
                    return ipInfo;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }

            //// Check if geolocation lookup was successful
            //if (geolocation.GetStatus() == 200)
            //{
            //    Console.WriteLine(geolocation.GetIPAddress());
            //    Console.WriteLine(geolocation.GetCountryName());
            //}
            //else
            //{
            //    Console.WriteLine(geolocation.GetMessage());
            //}

            //// Query geolocation for the calling machine's IP address for all fields
            //Geolocation geolocation = api.GetGeolocation();

            //if (geolocation.GetStatus() == 200)
            //{
            //    Console.WriteLine(geolocation.GetCountryCode2());
            //    Console.WriteLine(geolocation.GetTimezone().GetCurrentTime());
            //}
            //else
            //{
            //    Console.WriteLine(geolocation.GetMessage());
            //}
        }
    }
    public class IPInfo
    {
        public string Status { get; set; }
        public string District { get; set; }
        private string _country;
        public string Country
        {
            get
            {
                if (_country == "Hong Kong" || _country == "Taiwan")
                {
                    return "China";
                }
                return _country;
            }
            set
            {
                _country = value;
            }
        }
        private string _city;
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
            }
        }
        private string _regionName;
        public string RegionName
        {
            get
            {
                if (_country == "Hong Kong" || _country == "Taiwan")
                {
                    return _country;
                }
                if (_regionName == "Sai Kung District" || _regionName == "Central and Western District" || _regionName == "Kowloon City" || _regionName == "Kwai Tsing")
                {
                    return "Hong Kong";
                }
                if (_city == "Chai Wan")
                {
                    return "Hong Kong";
                }
                return _regionName;
            }
            set
            {
                _regionName = value;
            }
        }
        public string Lat { get; set; }
        public string Lon { get; set; }
    }
    public class IPInfoByBaiduResponse
    {
        public string Code { get; set; }
        public IPInfoByBaidu Data { get; set; }
    }
    public class IPInfoByBaidu
    {
        public string country { get; set; }
        public string city { get; set; }
        public string prov { get; set; }
        public string district { get; set; }
    }

    public class IPInfoByInIPResponse
    {
        public int Code { get; set; }
        public IPInfoByInIP Data { get; set; }
    }
    public class IPInfoByInIP
    {
        public string asn { get; set; }
        public string country { get; set; }
        public string country_cn { get; set; }
        public string country_code { get; set; }
        public string city { get; set; }
        public string city_cn { get; set; }
        public string city_code { get; set; }
        public string continent { get; set; }
        public string continent_cn { get; set; }
        public string continent_code { get; set; }
        public string ip { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string region { get; set; }
        public string region_cn { get; set; }
        public string region_code { get; set; }
    }
}
