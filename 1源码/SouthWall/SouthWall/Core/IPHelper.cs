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
        public string Country {  get; set; }
        public string City { get; set; }
        public string RegionName {  get; set; }
        public string Lat {  get; set; }
        public string Lon {  get; set; }
    }
}
