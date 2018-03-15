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
using MantaNecromante.MainBattle;

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

        private double topSide;
        private double botSide;

        private double leftSide;
        private double rightSide;

        private double x = 0, y = 0;
        private int ScreenHeight, ScreenWidth;

        private List<Image> MovableProps = new List<Image>();

        public MainStage() {

            this.InitializeComponent();

            Adjuster.AdjustWindow(Floor);
            Adjuster.adjustForCamera(Mansion, Hero, ref ScreenWidth, ref ScreenHeight);

            GridX_mult = (int)Mansion.Width / 100;
            GridY_mult = (int)Mansion.Height / 100;

            //CreateGrid();
            setBlocks();
            setEnemies();
            setAnItem();


            topSide = (3 * Hero.Height / 4) / GridY_mult;
            botSide = (1 * Hero.Height / 4) / GridY_mult;

            leftSide = 0;
            rightSide = Hero.Width / GridX_mult;

            Window.Current.CoreWindow.KeyDown += keySentinel;
            Window.Current.CoreWindow.KeyUp += keyDropped;

            walkTimer.Interval = System.TimeSpan.FromMilliseconds(1);
            walkTimer.Tick += walk;

            walkTimer.Start();
        }

        private void x_movePropsAlongWithCamera() {

            foreach (Image prop in MovableProps) {

                Canvas.SetLeft(prop, Canvas.GetLeft(prop) - x);
            }
        }

        private void y_movePropsAlongWithCamera() {

            foreach (Image prop in MovableProps) {

                Canvas.SetTop(prop, Canvas.GetTop(prop) - y);
            }
        }

        private void x_RetrievePropsPosition() {

            foreach (Image prop in MovableProps) {

                Canvas.SetLeft(prop, Canvas.GetLeft(prop) + x);
            }
        }

        private void y_RetrievePropsPosition() {

            foreach (Image prop in MovableProps) {

                Canvas.SetTop(prop, Canvas.GetTop(prop) + y);
            }
        }

        private void setBlocks() {
            Colliders[93, 20] = 1;
            Colliders[92, 20] = 1;
            Colliders[91, 20] = 1;
            Colliders[91, 21] = 1;
            Colliders[91, 22] = 1;
            Colliders[91, 23] = 1;
            Colliders[91, 24] = 1;
            Colliders[91, 25] = 1;
            Colliders[91, 26] = 1;
            Colliders[91, 27] = 1;
            Colliders[91, 28] = 1;
            Colliders[91, 29] = 1;
            Colliders[91, 30] = 1;
            Colliders[91, 31] = 1;
            Colliders[91, 32] = 1;
            Colliders[91, 33] = 1;
            Colliders[91, 34] = 1;
            Colliders[90, 34] = 1;
            Colliders[89, 34] = 1;
            Colliders[88, 34] = 1;
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
            Colliders[87, 20] = 1;
            Colliders[86, 20] = 1;
            Colliders[85, 20] = 1;
            Colliders[84, 20] = 1;
            Colliders[83, 20] = 1;
            Colliders[82, 20] = 1;
            Colliders[81, 20] = 1;
            Colliders[81, 19] = 1;
            Colliders[81, 18] = 1;
            Colliders[81, 17] = 1;
            Colliders[82, 17] = 1;
            Colliders[83, 17] = 1;
            Colliders[83, 16] = 1;
            Colliders[83, 15] = 1;
            Colliders[83, 14] = 1;
            Colliders[83, 13] = 1;
            Colliders[82, 13] = 1;
            Colliders[81, 13] = 1;
            Colliders[81, 12] = 1;
            Colliders[81, 11] = 1;
            Colliders[81, 10] = 1;
            Colliders[90, 17] = 1;
            Colliders[90, 16] = 1;
            Colliders[90, 15] = 1;
            Colliders[90, 14] = 1;
            Colliders[90, 13] = 1;
            Colliders[90, 13] = 1;
            Colliders[90, 12] = 1;
            Colliders[90, 11] = 1;
            Colliders[90, 10] = 1;
            Colliders[90, 9] = 1;
            Colliders[90, 8] = 1;
            Colliders[90, 7] = 1;
            Colliders[90, 6] = 1;
            Colliders[90, 5] = 1;
            Colliders[90, 4] = 1;
            Colliders[89, 4] = 1;
            Colliders[88, 4] = 1;
            Colliders[87, 4] = 1;
            Colliders[86, 5] = 1;
            Colliders[87, 6] = 1;
            Colliders[86, 7] = 1;
            Colliders[87, 8] = 1;
            Colliders[86, 8] = 1;
            Colliders[87, 9] = 1;
            Colliders[88, 9] = 1;
            Colliders[89, 10] = 1;
            Colliders[89, 11] = 1;
            Colliders[89, 12] = 1;
            Colliders[88, 12] = 1;
            Colliders[87, 13] = 1;
            Colliders[86, 13] = 1;
            Colliders[86, 15] = 1;
            Colliders[87, 15] = 1;
            Colliders[87, 14] = 1;
            Colliders[87, 16] = 1;
            Colliders[86, 17] = 1;
            Colliders[87, 17] = 1;
            Colliders[88, 17] = 1;
            Colliders[89, 17] = 1;
            Colliders[93, 19] = 1;
            Colliders[93, 18] = 1;
            Colliders[93, 17] = 1;
            Colliders[93, 16] = 1;
            Colliders[93, 15] = 1;
            Colliders[93, 14] = 1;
            Colliders[93, 13] = 1;
            Colliders[93, 12] = 1;
            Colliders[93, 11] = 1;
            Colliders[93, 10] = 1;
            Colliders[93, 9] = 1;
            Colliders[93, 8] = 1;
            Colliders[93, 7] = 1;
            Colliders[93, 6] = 1;
            Colliders[93, 5] = 1;
            Colliders[93, 4] = 1;
            Colliders[93, 3] = 1;
            Colliders[93, 2] = 1;
            Colliders[93, 1] = 1;
            Colliders[92, 1] = 1;
            Colliders[91, 1] = 1;
            Colliders[90, 1] = 1;
            Colliders[89, 1] = 1;
            Colliders[88, 1] = 1;
            Colliders[87, 1] = 1;
            Colliders[86, 1] = 1;
            Colliders[85, 1] = 1;
            Colliders[84, 1] = 1;
            Colliders[83, 1] = 1;
            Colliders[83, 2] = 1;
            Colliders[83, 3] = 1;
            Colliders[83, 4] = 1;
            Colliders[82, 4] = 1;
            Colliders[81, 4] = 1;
            Colliders[81, 5] = 1;
            Colliders[81, 6] = 1;
            Colliders[81, 7] = 1;
            Colliders[80, 7] = 1;
            Colliders[79, 7] = 1;
            Colliders[78, 7] = 1;
            Colliders[81, 10] = 1;
            Colliders[80, 10] = 1;
            Colliders[79, 10] = 1;
            Colliders[78, 10] = 1;
            Colliders[77, 10] = 1;
            Colliders[76, 10] = 1;
            Colliders[77, 7] = 1;
            Colliders[76, 7] = 1;
            Colliders[75, 7] = 1;
            Colliders[74, 7] = 1;
            Colliders[73, 7] = 1;
            Colliders[75, 10] = 1;
            Colliders[74, 10] = 1;
            Colliders[72, 10] = 1;
            Colliders[73, 10] = 1;
            Colliders[71, 10] = 1;
            Colliders[70, 10] = 1;
            Colliders[69, 10] = 1;
            Colliders[68, 10] = 1;
            Colliders[67, 10] = 1;
            Colliders[66, 10] = 1;
            Colliders[65, 10] = 1;
            Colliders[64, 10] = 1;
            Colliders[63, 10] = 1;
            Colliders[62, 10] = 1;
            Colliders[72, 7] = 1;
            Colliders[71, 7] = 1;
            Colliders[70, 7] = 1;
            Colliders[69, 7] = 1;
            Colliders[68, 7] = 1;
            Colliders[67, 7] = 1;
            Colliders[66, 7] = 1;
            Colliders[65, 7] = 1;
            Colliders[64, 7] = 1;
            Colliders[63, 7] = 1;
            Colliders[62, 7] = 1;
            Colliders[61, 10] = 1;
            Colliders[61, 7] = 1;
            Colliders[61, 6] = 1;
            Colliders[61, 5] = 1;
            Colliders[61, 4] = 1;
            Colliders[61, 3] = 1;
            Colliders[61, 2] = 1;
            Colliders[61, 1] = 1;
            Colliders[60, 1] = 1;
            Colliders[59, 1] = 1;
            Colliders[61, 11] = 1;
            Colliders[61, 12] = 1;
            Colliders[61, 15] = 1;
            Colliders[61, 16] = 1;
            Colliders[61, 17] = 1;
            Colliders[61, 18] = 1;
            Colliders[61, 19] = 1;
            Colliders[61, 20] = 1;
            Colliders[62, 15] = 1;
            Colliders[63, 15] = 1;
            Colliders[64, 15] = 1;
            Colliders[65, 15] = 1;
            Colliders[66, 15] = 1;
            Colliders[67, 15] = 1;
            Colliders[68, 15] = 1;
            Colliders[69, 15] = 1;
            Colliders[69, 16] = 1;
            Colliders[69, 17] = 1;
            Colliders[69, 18] = 1;
            Colliders[70, 19] = 1;
            Colliders[70, 18] = 1;
            Colliders[69, 20] = 1;
            Colliders[69, 21] = 1;
            Colliders[69, 22] = 1;
            Colliders[69, 23] = 1;
            Colliders[69, 24] = 1;
            Colliders[69, 25] = 1;
            Colliders[69, 26] = 1;
            Colliders[70, 26] = 1;
            Colliders[70, 27] = 1;
            Colliders[70, 28] = 1;
            Colliders[69, 28] = 1;
            Colliders[69, 29] = 1;
            Colliders[69, 30] = 1;
            Colliders[69, 31] = 1;
            Colliders[69, 32] = 1;
            Colliders[70, 32] = 1;
            Colliders[60, 20] = 1;
            Colliders[59, 20] = 1;
            Colliders[58, 20] = 1;
            Colliders[57, 20] = 1;
            Colliders[59, 2] = 1;
            Colliders[59, 3] = 1;
            Colliders[59, 4] = 1;
            Colliders[59, 5] = 1;
            Colliders[59, 6] = 1;
            Colliders[58, 6] = 1;
            Colliders[57, 6] = 1;
            Colliders[56, 6] = 1;
            Colliders[55, 6] = 1;
            Colliders[55, 5] = 1;
            Colliders[55, 4] = 1;
            Colliders[55, 3] = 1;
            Colliders[54, 4] = 1;
            Colliders[53, 4] = 1;
            Colliders[52, 4] = 1;
            Colliders[51, 4] = 1;
            Colliders[50, 5] = 1;
            Colliders[50, 7] = 1;
            Colliders[50, 8] = 1;
            Colliders[50, 9] = 1;
            Colliders[51, 9] = 1;
            Colliders[52, 9] = 1;
            Colliders[53, 9] = 1;
            Colliders[52, 10] = 1;
            Colliders[52, 11] = 1;
            Colliders[52, 12] = 1;
            Colliders[52, 13] = 1;
            Colliders[51, 13] = 1;
            Colliders[50, 13] = 1;
            Colliders[51, 14] = 1;
            Colliders[51, 15] = 1;
            Colliders[51, 12] = 1;
            Colliders[50, 15] = 1;
            Colliders[51, 16] = 1;
            Colliders[50, 17] = 1;
            Colliders[51, 17] = 1;
            Colliders[52, 17] = 1;
            Colliders[53, 17] = 1;
            Colliders[54, 17] = 1;
            Colliders[54, 16] = 1;
            Colliders[54, 15] = 1;
            Colliders[54, 14] = 1;
            Colliders[54, 12] = 1;
            Colliders[54, 13] = 1;
            Colliders[54, 11] = 1;
            Colliders[54, 10] = 1;
            Colliders[54, 9] = 1;
            Colliders[54, 8] = 1;
            Colliders[54, 7] = 1;
            Colliders[54, 6] = 1;
            Colliders[62, 12] = 1;
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
            Colliders[74, 12] = 1;
            Colliders[73, 12] = 1;
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
            Colliders[74, 26] = 1;
            Colliders[74, 27] = 1;
            Colliders[74, 28] = 1;
            Colliders[75, 28] = 1;
            Colliders[76, 28] = 1;
            Colliders[77, 28] = 1;
            Colliders[78, 28] = 1;
            Colliders[71, 32] = 1;
            Colliders[72, 32] = 1;
            Colliders[73, 32] = 1;
            Colliders[74, 32] = 1;
            Colliders[75, 32] = 1;
            Colliders[76, 32] = 1;
            Colliders[77, 32] = 1;
            Colliders[78, 32] = 1;
            Colliders[79, 32] = 1;
            Colliders[80, 32] = 1;
            Colliders[80, 30] = 1;
            Colliders[80, 31] = 1;
            Colliders[80, 29] = 1;
            Colliders[80, 28] = 1;
            Colliders[79, 28] = 1;
            Colliders[56, 20] = 1;
            Colliders[55, 20] = 1;
            Colliders[54, 20] = 1;
            Colliders[53, 20] = 1;
            Colliders[52, 20] = 1;
            Colliders[51, 20] = 1;
            Colliders[50, 20] = 1;
            Colliders[49, 20] = 1;
            Colliders[48, 20] = 1;
            Colliders[47, 20] = 1;
            Colliders[47, 21] = 1;
            Colliders[47, 22] = 1;
            Colliders[47, 23] = 1;
            Colliders[48, 23] = 1;
            Colliders[49, 23] = 1;
            Colliders[50, 23] = 1;
            Colliders[47, 26] = 1;
            Colliders[48, 26] = 1;
            Colliders[49, 26] = 1;
            Colliders[50, 26] = 1;
            Colliders[51, 26] = 1;
            Colliders[52, 26] = 1;
            Colliders[53, 26] = 1;
            Colliders[54, 26] = 1;
            Colliders[44, 17] = 1;
            Colliders[44, 18] = 1;
            Colliders[44, 19] = 1;
            Colliders[44, 20] = 1;
            Colliders[44, 21] = 1;
            Colliders[44, 22] = 1;
            Colliders[44, 23] = 1;
            Colliders[44, 24] = 1;
            Colliders[44, 25] = 1;
            Colliders[45, 17] = 1;
            Colliders[46, 17] = 1;
            Colliders[47, 17] = 1;
            Colliders[47, 16] = 1;
            Colliders[47, 15] = 1;
            Colliders[47, 14] = 1;
            Colliders[46, 13] = 1;
            Colliders[45, 13] = 1;
            Colliders[44, 13] = 1;
            Colliders[44, 12] = 1;
            Colliders[44, 11] = 1;
            Colliders[44, 10] = 1;
            Colliders[43, 10] = 1;
            Colliders[44, 6] = 1;
            Colliders[44, 7] = 1;
            Colliders[43, 7] = 1;
            Colliders[42, 7] = 1;
            Colliders[44, 5] = 1;
            Colliders[44, 4] = 1;
            Colliders[44, 3] = 1;
            Colliders[44, 2] = 1;
            Colliders[44, 1] = 1;
            Colliders[45, 1] = 1;
            Colliders[46, 1] = 1;
            Colliders[47, 1] = 1;
            Colliders[48, 1] = 1;
            Colliders[49, 1] = 1;
            Colliders[50, 1] = 1;
            Colliders[51, 1] = 1;
            Colliders[52, 1] = 1;
            Colliders[53, 1] = 1;
            Colliders[54, 1] = 1;
            Colliders[55, 1] = 1;
            Colliders[55, 2] = 1;

        }

        private void setAnItem() {

            Colliders[79, 30] = 2;

            Image chest = new Image();

            chest.Width = 80;
            chest.Height = 48;

            chest.Source = new BitmapImage(new Uri("ms-appx:///GameAssets/Maps/chest_idle.png"));

            Floor.Children.Add(chest);

            Canvas.SetLeft(chest, 30 * GridY_mult + Canvas.GetLeft(Mansion) + GridY_mult / 2 - chest.Width / 2);
            Canvas.SetTop(chest, 79 * GridX_mult + Canvas.GetTop(Mansion) + GridX_mult / 2 - chest.Height / 2);

            MovableProps.Add(chest);

        }

        private void setEnemies() {

            Colliders[72, 24] = 3;
        }

        private string wow;

        private void CheckCollision() {

            int y_upper = (int)(((Canvas.GetTop(Hero) - Canvas.GetTop(Mansion)) / GridY_mult) + topSide);
            int y_bottom = (int)((y_upper / GridY_mult) + botSide);

            int x_left = (int)((((Canvas.GetLeft(Hero)) - Canvas.GetLeft(Mansion)) / GridX_mult) + leftSide);
            int x_right = x_left + (int)(rightSide);

            //Colisão com objetos:
            if (Colliders[y_upper, x_left] == 1 || Colliders[y_upper, x_right] == 1 ||
                Colliders[y_bottom, x_left] == 1 || Colliders[y_bottom, x_right] == 1) {

                if (isCameraHorizontal) {
                    Canvas.SetLeft(Mansion, Canvas.GetLeft(Mansion) + x);

                    x_RetrievePropsPosition();

                    //Apenas para testes//
                    Canvas.SetLeft(Colliderss, Canvas.GetLeft(Colliderss) + x);
                }
                else
                    Canvas.SetLeft(Hero, Canvas.GetLeft(Hero) - x);


                if (isCameraVertical) {
                    Canvas.SetTop(Mansion, Canvas.GetTop(Mansion) + y);

                    y_RetrievePropsPosition();

                    //Apenas para testes//
                    Canvas.SetTop(Colliderss, Canvas.GetTop(Colliderss) + y);
                }
                else
                    Canvas.SetTop(Hero, Canvas.GetTop(Hero) - y);
            }
            else if (Colliders[y_upper, x_left] == 2 || Colliders[y_upper, x_right] == 2 ||
                     Colliders[y_bottom, x_left] == 2 || Colliders[y_bottom, x_right] == 2) {

                    Canvas.SetTop(infoBox, Canvas.GetTop(Hero) - Hero.Height / 2);
                    Canvas.SetLeft(infoBox, Canvas.GetLeft(Hero) + Hero.Width / 2 - infoBox.Width / 2);

                    infoBox.Opacity = 1;


            }
            else if (Colliders[y_upper, x_left] == 3 || Colliders[y_upper, x_right] == 3 ||
                     Colliders[y_bottom, x_left] == 3 || Colliders[y_bottom, x_right] == 3) {

                        this.Frame.Navigate(typeof(BattleStage));
                 } else {
                infoBox.Opacity = 1;
            }
            

                wow = Canvas.GetTop(infoBox) + ", " + Canvas.GetLeft(infoBox) + ", " + Canvas.GetLeft(Hero)  + ", " + Canvas.GetTop(Hero);
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

        private int column = 1, row = 1;

        private void exitInventory(object sender, RoutedEventArgs e) {

            Inventory.Opacity = (Inventory.Opacity == 1) ? 0 : 1;
        }

        private void gettingCell(object sender, TappedRoutedEventArgs e) {

            var cell = (Border)sender;
            cell.Background = new SolidColorBrush(Colors.Red);

            column = Grid.GetColumn(cell);
            row = Grid.GetRow(cell);

            Debug.WriteLine("Colliders[" + row + ", " + column + "] = 1;");

        }

        private void walk(object sender, object e) {

            if (Canvas.GetLeft(Hero) + (Hero.Width / 2) == ScreenWidth / 2) {

                if ((x < 0 && Canvas.GetLeft(Mansion) < 0) || (x > 0 && Canvas.GetLeft(Mansion) > ScreenWidth - Mansion.Width)) {

                    isCameraHorizontal = true;
                    Canvas.SetLeft(Mansion, Canvas.GetLeft(Mansion) - x);

                    x_movePropsAlongWithCamera();

                    //Apenas Teste//
                    Canvas.SetLeft(Colliderss, Canvas.GetLeft(Colliderss) - x);
                }
                else {

                    isCameraHorizontal = false;
                    Canvas.SetLeft(Hero, Canvas.GetLeft(Hero) + x);
                }
            }
            else {

                isCameraHorizontal = false;
                Canvas.SetLeft(Hero, Canvas.GetLeft(Hero) + x);
            }

            if (Canvas.GetTop(Hero) + (Hero.Height / 2) == ScreenHeight / 2) {

                if ((y < 0 && Canvas.GetTop(Mansion) < 0) || (y > 0 && Canvas.GetTop(Mansion) > ScreenHeight - Mansion.Height)) {

                    isCameraVertical = true;
                    Canvas.SetTop(Mansion, Canvas.GetTop(Mansion) - y);

                    y_movePropsAlongWithCamera();

                    //Apenas Teste//
                    Canvas.SetTop(Colliderss, Canvas.GetTop(Colliderss) - y);
                }
                else {

                    isCameraVertical = false;
                    Canvas.SetTop(Hero, Canvas.GetTop(Hero) + y);
                }

            }
            else {

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
                case Windows.System.VirtualKey.I:
                    isMovementKey = false;
                    Inventory.Opacity = (Inventory.Opacity == 1) ? 0 : 1;
                    break;
                case Windows.System.VirtualKey.W:
                case Windows.System.VirtualKey.Up:
                    y = -speed;
                    x = 0;
                    break;
                case Windows.System.VirtualKey.S:
                case Windows.System.VirtualKey.Down:
                    y = speed;
                    x = 0;
                    break;
                case Windows.System.VirtualKey.D:
                case Windows.System.VirtualKey.Right:
                    direction = runtoRight;
                    x = speed;
                    y = 0;
                    break;
                case Windows.System.VirtualKey.A:
                case Windows.System.VirtualKey.Left:
                    direction = runtoLeft;
                    x = -speed;
                    y = 0;
                    break;
                case Windows.System.VirtualKey.G:
                    Colliderss.Opacity = (Colliderss.Opacity == 1) ? 0 : 1;
                    isMovementKey = false;
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
                case Windows.System.VirtualKey.Up:
                case Windows.System.VirtualKey.S:
                case Windows.System.VirtualKey.Down:
                    y = 0;
                    break;
                case Windows.System.VirtualKey.A:
                case Windows.System.VirtualKey.Left:
                case Windows.System.VirtualKey.D:
                case Windows.System.VirtualKey.Right:
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
