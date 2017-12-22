using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.UI.UserControlsVM
{
    using BaseTypes;
    using GalaSoft.MvvmLight.Command;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Media.Media3D;



    /// <summary>
    /// Defines the Size of a Matrix by setting width and height.
    /// </summary>
    public struct MatrixSizeInfo
    {
        /// <summary>
        /// The Width and Height of the Matrix in pixels.
        /// </summary>
        public int width, height;

        /// <summary>
        /// The number of pixels e.g. width * height (is self calculated).
        /// </summary>
        public int size;
    }


    public class MatrixViewportViewModel : BaseViewModel
    {

        #region constructor and destructor

        /// <summary>
        /// Constructor for a new MatrixView.
        /// </summary>
        /// <param name="matrixSizeInfo"> The width and height of the Matrix as MatrixSizeInfo struct. </param>
        /// <param name="strokeThickness"> The Stroke Thicknes between the pixels. </param>
        public MatrixViewportViewModel(string title)
        {
            Title = title;

            MatrixSize = new MatrixSizeInfo { width = 10, height = 10 };

            #region InitViewport and Pixels
            // Check the orthographic camera which set it up and add it to the scene
            Camera = new OrthographicCamera(new Point3D(cameraWidth / 2, cameraWidth / 2, 1), new Vector3D(0, 0, -1), new Vector3D(0, 1, 0), cameraWidth);

            // Create the array with pixels
            pixels = new GeometryModel3D[sizeX, sizeY];

            // Create a modelVisual is like the frame of the scene
            ModelVisual3D = new ModelVisual3D()
            {
                // Create a new Matrix with each pixel as GeometryObject in a ModelGroup and add it to the modelVisual
                Content = InitMatrixModelGroup(),
            };



            ViewportSizeInitCommand = new RelayCommand<Viewport3D>((s) => SizeInit(s));

            // Add SizeChanged command for event handling
            ViewportSizeChangedCommand = new RelayCommand<SizeChangedEventArgs>((e) => SizeChanged(e));


            #endregion
        }

        #endregion

        #region properties

        public RelayCommand<Viewport3D> ViewportSizeInitCommand { get; }

        public RelayCommand<SizeChangedEventArgs> ViewportSizeChangedCommand { get; }

        /// <summary>
        /// The Camera for the scene.
        /// </summary>
        public Camera Camera { get; set; }

        /// <summary>
        /// The Model Visual which holds the actual model/scene.
        /// </summary>
        public ModelVisual3D ModelVisual3D { get; set; }

        /// <summary>
        /// Gets and sets the StrokeThicknes of all of the Matrixes.
        /// </summary>
        public static double StrokeThickness { get; set; }


        public Model3D MatrixModel { get; set; }

        #endregion


        // The width of the viewport3d camera view.
        private const double cameraWidth = 100;


        // The pixels as 3D GeometryModel.
        private GeometryModel3D[,] pixels;


        /// <summary>
        /// Gets and sets the MatrixSize over the MatrixInfo sctruct.
        /// </summary>
        public MatrixSizeInfo MatrixSize
        {
            get
            {
                return new MatrixSizeInfo { width = sizeX, height = sizeY, size = sizeY * sizeX };
            }
            set
            {
                sizeX = value.width;
                sizeY = value.height;
                size = value.width * value.height;
            }
        }
        private static int sizeX, sizeY;
        private static int size;




      


        #region methods

        /// Inits the pixels[x, y] array with a new GeometryModel3D for each 
        /// pixel and returns them all in a Model3DGroup.
        private Model3DGroup InitMatrixModelGroup()
        {
            // New Matrix modelGroup
            Model3DGroup newMatrix = new Model3DGroup();

            // add light
            DirectionalLight light = new DirectionalLight(Color.FromRgb(255, 255, 255), new Vector3D(-1, -1, -3));
            newMatrix.Children.Add(light);

            // adds each pixel and set its color to black 
            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    // Create a new pixel with the right size
                    MeshGeometry3D newMesh = CreatePixel(1, 1);

                    //Build the model object
                    Material material = new DiffuseMaterial(new SolidColorBrush(Color.FromRgb(0, 0, 0)));
                    GeometryModel3D rectModel = new GeometryModel3D(newMesh, material);

                    // Add each pixel to the pixels array
                    pixels[x, y] = rectModel;

                    // Add the model to the group
                    newMatrix.Children.Add(rectModel);
                }
            }

            return newMatrix;
        }


        /// <summary>
        /// Updates the Matrix with a new Image.
        /// </summary>
        /// <param name="image"> The image as an Color[] which should be shown. </param>
        public void UpdateImage(WriteableBitmap bmp)
        {
            
            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    // get the color out of the image
                    Color col = bmp.GetPixel(x, y);
                        
                    Material newMat = new DiffuseMaterial(new SolidColorBrush(col));

                    pixels[x, y].Material = newMat;
                }
            }

        }

        public void SizeInit(Viewport3D viewport)
        {
            double pixelWidth = (cameraWidth - (sizeX - 1) * StrokeThickness) / sizeX;

            double cameraHeight = (cameraWidth * (viewport.ActualHeight / viewport.ActualWidth));

            double pixelHeight = (cameraHeight - (sizeY - 1) * StrokeThickness) / sizeY;

            double offsetY = 0.5 * (cameraWidth - cameraHeight);


            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    // Create a translate transform for each pixel
                    TranslateTransform3D translateTransform = new TranslateTransform3D((pixelWidth + StrokeThickness) * x, ((pixelHeight + StrokeThickness) * y) + offsetY, 0);

                    // Create a new pixel with the right size
                    ChangePixelSize(pixelWidth, pixelHeight, ref pixels[x, y]);

                    // Transform the pixel to the right spot
                    pixels[x, y].Transform = translateTransform;
                }
            }
        }

        /// <summary>
        /// Resizes the Matrix when the parrent was resized.
        /// </summary>
        public void SizeChanged(SizeChangedEventArgs e)
        {

            double pixelWidth = (cameraWidth - (sizeX - 1) * StrokeThickness) / sizeX;

            double cameraHeight = (cameraWidth * (e.NewSize.Height / e.NewSize.Width));

            double pixelHeight = (cameraHeight - (sizeY - 1) * StrokeThickness) / sizeY;

            double offsetY = 0.5 * (cameraWidth - cameraHeight);


            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    // Create a translate transform for each pixel
                    TranslateTransform3D translateTransform = new TranslateTransform3D((pixelWidth + StrokeThickness) * x, ((pixelHeight + StrokeThickness) * y) + offsetY, 0);

                    // Create a new pixel with the right size
                    ChangePixelSize(pixelWidth, pixelHeight, ref pixels[x, y]);

                    // Transform the pixel to the right spot
                    pixels[x, y].Transform = translateTransform;
                }
            }
        }

        /// Changes the size of the ref GeometryModel3D to the given one.
        private void ChangePixelSize(double w, double h, ref GeometryModel3D pixel)
        {
            var geometry3D = pixel.Geometry as MeshGeometry3D;
            Point3DCollection positions = geometry3D.Positions;

            Point3D pos1 = positions[1];
            Point3D pos2 = positions[2];
            Point3D pos3 = positions[3];

            pos1.X = w;
            pos2.X = w;
            pos2.Y = h;
            pos3.Y = h;

            positions[1] = pos1;
            positions[2] = pos2;
            positions[3] = pos3;
        }

        /// Creates a new rect with the given size and return it as MeshGeometry3D object.
        private MeshGeometry3D CreatePixel(double w, double h)
        {
            MeshGeometry3D rectMesh = new MeshGeometry3D();

            rectMesh.Positions.Add(new Point3D(0, 0, 0));
            rectMesh.Positions.Add(new Point3D(w, 0, 0));
            rectMesh.Positions.Add(new Point3D(w, h, 0));
            rectMesh.Positions.Add(new Point3D(0, h, 0));

            rectMesh.TriangleIndices.Add(0);
            rectMesh.TriangleIndices.Add(1);
            rectMesh.TriangleIndices.Add(2);
            rectMesh.TriangleIndices.Add(2);
            rectMesh.TriangleIndices.Add(3);
            rectMesh.TriangleIndices.Add(0);

            return rectMesh;
        }
        #endregion

    }
}
