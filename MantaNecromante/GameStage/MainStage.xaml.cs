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

        //Timer usado para o movimento:
        //..................................//
        DispatcherTimer walkTimer = new DispatcherTimer();  //Timer onde a magia acontece.
        //..................................//

        //Bools auto-explicativos:
        //..................................//
        private bool isCameraHorizontal, isCameraVertical;  //Checa se a "câmera" se move na horizontal, vertical, ou nenhum dos dois.
        private bool isMovementKey;
        private bool isOptionsMenuOpen;
        private bool isInventoryOpen;
        //..................................//

        //BitmapImages para salvar os gifs de movimento das classes que o herói pode ser:
        //..................................//
        private BitmapImage direction;
        private BitmapImage runtoLeft, runtoRight;
        private BitmapImage idletoLeft, idletoRight;
        //..................................//

        //Variáveis relacionados à matriz interna que controla as interações do herói com o mapa:
        //..................................//
        private Map_controller controller = new Map_controller();              //Conexão com a back end, que entrega à front end os itens, inimigos, etc... 
        private int GridX_mult, GridY_mult;             //Tamanho de cada célula da matriz (largura e altura), relativo ao tamanho do mapa (ajustável).
        private int[,] Colliders = new int[102, 102];   //Matriz do mapa.

        private int left_column, right_column;          //Utiliza-se dos doubles abaixo para determinar as células da matriz em que o héroi está.
        private int upper_row, bottom_row;              // Idem ao que está acima.
        //..................................//


        //Doubles para determinar os vértices efetivos da herói nos quais será checado a colisão (utilizados nos int's acima):
        //..................................//
        private double topSide, botSide;
        private double leftSide, rightSide;
        //.................................//

        //x e y que determinam o quanto para a horizontal e vertical o herói vai se deslocar:
        //..................................//
        private double x = 0, y = 0;
        private int ScreenHeight, ScreenWidth;
        //..................................//

        //Valores estáticos, pois não se conseguiu fazer a matriz de colisão ajustável:
        //..................................//
        private static int chestHeight = 48, chestWidth = 80;
        //..................................//

        //para testes:
        //..................................//
        private Grid Colliderss = new Grid();
        private int column = 1, row = 1;
        private string testBox;
        //..................................//

        //Lista de Imagens dos inimigos e itens que precisam se mover junto com o mapa para criar a ilusão de movimento de câmera:
        //..........................................................//
        private List<Image> MovableProps = new List<Image>();
        //..........................................................//

        public MainStage() {

            this.InitializeComponent();
            
            //Fazendo os ajustes para para tudo rodar "perfeitamente":
            //..................................//
            Adjuster.AdjustWindow(Floor);
            Adjuster.adjustForCamera(Mansion, Hero, ref ScreenWidth, ref ScreenHeight, ref GridX_mult, ref GridY_mult, ref topSide, ref botSide, ref leftSide, ref rightSide);
            //..................................//

           //CreateGrid();
            setBlocks();
            setEnemies();
            SetItem();

            setAllMenusReady();

            this.KeyDown += keySentinel;
            this.KeyUp += keyDropped;

            walkTimer.Interval = System.TimeSpan.FromMilliseconds(1);
            walkTimer.Tick += walk;

            walkTimer.Start();
        }

        private void setAllMenusReady() {

            Floor.Children.Remove(quick_menu);
            Floor.Children.Add(quick_menu);

            Floor.Children.Remove(Inventory);
            isInventoryOpen = false;

            Floor.Children.Remove(OptionsMenu);
            isOptionsMenuOpen = false;
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
            Colliders[73, 13] = 1;
            Colliders[73, 14] = 1;
            Colliders[73, 15] = 1;
            Colliders[73, 16] = 1;
            Colliders[73, 17] = 1;
            Colliders[73, 18] = 1;
            Colliders[73, 19] = 1;
            Colliders[73, 20] = 1;
            Colliders[73, 21] = 1;
            Colliders[73, 22] = 1;
            Colliders[73, 23] = 1;
            Colliders[73, 24] = 1;
            Colliders[73, 25] = 1;
            Colliders[73, 26] = 1;
            Colliders[73, 27] = 1;
            Colliders[73, 28] = 1;

        }

        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// 


        //BACK END
        private void SetItem() {

            Colliders[79, 30] = 2;

            Image chest = new Image();

            chest.Width = chestWidth;
            chest.Height = chestHeight;

            chest.Source = new BitmapImage(new Uri("ms-appx:///GameAssets/Maps/chest_idle.png"));

            Floor.Children.Add(chest);

            Canvas.SetLeft(chest, 30 * GridX_mult + Canvas.GetLeft(Mansion) + GridX_mult / 2 - chest.Width / 2);
            Canvas.SetTop(chest, 79 * GridY_mult + Canvas.GetTop(Mansion) + GridY_mult / 2 - chest.Height / 2);

            MovableProps.Add(chest);
        }

        public Image getItem(int row, int column) {

            Image foe = new Image();

            double x = column * GridX_mult + Canvas.GetLeft(Mansion) + GridX_mult / 2 - chestWidth / 2;
            double y = row * GridY_mult + Canvas.GetTop(Mansion) + GridY_mult / 2 - chestHeight / 2;

            foreach (Image item in MovableProps) {

                if (Canvas.GetTop(item) == y || Canvas.GetLeft(item) == x) {

                    return item;
                }
            }

            return null;
        }


        //private Image getEnemy(int row, int column) {

        //    Image foe = new Image();

        //    double x = column * GridX_mult + Canvas.GetLeft(Mansion) + GridX_mult / 2 - foe.Width / 2;
        //    double y = row * GridY_mult + Canvas.GetTop(Mansion) + GridY_mult / 2 - foe.Height / 2;

        //    foreach (Image item in MovableProps) {

        //        if (Canvas.GetTop(item) == y || Canvas.GetLeft(item) == x) {

        //            return item;
        //        }
        //    }

        //    return null;

        //}

        private void setEnemies() {

            Colliders[72, 24] = 3;

            controller.setMob(72, 24);

            Mob foe = controller.FindMob(72, 24);

            Image enemy = new Image();

            enemy.Height = Hero.Height;
            enemy.Width = enemy.Width;

            enemy.Source = foe.Sprite;

            Floor.Children.Add(enemy);

            Canvas.SetLeft(enemy, 24 * GridX_mult + Canvas.GetLeft(Mansion) + GridX_mult / 2 - enemy.Width / 2);
            Canvas.SetTop(enemy, 72 * GridY_mult + Canvas.GetTop(Mansion) + GridY_mult / 2 - enemy.Height / 2);

            MovableProps.Add(enemy);
        }

        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        ///

        //Aqui é feito o retorno do herói/Mapa a sua posição anterior à atual, em que ele colide com o cenário:
        //Caso seja o mapa se movendo, ele é quem retorna a coordenada de canvas anterior.
        //..........................................................//
        private void bumpWithScenario() {

            if (isCameraHorizontal) {
                Canvas.SetLeft(Mansion, Canvas.GetLeft(Mansion) + x);

                x_RetrievePropsPosition();

                //Apenas para testes//
                Canvas.SetLeft(Colliderss, Canvas.GetLeft(Colliderss) + x);
            }
            else {

                Canvas.SetLeft(Hero, Canvas.GetLeft(Hero) - x);
            }


            if (isCameraVertical) {
                Canvas.SetTop(Mansion, Canvas.GetTop(Mansion) + y);

                y_RetrievePropsPosition();

                //Apenas para testes//
                Canvas.SetTop(Colliderss, Canvas.GetTop(Colliderss) + y);
            }
            else {

                Canvas.SetTop(Hero, Canvas.GetTop(Hero) - y);
            }
        }
        //..........................................................//

        private void CheckCollision() {

            //Aqui há o cálculo para determinar em quais células da matriz os vértices do herói estão:
            //....................................................................................................\\
            upper_row = (int)(((Canvas.GetTop(Hero) - Canvas.GetTop(Mansion)) / GridY_mult) + topSide);
            bottom_row = (int)((upper_row / GridY_mult) + botSide);

            left_column = (int)((((Canvas.GetLeft(Hero)) - Canvas.GetLeft(Mansion)) / GridX_mult) + leftSide);
            right_column = left_column + (int)(rightSide);
            //....................................................................................................\\


            //Aqui procuro saber se as 4 células da matriz em que se situam os vértices do herói estão em células de colisão (1),
            //células de inimigos (3) ou células de itens (2) ou em nenhuma das três (0).
            //..............................................................................................................................................................................................................\\
            if (Colliders[upper_row, left_column] == 1 || Colliders[upper_row, right_column] == 1 || Colliders[bottom_row, left_column] == 1 || Colliders[bottom_row, right_column] == 1) {

                bumpWithScenario();
            }
            else if (Colliders[upper_row, left_column] == 2 || Colliders[upper_row, right_column] == 2 || Colliders[bottom_row, left_column] == 2 || Colliders[bottom_row, right_column] == 2) {

                Canvas.SetTop(infoBox, Canvas.GetTop(Hero) - Hero.Height / 2);
                Canvas.SetLeft(infoBox, Canvas.GetLeft(Hero) + Hero.Width / 2 - infoBox.Width / 2);

                infoBox.Opacity = 1;
            }
            else if (Colliders[upper_row, left_column] == 3 || Colliders[upper_row, right_column] == 3 || Colliders[bottom_row, left_column] == 3 || Colliders[bottom_row, right_column] == 3) {

                int x = 0, y = 0;

                if (Colliders[upper_row, left_column] == 3) {

                    x = left_column;
                    y = upper_row;
                }
                else if (Colliders[upper_row, right_column] == 3) {

                    x = right_column;
                    y = upper_row;
                }
                else if (Colliders[bottom_row, left_column] == 3) {

                    x = left_column;
                    y = bottom_row;
                }
                else if (Colliders[bottom_row, right_column] == 3) {

                    x = right_column;
                    y = bottom_row;
                }

                //       Image foe = getEnemy(y, x);
                Colliders[y, x] = 0;

                this.Frame.Navigate(typeof(BattleStage));
            }
            else {

                infoBox.Opacity = 0;
            }
        }

        //Ao apertar 'E' para interagir, é necessário saber se uma das célula em que se encontra os vértices do herói é uma célula de itens.
        //..............................................................................................//
        private void interact() {

            Image item = new Image();
            int x = 0, y = 0;

            if (Colliders[upper_row, left_column] == 2) {

                x = left_column;
                y = upper_row;
            }
            else if (Colliders[upper_row, right_column] == 2) {

                x = right_column;
                y = upper_row;
            }
            else if (Colliders[bottom_row, left_column] == 2) {

                x = left_column;
                y = bottom_row;
            }
            else if (Colliders[bottom_row, right_column] == 2) {

                x = right_column;
                y = bottom_row;
            }

            item = getItem(y, x);
            Colliders[y, x] = 1;

            item.Source = new BitmapImage(new Uri("ms-appx:///GameAssets/Maps/chest_open.gif"));
        }
        //..............................................................................................//

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

        private void exitInventory(object sender, RoutedEventArgs e) {

            Floor.Children.Remove(Inventory);
            isInventoryOpen = false;
        }

        private void gettingCell(object sender, TappedRoutedEventArgs e) {

            var cell = (Border)sender;
            cell.Background = new SolidColorBrush(Colors.Red);

            column = Grid.GetColumn(cell);
            row = Grid.GetRow(cell);

            Debug.WriteLine("Colliders[" + row + ", " + column + "] = 1;");

        }

        private void walk(object sender, object e) {

            //If statements para decidir qual imagem deve se mover: a do herói ou a do mapa,gerando a ilusão de uma câmera seguindo o personagem.
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

            Player chosen = (Player)e.Parameter;


            idletoLeft = chosen.Sprite_idle_left;
            idletoRight = chosen.Sprite_idle_right;

            runtoLeft = chosen.Sprite_walking_left;
            runtoRight = chosen.Sprite_walking_right;

            direction = runtoRight;
            Hero.Source = idletoRight;
        }

        private void keySentinel(object sender, KeyRoutedEventArgs e) {

            isMovementKey = true;

            int speed = 5;

            switch (e.Key) {

                case Windows.System.VirtualKey.Escape:

                    isMovementKey = false;

                    if (isOptionsMenuOpen) Floor.Children.Remove(OptionsMenu);
                    else Floor.Children.Add(OptionsMenu);

                    isOptionsMenuOpen ^= true;

                    break;
                case Windows.System.VirtualKey.I:

                    isMovementKey = false;

                    if (isInventoryOpen) Floor.Children.Remove(Inventory);
                    else Floor.Children.Add(Inventory);

                    isInventoryOpen ^= true;

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
                case Windows.System.VirtualKey.E:

                    interact();
                    break;
                default:
                    isMovementKey = false;
                    break;
            }

            if (Hero.Source != direction && isMovementKey) Hero.Source = direction;
        }

        private void keyDropped(object sender, KeyRoutedEventArgs e) {

            BitmapImage stand = null;

            switch (e.Key) {

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

            Floor.Children.Remove(OptionsMenu);
            isOptionsMenuOpen = false;
        }
    }
}
