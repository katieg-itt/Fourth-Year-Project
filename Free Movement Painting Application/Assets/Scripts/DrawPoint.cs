using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

public class DrawPoint : MonoBehaviour {

  

    // Use this for initialization
    void Start () {

      

    }

    // Update is called once per frame
    void Update () {

      

    }
    //private void DrawLine(Texture2D a_Texture, int x1, int y1, int x2, int y2, Color a_Color)
    //{
    //    int width = 10;
    //    int height = 10;

    //    int dy = y2 - y1;
    //    int dx = x2 - x1;

    //    int stepy = 1;
    //    if (dy < 0)
    //    {
    //        dy = -dy;
    //        stepy = -1;
    //    }

    //    int stepx = 1;
    //    if (dx < 0)
    //    {
    //        dx = -dx;
    //        stepx = -1;
    //    }

    //    dy <<= 1;
    //    dx <<= 1;

    //    if (x1 >= 0 && x1 < width && y1 >= 0 && y1 < height)
    //        for (int x = -1; x <= 1; x++)
    //            for (int y = -1; y <= 1; y++)
    //                a_Texture.SetPixel(x1 + x, y1 + y, a_Color);

    //    if (dx > dy)
    //    {
    //        int fraction = dy - (dx >> 1);

    //        while (x1 != x2)
    //        {
    //            if (fraction >= 0)
    //            {
    //                y1 += stepy;
    //                fraction -= dx;
    //            }

    //            x1 += stepx;
    //            fraction += dy;

    //            if (x1 >= 0 && x1 < width && y1 >= 0 && y1 < height)
    //                for (int x = -1; x <= 1; x++)
    //                    for (int y = -1; y <= 1; y++)
    //                        a_Texture.SetPixel(x1 + x, y1 + y, a_Color);
    //        }
    //    }
    //    else
    //    {
    //        int fraction = dx - (dy >> 1);

    //        while (y1 != y2)
    //        {
    //            if (fraction >= 0)
    //            {
    //                x1 += stepx;
    //                fraction -= dy;
    //            }

    //            y1 += stepy;
    //            fraction += dx;

    //            if (x1 >= 0 && x1 < width && y1 >= 0 && y1 < height)
    //                for (int x = -1; x <= 1; x++)
    //                    for (int y = -1; y <= 1; y++)
    //                        a_Texture.SetPixel(x1 + x, y1 + y, a_Color);
    //        }
    //    }

    //}

    //public static void DrawPoint(this Canvas canvas, Windows.Kinect.Joint joint, CoordinateMapper mapper)
    //{
    //    if (joint.TrackingState == TrackingState.NotTracked) return;

    //    Point point = joint.Scale(mapper);

    //    Ellipse ellipse = new Ellipse
    //    {
    //        Width = 20,
    //        Height = 20,
    //        Fill = new SolidColorBrush(Colors.LightBlue)
    //    };

    //    Canvas.SetLeft(ellipse, point.X - ellipse.Width / 2);
    //    Canvas.SetTop(ellipse, point.Y - ellipse.Height / 2);

    //    canvas.Children.Add(ellipse);
    //}

    //Scale
    //public static Point Scale(this Joint joint, CoordinateMapper mapper)
    //    {
    //        Point point = new Point();

    //        ColorSpacePoint colorPoint = mapper.MapCameraPointToColorSpace(joint.Position);
    //        point.X *= float.IsInfinity(colorPoint.X) ? 0.0 : colorPoint.X;
    //        point.Y *= float.IsInfinity(colorPoint.Y) ? 0.0 : colorPoint.Y;

    //        return point;
    //    }

}
