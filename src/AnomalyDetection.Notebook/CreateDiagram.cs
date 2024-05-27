using System.Drawing; 
using System.Drawing.Imaging;
using System.Text.Json; 
using System.IO;

public static class Tool {

    public static async Task<bool> CreateDiagram(string diagramFile, int diagramWidth, int diagramHeight, PointF[] pointsReference, PointF[] pointsDegradation)
    {
        Bitmap bitmap = new Bitmap(diagramWidth + 200, diagramHeight + 200);
        bitmap.MakeTransparent(Color.White);
        
        using (Graphics graphics = Graphics.FromImage(bitmap))
        {
            //Set white background
            graphics.Clear(Color.White);

            //Draw x axis
            graphics.DrawLine(
                Pens.Black, 
                GetCoordinates(0, 0), 
                GetCoordinates(0, 1000)
            );

            //Draw x axis units
            for (int j=0; j<=10; j++) {
                graphics.DrawString(
                    $"{j}", 
                    new Font("Arial", 10), 
                    Brushes.Black, 
                    GetCoordinates(j*100, -5)
                );
            }
            graphics.DrawString(
                "Time in seconds", 
                new Font("Arial", 10), 
                Brushes.Black, 
                GetCoordinates(0, -20)
            );

            //Draw y axis 
            graphics.DrawLine(
                Pens.Black, 
                GetCoordinates(0, 0), 
                GetCoordinates(diagramHeight, 0)
            );
            for (int ii=1; ii<=9; ii++) {
                graphics.DrawString(
                    $"{ii}", 
                    new Font("Arial", 10), 
                    Brushes.Black, 
                    GetCoordinates(-15, ii*100)
                );
            }

            //Draw y axis units
            string yLabel = "Pressure in psi";
            int i=0;
            foreach(char c in yLabel)
            {
                graphics.DrawString(
                    $"{c}", 
                    new Font("Arial", 10), 
                    Brushes.Black, 
                    GetCoordinates(-35, diagramHeight - (i++ * 20))
                );
            }
            
            //Draw reference points
            graphics.DrawLines(
                Pens.Black, 
                GetCoordinates(pointsReference)                    
            );
            
            //Draw degradation points
            graphics.DrawLines(
                Pens.Red, 
                GetCoordinates(pointsDegradation)                    
            );

            // Save the bitmap as a JPEG image
            bitmap.Save(diagramFile, ImageFormat.Jpeg);
        }

        
        return true; 
    }

    private static PointF GetCoordinates(int x, int y, int borderSize = 200, int diagramHeight = 1000)
    {
        return new PointF(
            x + (borderSize/2),
            (diagramHeight - y) + (borderSize/2)  
        );
    }

    private static PointF[] GetCoordinates(PointF[] points, int borderSize = 200, int diagramHeight = 1000)
    {
        PointF[] result = new PointF[points.Length];
        for(int ii=0; ii<points.Length; ii++)
        {
            result[ii] = new PointF(
                points[ii].X + (borderSize/2),
                (diagramHeight - points[ii].Y) + (borderSize/2)  
            );
        }
        return result;
    }


}
