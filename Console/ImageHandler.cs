namespace Console;
using Emgu.CV;
using Emgu.CV.Structure;

public class ImageHandler
{
    public static void CopyToImage<TColor1, TColor2, TDepth>(ref Image<TColor1, TDepth> source, ref Image<TColor2, TDepth> dst, int offsetX = 0, int offsetY = 0)
        where TColor1 : struct, IColor
        where TColor2 : struct, IColor
        where TDepth : new()
    {
        TDepth[,,] sourceData = source.Data;
        TDepth[,,] dstData = dst.Data;
        for (uint y = 0; y < sourceData.GetLength(0); y++)
        {
            for (uint x = 0; x < sourceData.GetLength(1); x++)
            {
                for (uint z = 0; z < 3; z++)
                {
                    if (x + offsetX < dstData.GetLength(1) && y + offsetY < dstData.GetLength(0))
                    {
                        if (sourceData.GetLength(2) > 1)
                            dstData[y + offsetY, x + offsetX, z] = sourceData[y, x, z];
                        else
                            dstData[y + offsetY, x + offsetX, z] = sourceData[y, x, 0];
                    }
                }
            }
        }
        dst.Data = dstData;
    }

    public static void GrayCopyChannelToImage<TColor1, TColor2, TDepth>(ref Image<TColor1, TDepth> source, ref Image<TColor2, TDepth> dst, int canal, int offsetX = 0, int offsetY = 0)
        where TColor1 : struct, IColor
        where TColor2 : struct, IColor
        where TDepth : new()
    {
        TDepth[,,] sourceData = source.Data;
        TDepth[,,] dstData = dst.Data;
        for (uint y = 0; y < sourceData.GetLength(0); y++)
        {
            for (uint x = 0; x < sourceData.GetLength(1); x++)
            {
                for (uint z = 0; z < sourceData.GetLength(2); z++)
                {
                    if (x + offsetX < dstData.GetLength(1) && y + offsetY < dstData.GetLength(0))
                    {
                        dstData[y + offsetY, x + offsetX, z] = sourceData[y, x, canal];
                    }
                }
            }
        }
        dst.Data = dstData;
    }
}