using betareborn.Stats;
using java.util;

namespace betareborn.Threading
{
    public class ThreadStatSyncherSend : java.lang.Thread
    {
        readonly Map field_27233_a;
        readonly StatsSyncher field_27232_b;

        public ThreadStatSyncherSend(StatsSyncher var1, Map var2)
        {
            field_27232_b = var1;
            field_27233_a = var2;
        }


        public override void run()
        {
            try
            {
                StatsSyncher.func_27412_a(field_27232_b, field_27233_a, StatsSyncher.func_27414_e(field_27232_b), StatsSyncher.func_27417_f(field_27232_b), StatsSyncher.func_27419_g(field_27232_b));
            }
            catch (java.lang.Exception var5)
            {
                var5.printStackTrace();
            }
            finally
            {
                StatsSyncher.func_27416_a(field_27232_b, false);
            }

        }
    }

}