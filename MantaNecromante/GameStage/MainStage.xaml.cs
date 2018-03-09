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
            CreateGrid();
            Debug.WriteLine(ScreenWidth);

            setBlocks();

        }

        private void setBlocks() {

            Colliders[92, 5] = 1;
            Colliders[91, 5] = 1;
            Colliders[90, 5] = 1;
            Colliders[89, 5] = 1;
            Colliders[88, 5] = 1;
            Colliders[88, 7] = 1;
            Colliders[88, 9] = 1;
            Colliders[89, 9] = 1;
            Colliders[90, 9] = 1;
            Colliders[91, 9] = 1;
            Colliders[92, 9] = 1;
            Colliders[92, 8] = 1;
            Colliders[92, 7] = 1;
            Colliders[92, 6] = 1;
            Colliders[92, 10] = 1;
            Colliders[92, 11] = 1;
            Colliders[92, 12] = 1;
            Colliders[91, 10] = 1;
            Colliders[91, 11] = 1;
            Colliders[91, 12] = 1;
            Colliders[92, 13] = 1;
            Colliders[92, 14] = 1;
            Colliders[92, 15] = 1;
            Colliders[92, 16] = 1;
            Colliders[92, 17] = 1;
            Colliders[90, 17] = 1;
            Colliders[89, 17] = 1;
            Colliders[89, 16] = 1;
            Colliders[89, 15] = 1;
            Colliders[88, 17] = 1;
            Colliders[88, 15] = 1;
            Colliders[88, 13] = 1;
            Colliders[89, 13] = 1;
            Colliders[90, 13] = 1;
            Colliders[89, 14] = 1;
            Colliders[96, 1] = 1;
            Colliders[96, 2] = 1;
            Colliders[96, 3] = 1;
            Colliders[96, 4] = 1;
            Colliders[96, 5] = 1;
            Colliders[96, 6] = 1;
            Colliders[96, 7] = 1;
            Colliders[96, 8] = 1;
            Colliders[96, 9] = 1;
            Colliders[96, 10] = 1;
            Colliders[96, 11] = 1;
            Colliders[96, 12] = 1;
            Colliders[96, 13] = 1;
            Colliders[96, 14] = 1;
            Colliders[96, 15] = 1;
            Colliders[96, 16] = 1;
            Colliders[96, 17] = 1;
            Colliders[96, 18] = 1;
            Colliders[96, 19] = 1;
            Colliders[96, 20] = 1;
            Colliders[97, 20] = 1;
            Colliders[98, 20] = 1;
            Colliders[95, 21] = 1;
            Colliders[94, 21] = 1;
            Colliders[94, 22] = 1;
            Colliders[94, 23] = 1;
            Colliders[94, 24] = 1;
            Colliders[94, 25] = 1;
            Colliders[94, 26] = 1;
            Colliders[94, 27] = 1;
            Colliders[94, 28] = 1;
            Colliders[94, 29] = 1;
            Colliders[94, 30] = 1;
            Colliders[94, 31] = 1;
            Colliders[94, 32] = 1;
            Colliders[94, 33] = 1;
            Colliders[94, 34] = 1;
            Colliders[93, 35] = 1;
            Colliders[92, 35] = 1;
            Colliders[91, 35] = 1;
            Colliders[90, 35] = 1;
            Colliders[89, 35] = 1;
            Colliders[89, 34] = 1;
            Colliders[89, 33] = 1;
            Colliders[89, 32] = 1;
            Colliders[89, 31] = 1;
            Colliders[89, 30] = 1;
            Colliders[89, 29] = 1;
            Colliders[89, 28] = 1;
            Colliders[89, 27] = 1;
            Colliders[89, 26] = 1;
            Colliders[89, 25] = 1;
            Colliders[89, 24] = 1;
            Colliders[89, 23] = 1;
            Colliders[89, 22] = 1;
            Colliders[89, 21] = 1;
            Colliders[88, 21] = 1;
            Colliders[87, 21] = 1;
            Colliders[86, 21] = 1;
            Colliders[85, 21] = 1;
            Colliders[85, 2] = 1;
            Colliders[85, 3] = 1;
            Colliders[83, 4] = 1;
            Colliders[84, 4] = 1;
            Colliders[84, 3] = 1;
            Colliders[89, 6] = 1;
            Colliders[89, 8] = 1;
            Colliders[96, 21] = 1;
            Colliders[95, 1] = 1;
            Colliders[94, 1] = 1;
            Colliders[93, 1] = 1;
            Colliders[92, 1] = 1;
            Colliders[91, 1] = 1;
            Colliders[90, 1] = 1;
            Colliders[89, 1] = 1;
            Colliders[88, 1] = 1;
            Colliders[87, 1] = 1;
            Colliders[86, 1] = 1;
            Colliders[85, 1] = 1;
            Colliders[82, 5] = 1;
            Colliders[82, 6] = 1;
            Colliders[82, 4] = 1;
            Colliders[82, 10] = 1;
            Colliders[82, 11] = 1;
            Colliders[82, 12] = 1;
            Colliders[82, 13] = 1;
            Colliders[83, 13] = 1;
            Colliders[84, 13] = 1;
            Colliders[84, 14] = 1;
            Colliders[84, 15] = 1;
            Colliders[84, 16] = 1;
            Colliders[84, 17] = 1;
            Colliders[83, 17] = 1;
            Colliders[85, 17] = 1;
            Colliders[85, 16] = 1;
            Colliders[85, 15] = 1;
            Colliders[85, 14] = 1;
            Colliders[82, 18] = 1;
            Colliders[82, 19] = 1;
            Colliders[82, 20] = 1;
            Colliders[82, 21] = 1;
            Colliders[83, 21] = 1;
            Colliders[84, 21] = 1;
            Colliders[81, 10] = 1;
            Colliders[80, 10] = 1;
            Colliders[79, 10] = 1;
            Colliders[78, 10] = 1;
            Colliders[81, 6] = 1;
            Colliders[80, 6] = 1;
            Colliders[79, 6] = 1;
            Colliders[78, 6] = 1;
            Colliders[77, 10] = 1;
            Colliders[76, 10] = 1;
            Colliders[75, 10] = 1;
            Colliders[74, 10] = 1;
            Colliders[73, 10] = 1;
            Colliders[72, 10] = 1;
            Colliders[71, 10] = 1;
            Colliders[70, 10] = 1;
            Colliders[69, 10] = 1;
            Colliders[68, 10] = 1;
            Colliders[67, 10] = 1;
            Colliders[66, 10] = 1;
            Colliders[65, 10] = 1;
            Colliders[64, 10] = 1;
            Colliders[63, 10] = 1;
            Colliders[63, 11] = 1;
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
            Colliders[75, 12] = 1;
            Colliders[76, 12] = 1;
            Colliders[76, 13] = 1;
            Colliders[76, 14] = 1;
            Colliders[76, 15] = 1;
            Colliders[76, 16] = 1;
            Colliders[76, 17] = 1;
            Colliders[76, 18] = 1;
            Colliders[76, 19] = 1;
            Colliders[76, 20] = 1;
            Colliders[76, 21] = 1;
            Colliders[76, 22] = 1;
            Colliders[76, 23] = 1;
            Colliders[76, 24] = 1;
            Colliders[76, 25] = 1;
            Colliders[76, 26] = 1;
            Colliders[76, 27] = 1;
            Colliders[76, 28] = 1;
            Colliders[77, 28] = 1;
            Colliders[78, 28] = 1;
            Colliders[79, 28] = 1;
            Colliders[80, 28] = 1;
            Colliders[77, 6] = 1;
            Colliders[76, 6] = 1;
            Colliders[75, 6] = 1;
            Colliders[74, 6] = 1;
            Colliders[72, 6] = 1;
            Colliders[73, 6] = 1;
            Colliders[71, 6] = 1;
            Colliders[70, 6] = 1;
            Colliders[69, 6] = 1;
            Colliders[68, 6] = 1;
            Colliders[67, 6] = 1;
            Colliders[66, 6] = 1;
            Colliders[65, 6] = 1;
            Colliders[64, 6] = 1;
            Colliders[63, 6] = 1;
            Colliders[63, 5] = 1;
            Colliders[63, 4] = 1;
            Colliders[63, 3] = 1;
            Colliders[63, 2] = 1;
            Colliders[63, 1] = 1;
            Colliders[62, 1] = 1;
            Colliders[55, 5] = 1;
            Colliders[55, 6] = 1;
            Colliders[55, 7] = 1;
            Colliders[55, 8] = 1;
            Colliders[55, 9] = 1;
            Colliders[54, 9] = 1;
            Colliders[54, 10] = 1;
            Colliders[54, 11] = 1;
            Colliders[55, 10] = 1;
            Colliders[55, 11] = 1;
            Colliders[55, 12] = 1;
            Colliders[55, 13] = 1;
            Colliders[55, 14] = 1;
            Colliders[55, 15] = 1;
            Colliders[55, 16] = 1;
            Colliders[55, 17] = 1;
            Colliders[54, 17] = 1;
            Colliders[53, 17] = 1;
            Colliders[52, 17] = 1;
            Colliders[51, 17] = 1;
            Colliders[51, 15] = 1;
            Colliders[52, 15] = 1;
            Colliders[53, 15] = 1;
            Colliders[54, 15] = 1;
            Colliders[54, 16] = 1;
            Colliders[53, 16] = 1;
            Colliders[52, 16] = 1;
            Colliders[54, 14] = 1;
            Colliders[53, 14] = 1;
            Colliders[52, 14] = 1;
            Colliders[52, 13] = 1;
            Colliders[53, 13] = 1;
            Colliders[54, 13] = 1;
            Colliders[51, 13] = 1;
            Colliders[52, 9] = 1;
            Colliders[53, 9] = 1;
            Colliders[51, 7] = 1;
            Colliders[51, 9] = 1;
            Colliders[51, 5] = 1;
            Colliders[52, 5] = 1;
            Colliders[53, 5] = 1;
            Colliders[54, 5] = 1;
            Colliders[56, 5] = 1;
            Colliders[57, 6] = 1;
            Colliders[58, 6] = 1;
            Colliders[59, 6] = 1;
            Colliders[60, 5] = 1;
            Colliders[60, 6] = 1;
            Colliders[60, 4] = 1;
            Colliders[60, 3] = 1;
            Colliders[60, 2] = 1;
            Colliders[61, 1] = 1;
            Colliders[69, 19] = 1;
            Colliders[70, 16] = 1;
            Colliders[69, 16] = 1;
            Colliders[68, 16] = 1;
            Colliders[67, 16] = 1;
            Colliders[66, 16] = 1;
            Colliders[65, 16] = 1;
            Colliders[64, 16] = 1;
            Colliders[63, 16] = 1;
            Colliders[70, 17] = 1;
            Colliders[70, 18] = 1;
            Colliders[71, 19] = 1;
            Colliders[72, 19] = 1;
            Colliders[71, 20] = 1;
            Colliders[70, 20] = 1;
            Colliders[70, 21] = 1;
            Colliders[70, 22] = 1;
            Colliders[70, 23] = 1;
            Colliders[70, 24] = 1;
            Colliders[70, 25] = 1;
            Colliders[70, 26] = 1;
            Colliders[70, 27] = 1;
            Colliders[70, 28] = 1;
            Colliders[71, 28] = 1;
            Colliders[71, 27] = 1;
            Colliders[72, 27] = 1;
            Colliders[72, 28] = 1;
            Colliders[70, 29] = 1;
            Colliders[70, 32] = 1;
            Colliders[70, 33] = 1;
            Colliders[71, 33] = 1;
            Colliders[72, 33] = 1;
            Colliders[73, 33] = 1;
            Colliders[74, 33] = 1;
            Colliders[75, 33] = 1;
            Colliders[76, 33] = 1;
            Colliders[77, 33] = 1;
            Colliders[78, 33] = 1;
            Colliders[79, 33] = 1;
            Colliders[80, 33] = 1;
            Colliders[81, 33] = 1;
            Colliders[82, 33] = 1;
            Colliders[83, 32] = 1;
            Colliders[83, 31] = 1;
            Colliders[83, 30] = 1;
            Colliders[83, 29] = 1;
            Colliders[83, 28] = 1;
            Colliders[82, 28] = 1;
            Colliders[81, 28] = 1;
            Colliders[83, 33] = 1;
            Colliders[63, 17] = 1;
            Colliders[63, 18] = 1;
            Colliders[63, 19] = 1;
            Colliders[63, 20] = 1;
            Colliders[63, 21] = 1;
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
            Colliders[47, 17] = 1;
            Colliders[46, 17] = 1;
            Colliders[45, 17] = 1;
            Colliders[45, 18] = 1;
            Colliders[45, 19] = 1;
            Colliders[45, 20] = 1;
            Colliders[45, 21] = 1;
            Colliders[45, 22] = 1;
            Colliders[45, 23] = 1;
            Colliders[45, 24] = 1;
            Colliders[45, 25] = 1;
            Colliders[45, 26] = 1;
            Colliders[47, 16] = 1;
            Colliders[47, 15] = 1;
            Colliders[47, 14] = 1;
            Colliders[47, 13] = 1;
            Colliders[46, 13] = 1;
            Colliders[45, 13] = 1;
            Colliders[45, 12] = 1;
            Colliders[45, 11] = 1;
            Colliders[45, 10] = 1;
            Colliders[45, 7] = 1;
            Colliders[45, 6] = 1;
            Colliders[45, 5] = 1;
            Colliders[45, 4] = 1;
            Colliders[45, 3] = 1;
            Colliders[45, 2] = 1;
            Colliders[46, 2] = 1;
            Colliders[47, 2] = 1;
            Colliders[48, 2] = 1;
            Colliders[49, 2] = 1;
            Colliders[50, 2] = 1;
            Colliders[51, 2] = 1;
            Colliders[52, 2] = 1;
            Colliders[53, 2] = 1;
            Colliders[54, 2] = 1;
            Colliders[55, 2] = 1;
            Colliders[56, 2] = 1;
            Colliders[56, 4] = 1;
            Colliders[56, 3] = 1;
        }

        private string wow;

        private void CheckCollision() {

            int y_upper = (int)((Canvas.GetTop(Hero) + (3 * Hero.Height / 4) - Canvas.GetTop(Mansion)) / GridY_mult);
            int y_bottom = y_upper + (int)(Hero.Height / 4) / GridY_mult;

            int x_left = (int)((Canvas.GetLeft(Hero) + Canvas.GetLeft(Mansion)) / GridX_mult);
            int x_right = x_left + (int)Hero.Width / GridX_mult;

            //Colisão com objetos:
            if (Colliders[y_upper, x_left] == 1 || Colliders[y_upper, x_right] == 1 ||
                Colliders[y_bottom, x_left] == 1 || Colliders[y_bottom, x_right] == 1) {

                Canvas.SetTop(Hero, Canvas.GetTop(Hero) - y);
                Canvas.SetLeft(Hero, Canvas.GetLeft(Hero) - x);
            }

            wow = "[" + (x_left)   + "][" + y_upper + "]\n" + Canvas.GetTop(Hero);
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

            Debug.Write(" Colliders[" + row + "][" + column + "] = ");  

        }

        private void walk(object sender, object e) {

            if (Canvas.GetLeft(Hero) + (Hero.Width / 2) == ScreenWidth / 2) {

                if ((x < 0 && Canvas.GetLeft(Mansion) < 0) || (x > 0 && Canvas.GetLeft(Mansion) > ScreenWidth - Mansion.Width)) {

                    isCameraHorizontal = true;
                    Canvas.SetLeft(Mansion, Canvas.GetLeft(Mansion) - x);
                    //Canvas.SetLeft(Colliders, Canvas.GetLeft(Colliders) - x);
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
                    //Canvas.SetTop(Colliders, Canvas.GetTop(Colliders) - y);
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
