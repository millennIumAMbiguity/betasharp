using java.awt;

namespace betareborn
{
    public class CanvasCrashReport : Canvas
    {
        public CanvasCrashReport(int var1)
        {
            setPreferredSize(new(var1, var1));
            setMinimumSize(new(var1, var1));
        }
    }
}