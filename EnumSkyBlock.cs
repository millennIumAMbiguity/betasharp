namespace betareborn
{
    public readonly struct EnumSkyBlock : IEquatable<EnumSkyBlock>
    {
        public static readonly EnumSkyBlock Sky = new(15);
        public static readonly EnumSkyBlock Block = new(0);

        public readonly int field_1722_c;

        private EnumSkyBlock(int var3)
        {
            field_1722_c = var3;
        }

        public override bool Equals(object? obj)
        {
            return obj is EnumSkyBlock other && Equals(other);
        }

        public bool Equals(EnumSkyBlock other)
        {
            return field_1722_c == other.field_1722_c;
        }

        public static bool operator ==(EnumSkyBlock left, EnumSkyBlock right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(EnumSkyBlock left, EnumSkyBlock right)
        {
            return !(left == right);
        }
    }
}