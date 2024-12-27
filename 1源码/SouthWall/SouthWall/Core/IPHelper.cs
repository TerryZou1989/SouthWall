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
        public string City { get; set; }
        private string _regionName;
        public string RegionName { 
            get {
                if (_country == "Hong Kong" || _country == "Taiwan")
                {
                    return _country;
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
