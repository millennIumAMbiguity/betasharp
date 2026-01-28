using java.lang;

namespace betareborn
{
    class NetworkReaderThread : java.lang.Thread
    {
        public readonly NetworkManager netManager;

        public NetworkReaderThread(NetworkManager var1, string var2) : base(var2)
        {
            this.netManager = var1;
        }


        public override void run()
        {
            object var1 = NetworkManager.threadSyncObject;
            lock (var1)
            {
                ++NetworkManager.numReadThreads;
            }

            while (true)
            {
                bool var12 = false;

                try
                {
                    var12 = true;
                    if (!NetworkManager.isRunning(this.netManager))
                    {
                        var12 = false;
                        break;
                    }

                    if (NetworkManager.isServerTerminating(this.netManager))
                    {
                        var12 = false;
                        break;
                    }

                    while (NetworkManager.readNetworkPacket(this.netManager))
                    {
                    }

                    try
                    {
                        sleep(100L);
                    }
                    catch (InterruptedException var15)
                    {
                    }
                }
                finally
                {
                    if (var12)
                    {
                        object var5 = NetworkManager.threadSyncObject;
                        lock (var5)
                        {
                            --NetworkManager.numReadThreads;
                        }
                    }
                }
            }

            var1 = NetworkManager.threadSyncObject;
            lock (var1)
            {
                --NetworkManager.numReadThreads;
            }
        }
    }

}