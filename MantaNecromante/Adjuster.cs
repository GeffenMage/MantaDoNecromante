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
       
        //Classe para Ajustar todas as telas para qualquer tamanho.
        public static void AdjustWindow(Canvas Floor) {

            double defaultWidth = 1440;
            double defaultHeight = 900;

            Floor.Height = Window.Current.Bounds.Height;
            Floor.Width = Window.Current.Bounds.Width;

            List<UIElement> Images = Floor.Children.ToList();

            //Variáveis de meus botões personalizados:
            Button childBtn = null;
            Image childImg = null;

            double oldHeight = 0, oldWidth = 0, itemRatio, oldFont = 0;
            double relative_X, relative_Y;

            foreach (UIElement item in Images) {

                relative_X = Canvas.GetLeft(item) / defaultWidth;
                relative_Y = Canvas.GetTop(item) / defaultHeight;

                Canvas.SetLeft(item, Floor.Width * relative_X);
                Canvas.SetTop(item, Floor.Height * relative_Y);

                if (item is Image) {

                    oldWidth = ((Image)item).Width;
                    oldHeight = ((Image)item).Height;
                }
                else if (item is Grid) {

                    oldWidth = ((Grid)item).Width;
                    oldHeight = ((Grid)item).Height;

                    for (int i = 0; i< VisualTreeHelper.GetChildrenCount(item); i++) {

                        if (VisualTreeHelper.GetChild(item, i) is Button) {

                            childBtn = (Button)VisualTreeHelper.GetChild(item, i);
                            oldFont = childBtn.FontSize;

                        } else {

                            childImg = (Image)VisualTreeHelper.GetChild(item, i);
                        }
                    }
                } else {

                    Canvas.SetLeft(item, Floor.Width * relative_X);
                    Canvas.SetTop(item, Floor.Height * relative_Y);
                }

                itemRatio = oldWidth / oldHeight;

                if (item is Image) {

                    if (oldWidth > oldHeight) {

                        ((Image)item).Width = (Floor.Width * oldWidth) / defaultWidth;
                        ((Image)item).Height = ((Image)item).Width * (1 / itemRatio);
                    }
                    else {

                        ((Image)item).Height = (Floor.Height * oldHeight) / defaultHeight;
                        ((Image)item).Width = ((Image)item).Height * itemRatio;

                      //Debug.WriteLine("Previous: " + oldWidth + "," + oldHeight);
                      //Debug.WriteLine("New: " + ((Image)item).Width + "," + ((Image)item).Height);
                    }
                }
                else if (item is Grid) {

                    if (oldWidth > oldHeight) {

                        ((Grid)item).Width = (Floor.Width * oldWidth) / defaultWidth;
                        ((Grid)item).Height = ((Grid)item).Width * (1 / itemRatio);
                    }
                    else {

                        ((Grid)item).Height = (Floor.Height * oldHeight) / defaultHeight;
                        ((Grid)item).Width = ((Grid)item).Height * itemRatio;

                    }

                    childImg.Width = ((Grid)item).Width;
                    childImg.Height = ((Grid)item).Height;

                    childBtn.Width = (childBtn.Width * childImg.Width) / oldWidth;
                    childBtn.Height = (childBtn.Height * childImg.Height) / oldHeight;

                    childBtn.FontSize = oldFont * childBtn.Height / oldHeight - 1;
                }
            }
        }
    }
}
