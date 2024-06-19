using System.Globalization;
using System.Resources;

namespace MyNamespace
{
    public static class Localization
    {
        private static ResourceManager _scriptResourceManager = new ResourceManager("WPF_Calculator.Resources.Texts.Script", typeof(Localization).Assembly);

        public static string GetString(string key)
        {
            string? value = _scriptResourceManager.GetString(key, CultureInfo.CurrentUICulture);
            
            if (value == null)
            {
                throw new KeyNotFoundException();
            }
            
            return value;
        }
    }
}