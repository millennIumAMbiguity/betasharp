using java.lang;

namespace betareborn
{
    public class NetworkMasterThread : java.lang.Thread
    {
        public readonly NetworkManager netManager;

        public NetworkMasterThread(NetworkManager var1)
        {
            netManager = var1;
        }


        public override void run()
        {
            try
            {
                java.lang.Thread.sleep(5000L);
                if (NetworkManager.getReadThread(this.netManager).isAlive())
                {
                    try
                    {
                        NetworkManager.getReadThread(this.netManager).stop();
                    }
                    catch (Throwable var3)
                    {
                    }
                }

                if (NetworkManager.getWriteThread(this.netManager).isAlive())
                {
                    try
                    {
                        NetworkManager.getWriteThread(this.netManager).stop();
                    }
                    catch (Throwable var2)
                    {
                    }
                }
            }
            catch (InterruptedException var4)
            {
                var4.printStackTrace();
            }

        }
    }

}