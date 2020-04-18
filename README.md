# DDA-algorithm
DDA algorithm implemention Tasting algorithm
written by louk sky walker 
and its free use lin.
take it change it sell it
Its Yours

add me star if u LIKE 

STAR STAR 

THE IMPLEMATION:::::::




public void DrawLine(Point a, Point b)
        {
            int counts;
            double dx, dy;
            double x, y, disx, disy;

            x = a.X;
            y = a.Y;
            PixelSet(new Point(x, y));


            dx = b.X - a.X;
            dy = b.Y - a.Y;


            counts = (int)Math.Max(dx, dy);


            disx = dx / counts;
            disy = dy / counts;

            for(int i= 0;i<counts; i++)
            {
                x += disx;
                y += disy;

                PixelSet(new Point(x, y));
            }
            

        }
        
    FULL CODE IN REPO 
    CS 
    CSHARP 
    C# 
    C SHARP 
    
    PEACE AND LOVE
