using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace grafics_ex_one_lsw
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //אתחול מערך הנקודות 
            pr = new Point[4];

            //אתחול מותאם אישית
            init();
        }

        static WriteableBitmap writeableBitmap;


        // מערך נקודות יאותחל לגודל 4 
        private static Point[] pr;
        //סופר למערך 
        private static int count = 0;


        //משתנה עזר לסימון הצורה שכעת נבחרה לצייר
        private static int shape = 0;
        //0 - אף צורה
        //1 - קו ישר
        //2 - מעגל 
        //3 - עקומת בזייה



        //פונקציית אתחול ללוח מופעלת גם ביצירת המסך הרצת קובץ האקסקיוטבל
        public void init()
        {

            chosen = new SolidColorBrush(Colors.Black);


            shape = 0;

            count = 0;

            log.Text = "שלום , כאן יוצגו הוראות לביצוע עבור כל כפתור אשר ילחץ כפתור ניקוי הלוח ישיב הודעה" +
                            " זו לתצוגה וינקה את הלוח מכל ציור " +
                            "שים לב בכל שלב תוכל ללחוץ על כפתור מחיקת הלוח ולאפס את הפעולה אותה בחרת לבצע כעת ";
            pointlog.Text = "כאן יוצגו הנקודות ";
            canvas.Children.Clear();
        }


        //קו ישר פונקציה המשנה את דגל התוכנה ל1 קו ישר
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            shape = 1;
            log.Text = "בחרת לבצע ציור קו ישר , כעת עלייך להזין שתי נקודות על ידי הקלקה של העכבר על גבי המסך הוורדרד";
        }

        // מעגל פונקצייה המשנה את הדגל לציור מעגל
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            shape = 2;
            log.Text = "בחרת לצייר מעגל כעת עלייך להזין שתי לחיצות על גבי המסך הוורדרד , לחיצה ראשונה למרכז המעגל והשניה תקבע את הרדיוס";
        }

        //ציור עקומת בזייה שינוי הדגל למספר 3 המייצג עקומה
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            shape = 3;
            log.Text = "בחרת לצייר עקומת בזייה כעת עלייך לבצע הקלקה של 4 נקודות על ידי העכבר ";
        }

        //פוקנציה זו מקבלת לחיצות מהעכבר באופן תמידי על גבי הלוח 
        //במקרה שנבחרה צור הפונקציה גם תקלוט נקודות למערך לציור ותצייר בסופו של דבר את המבוקש
        private void GetPoints(object sender, MouseButtonEventArgs e)
        {
            if(shape != 0)
            {
                switch(shape)
                {
                    case 1:
                        pr[count++] = e.GetPosition(canvas);
                        if (count == 2)
                        {
                            DrawLine(pr[0], pr[1]);
                            count = 0;
                            shape = 0;
                        }
                        break;
                    case 2:
                        pr[count++] = e.GetPosition(canvas);
                        if (count == 2)
                        {
                            //אתחול הערכים חזרה לאחר הציור
                            count = 0;
                            shape = 0;
                        }
                        break;
                    case 3:
                        pr[count++] = e.GetPosition(canvas);
                        if (count == 4)
                        {
                            //אתחול הערכים חזרה לאחר הציור
                            count = 0;
                            shape = 0;
                        }
                        break;
                }
                pointlog.Text +=    Environment.NewLine+ "x-> " + e.GetPosition(canvas).X +" ,y-> "+ e.GetPosition(canvas).Y + "   התקבלה לחיצה   ";
            }
        }

        // כפתור מחיקת הלוח קורא פשוט לפונקציית אתחול
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //אתחול
            init();
        }


        //פונקציית ציור קו ישר
        // נעשה שימוש במחלקה פוינט אשר מכילה התוכה ערכי נקודות איקס וואי 
        //DDA 
        //ציור קו
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


        //על מנת לצייר פיקסל פיקסל לקאנבאס נשתמש בטריק 
        //אני מצייר קו בגודל פיקסל 
        //wpf
        //לא מעניק אפשרות ציור ישרה פשוטה אפשר כמובן להמיר את התמונה לביט מאפ 
        //על מנת להשאיר את הקוד ברור וקריא נשתמש בטריק הנ"ל 


        public void PixelSet(Point a)
        {

            Line l = new Line();
            l.X1 = a.X;
            l.X2 = a.X + 1;
            l.Y1 = a.Y;
            l.Y2 = a.Y + 1;
            l.Stroke = chosen;
            l.StrokeThickness = sizes;
            canvas.Children.Add(l);
        }

        //אקסטרה שינוי צבע לצייר
        private static SolidColorBrush chosen;
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            chosen = new SolidColorBrush(Colors.Blue);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            chosen = new SolidColorBrush(Colors.Green);
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            chosen = new SolidColorBrush(Colors.Purple);
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            chosen = new SolidColorBrush(Colors.FloralWhite);
        }

        //אקסטרה גודל הקו
        private static int sizes = 2;

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            if (sizes != 10)
                sizes += 2;
            
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            if (sizes != 2)
            {
                sizes -= 2;
            }
        }
    }
}
