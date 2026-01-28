using java.net;

namespace betareborn.Threading
{
    public class ThreadCheckHasPaid : java.lang.Thread
    {
        public readonly Minecraft field_28146_a;


        public ThreadCheckHasPaid(Minecraft var1)
        {
            this.field_28146_a = var1;
        }

        public override void run()
        {
            try
            {
                HttpURLConnection var1 = (HttpURLConnection)(new URL("https://login.minecraft.net/session?name=" + this.field_28146_a.session.username + "&session=" + this.field_28146_a.session.sessionId)).openConnection();
                var1.connect();
                if (var1.getResponseCode() == 400)
                {
                    Minecraft.hasPaidCheckTime = java.lang.System.currentTimeMillis();
                }

                var1.disconnect();
            }
            catch (java.lang.Exception var2)
            {
                var2.printStackTrace();
            }

        }
    }

}