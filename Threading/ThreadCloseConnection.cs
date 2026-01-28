namespace betareborn.Threading
{
    public class ThreadCloseConnection : java.lang.Thread
    {
        public readonly NetworkManager field_28109_a;

        public ThreadCloseConnection(NetworkManager var1)
        {
            this.field_28109_a = var1;
        }


        public override void run()
        {
            try
            {
                java.lang.Thread.sleep(2000L);
                if (NetworkManager.isRunning(this.field_28109_a))
                {
                    NetworkManager.getWriteThread(this.field_28109_a).interrupt();
                    this.field_28109_a.networkShutdown("disconnect.closed", new Object[0]);
                }
            }
            catch (java.lang.Exception var2)
            {
                var2.printStackTrace();
            }

        }
    }

}