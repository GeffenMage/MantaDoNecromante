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
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MantaNecromante.GameStage {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainStage : Page {

        DispatcherTimer walkTimer = new DispatcherTimer();

        private bool isMovementKey;
        private BitmapImage direction;
        private BitmapImage runtoLeft, runtoRight;
        private BitmapImage idletoLeft, idletoRight;


        private double x, y;
        private string choice;
        private int ScreenHeight, ScreenWidth;

        public MainStage() {

            this.InitializeComponent();

            ScreenWidth = (int)Window.Current.Bounds.Width;
            ScreenHeight = (int)Window.Current.Bounds.Height;

            int x_adjust = ScreenWidth % 10;
            int y_adjust = ScreenHeight % 10;

            ScreenWidth -= x_adjust;
            ScreenHeight -= y_adjust;


            Adjuster.AdjustWindow(Floor);
            Adjuster.adjustForCamera(Mansion, Hero);



            Debug.WriteLine(Start.Height + "," + Start.Width);

            Window.Current.CoreWindow.KeyDown += keySentinel;
            Window.Current.CoreWindow.KeyUp += keyDropped;

            walkTimer.Interval = System.TimeSpan.FromMilliseconds(1);
            walkTimer.Tick += walk;

            walkTimer.Start();

            Debug.WriteLine(ScreenWidth);

            //DrawGrid();

        }

        //private void DrawGrid() {


        //    for (int i = 0; i < 71; i++) {

        //        ColumnDefinition cd = new ColumnDefinition();
        //        cd.Width = new GridLength(10, GridUnitType.Pixel);
        //        test.ColumnDefinitions.Add(cd);
        //    }

        //    for (int i = 0; i < 79; i++) {

        //        RowDefinition cd = new RowDefinition();
        //        cd.Height = new GridLength(10, GridUnitType.Pixel);
        //        test.RowDefinitions.Add(cd);
        //    }

        //    for (int i = 0; i < 80; i++) {

        //        Line line = new Line();
        //        line.Stroke = new SolidColorBrush(Windows.UI.Colors.SeaGreen);
        //        line.StrokeThickness = 0.5;

        //        line.X1 = 0;
        //        line.X2 = 710;

        //        line.Y1 = 0;
        //        line.Y2 = 0;

        //        Canvas.SetTop(line, i * 10);
        //        Canvas.SetLeft(line, 0);

        //        Floor.Children.Add(line);
        //    }

        //    for (int i = 0; i < 72; i++) {

        //        Line line = new Line();
        //        line.Stroke = new SolidColorBrush(Windows.UI.Colors.SeaGreen);
        //        line.StrokeThickness = 0.5;

        //        line.X1 = 0;
        //        line.X2 = 0;

        //        line.Y1 = 0;
        //        line.Y2 = 791;

        //        Canvas.SetTop(line, 0);
        //        Canvas.SetLeft(line, i * 10);

        //        Floor.Children.Add(line);
        //    }

        //    for (int i = 0; i < 72; i++) {

        //        for (int j = 0; j < 80; j++) {

        //            Border border = new Border();

        //            border.Opacity = 0.3;
        //            border.BorderThickness = new Thickness(0.5);
        //            border.BorderBrush = new SolidColorBrush(Colors.Gray);

        //            border.HorizontalAlignment = HorizontalAlignment.Stretch;
        //            border.VerticalAlignment = VerticalAlignment.Stretch;

        //            Grid.SetColumn(border, i);
        //            Grid.SetRow(border, j);

        //            border.Background = new SolidColorBrush(Colors.Transparent);
        //            border.Tapped += Test_Tapped;

        //            test.Children.Add(border);
        //        }
        //    }
        //}

        //private void Test_Tapped(object sender, TappedRoutedEventArgs e) {

        //    // First way:
        //    var border = (Border)sender;
        //    var column = Grid.GetColumn(border);
        //    var row = Grid.GetRow(border);

        //    // Second way
        //    var point = e.GetPosition(Window.Current.Content);

        //    border.Opacity = 1;
        //    border.BorderBrush = new SolidColorBrush(Colors.Red);

        //    Debug.Write("CollisionGrid[" + row + "][" + column + "]; ");
        //}

        private void walk(object sender, object e) {

            if (Canvas.GetLeft(Hero) + (Hero.Width / 2) == ScreenWidth / 2) {

                if ((x < 0 && Canvas.GetLeft(Mansion) < 0) || (x > 0 && Canvas.GetLeft(Mansion) > ScreenWidth - Mansion.Width)) {

                    Canvas.SetLeft(Mansion, Canvas.GetLeft(Mansion) - x);
                }
                else {

                    Canvas.SetLeft(Hero, Canvas.GetLeft(Hero) + x);
                }

            }
            else {

                Canvas.SetLeft(Hero, Canvas.GetLeft(Hero) + x);
            }

            if (Canvas.GetTop(Hero) + (Hero.Height / 2) == ScreenHeight / 2) {

                if ((y < 0 && Canvas.GetTop(Mansion) < 0) || (y > 0 && Canvas.GetTop(Mansion) > ScreenHeight - Mansion.Height)) {

                    Canvas.SetTop(Mansion, Canvas.GetTop(Mansion) - y);
                }
                else {

                    Canvas.SetTop(Hero, Canvas.GetTop(Hero) + y);
                }

            }
            else {

                Canvas.SetTop(Hero, Canvas.GetTop(Hero) + y);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {

            string var = (string)e.Parameter;
            choice = var;

            idletoLeft = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/" + choice + "/idleLeft.gif"));
            idletoRight = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/" + choice + "/idleRight.gif"));

            runtoLeft = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/" + choice + "/walkLeft.gif"));
            runtoRight = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/" + choice + "/walkRight.gif"));

            direction = runtoRight;
            Hero.Source = idletoRight;
        }

        private void keySentinel(CoreWindow sender, KeyEventArgs e) {

            isMovementKey = true;

            switch (e.VirtualKey) {
                case Windows.System.VirtualKey.Escape:
                    isMovementKey = false;
                    OptionsMenu.Opacity = (OptionsMenu.Opacity == 1) ? 0 : 1;
                    break;
                case Windows.System.VirtualKey.W:
                    y = -5;
                    x = 0;
                    break;
                case Windows.System.VirtualKey.S:
                    y = 5;
                    x = 0;
                    break;
                case Windows.System.VirtualKey.D:
                    direction = runtoRight;
                    x = 5;
                    y = 0;
                    break;
                case Windows.System.VirtualKey.A:
                    direction = runtoLeft;
                    x = -5;
                    y = 0;
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
            else stand = idletoRight;

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
