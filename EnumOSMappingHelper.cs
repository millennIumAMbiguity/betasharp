using java.lang;

namespace betareborn
{
    public class EnumOSMappingHelper
    {
        public static readonly int[] enumOSMappingArray = new int[System.Enum.GetValues<EnumOS2>().Length];

        static EnumOSMappingHelper()
        {
            try
            {
                enumOSMappingArray[(int)EnumOS2.linux] = 1;
            }
            catch (NoSuchFieldError var4)
            {
            }

            try
            {
                enumOSMappingArray[(int)EnumOS2.solaris] = 2;
            }
            catch (NoSuchFieldError var3)
            {
            }

            try
            {
                enumOSMappingArray[(int)EnumOS2.windows] = 3;
            }
            catch (NoSuchFieldError var2)
            {
            }

            try
            {
                enumOSMappingArray[(int)EnumOS2.macos] = 4;
            }
            catch (NoSuchFieldError var1)
            {
            }

        }
    }

}