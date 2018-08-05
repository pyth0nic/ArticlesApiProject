using System.Globalization;

namespace Articles.Db.Repositories.Util
{
    public static class TagNormaliser
    {
        public static string Normalise(string tag)
        {
            // todo put culture info in config
            TextInfo textInfo = new CultureInfo("en-AU",false).TextInfo;
            return textInfo.ToTitleCase(tag);
        }
    }
}