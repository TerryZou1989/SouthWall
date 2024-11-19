namespace SouthWall
{
    public class AppSettingsHelper
    {
        private static IConfigurationSection appSections = null;
        public static string AppSetting(string key)
        {
            string str = string.Empty;
            if (appSections.GetSection(key) != null)
            {
                str = appSections.GetSection(key).Value;
            }
            return str;
        }
        public static T AppSetting<T>(string key)
        {
            T t = default(T);
            if (appSections.GetSection(key) != null)
            {
                t = appSections.GetSection(key).Get<T>();
            }
            return t;
        }
        

        public static void SetAppSettings(IConfigurationSection section)
        {
            appSections = section;         
        }
    }
}
