namespace SouthWall
{
    public class Config
    {
        public static readonly string SiteName = AppSettingsHelper.AppSetting("SiteName");
        public static readonly string SiteKeyWords = AppSettingsHelper.AppSetting("SiteKeyWords");
        public static readonly string SiteDescription = AppSettingsHelper.AppSetting("SiteDescription");
        public static readonly string DBConnectionString = AppSettingsHelper.AppSetting("DBConnectionString");
        public static readonly string JWTKey = AppSettingsHelper.AppSetting("JWTKey");
        public static readonly int JWTExpired = AppSettingsHelper.AppSetting("JWTExpired").ToInt32();
        public static readonly string AdminEmail = AppSettingsHelper.AppSetting("AdminEmail");
        public static readonly string ServiceMail = AppSettingsHelper.AppSetting("ServiceMail");
        public static readonly string ServiceMailName = AppSettingsHelper.AppSetting("ServiceMailName");
        public static readonly string ServiceMailPassword = AppSettingsHelper.AppSetting("ServiceMailPassword");
        public static readonly int ServiceMailPort = AppSettingsHelper.AppSetting("ServiceMailPort").ToInt32();
        public static readonly bool ServiceMailSSL = AppSettingsHelper.AppSetting("ServiceMailSSL").ToBoolean();
        public static readonly string ServiceMailSMTP = AppSettingsHelper.AppSetting("ServiceMailSMTP");
    }
}
