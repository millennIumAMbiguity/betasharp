namespace betareborn
{
    public class StringTranslate : java.lang.Object
    {
        private static readonly StringTranslate instance = new();
        private readonly java.util.Properties translateTable = new();

        private StringTranslate()
        {
            try
            {
                translateTable.load(new java.io.StringReader(AssetManager.Instance.getAsset("lang/en_US.lang").getTextContent()));
                translateTable.load(new java.io.StringReader(AssetManager.Instance.getAsset("lang/stats_US.lang").getTextContent()));
            }
            catch (java.io.IOException var2)
            {
                var2.printStackTrace();
            }

        }

        public static StringTranslate getInstance()
        {
            return instance;
        }

        public string translateKey(string var1)
        {
            return translateTable.getProperty(var1, var1);
        }

        public string translateKeyFormat(string var1, params object[] var2)
        {
            string var3 = translateTable.getProperty(var1, var1);
            return string.Format(var3, var2);
        }

        public string translateNamedKey(string var1)
        {
            return translateTable.getProperty(var1 + ".name", "");
        }
    }
}