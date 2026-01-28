using java.io;
using java.util.regex;

namespace betareborn.Chunks
{
    public class ChunkFilePattern : java.lang.Object, FilenameFilter
    {
        public static readonly Pattern field_22189_a = Pattern.compile("c\\.(-?[0-9a-z]+)\\.(-?[0-9a-z]+)\\.dat");

        private ChunkFilePattern()
        {
        }

        public bool accept(java.io.File var1, string var2)
        {
            Matcher var3 = field_22189_a.matcher(var2);
            return var3.matches();
        }

        public ChunkFilePattern(Empty2 var1) : this()
        {
        }

    }
}