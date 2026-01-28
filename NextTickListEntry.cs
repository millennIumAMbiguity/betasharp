using java.lang;

namespace betareborn
{
    public class NextTickListEntry : java.lang.Object, Comparable
    {
        private static long nextTickEntryID = 0L;
        public int xCoord;
        public int yCoord;
        public int zCoord;
        public int blockID;
        public long scheduledTime;
        private readonly long tickEntryID = nextTickEntryID++;

        public NextTickListEntry(int var1, int var2, int var3, int var4)
        {
            xCoord = var1;
            yCoord = var2;
            zCoord = var3;
            blockID = var4;
        }

        public override bool equals(object var1)
        {
            if (var1 is not NextTickListEntry)
            {
                return false;
            }
            else
            {
                NextTickListEntry var2 = (NextTickListEntry)var1;
                return xCoord == var2.xCoord && yCoord == var2.yCoord && zCoord == var2.zCoord && blockID == var2.blockID;
            }
        }

        public override int hashCode()
        {
            return (xCoord * 128 * 1024 + zCoord * 128 + yCoord) * 256 + blockID;
        }

        public NextTickListEntry setScheduledTime(long var1)
        {
            scheduledTime = var1;
            return this;
        }

        public int comparer(NextTickListEntry var1)
        {
            return scheduledTime < var1.scheduledTime ? -1 : (scheduledTime > var1.scheduledTime ? 1 : (tickEntryID < var1.tickEntryID ? -1 : (tickEntryID > var1.tickEntryID ? 1 : 0)));
        }

        public int CompareTo(object? var1)
        {
            return comparer((NextTickListEntry)var1!);
        }
    }
}