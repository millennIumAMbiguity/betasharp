namespace betareborn
{
    public class ChatLine : java.lang.Object
    {
        public string message;
        public int updateCounter;

        public ChatLine(string var1)
        {
            message = var1;
            updateCounter = 0;
        }
    }
}