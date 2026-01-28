using betareborn.Stats;

namespace betareborn.Threading
{
    public class ThreadStatSyncherReceive : java.lang.Thread
    {
        readonly StatsSyncher field_27231_a;

        public ThreadStatSyncherReceive(StatsSyncher var1)
        {
            field_27231_a = var1;
        }


        public override void run()
        {
            try
            {
                if (StatsSyncher.func_27422_a(field_27231_a) != null)
                {
                    StatsSyncher.func_27412_a(field_27231_a, StatsSyncher.func_27422_a(field_27231_a), StatsSyncher.func_27423_b(field_27231_a), StatsSyncher.func_27411_c(field_27231_a), StatsSyncher.func_27413_d(field_27231_a));
                }
                else if (StatsSyncher.func_27423_b(field_27231_a).exists())
                {
                    StatsSyncher.func_27421_a(field_27231_a, StatsSyncher.func_27409_a(field_27231_a, StatsSyncher.func_27423_b(field_27231_a), StatsSyncher.func_27411_c(field_27231_a), StatsSyncher.func_27413_d(field_27231_a)));
                }
            }
            catch (java.lang.Exception var5)
            {
                var5.printStackTrace();
            }
            finally
            {
                StatsSyncher.func_27416_a(field_27231_a, false);
            }

        }
    }

}