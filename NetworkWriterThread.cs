using java.lang;

namespace betareborn
{
    public class NetworkWriterThread : java.lang.Thread
    {
        public readonly NetworkManager netManager;

        public NetworkWriterThread(NetworkManager var1, string var2) : base(var2)
        {
            this.netManager = var1;
        }


        public override void run()
        {
            object var1 = NetworkManager.threadSyncObject;
            lock (var1)
            {
                ++NetworkManager.numWriteThreads;
            }

            while (true)
            {
                bool var13 = false;

                try
                {
                    var13 = true;
                    if (!NetworkManager.isRunning(this.netManager))
                    {
                        var13 = false;
                        break;
                    }

                    while (NetworkManager.sendNetworkPacket(this.netManager))
                    {
                    }

                    try
                    {
                        sleep(100L);
                    }
                    catch (InterruptedException var16)
                    {
                    }

                    try
                    {
                        if (NetworkManager.func_28140_f(this.netManager) != null)
                        {
                            NetworkManager.func_28140_f(this.netManager).flush();
                        }
                    }
                    catch (java.io.IOException var18)
                    {
                        if (!NetworkManager.func_28138_e(this.netManager))
                        {
                            NetworkManager.func_30005_a(this.netManager, var18);
                        }

                        var18.printStackTrace();
                    }
                }
                finally
                {
                    if (var13)
                    {
                        object var5 = NetworkManager.threadSyncObject;
                        lock (var5)
                        {
                            --NetworkManager.numWriteThreads;
                        }
                    }
                }
            }

            var1 = NetworkManager.threadSyncObject;
            lock (var1)
            {
                --NetworkManager.numWriteThreads;
            }
        }
    }

}