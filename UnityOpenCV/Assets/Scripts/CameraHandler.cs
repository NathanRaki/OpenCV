using System;
using UnityEngine;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using UnityEngine.UI;

public class CameraHandler : MonoBehaviour
{
    private VideoCapture _videoCapture;
    private Mat _frame;
    private Texture2D _texture;
    private Material _material;
    [SerializeField] private RawImage rawImage;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting capture");

        //_videoCapture = new VideoCapture(1);
        _videoCapture = new VideoCapture("C:/Users/ncoustance/Desktop/Repositories/OpenCV/UnityOpenCV/Assets/demo.mp4");
        _videoCapture.SetCaptureProperty(CapProp.Fps, 30);
        //_frame = new Mat();
        _videoCapture.FlipVertical = true;
        _videoCapture.ImageGrabbed += ProcessFrame;
        _videoCapture.Start();


        _texture = new Texture2D(_videoCapture.Width, _videoCapture.Height, TextureFormat.RGB24, false);
        _material = rawImage.GetComponent<RawImage>().material;
    }

    private void ProcessFrame(object sender, EventArgs e)
    {
        if (_videoCapture.IsOpened)
        {
            _videoCapture.Retrieve(_frame);
        }
    }

    private void DisplayFrameOnPlane()
    {
        if (_frame.IsEmpty) { return; }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_videoCapture.IsOpened)
        {
            if (_frame == null)
                _frame = new Mat();
            _videoCapture.Grab();

            Mat fixedFrame = new Mat();
            CvInvoke.CvtColor(_frame, fixedFrame, ColorConversion.Bgr2Rgb);
            using (Image<Rgb, byte> currentImage = fixedFrame.ToImage<Rgb, byte>())
            {
                _texture.LoadRawTextureData(currentImage.Bytes);
                _texture.Apply();
                _material.mainTexture = _texture;
            }
        }
    }

    private void OnDestroy()
    {
        if (_videoCapture != null)
        {
            _videoCapture.Stop();
            _videoCapture.Dispose();
        }
    }
}
