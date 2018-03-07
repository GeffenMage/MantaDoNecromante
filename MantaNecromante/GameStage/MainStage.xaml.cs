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

        }

        private void walk(object sender, object e) {

            if (Canvas.GetLeft(Hero) + (Hero.Width / 2) == ScreenWidth / 2) {

                if ((x < 0 && Canvas.GetLeft(Mansion) < 0) || (x > 0 && Canvas.GetLeft(Mansion) > ScreenWidth - Mansion.Width)) {

                    Canvas.SetLeft(Mansion, Canvas.GetLeft(Mansion) - x);
                }
                else {

                    Canvas.SetLeft(Hero, Canvas.GetLeft(Hero) + x);
                }

            } else {

                Canvas.SetLeft(Hero, Canvas.GetLeft(Hero) + x);
            }

            if (Canvas.GetTop(Hero) + (Hero.Height / 2) == ScreenHeight / 2) {

                if ((y < 0 && Canvas.GetTop(Mansion) < 0) || (y > 0 && Canvas.GetTop(Mansion) > ScreenHeight - Mansion.Height)) {

                    Canvas.SetTop(Mansion, Canvas.GetTop(Mansion) - y);
                }
                else {

                    Canvas.SetTop(Hero, Canvas.GetTop(Hero) + y);
                }

            } else {

                Canvas.SetTop(Hero, Canvas.GetTop(Hero) + y);
            }
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
