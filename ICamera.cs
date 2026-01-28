namespace betareborn
{
    public interface ICamera
    {
        bool isBoundingBoxInFrustum(AxisAlignedBB var1);

        void setPosition(double var1, double var3, double var5);
    }

}