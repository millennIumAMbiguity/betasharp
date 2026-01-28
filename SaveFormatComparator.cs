using java.lang;

namespace betareborn
{
    public class SaveFormatComparator : java.lang.Object, Comparable
    {
        private readonly string fileName;
        private readonly string displayName;
        private readonly long field_22169_c;
        private readonly long field_22168_d;
        private readonly bool field_22167_e;

        public SaveFormatComparator(string var1, string var2, long var3, long var5, bool var7)
        {
            fileName = var1;
            displayName = var2;
            field_22169_c = var3;
            field_22168_d = var5;
            field_22167_e = var7;
        }

        public string getFileName()
        {
            return fileName;
        }

        public string getDisplayName()
        {
            return displayName;
        }

        public long func_22159_c()
        {
            return field_22168_d;
        }

        public bool func_22161_d()
        {
            return field_22167_e;
        }

        public long func_22163_e()
        {
            return field_22169_c;
        }

        public int func_22160_a(SaveFormatComparator var1)
        {
            return field_22169_c < var1.field_22169_c ? 1 : (field_22169_c > var1.field_22169_c ? -1 : fileName.CompareTo(var1.fileName));
        }

        public int CompareTo(object? var1)
        {
            return func_22160_a((SaveFormatComparator)var1!);
        }
    }
}