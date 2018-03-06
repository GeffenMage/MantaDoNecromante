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

        public static void adjustForCamera(Image Map, Image Hero) {

            int x_adjust = (int)Canvas.GetLeft(Map) % 5;
            x_adjust = (int)Canvas.GetLeft(Map) - x_adjust;

            int y_adjust = (int)Canvas.GetTop(Map) % 5;
            y_adjust = (int)Canvas.GetTop(Map) - y_adjust;

            int widthAjust = (int)Map.Width % 5;
            widthAjust = (int)Map.Width - widthAjust;

            int heightAjust = (int)Map.Height % 5;
            heightAjust = (int)Map.Height - heightAjust;

            Canvas.SetLeft(Map, x_adjust);
            Canvas.SetTop(Map,y_adjust);

            Map.Width = widthAjust;
            Map.Height = heightAjust;

            x_adjust = (int)Canvas.GetLeft(Hero) % 5;
            x_adjust = (int)Canvas.GetLeft(Hero) - x_adjust;

            y_adjust = (int)Canvas.GetTop(Hero) % 5;
            y_adjust = (int)Canvas.GetTop(Hero) - y_adjust;

            widthAjust = (int)Hero.Width % 5;
            widthAjust = (int)Hero.Width - widthAjust;

            heightAjust = (int)Hero.Height % 5;
            heightAjust = (int)Hero.Height - heightAjust;

            Canvas.SetLeft(Hero, x_adjust);
            Canvas.SetTop(Hero, y_adjust);

            Hero.Width = widthAjust;
            Hero.Height = heightAjust;
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
                            childBtn.FontSize = childBtn.FontSize * x_ratio;

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
