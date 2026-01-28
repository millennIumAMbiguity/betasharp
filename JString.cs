namespace betareborn
{
    public class JString : java.lang.Object
    {
        public string value;

        public JString(string s)
        {
            value = s;
        }

        public override bool equals(object obj)
        {
            return value.Equals(obj);
        }

        public override int hashCode()
        {
            return value.GetHashCode();
        }
    }
}
