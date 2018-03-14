using Extension;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using NecromanteLL;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MantaNecromante.GameStage {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainStage : Page {

        DispatcherTimer walkTimer = new DispatcherTimer();

        private bool isCameraHorizontal, isCameraVertical;
        private bool isMovementKey;
        private BitmapImage direction;
        private BitmapImage runtoLeft, runtoRight;
        private BitmapImage idletoLeft, idletoRight;

        private double x_oldMap, y_oldMap, x_oldHero, y_oldHero;
        private int GridX_mult, GridY_mult;
        private int[,] Colliders = new int[102, 102];

        private double x = 0,  y = 0;
        private int ScreenHeight, ScreenWidth;

        public MainStage() {

            this.InitializeComponent();

            //ScreenWidth = (int)Window.Current.Bounds.Width;
            //ScreenHeight = (int)Window.Current.Bounds.Height;

            //int x_adjust = ScreenWidth % 10;
            //int y_adjust = ScreenHeight % 10;

            //ScreenWidth -= x_adjust;
            //ScreenHeight -= y_adjust;

            Adjuster.AdjustWindow(Floor);

            y_oldMap = Mansion.Height;
            x_oldMap = Mansion.Width;
            y_oldHero = Hero.Height;
            x_oldHero = Hero.Width;
                
            Adjuster.adjustForCamera(Mansion, Hero, ref ScreenWidth, ref ScreenHeight);


            GridX_mult = (int)Mansion.Width / 100;
            GridY_mult = (int)Mansion.Height / 100;


            Debug.WriteLine(Start.Height + "," + Start.Width);

            Window.Current.CoreWindow.KeyDown += keySentinel;
            Window.Current.CoreWindow.KeyUp += keyDropped;

            walkTimer.Interval = System.TimeSpan.FromMilliseconds(1);
            walkTimer.Tick += walk;

            walkTimer.Start();
            Debug.WriteLine("Local: " + Canvas.GetLeft(Hero) + ", " + Canvas.GetTop(Hero));
            //CreateGrid();
            Debug.WriteLine(ScreenWidth);

            setBlocks();

        }

        private void setBlocks() {

            Colliders[83, 1] = 1;
Colliders[84, 1] = 1;
Colliders[85, 1] = 1;
Colliders[86, 1] = 1;
Colliders[87, 1] = 1;
Colliders[88, 1] = 1;
Colliders[89, 1] = 1;
Colliders[90, 1] = 1;
Colliders[91, 1] = 1;
Colliders[92, 1] = 1;
Colliders[93, 1] = 1;
Colliders[94, 1] = 1;
Colliders[94, 2] = 1;
Colliders[94, 3] = 1;
Colliders[94, 4] = 1;
Colliders[94, 5] = 1;
Colliders[94, 6] = 1;
Colliders[94, 7] = 1;
Colliders[94, 8] = 1;
Colliders[94, 9] = 1;
Colliders[94, 10] = 1;
Colliders[94, 11] = 1;
Colliders[94, 12] = 1;
Colliders[94, 13] = 1;
Colliders[94, 14] = 1;
Colliders[94, 15] = 1;
Colliders[94, 16] = 1;
Colliders[94, 17] = 1;
Colliders[94, 18] = 1;
Colliders[94, 19] = 1;
Colliders[94, 20] = 1;
Colliders[94, 21] = 1;
Colliders[93, 21] = 1;
Colliders[92, 21] = 1;
Colliders[90, 17] = 1;
Colliders[90, 16] = 1;
Colliders[90, 15] = 1;
Colliders[90, 14] = 1;
Colliders[90, 13] = 1;
Colliders[90, 12] = 1;
Colliders[90, 11] = 1;
Colliders[90, 10] = 1;
Colliders[90, 9] = 1;
Colliders[90, 8] = 1;
Colliders[90, 7] = 1;
Colliders[90, 6] = 1;
Colliders[90, 5] = 1;
Colliders[89, 5] = 1;
Colliders[88, 5] = 1;
Colliders[88, 6] = 1;
Colliders[89, 6] = 1;
Colliders[88, 8] = 1;
Colliders[89, 7] = 1;
Colliders[88, 7] = 1;
Colliders[89, 8] = 1;
Colliders[88, 9] = 1;
Colliders[89, 10] = 1;
Colliders[89, 11] = 1;
Colliders[89, 12] = 1;
Colliders[88, 13] = 1;
Colliders[87, 5] = 1;
Colliders[87, 8] = 1;
Colliders[87, 9] = 1;
Colliders[87, 13] = 1;
Colliders[87, 14] = 1;
Colliders[87, 15] = 1;
Colliders[87, 16] = 1;
Colliders[87, 17] = 1;
Colliders[88, 17] = 1;
Colliders[89, 17] = 1;
Colliders[81, 17] = 1;
Colliders[82, 17] = 1;
Colliders[83, 17] = 1;
Colliders[83, 16] = 1;
Colliders[83, 15] = 1;
Colliders[83, 14] = 1;
Colliders[83, 13] = 1;
Colliders[82, 13] = 1;
Colliders[81, 13] = 1;
Colliders[83, 2] = 1;
Colliders[83, 3] = 1;
Colliders[92, 22] = 1;
Colliders[92, 23] = 1;
Colliders[92, 24] = 1;
Colliders[92, 25] = 1;
Colliders[92, 26] = 1;
Colliders[92, 27] = 1;
Colliders[92, 28] = 1;
Colliders[92, 29] = 1;
Colliders[92, 30] = 1;
Colliders[92, 31] = 1;
Colliders[92, 32] = 1;
Colliders[92, 33] = 1;
Colliders[92, 34] = 1;
Colliders[92, 35] = 1;
Colliders[91, 35] = 1;
Colliders[90, 35] = 1;
Colliders[89, 35] = 1;
Colliders[88, 35] = 1;
Colliders[87, 35] = 1;
Colliders[90, 34] = 1;
Colliders[87, 34] = 1;
Colliders[87, 33] = 1;
Colliders[87, 32] = 1;
Colliders[87, 31] = 1;
Colliders[87, 30] = 1;
Colliders[87, 29] = 1;
Colliders[87, 28] = 1;
Colliders[87, 27] = 1;
Colliders[87, 26] = 1;
Colliders[87, 25] = 1;
Colliders[87, 24] = 1;
Colliders[87, 23] = 1;
Colliders[87, 22] = 1;
Colliders[87, 21] = 1;
Colliders[86, 21] = 1;
Colliders[85, 21] = 1;
Colliders[84, 21] = 1;
Colliders[83, 21] = 1;
Colliders[82, 21] = 1;
Colliders[81, 21] = 1;
Colliders[80, 20] = 1;
Colliders[80, 19] = 1;
Colliders[80, 18] = 1;
Colliders[80, 21] = 1;
Colliders[80, 17] = 1;
Colliders[81, 12] = 1;
Colliders[81, 11] = 1;
Colliders[81, 10] = 1;
Colliders[80, 10] = 1;
Colliders[79, 10] = 1;
Colliders[78, 10] = 1;
Colliders[77, 10] = 1;
Colliders[81, 6] = 1;
Colliders[81, 5] = 1;
Colliders[81, 4] = 1;
Colliders[82, 4] = 1;
Colliders[83, 4] = 1;
Colliders[80, 6] = 1;
Colliders[79, 6] = 1;
Colliders[78, 6] = 1;
Colliders[77, 6] = 1;
Colliders[76, 6] = 1;
Colliders[75, 6] = 1;
Colliders[74, 6] = 1;
Colliders[73, 6] = 1;
Colliders[72, 6] = 1;
Colliders[71, 6] = 1;
Colliders[70, 6] = 1;
Colliders[69, 6] = 1;
Colliders[68, 6] = 1;
Colliders[67, 6] = 1;
Colliders[66, 6] = 1;
Colliders[65, 6] = 1;
Colliders[64, 6] = 1;
Colliders[63, 6] = 1;
Colliders[62, 6] = 1;
Colliders[62, 5] = 1;
Colliders[62, 4] = 1;
Colliders[62, 3] = 1;
Colliders[62, 10] = 1;
Colliders[62, 11] = 1;
Colliders[62, 12] = 1;
Colliders[63, 10] = 1;
Colliders[64, 10] = 1;
Colliders[65, 10] = 1;
Colliders[66, 10] = 1;
Colliders[67, 10] = 1;
Colliders[68, 10] = 1;
Colliders[69, 10] = 1;
Colliders[70, 10] = 1;
Colliders[71, 10] = 1;
Colliders[72, 10] = 1;
Colliders[73, 10] = 1;
Colliders[74, 10] = 1;
Colliders[75, 10] = 1;
Colliders[76, 10] = 1;
Colliders[63, 12] = 1;
Colliders[64, 12] = 1;
Colliders[65, 12] = 1;
Colliders[66, 12] = 1;
Colliders[67, 12] = 1;
Colliders[68, 12] = 1;
Colliders[69, 12] = 1;
Colliders[70, 12] = 1;
Colliders[71, 12] = 1;
Colliders[72, 12] = 1;
Colliders[73, 12] = 1;
Colliders[74, 12] = 1;
Colliders[74, 13] = 1;
Colliders[74, 14] = 1;
Colliders[74, 15] = 1;
Colliders[74, 16] = 1;
Colliders[74, 17] = 1;
Colliders[74, 18] = 1;
Colliders[74, 19] = 1;
Colliders[74, 20] = 1;
Colliders[74, 21] = 1;
Colliders[74, 22] = 1;
Colliders[74, 23] = 1;
Colliders[74, 24] = 1;
Colliders[74, 25] = 1;
Colliders[74, 27] = 1;
Colliders[74, 26] = 1;
Colliders[74, 28] = 1;
Colliders[75, 28] = 1;
Colliders[76, 28] = 1;
Colliders[77, 28] = 1;
Colliders[78, 28] = 1;
Colliders[79, 28] = 1;
Colliders[80, 28] = 1;
Colliders[80, 29] = 1;
Colliders[81, 29] = 1;
Colliders[81, 30] = 1;
Colliders[81, 31] = 1;
Colliders[81, 32] = 1;
Colliders[80, 32] = 1;
Colliders[79, 32] = 1;
Colliders[78, 32] = 1;
Colliders[77, 32] = 1;
Colliders[76, 32] = 1;
Colliders[75, 32] = 1;
Colliders[74, 32] = 1;
Colliders[73, 32] = 1;
Colliders[72, 32] = 1;
Colliders[71, 32] = 1;
Colliders[70, 32] = 1;
Colliders[69, 32] = 1;
Colliders[68, 32] = 1;
Colliders[68, 31] = 1;
Colliders[68, 30] = 1;
Colliders[68, 29] = 1;
Colliders[68, 28] = 1;
Colliders[70, 27] = 1;
Colliders[69, 27] = 1;
Colliders[68, 27] = 1;
Colliders[69, 26] = 1;
Colliders[68, 26] = 1;
Colliders[68, 25] = 1;
Colliders[68, 24] = 1;
Colliders[68, 23] = 1;
Colliders[68, 22] = 1;
Colliders[68, 21] = 1;
Colliders[68, 20] = 1;
Colliders[68, 19] = 1;
Colliders[69, 19] = 1;
Colliders[69, 28] = 1;
Colliders[69, 20] = 1;
Colliders[69, 18] = 1;
Colliders[68, 18] = 1;
Colliders[70, 19] = 1;
Colliders[68, 17] = 1;
Colliders[68, 16] = 1;
Colliders[68, 15] = 1;
Colliders[67, 15] = 1;
Colliders[66, 15] = 1;
Colliders[65, 15] = 1;
Colliders[64, 15] = 1;
Colliders[63, 15] = 1;
Colliders[62, 15] = 1;
Colliders[62, 16] = 1;
Colliders[62, 17] = 1;
Colliders[62, 2] = 1;
Colliders[62, 1] = 1;
Colliders[61, 1] = 1;
Colliders[60, 1] = 1;
Colliders[59, 1] = 1;
Colliders[59, 2] = 1;
Colliders[59, 3] = 1;
Colliders[59, 4] = 1;
Colliders[59, 5] = 1;
Colliders[59, 6] = 1;
Colliders[58, 6] = 1;
Colliders[57, 6] = 1;
Colliders[56, 6] = 1;
Colliders[55, 6] = 1;
Colliders[54, 6] = 1;
Colliders[54, 7] = 1;
Colliders[54, 8] = 1;
Colliders[54, 9] = 1;
Colliders[54, 10] = 1;
Colliders[54, 11] = 1;
Colliders[54, 12] = 1;
Colliders[54, 13] = 1;
Colliders[54, 14] = 1;
Colliders[54, 15] = 1;
Colliders[54, 16] = 1;
Colliders[54, 17] = 1;
Colliders[53, 17] = 1;
Colliders[52, 17] = 1;
Colliders[51, 17] = 1;
Colliders[50, 17] = 1;
Colliders[51, 16] = 1;
Colliders[52, 16] = 1;
Colliders[51, 15] = 1;
Colliders[51, 14] = 1;
Colliders[51, 13] = 1;
Colliders[50, 13] = 1;
Colliders[52, 13] = 1;
Colliders[53, 13] = 1;
Colliders[53, 12] = 1;
Colliders[53, 11] = 1;
Colliders[53, 10] = 1;
Colliders[53, 9] = 1;
Colliders[52, 9] = 1;
Colliders[51, 9] = 1;
Colliders[51, 8] = 1;
Colliders[51, 7] = 1;
Colliders[51, 6] = 1;
Colliders[50, 5] = 1;
Colliders[51, 5] = 1;
Colliders[52, 5] = 1;
Colliders[53, 5] = 1;
Colliders[54, 5] = 1;
Colliders[55, 4] = 1;
Colliders[55, 3] = 1;
Colliders[55, 2] = 1;
Colliders[55, 1] = 1;
Colliders[54, 1] = 1;
Colliders[53, 1] = 1;
Colliders[52, 1] = 1;
Colliders[51, 1] = 1;
Colliders[50, 1] = 1;
Colliders[49, 1] = 1;
Colliders[48, 1] = 1;
Colliders[62, 18] = 1;
Colliders[62, 19] = 1;
Colliders[62, 20] = 1;
Colliders[62, 21] = 1;
Colliders[61, 21] = 1;
Colliders[60, 21] = 1;
Colliders[59, 21] = 1;
Colliders[58, 21] = 1;
Colliders[57, 21] = 1;
Colliders[56, 21] = 1;
Colliders[55, 21] = 1;
Colliders[54, 21] = 1;
Colliders[53, 21] = 1;
Colliders[52, 21] = 1;
Colliders[51, 21] = 1;
Colliders[50, 21] = 1;
Colliders[49, 21] = 1;
Colliders[48, 21] = 1;
Colliders[48, 22] = 1;
Colliders[49, 22] = 1;
Colliders[50, 22] = 1;
Colliders[51, 22] = 1;
Colliders[52, 22] = 1;
Colliders[53, 22] = 1;
Colliders[54, 22] = 1;
Colliders[55, 22] = 1;
Colliders[55, 22] = 1;
Colliders[56, 22] = 1;
Colliders[57, 22] = 1;
Colliders[57, 23] = 1;
Colliders[57, 25] = 1;
Colliders[57, 24] = 1;
Colliders[57, 26] = 1;
Colliders[57, 27] = 1;
Colliders[57, 28] = 1;
Colliders[58, 28] = 1;
Colliders[59, 28] = 1;
Colliders[60, 28] = 1;
Colliders[60, 29] = 1;
Colliders[60, 30] = 1;
Colliders[60, 31] = 1;
Colliders[60, 32] = 1;
Colliders[60, 33] = 1;
Colliders[60, 34] = 1;
Colliders[59, 34] = 1;
Colliders[58, 34] = 1;
Colliders[57, 34] = 1;
Colliders[56, 34] = 1;
Colliders[55, 34] = 1;
Colliders[54, 34] = 1;
Colliders[53, 34] = 1;
Colliders[52, 34] = 1;
Colliders[51, 34] = 1;
Colliders[50, 34] = 1;
Colliders[49, 34] = 1;
Colliders[48, 34] = 1;
Colliders[47, 34] = 1;
Colliders[47, 33] = 1;
Colliders[47, 32] = 1;
Colliders[47, 31] = 1;
Colliders[47, 30] = 1;
Colliders[47, 29] = 1;
Colliders[47, 28] = 1;
Colliders[48, 28] = 1;
Colliders[49, 28] = 1;
Colliders[50, 28] = 1;
Colliders[51, 28] = 1;
Colliders[52, 28] = 1;
Colliders[53, 28] = 1;
Colliders[54, 28] = 1;
Colliders[54, 27] = 1;
Colliders[54, 26] = 1;
Colliders[53, 26] = 1;
Colliders[52, 26] = 1;
Colliders[51, 26] = 1;
Colliders[50, 26] = 1;
Colliders[49, 26] = 1;
Colliders[48, 26] = 1;
Colliders[47, 26] = 1;
Colliders[46, 26] = 1;
Colliders[45, 26] = 1;
Colliders[46, 13] = 1;
Colliders[46, 14] = 1;
Colliders[46, 15] = 1;
Colliders[46, 16] = 1;
Colliders[46, 17] = 1;
Colliders[45, 17] = 1;
Colliders[45, 16] = 1;
Colliders[45, 15] = 1;
Colliders[45, 14] = 1;
Colliders[45, 13] = 1;
Colliders[47, 1] = 1;
Colliders[46, 1] = 1;
Colliders[45, 1] = 1;
Colliders[44, 1] = 1;
Colliders[44, 2] = 1;
Colliders[44, 3] = 1;
Colliders[44, 4] = 1;
Colliders[44, 5] = 1;
Colliders[44, 6] = 1;
Colliders[44, 7] = 1;
Colliders[44, 8] = 1;
            Colliders[44, 9] = 1;
Colliders[44, 10] = 1;
Colliders[44, 11] = 1;
Colliders[44, 12] = 1;
Colliders[44, 13] = 1;
Colliders[44, 17] = 1;
Colliders[44, 18] = 1;
Colliders[44, 19] = 1;
Colliders[44, 20] = 1;
Colliders[44, 21] = 1;
Colliders[44, 22] = 1;
Colliders[44, 23] = 1;
Colliders[44, 24] = 1;
Colliders[44, 25] = 1;
Colliders[44, 26] = 1;
Colliders[55, 5] = 1;
Colliders[40, 40] = 1;
        }

        private string wow;

        private void CheckCollision() {

            int y_upper = (int)((Canvas.GetTop(Hero) + (3 * y_oldHero / 4) - Canvas.GetTop(Mansion)) / GridY_mult);
            int y_bottom = (int)(y_upper + (y_oldHero / 4) / GridY_mult);

            int x_left = (int)(((Canvas.GetLeft(Hero)) - Canvas.GetLeft(Mansion)) / GridX_mult);
            int x_right = x_left + (int)x_oldHero / GridX_mult;

            //Colisão com objetos:
            if (Colliders[y_upper, x_left] == 1 || Colliders[y_upper, x_right] == 1 ||
                Colliders[y_bottom, x_left] == 1 || Colliders[y_bottom, x_right] == 1) {

                if (isCameraHorizontal)
                    Canvas.SetLeft(Mansion, Canvas.GetLeft(Mansion) + x);
                else
                    Canvas.SetLeft(Hero, Canvas.GetLeft(Hero) - x);


                if (isCameraVertical) 
                    Canvas.SetTop(Mansion, Canvas.GetTop(Mansion) + y);
                else 
                    Canvas.SetTop(Hero, Canvas.GetTop(Hero) - y);
            }

            wow = "[" + (x_left)   + ", " + y_upper + "]\n" + "[" + x_right + ", " + y_bottom + "]\n" +  Canvas.GetTop(Hero);
            teste.Text = wow;
        }

        private Grid Colliderss = new Grid();

        private void CreateGrid() {

            int x, y, divider = 100;

            x = (int)Mansion.Width / divider;
            y = (int)Mansion.Height / divider;

            divider += 2;

            for (int i = 0; i < divider; i++) {

                RowDefinition rd = new RowDefinition();
                rd.Height = new GridLength(y, GridUnitType.Pixel);

                Colliderss.RowDefinitions.Add(rd);

                for (int j = 0; j < divider; j++) {

                    ColumnDefinition cd = new ColumnDefinition();
                    cd.Width = new GridLength(x, GridUnitType.Pixel);

                    Colliderss.ColumnDefinitions.Add(cd);
                }
            }

            for (int j = 0; j < divider; j++) {

                for (int k = 0; k < divider; k++) {

                    Border border = new Border();
                    border.BorderThickness = new Thickness(1);

                    border.Background = new SolidColorBrush(Colors.Transparent);
                    border.Opacity = 0.5;

                    border.BorderBrush = new SolidColorBrush(Colors.Red);

                    border.HorizontalAlignment = HorizontalAlignment.Stretch;
                    border.VerticalAlignment = VerticalAlignment.Stretch;

                    Grid.SetColumn(border, j);
                    Grid.SetRow(border, k);

                    Colliderss.Children.Add(border);


                    border.Tapped += gettingCell;
                }
            }

            Floor.Children.Add(Colliderss);
            Canvas.SetTop(Colliderss, Canvas.GetTop(Mansion));
            Canvas.SetLeft(Colliderss, Canvas.GetLeft(Mansion));

        }

        private void gettingCell(object sender, TappedRoutedEventArgs e) {

            var cell = (Border)sender;
            cell.Background = new SolidColorBrush(Colors.Red);

            int column = Grid.GetColumn(cell);
            int row = Grid.GetRow(cell);

            Debug.WriteLine("Colliders[" + row + ", " + column + "] = 1");  

        }

        private void walk(object sender, object e) {

            if (Canvas.GetLeft(Hero) + (Hero.Width / 2) == ScreenWidth / 2) {

                if ((x < 0 && Canvas.GetLeft(Mansion) < 0) || (x > 0 && Canvas.GetLeft(Mansion) > ScreenWidth - Mansion.Width)) {

                    isCameraHorizontal = true;
                    Canvas.SetLeft(Mansion, Canvas.GetLeft(Mansion) - x);
                    Canvas.SetLeft(Colliderss, Canvas.GetLeft(Colliderss) - x);
                }
                else {

                    isCameraHorizontal = false;
                    Canvas.SetLeft(Hero, Canvas.GetLeft(Hero) + x);
                }

            } else {

                isCameraHorizontal = false;
                Canvas.SetLeft(Hero, Canvas.GetLeft(Hero) + x);
            }

            if (Canvas.GetTop(Hero) + (Hero.Height / 2) == ScreenHeight / 2) {

                if ((y < 0 && Canvas.GetTop(Mansion) < 0) || (y > 0 && Canvas.GetTop(Mansion) > ScreenHeight - Mansion.Height)) {

                    isCameraVertical = true;
                    Canvas.SetTop(Mansion, Canvas.GetTop(Mansion) - y);
                    Canvas.SetTop(Colliderss, Canvas.GetTop(Colliderss) - y);
                }
                else {

                    isCameraVertical = false;
                    Canvas.SetTop(Hero, Canvas.GetTop(Hero) + y);
                }

            } else {

                isCameraVertical = false;
                Canvas.SetTop(Hero, Canvas.GetTop(Hero) + y);
            }

            
            CheckCollision();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {

            Player va = (Player)e.Parameter;


            idletoLeft = va.Sprite_idle_left;
            idletoRight = va.Sprite_idle_right;

            runtoLeft = va.Sprite_walking_left;
            runtoRight = va.Sprite_walking_right;

            direction = runtoRight;
            Hero.Source = idletoRight;
        }

        private void keySentinel(CoreWindow sender, KeyEventArgs e) {

            isMovementKey = true;

            int speed = 5;

            switch (e.VirtualKey) {

                case Windows.System.VirtualKey.Escape:
                    isMovementKey = false;
                    OptionsMenu.Opacity = (OptionsMenu.Opacity == 1) ? 0 : 1;
                    break;
                case Windows.System.VirtualKey.W:
                    y = -speed;
                    x = 0;
                    break;
                case Windows.System.VirtualKey.S:
                    y = speed;
                    x = 0;
                    break;
                case Windows.System.VirtualKey.D:
                    direction = runtoRight;
                    x = speed;
                    y = 0;
                    break;
                case Windows.System.VirtualKey.A:
                    direction = runtoLeft;
                    x = -speed;
                    y = 0;
                    break;
                case Windows.System.VirtualKey.G:
                    Colliderss.Opacity = (Colliderss.Opacity == 1) ? 0 : 1;
                    break;
                default:
                    isMovementKey = false;
                    break;
            }

            if (Hero.Source != direction && isMovementKey) Hero.Source = direction;
        }


        private void keyDropped(CoreWindow sender, KeyEventArgs e) {

            BitmapImage stand = null;

            switch (e.VirtualKey) {

                case Windows.System.VirtualKey.Escape:
                    isMovementKey = false;
                    break;
                case Windows.System.VirtualKey.W:
                case Windows.System.VirtualKey.S:
                    y = 0;
                    break;
                case Windows.System.VirtualKey.A:
                case Windows.System.VirtualKey.D:
                    x = 0;
                    break;
            }

            if (direction == runtoLeft) stand = idletoLeft;
            else if (direction == runtoRight) stand = idletoRight;

            if (isMovementKey) {

                Hero.Source = stand;
            }
        }

            private void Exit(object sender, RoutedEventArgs e) {

            CoreApplication.Exit();
        }

        private void Continue(object sender, RoutedEventArgs e) {

            OptionsMenu.Opacity = 0;
        }
    }
}
