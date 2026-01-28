using java.lang;

namespace betareborn
{
    public class EnumOptionsMappingHelper : java.lang.Object
    {
        public static readonly int[] enumOptionsMappingHelperArray = new int[EnumOptions.values().Length];

        static EnumOptionsMappingHelper()
        {
            try
            {
                enumOptionsMappingHelperArray[EnumOptions.INVERT_MOUSE.ordinal()] = 1;
            }
            catch (NoSuchFieldError var5)
            {
            }

            try
            {
                enumOptionsMappingHelperArray[EnumOptions.VIEW_BOBBING.ordinal()] = 2;
            }
            catch (NoSuchFieldError var4)
            {
            }

            try
            {
                enumOptionsMappingHelperArray[EnumOptions.ADVANCED_OPENGL.ordinal()] = 4;
            }
            catch (NoSuchFieldError var2)
            {
            }

            try
            {
                enumOptionsMappingHelperArray[EnumOptions.AMBIENT_OCCLUSION.ordinal()] = 5;
            }
            catch (NoSuchFieldError var1)
            {
            }

        }
    }

}