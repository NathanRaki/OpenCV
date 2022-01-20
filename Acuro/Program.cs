using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;
using Emgu.CV.Aruco;

namespace Acuro
{
    class AcuroOpencv
    {
        public void Start(string fileName)
        {
            Mat image = new Mat(fileName);
            // Markers ID
            VectorOfInt markersId = new VectorOfInt();
            // marker corners & rejected candidates
            VectorOfVectorOfPointF markersCorner = new VectorOfVectorOfPointF();
            VectorOfVectorOfPointF rejectedCandidates = new VectorOfVectorOfPointF();
            // Detector parameters for tuning the algorithm
            DetectorParameters parameters = DetectorParameters.GetDefault();
            // dictionary of aruco's markers
            Dictionary dictMarkers = new Dictionary(Dictionary.PredefinedDictionaryName.Dict6X6_250);
            Console.WriteLine(image.Cols);
            /*
            // convert image
            Mat grayFrame = new Mat(image.Width, image.Height, DepthType.Cv8U, 1);
            CvInvoke.CvtColor(image, grayFrame, ColorConversion.Bgr2Gray);
            // detect markers
            ArucoInvoke.DetectMarkers(image, dictMarkers, markersCorner, markersId, parameters, rejectedCandidates);

            Mat displayImage = new Mat(image.Width, image.Height, DepthType.Cv8U, 3);
            image.CopyTo(displayImage);

            if (markersId.Size > 0)
            {
                ArucoInvoke.DrawDetectedMarkers(displayImage, markersCorner, markersId, new MCvScalar(0, 255, 0));
            }

            CvInvoke.Imshow("Markers", displayImage);
            CvInvoke.WaitKey();
            */
        }

    }
    class Program
    {
        static void Main()
        {
            AcuroOpencv acuroOpencv = new AcuroOpencv();
            acuroOpencv.Start("../../../images/image.png");
        }
    }
}
