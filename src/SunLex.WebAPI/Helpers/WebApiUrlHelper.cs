namespace SunLex.WebAPI.Helpers
{
    public static class WebApiUrlHelper
    {
        public static string GetWordsTranslationsUrl(string dictionaryName)
            => $"https://localhost:44385/dict/{dictionaryName}/translations/";
    }
}