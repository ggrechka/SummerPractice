using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace PraktikaSummer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DrawGraph();
        }

        double f1(double x)
        {
            return Math.Pow(x, 4) * Math.Pow(Math.Log(Math.Abs(x)) + 1, 2);
        }

        double f2(double x)
        {
            return Math.Pow(x, 4) * Math.Pow(Math.Log(Math.Abs(x)) - 1, 2);
        }

        double g(double x, double y)
        {
            return 4 * y / x + 2 * x * Math.Sqrt(y);
        }

        public void DrawGraph()
        {
            // Получим панель для рисования
            GraphPane pane = zedGraph.GraphPane;

            // Изменим тест надписи по оси X
            pane.XAxis.Title.Text = "x";

            // Изменим текст по оси Y
            pane.YAxis.Title.Text = "y";

            // Изменим текст заголовка графика
            pane.Title.Text = "Графики точного и приближенных решений, полученных для различных значений N";

            // Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
            pane.CurveList.Clear();

            // Создадим списки точек
            PointPairList list1 = new PointPairList();
            PointPairList list2 = new PointPairList();
            PointPairList list3 = new PointPairList();
            PointPairList list4 = new PointPairList();
            PointPairList list5 = new PointPairList();

            int n = 200;
            double a = 1, b = 2, h = (b - a) / (n - 1), x, y;
            // Заполняем список точек
            for (int i = 0; i < n; i++)
            {
                x = a + i * h;
                double y1 = f1(x), y2 = f2(x);
                // добавим в списки точки
                list1.Add(x, y1);
                list2.Add(x, y2);
            }

            string writePath = "Максимальные невязки при N = 5, 20, 100.txt";

            n = 5;
            h = (b - a) / (n - 1);
            x = a;
            y = 1;
            double mx1 = Math.Abs(y - f1(x)), mx2 = Math.Abs(y - f2(x));
            for (int i = 1; i < n; i++)
            {
                y = y + h * g(x, y);
                x = a + i * h;
                mx1 = Math.Max(mx1, Math.Abs(y - f1(x)));
                mx2 = Math.Max(mx2, Math.Abs(y - f2(x)));
                // добавим в список точку
                list3.Add(x, y);
            }
            using (StreamWriter sw = new StreamWriter(writePath, false, Encoding.Default))
            {
                sw.WriteLine($"Максимальная невязка при N = 5: {Math.Min(mx1, mx2)}");
            }

            n = 20;
            h = (b - a) / (n - 1);
            x = a;
            y = 1;
            mx1 = Math.Abs(y - f1(x));
            mx2 = Math.Abs(y - f2(x));
            for (int i = 1; i < n; i++)
            {
                y = y + h * g(x, y);
                x = a + i * h;
                mx1 = Math.Max(mx1, Math.Abs(y - f1(x)));
                mx2 = Math.Max(mx2, Math.Abs(y - f2(x)));
                // добавим в список точку
                list4.Add(x, y);
            }
            using (StreamWriter sw = new StreamWriter(writePath, true, Encoding.Default))
            {
                sw.WriteLine($"Максимальная невязка при N = 20: {Math.Min(mx1, mx2)}");
            }

            n = 100;
            h = (b - a) / (n - 1);
            x = a;
            y = 1;
            mx1 = Math.Abs(y - f1(x));
            mx2 = Math.Abs(y - f2(x));
            for (int i = 1; i < n; i++)
            {
                y = y + h * g(x, y);
                x = a + i * h;
                mx1 = Math.Max(mx1, Math.Abs(y - f1(x)));
                mx2 = Math.Max(mx2, Math.Abs(y - f2(x)));
                // добавим в список точку
                list5.Add(x, y);
            }
            using (StreamWriter sw = new StreamWriter(writePath, true, Encoding.Default))
            {
                sw.WriteLine($"Максимальная невязка при N = 100: {Math.Min(mx1, mx2)}");
            }

            // Создадим кривую с названием "Точное решение",
            // которая будет рисоваться фиолетовым цветом (Color.Violet),
            // Опорные точки выделяться не будут (SymbolType.None)
            LineItem f1_curve = pane.AddCurve("Точное решение", list1, Color.Violet, SymbolType.None);
            f1_curve.Line.Width = 3;
            // Создадим кривую с названием "Точное решение",
            // которая будет рисоваться черным цветом (Color.Black),
            // Опорные точки выделяться не будут (SymbolType.None)
            LineItem f2_curve = pane.AddCurve("Точное решение", list2, Color.Black, SymbolType.None);
            f2_curve.Line.Width = 3;
            // Создадим кривую с названием "N = 5",
            // которая будет рисоваться зеленым цветом (Color.Green),
            // Опорные точки выделяться не будут (SymbolType.None)
            LineItem f3_curve = pane.AddCurve("N = 5", list3, Color.Green, SymbolType.None);
            f3_curve.Line.Width = 3;
            // Создадим кривую с названием "N = 20",
            // которая будет рисоваться синим цветом (Color.Blue),
            // Опорные точки выделяться не будут (SymbolType.None)
            LineItem f4_curve = pane.AddCurve("N = 20", list4, Color.Blue, SymbolType.None);
            f4_curve.Line.Width = 3;
            // Создадим кривую с названием "N = 100",
            // которая будет рисоваться желтым цветом (Color.Yellow),
            // Опорные точки выделяться не будут (SymbolType.None)
            LineItem f5_curve = pane.AddCurve("N = 100", list5, Color.Yellow, SymbolType.None);
            f5_curve.Line.Width = 3;
            // Вызываем метод AxisChange (), чтобы обновить данные об осях.
            // В противном случае на рисунке будет показана только часть графика,
            // которая умещается в интервалы по осям, установленные по умолчанию
            zedGraph.AxisChange();

            // Обновляем график
            zedGraph.Invalidate();
        }
    }
}
