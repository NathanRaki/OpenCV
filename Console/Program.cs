using Console;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;

/* Afficher une image avec EmguCV */
Mat matrix = new Mat("../../../crochet.jpg");
void DisplayImage(string name, IInputArray image)
{
    CvInvoke.Imshow(name, image);
    CvInvoke.WaitKey();
}

/* Accès aux pixels */
Image<Bgr, Byte> sourceBgr = matrix.ToImage<Bgr, Byte>();
Image<Bgr, Byte> destinationBgr = new Image<Bgr,Byte>(sourceBgr.Size.Width + 20, sourceBgr.Size.Height + 100);
DisplayImage("Source: Bgr image", sourceBgr);
ImageHandler.CopyToImage(ref sourceBgr, ref destinationBgr, 20, 100);
DisplayImage("Destination: Bgr image with offset", destinationBgr);

Image<Gray, Byte> sourceGray = matrix.ToImage<Gray, Byte>();
Image<Bgr, Byte> destinationGray = new Image<Bgr,Byte>(sourceBgr.Size.Width, sourceBgr.Size.Height);
DisplayImage("Source: Gray image", sourceGray);
ImageHandler.CopyToImage(ref sourceGray, ref destinationGray);
DisplayImage("Destination: Bgr image", destinationGray);

Image<Bgr, Byte> destinationChannel = new Image<Bgr,Byte>(sourceBgr.Size.Width, sourceBgr.Size.Height);
ImageHandler.GrayCopyChannelToImage(ref sourceBgr, ref destinationChannel, 2);
DisplayImage("Destination: Bgr image with 1 channel", destinationChannel);

/* Première manipulation d'images */
Image<Gray,Byte> dstGray = new Image<Gray,Byte>(sourceBgr.Size.Width, sourceBgr.Size.Height);
CvInvoke.CvtColor(sourceBgr, dstGray, ColorConversion.Bgr2Gray);
DisplayImage("CvtColor: Bgr to Gray", dstGray);

Image<Bgr,Byte> flippedImage = new Image<Bgr,Byte>(sourceBgr.Size.Width, sourceBgr.Size.Height);
CvInvoke.Flip(sourceBgr, flippedImage, FlipType.Vertical);
DisplayImage("Flip: Flipped image", flippedImage);

int width = sourceBgr.Size.Width + dstGray.Size.Width + flippedImage.Size.Width;
int height = sourceBgr.Size.Height;
Image<Bgr, Byte> mixedImage = new Image<Bgr, byte>(width, height);
ImageHandler.CopyToImage(ref sourceBgr, ref mixedImage);
ImageHandler.CopyToImage(ref dstGray, ref mixedImage, sourceBgr.Size.Width);
ImageHandler.CopyToImage(ref flippedImage, ref mixedImage, sourceBgr.Size.Width + dstGray.Size.Width);
DisplayImage("Mixed image", mixedImage);

/* Seuillage d'une image HSV */
Image<Hsv,Byte> imageHsv = new Image<Hsv, byte>(sourceBgr.Size.Width, sourceBgr.Size.Height);
CvInvoke.CvtColor(sourceBgr, imageHsv, ColorConversion.Bgr2Hsv);

int hsvCanalsWidth = imageHsv.Size.Width * 3;
Image<Hsv, Byte> hsvCanals = new Image<Hsv, byte>(hsvCanalsWidth, imageHsv.Size.Height);
ImageHandler.GrayCopyChannelToImage(ref imageHsv, ref hsvCanals, 0);
ImageHandler.GrayCopyChannelToImage(ref imageHsv, ref hsvCanals, 1, imageHsv.Size.Width);
ImageHandler.GrayCopyChannelToImage(ref imageHsv, ref hsvCanals, 2, imageHsv.Size.Width * 2);
DisplayImage("HSV Canals", hsvCanals);
