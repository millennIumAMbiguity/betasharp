using java.io;
using java.util.regex;

namespace betareborn.Chunks
{
    public class ChunkFolderPattern : FileFilter
    {

        public static readonly Pattern field_22392_a = Pattern.compile("[0-9a-z]|([0-9a-z][0-9a-z])");

        private ChunkFolderPattern()
        {
        }

        public bool accept(java.io.File var1)
        {
            if (var1.isDirectory())
            {
                Matcher var2 = field_22392_a.matcher(var1.getName());
                return var2.matches();
            }
            else
            {
                return false;
            }
        }

        public ChunkFolderPattern(Empty2 var1) : this()
        {
        }
    }

}