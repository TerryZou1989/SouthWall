using Org.BouncyCastle.Bcpg.OpenPgp;

namespace SouthWall
{
    public class IPHelper
    {
        public static async Task<IPInfo> GetIPInfo(string ip)
        {
            string url = $"http://ip-api.com/json/{ip}";
            string res=await HttpHelper.GetAsync(url) ;
            if(!string.IsNullOrEmpty(res))
            {
                IPInfo info = res.ToObject<IPInfo>() ;
                return info ;
            }
            return new IPInfo();
        }
    }
    public class IPInfo
    {
        private string _country;
        public string Country { get {
                if(_country == "Hong Kong"|| _country == "Taiwan")
                {
                    return "China";                    
                }
                return _country;
            } set { 
                    _country = value;               
            } 
        }
        private string _city;
        public string City
        {
            get { return _city; } set
            {
                _city = value;
            }
        }
        private string _regionName;
        public string RegionName { 
            get {
                if (_country == "Hong Kong" || _country == "Taiwan")
                {
                    return _country;
                }
                if (_regionName == "Sai Kung District"||_regionName== "Central and Western District"||_regionName== "Kowloon City"||_regionName== "Kwai Tsing")
                {
                    return "Hong Kong";
                }
                if(_city== "Chai Wan")
                {
                    return "Hong Kong";
                }
                return _regionName;
            }
            set {
                _regionName = value;
            }
        }
        public string Lat {  get; set; }
        public string Lon {  get; set; }
    }
}
