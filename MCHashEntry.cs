using java.lang;

namespace betareborn
{
    internal class MCHashEntry : java.lang.Object
    {
        public readonly int hashEntry;
        public java.lang.Object valueEntry;
        public MCHashEntry nextEntry;
        public readonly int slotHash;

        public MCHashEntry(int var1, int var2, java.lang.Object var3, MCHashEntry var4)
        {
            valueEntry = var3;
            nextEntry = var4;
            hashEntry = var2;
            slotHash = var1;
        }

        public int getHash()
        {
            return hashEntry;
        }

        public java.lang.Object getValue()
        {
            return valueEntry;
        }

        public override bool equals(object var1)
        {
            if (var1 is not MCHashEntry)
            {
                return false;
            }
            else
            {
                MCHashEntry var2 = (MCHashEntry)var1;
                Integer var3 = Integer.valueOf(getHash());
                Integer var4 = Integer.valueOf(var2.getHash());
                if (var3 == var4 || var3 != null && var3.equals(var4))
                {
                    java.lang.Object var5 = getValue();
                    java.lang.Object var6 = var2.getValue();
                    if (var5 == var6 || var5 != null && var5.equals(var6))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public override int hashCode()
        {
            return MCHash.getHash(hashEntry);
        }

        public override string toString()
        {
            return getHash() + "=" + getValue();
        }
    }

}