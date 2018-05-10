using NecromanteLL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Extension {

   

    public static class Adjuster {

        static double defaultWidth = 1440;
        static double defaultHeight = 900;

        public static void adjustForCamera(Image Map, Image Hero, ref int ScreenWidth, ref int ScreenHeight, ref int Cell_Width, ref int Cell_Height, ref double topSide, ref double botSide, ref double leftSide, ref double rightSide) {
   
            ScaleUp(Map);
            ScaleUp(Hero);
            

            ScreenWidth = (int)Window.Current.Bounds.Width;
            ScreenHeight = (int)Window.Current.Bounds.Height;

            int x_adjust = ScreenWidth % 10;
            int y_adjust = ScreenHeight % 10;

            ScreenWidth -= x_adjust;
            ScreenHeight -= y_adjust;

            Cell_Width = (int)Map.Width / 100;
            Cell_Height = (int)Map.Height / 100;

            topSide = (3 * Hero.Height / 4) / Cell_Height;
            botSide = (1 * Hero.Height / 4) / Cell_Height;

            leftSide = 0;
            rightSide = Hero.Width / Cell_Width;
        }

        private static void ScaleUp(Image item) {
           
            int fill = 10;

            int x_adjust = (int)Canvas.GetLeft(item) % fill;
            x_adjust = (int)Canvas.GetLeft(item) - x_adjust;

            int y_adjust = (int)Canvas.GetTop(item) % fill;
            y_adjust = (int)Canvas.GetTop(item) - y_adjust;

            int widthAjust = (int)item.Width % fill;
            widthAjust = (int)item.Width - widthAjust;

            int heightAjust = (int)item.Height % fill;
            heightAjust = (int)item.Height - heightAjust;

            Canvas.SetLeft(item, x_adjust);
            Canvas.SetTop(item, y_adjust);

            item.Width = widthAjust;
            item.Height = heightAjust;

        }

        //Classe para Ajustar todas as telas para qualquer tamanho.
        public static void AdjustWindow(Canvas Floor) {

            double x_ratio, y_ratio;

            Button childBtn;

            Image childImg;

            List<UIElement> Images = Floor.Children.ToList();

            Floor.Width = Window.Current.Bounds.Width;
            Floor.Height = Window.Current.Bounds.Height;

            x_ratio = Floor.Width / defaultWidth;
            y_ratio = Floor.Height / defaultHeight;

            foreach (UIElement item in Images) {

                Canvas.SetLeft(item, Canvas.GetLeft(item) * x_ratio);
                Canvas.SetTop(item, Canvas.GetTop(item) * y_ratio);

                if (item is Image) {

                    ((Image)item).Width = ((Image)item).Width * x_ratio;
                    ((Image)item).Height = ((Image)item).Height * y_ratio;
                }
                else if (item is Grid) {

                    ((Grid)item).Width = ((Grid)item).Width * x_ratio;
                    ((Grid)item).Height = ((Grid)item).Height * y_ratio;

                    for (int i = 0; i < VisualTreeHelper.GetChildrenCount(item); i++) {
                         
                        if (VisualTreeHelper.GetChild(item, i) is Button) {

                            childBtn = (Button)VisualTreeHelper.GetChild(item, i);

                            childBtn.Width = childBtn.Width * x_ratio;
                            childBtn.Height = childBtn.Height * y_ratio;
                            childBtn.FontSize = childBtn.FontSize * x_ratio - 3;

                        }
                        else {

                            childImg = (Image)VisualTreeHelper.GetChild(item, i);

                            childImg.Width = childImg.Width * x_ratio;
                            childImg.Height = childImg.Height * y_ratio;
                        }
                    }
                }
                else {

                    Canvas.SetLeft(item, Canvas.GetLeft(item) * x_ratio);
                    Canvas.SetTop(item, Canvas.GetTop(item) * y_ratio);
                }
            }    
        }
    }
}
