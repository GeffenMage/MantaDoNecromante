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
        private bool isMovementKey;                         //Checa se a tecla pressionada é (WASD) de movimento.
        private bool isOptionsMenuOpen;                     //Checa se o menu estão no xalm;
        private bool isInventoryOpen;                       //Idem ao que está acima.
        private bool isInteractive;                         //Checa se na região do herói há algum item coletável. 
        //..................................//

        //BitmapImages para salvar os gifs de movimento das classes que o variam conforme a escolhe do usuário no menu de classes:
        //..................................//
        private BitmapImage direction;
        private BitmapImage runtoLeft, runtoRight;
        private BitmapImage idletoLeft, idletoRight;
        //..................................//

        //Variáveis relacionadas à matriz interna que controla as interações do herói com o mapa:
        //..................................//
        private Map_controller controller = new Map_controller();              //Conexão com a back end, que entrega à front end os itens, inimigos, etc... 
        private int[,] CollisionMatrix = new int[102, 102];                    //Matriz do mapa.
        private int Cell_Width, Cell_Height;                                   //Tamanho de cada célula da matriz (largura e altura), relativo ao tamanho do mapa (ajustável).

        private int left_column, right_column;          //Utiliza-se dos doubles abaixo para determinar as células da matriz em que o héroi está.
        private int upper_row, bottom_row;              // Idem ao que está acima.
        //..................................//

        //Doubles para determinar os vértices efetivos da herói nos quais será checado a colisão (utilizados nos int's acima):
        //..................................//
        private double topSide, botSide;
        private double leftSide, rightSide;
        //.................................//

        //A classe que o usuário escolheu e inimigo com o qual ele batalhará:
        //..................................//
        private Player chosen;
        private Mob foe;
        //..................................//

        //x e y que determinam o quanto para a horizontal e vertical o herói vai se deslocar:
        //..................................//
        private double x = 0, y = 0;
        private int ScreenHeight, ScreenWidth;      //Tamanho da tela usado para cálculos de ajuste.
        //..................................//

        //Valores estáticos, pois não se conseguiu fazer a matriz de colisão ajustável:
        //..................................//
        private static int chestHeight = 48, chestWidth = 80;
        //..................................//

        //para testes:
        //..................................//
        private Grid CollisionGrid = new Grid();
        private int column = 1, row = 1;
        private string testBox;
        //..................................//

        //Constantes para células que contem colisão, item ou inimigo:
        //.....................................................//
        private static int GROUND = 0, COLLISION = 1, ITEM = 2, ENEMY = 3;
        //.....................................................//

        //Lista de Imagens dos inimigos e itens que precisam se mover junto com o mapa para criar a ilusão de movimento de câmera:
        //..........................................................//
        private List<Image> MovableProps = new List<Image>();
        //..........................................................//

        public MainStage() {

            this.InitializeComponent();

            Window.Current.CoreWindow.KeyDown += KeySentinel;
            Window.Current.CoreWindow.KeyUp += KeyDropped;

            //Fazendo os ajustes para para tudo rodar "perfeitamente":
            //....................................................................................................//
            Adjuster.AdjustWindow(Floor);
            Adjuster.adjustForCamera(Mansion, Hero, ref ScreenWidth, ref ScreenHeight, ref Cell_Width, ref Cell_Height, ref topSide, ref botSide, ref leftSide, ref rightSide);
            //....................................................................................................//

            //CreateGrid();
            setBlocks();
            SetEnemies();
            SetItems();

            //Os menus são feitos no xalm, mas antes de o jogo começar, são retirados, exceto o quickmenu, que é retirado e posto de volta,
            //para ficar a frente de todas as outras imagens.
            setAllMenusReady();

            walkTimer.Interval = System.TimeSpan.FromMilliseconds(1);
            walkTimer.Tick += Walk;

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

        //Blocos de colisão: paredes, mesas, etc...
        private void setBlocks() {

            CollisionMatrix[93, 20] = COLLISION;
            CollisionMatrix[92, 20] = COLLISION;
            CollisionMatrix[91, 20] = COLLISION;
            CollisionMatrix[91, 21] = COLLISION;
            CollisionMatrix[91, 22] = COLLISION;
            CollisionMatrix[91, 23] = COLLISION;
            CollisionMatrix[91, 24] = COLLISION;
            CollisionMatrix[91, 25] = COLLISION;
            CollisionMatrix[91, 26] = COLLISION;
            CollisionMatrix[91, 27] = COLLISION;
            CollisionMatrix[91, 28] = COLLISION;
            CollisionMatrix[91, 29] = COLLISION;
            CollisionMatrix[91, 30] = COLLISION;
            CollisionMatrix[91, 31] = COLLISION;
            CollisionMatrix[91, 32] = COLLISION;
            CollisionMatrix[91, 33] = COLLISION;
            CollisionMatrix[91, 34] = COLLISION;
            CollisionMatrix[90, 34] = COLLISION;
            CollisionMatrix[89, 34] = COLLISION;
            CollisionMatrix[88, 34] = COLLISION;
            CollisionMatrix[87, 34] = COLLISION;
            CollisionMatrix[87, 33] = COLLISION;
            CollisionMatrix[87, 32] = COLLISION;
            CollisionMatrix[87, 31] = COLLISION;
            CollisionMatrix[87, 30] = COLLISION;
            CollisionMatrix[87, 29] = COLLISION;
            CollisionMatrix[87, 28] = COLLISION;
            CollisionMatrix[87, 27] = COLLISION;
            CollisionMatrix[87, 26] = COLLISION;
            CollisionMatrix[87, 25] = COLLISION;
            CollisionMatrix[87, 24] = COLLISION;
            CollisionMatrix[87, 23] = COLLISION;
            CollisionMatrix[87, 22] = COLLISION;
            CollisionMatrix[87, 21] = COLLISION;
            CollisionMatrix[87, 20] = COLLISION;
            CollisionMatrix[86, 20] = COLLISION;
            CollisionMatrix[85, 20] = COLLISION;
            CollisionMatrix[84, 20] = COLLISION;
            CollisionMatrix[83, 20] = COLLISION;
            CollisionMatrix[82, 20] = COLLISION;
            CollisionMatrix[81, 20] = COLLISION;
            CollisionMatrix[81, 19] = COLLISION;
            CollisionMatrix[81, 18] = COLLISION;
            CollisionMatrix[81, 17] = COLLISION;
            CollisionMatrix[82, 17] = COLLISION;
            CollisionMatrix[83, 17] = COLLISION;
            CollisionMatrix[83, 16] = COLLISION;
            CollisionMatrix[83, 15] = COLLISION;
            CollisionMatrix[83, 14] = COLLISION;
            CollisionMatrix[83, 13] = COLLISION;
            CollisionMatrix[82, 13] = COLLISION;
            CollisionMatrix[81, 13] = COLLISION;
            CollisionMatrix[81, 12] = COLLISION;
            CollisionMatrix[81, 11] = COLLISION;
            CollisionMatrix[81, 10] = COLLISION;
            CollisionMatrix[90, 17] = COLLISION;
            CollisionMatrix[90, 16] = COLLISION;
            CollisionMatrix[90, 15] = COLLISION;
            CollisionMatrix[90, 14] = COLLISION;
            CollisionMatrix[90, 13] = COLLISION;
            CollisionMatrix[90, 13] = COLLISION;
            CollisionMatrix[90, 12] = COLLISION;
            CollisionMatrix[90, 11] = COLLISION;
            CollisionMatrix[90, 10] = COLLISION;
            CollisionMatrix[90, 9] = COLLISION;
            CollisionMatrix[90, 8] = COLLISION;
            CollisionMatrix[90, 7] = COLLISION;
            CollisionMatrix[90, 6] = COLLISION;
            CollisionMatrix[90, 5] = COLLISION;
            CollisionMatrix[90, 4] = COLLISION;
            CollisionMatrix[89, 4] = COLLISION;
            CollisionMatrix[88, 4] = COLLISION;
            CollisionMatrix[87, 4] = COLLISION;
            CollisionMatrix[86, 5] = COLLISION;
            CollisionMatrix[87, 6] = COLLISION;
            CollisionMatrix[86, 7] = COLLISION;
            CollisionMatrix[87, 8] = COLLISION;
            CollisionMatrix[86, 8] = COLLISION;
            CollisionMatrix[87, 9] = COLLISION;
            CollisionMatrix[88, 9] = COLLISION;
            CollisionMatrix[89, 10] = COLLISION;
            CollisionMatrix[89, 11] = COLLISION;
            CollisionMatrix[89, 12] = COLLISION;
            CollisionMatrix[88, 12] = COLLISION;
            CollisionMatrix[87, 13] = COLLISION;
            CollisionMatrix[86, 13] = COLLISION;
            CollisionMatrix[86, 15] = COLLISION;
            CollisionMatrix[87, 15] = COLLISION;
            CollisionMatrix[87, 14] = COLLISION;
            CollisionMatrix[87, 16] = COLLISION;
            CollisionMatrix[86, 17] = COLLISION;
            CollisionMatrix[87, 17] = COLLISION;
            CollisionMatrix[88, 17] = COLLISION;
            CollisionMatrix[89, 17] = COLLISION;
            CollisionMatrix[93, 19] = COLLISION;
            CollisionMatrix[93, 18] = COLLISION;
            CollisionMatrix[93, 17] = COLLISION;
            CollisionMatrix[93, 16] = COLLISION;
            CollisionMatrix[93, 15] = COLLISION;
            CollisionMatrix[93, 14] = COLLISION;
            CollisionMatrix[93, 13] = COLLISION;
            CollisionMatrix[93, 12] = COLLISION;
            CollisionMatrix[93, 11] = COLLISION;
            CollisionMatrix[93, 10] = COLLISION;
            CollisionMatrix[93, 9] = COLLISION;
            CollisionMatrix[93, 8] = COLLISION;
            CollisionMatrix[93, 7] = COLLISION;
            CollisionMatrix[93, 6] = COLLISION;
            CollisionMatrix[93, 5] = COLLISION;
            CollisionMatrix[93, 4] = COLLISION;
            CollisionMatrix[93, 3] = COLLISION;
            CollisionMatrix[93, 2] = COLLISION;
            CollisionMatrix[93, 1] = COLLISION;
            CollisionMatrix[92, 1] = COLLISION;
            CollisionMatrix[91, 1] = COLLISION;
            CollisionMatrix[90, 1] = COLLISION;
            CollisionMatrix[89, 1] = COLLISION;
            CollisionMatrix[88, 1] = COLLISION;
            CollisionMatrix[87, 1] = COLLISION;
            CollisionMatrix[86, 1] = COLLISION;
            CollisionMatrix[85, 1] = COLLISION;
            CollisionMatrix[84, 1] = COLLISION;
            CollisionMatrix[83, 1] = COLLISION;
            CollisionMatrix[83, 2] = COLLISION;
            CollisionMatrix[83, 3] = COLLISION;
            CollisionMatrix[83, 4] = COLLISION;
            CollisionMatrix[82, 4] = COLLISION;
            CollisionMatrix[81, 4] = COLLISION;
            CollisionMatrix[81, 5] = COLLISION;
            CollisionMatrix[81, 6] = COLLISION;
            CollisionMatrix[81, 7] = COLLISION;
            CollisionMatrix[80, 7] = COLLISION;
            CollisionMatrix[79, 7] = COLLISION;
            CollisionMatrix[78, 7] = COLLISION;
            CollisionMatrix[81, 10] = COLLISION;
            CollisionMatrix[80, 10] = COLLISION;
            CollisionMatrix[79, 10] = COLLISION;
            CollisionMatrix[78, 10] = COLLISION;
            CollisionMatrix[77, 10] = COLLISION;
            CollisionMatrix[76, 10] = COLLISION;
            CollisionMatrix[77, 7] = COLLISION;
            CollisionMatrix[76, 7] = COLLISION;
            CollisionMatrix[75, 7] = COLLISION;
            CollisionMatrix[74, 7] = COLLISION;
            CollisionMatrix[73, 7] = COLLISION;
            CollisionMatrix[75, 10] = COLLISION;
            CollisionMatrix[74, 10] = COLLISION;
            CollisionMatrix[72, 10] = COLLISION;
            CollisionMatrix[73, 10] = COLLISION;
            CollisionMatrix[71, 10] = COLLISION;
            CollisionMatrix[70, 10] = COLLISION;
            CollisionMatrix[69, 10] = COLLISION;
            CollisionMatrix[68, 10] = COLLISION;
            CollisionMatrix[67, 10] = COLLISION;
            CollisionMatrix[66, 10] = COLLISION;
            CollisionMatrix[65, 10] = COLLISION;
            CollisionMatrix[64, 10] = COLLISION;
            CollisionMatrix[63, 10] = COLLISION;
            CollisionMatrix[62, 10] = COLLISION;
            CollisionMatrix[72, 7] = COLLISION;
            CollisionMatrix[71, 7] = COLLISION;
            CollisionMatrix[70, 7] = COLLISION;
            CollisionMatrix[69, 7] = COLLISION;
            CollisionMatrix[68, 7] = COLLISION;
            CollisionMatrix[67, 7] = COLLISION;
            CollisionMatrix[66, 7] = COLLISION;
            CollisionMatrix[65, 7] = COLLISION;
            CollisionMatrix[64, 7] = COLLISION;
            CollisionMatrix[63, 7] = COLLISION;
            CollisionMatrix[62, 7] = COLLISION;
            CollisionMatrix[61, 10] = COLLISION;
            CollisionMatrix[61, 7] = COLLISION;
            CollisionMatrix[61, 6] = COLLISION;
            CollisionMatrix[61, 5] = COLLISION;
            CollisionMatrix[61, 4] = COLLISION;
            CollisionMatrix[61, 3] = COLLISION;
            CollisionMatrix[61, 2] = COLLISION;
            CollisionMatrix[61, 1] = COLLISION;
            CollisionMatrix[60, 1] = COLLISION;
            CollisionMatrix[59, 1] = COLLISION;
            CollisionMatrix[61, 11] = COLLISION;
            CollisionMatrix[61, 12] = COLLISION;
            CollisionMatrix[61, 15] = COLLISION;
            CollisionMatrix[61, 16] = COLLISION;
            CollisionMatrix[61, 17] = COLLISION;
            CollisionMatrix[61, 18] = COLLISION;
            CollisionMatrix[61, 19] = COLLISION;
            CollisionMatrix[61, 20] = COLLISION;
            CollisionMatrix[62, 15] = COLLISION;
            CollisionMatrix[63, 15] = COLLISION;
            CollisionMatrix[64, 15] = COLLISION;
            CollisionMatrix[65, 15] = COLLISION;
            CollisionMatrix[66, 15] = COLLISION;
            CollisionMatrix[67, 15] = COLLISION;
            CollisionMatrix[68, 15] = COLLISION;
            CollisionMatrix[69, 15] = COLLISION;
            CollisionMatrix[69, 16] = COLLISION;
            CollisionMatrix[69, 17] = COLLISION;
            CollisionMatrix[69, 18] = COLLISION;
            CollisionMatrix[70, 19] = COLLISION;
            CollisionMatrix[70, 18] = COLLISION;
            CollisionMatrix[69, 20] = COLLISION;
            CollisionMatrix[69, 21] = COLLISION;
            CollisionMatrix[69, 22] = COLLISION;
            CollisionMatrix[69, 23] = COLLISION;
            CollisionMatrix[69, 24] = COLLISION;
            CollisionMatrix[69, 25] = COLLISION;
            CollisionMatrix[69, 26] = COLLISION;
            CollisionMatrix[70, 26] = COLLISION;
            CollisionMatrix[70, 27] = COLLISION;
            CollisionMatrix[70, 28] = COLLISION;
            CollisionMatrix[69, 28] = COLLISION;
            CollisionMatrix[69, 29] = COLLISION;
            CollisionMatrix[69, 30] = COLLISION;
            CollisionMatrix[69, 31] = COLLISION;
            CollisionMatrix[69, 32] = COLLISION;
            CollisionMatrix[70, 32] = COLLISION;
            CollisionMatrix[60, 20] = COLLISION;
            CollisionMatrix[59, 20] = COLLISION;
            CollisionMatrix[58, 20] = COLLISION;
            CollisionMatrix[57, 20] = COLLISION;
            CollisionMatrix[59, 2] = COLLISION;
            CollisionMatrix[59, 3] = COLLISION;
            CollisionMatrix[59, 4] = COLLISION;
            CollisionMatrix[59, 5] = COLLISION;
            CollisionMatrix[59, 6] = COLLISION;
            CollisionMatrix[58, 6] = COLLISION;
            CollisionMatrix[57, 6] = COLLISION;
            CollisionMatrix[56, 6] = COLLISION;
            CollisionMatrix[55, 6] = COLLISION;
            CollisionMatrix[55, 5] = COLLISION;
            CollisionMatrix[55, 4] = COLLISION;
            CollisionMatrix[55, 3] = COLLISION;
            CollisionMatrix[54, 4] = COLLISION;
            CollisionMatrix[53, 4] = COLLISION;
            CollisionMatrix[52, 4] = COLLISION;
            CollisionMatrix[51, 4] = COLLISION;
            CollisionMatrix[50, 5] = COLLISION;
            CollisionMatrix[50, 7] = COLLISION;
            CollisionMatrix[50, 8] = COLLISION;
            CollisionMatrix[50, 9] = COLLISION;
            CollisionMatrix[51, 9] = COLLISION;
            CollisionMatrix[52, 9] = COLLISION;
            CollisionMatrix[53, 9] = COLLISION;
            CollisionMatrix[52, 10] = COLLISION;
            CollisionMatrix[52, 11] = COLLISION;
            CollisionMatrix[52, 12] = COLLISION;
            CollisionMatrix[52, 13] = COLLISION;
            CollisionMatrix[51, 13] = COLLISION;
            CollisionMatrix[50, 13] = COLLISION;
            CollisionMatrix[51, 14] = COLLISION;
            CollisionMatrix[51, 15] = COLLISION;
            CollisionMatrix[51, 12] = COLLISION;
            CollisionMatrix[50, 15] = COLLISION;
            CollisionMatrix[51, 16] = COLLISION;
            CollisionMatrix[50, 17] = COLLISION;
            CollisionMatrix[51, 17] = COLLISION;
            CollisionMatrix[52, 17] = COLLISION;
            CollisionMatrix[53, 17] = COLLISION;
            CollisionMatrix[54, 17] = COLLISION;
            CollisionMatrix[54, 16] = COLLISION;
            CollisionMatrix[54, 15] = COLLISION;
            CollisionMatrix[54, 14] = COLLISION;
            CollisionMatrix[54, 12] = COLLISION;
            CollisionMatrix[54, 13] = COLLISION;
            CollisionMatrix[54, 11] = COLLISION;
            CollisionMatrix[54, 10] = COLLISION;
            CollisionMatrix[54, 9] = COLLISION;
            CollisionMatrix[54, 8] = COLLISION;
            CollisionMatrix[54, 7] = COLLISION;
            CollisionMatrix[54, 6] = COLLISION;
            CollisionMatrix[62, 12] = COLLISION;
            CollisionMatrix[63, 12] = COLLISION;
            CollisionMatrix[64, 12] = COLLISION;
            CollisionMatrix[65, 12] = COLLISION;
            CollisionMatrix[66, 12] = COLLISION;
            CollisionMatrix[67, 12] = COLLISION;
            CollisionMatrix[68, 12] = COLLISION;
            CollisionMatrix[69, 12] = COLLISION;
            CollisionMatrix[70, 12] = COLLISION;
            CollisionMatrix[71, 12] = COLLISION;
            CollisionMatrix[72, 12] = COLLISION;
            CollisionMatrix[74, 12] = COLLISION;
            CollisionMatrix[73, 12] = COLLISION;
            CollisionMatrix[74, 13] = COLLISION;
            CollisionMatrix[74, 14] = COLLISION;
            CollisionMatrix[74, 15] = COLLISION;
            CollisionMatrix[74, 16] = COLLISION;
            CollisionMatrix[74, 17] = COLLISION;
            CollisionMatrix[74, 18] = COLLISION;
            CollisionMatrix[74, 19] = COLLISION;
            CollisionMatrix[74, 20] = COLLISION;
            CollisionMatrix[74, 21] = COLLISION;
            CollisionMatrix[74, 22] = COLLISION;
            CollisionMatrix[74, 23] = COLLISION;
            CollisionMatrix[74, 24] = COLLISION;
            CollisionMatrix[74, 25] = COLLISION;
            CollisionMatrix[74, 26] = COLLISION;
            CollisionMatrix[74, 27] = COLLISION;
            CollisionMatrix[74, 28] = COLLISION;
            CollisionMatrix[75, 28] = COLLISION;
            CollisionMatrix[76, 28] = COLLISION;
            CollisionMatrix[77, 28] = COLLISION;
            CollisionMatrix[78, 28] = COLLISION;
            CollisionMatrix[71, 32] = COLLISION;
            CollisionMatrix[72, 32] = COLLISION;
            CollisionMatrix[73, 32] = COLLISION;
            CollisionMatrix[74, 32] = COLLISION;
            CollisionMatrix[75, 32] = COLLISION;
            CollisionMatrix[76, 32] = COLLISION;
            CollisionMatrix[77, 32] = COLLISION;
            CollisionMatrix[78, 32] = COLLISION;
            CollisionMatrix[79, 32] = COLLISION;
            CollisionMatrix[80, 32] = COLLISION;
            CollisionMatrix[80, 30] = COLLISION;
            CollisionMatrix[80, 31] = COLLISION;
            CollisionMatrix[80, 29] = COLLISION;
            CollisionMatrix[80, 28] = COLLISION;
            CollisionMatrix[79, 28] = COLLISION;
            CollisionMatrix[56, 20] = COLLISION;
            CollisionMatrix[55, 20] = COLLISION;
            CollisionMatrix[54, 20] = COLLISION;
            CollisionMatrix[53, 20] = COLLISION;
            CollisionMatrix[52, 20] = COLLISION;
            CollisionMatrix[51, 20] = COLLISION;
            CollisionMatrix[50, 20] = COLLISION;
            CollisionMatrix[49, 20] = COLLISION;
            CollisionMatrix[48, 20] = COLLISION;
            CollisionMatrix[47, 20] = COLLISION;
            CollisionMatrix[47, 21] = COLLISION;
            CollisionMatrix[47, 22] = COLLISION;
            CollisionMatrix[47, 23] = COLLISION;
            CollisionMatrix[48, 23] = COLLISION;
            CollisionMatrix[49, 23] = COLLISION;
            CollisionMatrix[50, 23] = COLLISION;
            CollisionMatrix[47, 26] = COLLISION;
            CollisionMatrix[48, 26] = COLLISION;
            CollisionMatrix[49, 26] = COLLISION;
            CollisionMatrix[50, 26] = COLLISION;
            CollisionMatrix[51, 26] = COLLISION;
            CollisionMatrix[52, 26] = COLLISION;
            CollisionMatrix[53, 26] = COLLISION;
            CollisionMatrix[54, 26] = COLLISION;
            CollisionMatrix[44, 17] = COLLISION;
            CollisionMatrix[44, 18] = COLLISION;
            CollisionMatrix[44, 19] = COLLISION;
            CollisionMatrix[44, 20] = COLLISION;
            CollisionMatrix[44, 21] = COLLISION;
            CollisionMatrix[44, 22] = COLLISION;
            CollisionMatrix[44, 23] = COLLISION;
            CollisionMatrix[44, 24] = COLLISION;
            CollisionMatrix[44, 25] = COLLISION;
            CollisionMatrix[45, 17] = COLLISION;
            CollisionMatrix[46, 17] = COLLISION;
            CollisionMatrix[47, 17] = COLLISION;
            CollisionMatrix[47, 16] = COLLISION;
            CollisionMatrix[47, 15] = COLLISION;
            CollisionMatrix[47, 14] = COLLISION;
            CollisionMatrix[46, 13] = COLLISION;
            CollisionMatrix[45, 13] = COLLISION;
            CollisionMatrix[44, 13] = COLLISION;
            CollisionMatrix[44, 12] = COLLISION;
            CollisionMatrix[44, 11] = COLLISION;
            CollisionMatrix[44, 10] = COLLISION;
            CollisionMatrix[43, 10] = COLLISION;
            CollisionMatrix[44, 6] = COLLISION;
            CollisionMatrix[44, 7] = COLLISION;
            CollisionMatrix[43, 7] = COLLISION;
            CollisionMatrix[42, 7] = COLLISION;
            CollisionMatrix[44, 5] = COLLISION;
            CollisionMatrix[44, 4] = COLLISION;
            CollisionMatrix[44, 3] = COLLISION;
            CollisionMatrix[44, 2] = COLLISION;
            CollisionMatrix[44, 1] = COLLISION;
            CollisionMatrix[45, 1] = COLLISION;
            CollisionMatrix[46, 1] = COLLISION;
            CollisionMatrix[47, 1] = COLLISION;
            CollisionMatrix[48, 1] = COLLISION;
            CollisionMatrix[49, 1] = COLLISION;
            CollisionMatrix[50, 1] = COLLISION;
            CollisionMatrix[51, 1] = COLLISION;
            CollisionMatrix[52, 1] = COLLISION;
            CollisionMatrix[53, 1] = COLLISION;
            CollisionMatrix[54, 1] = COLLISION;
            CollisionMatrix[55, 1] = COLLISION;
            CollisionMatrix[55, 2] = COLLISION;
            CollisionMatrix[73, 13] = COLLISION;
            CollisionMatrix[73, 14] = COLLISION;
            CollisionMatrix[73, 15] = COLLISION;
            CollisionMatrix[73, 16] = COLLISION;
            CollisionMatrix[73, 17] = COLLISION;
            CollisionMatrix[73, 18] = COLLISION;
            CollisionMatrix[73, 19] = COLLISION;
            CollisionMatrix[73, 20] = COLLISION;
            CollisionMatrix[73, 21] = COLLISION;
            CollisionMatrix[73, 22] = COLLISION;
            CollisionMatrix[73, 23] = COLLISION;
            CollisionMatrix[73, 24] = COLLISION;
            CollisionMatrix[73, 25] = COLLISION;
            CollisionMatrix[73, 26] = COLLISION;
            CollisionMatrix[73, 27] = COLLISION;
            CollisionMatrix[73, 28] = COLLISION;

        }

        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// 

        //BACK END
        private void SetItems() {

            CreateItem(79, 30);
        }

        private void CreateItem(int row, int column) {

            CollisionMatrix[row, column] = COLLISION;
            CollisionMatrix[row - 1, column] = ITEM;

            Image chest = new Image();

            chest.Width = chestWidth;
            chest.Height = chestHeight;

            chest.Source = new BitmapImage(new Uri("ms-appx:///GameAssets/Maps/chest_idle.png"));

            Floor.Children.Add(chest);

            Canvas.SetLeft(chest, column * Cell_Width + Canvas.GetLeft(Mansion) + Cell_Width / 2 - chest.Width / 2);
            Canvas.SetTop(chest, row * Cell_Height + Canvas.GetTop(Mansion) + Cell_Height / 2 - chest.Height / 2);

            MovableProps.Add(chest);
        }

        private Image GetItem(int row, int column) {

            Image foe = new Image();

            double x = column * Cell_Width + Canvas.GetLeft(Mansion) + Cell_Width / 2 - chestWidth / 2;
            double y = row * Cell_Height + Canvas.GetTop(Mansion) + Cell_Height / 2 - chestHeight / 2;

            foreach (Image item in MovableProps) {

                if (Canvas.GetTop(item) == y || Canvas.GetLeft(item) == x) {

                    return item;
                }
            }

            return null;
        }

        //private Image getEnemy(int row, int column) {

        //    Image foe = new Image();

        //    double x = column * Cell_Width + Canvas.GetLeft(Mansion) + Cell_Width / 2 - foe.Width / 2;
        //    double y = row * Cell_Height + Canvas.GetTop(Mansion) + Cell_Height / 2 - foe.Height / 2;

        //    foreach (Image item in MovableProps) {

        //        if (Canvas.GetTop(item) == y || Canvas.GetLeft(item) == x) {

        //            return item;
        //        }
        //    }

        //    return null;

        //}

        private void CreateEnemy(int row, int column) {

            CollisionMatrix[row, column] = ENEMY;

            controller.setMob(row, column);

            foe = controller.FindMob(row, column);

            Image enemy = new Image();

            enemy.Height = Hero.Height * 4 / 3;
            enemy.Width = Hero.Width * 4 / 3;

            enemy.Source = foe.Sprite;

            Floor.Children.Add(enemy);

            Canvas.SetLeft(enemy, column * Cell_Width + Canvas.GetLeft(Mansion) + Cell_Width / 2 - enemy.Width / 2);
            Canvas.SetTop(enemy, row * Cell_Height + Canvas.GetTop(Mansion) + Cell_Height / 2 - enemy.Height / 2);

            MovableProps.Add(enemy);
        }

        private void SetEnemies() {

            CreateEnemy(72, 24);
        }

        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        ///

        //Aqui é feito o retorno do herói/Mapa a sua posição anterior à atual, em que ele colide com o cenário:
        //Caso seja o mapa se movendo, ele é quem retorna à coordenada de canvas anterior.
        //..........................................................//
        private void BumpWithScenario() {

            if (isCameraHorizontal) {
                Canvas.SetLeft(Mansion, Canvas.GetLeft(Mansion) + x);

                x_RetrievePropsPosition();

                //Apenas para testes//
                Canvas.SetLeft(CollisionGrid, Canvas.GetLeft(CollisionGrid) + x);
            }
            else {

                Canvas.SetLeft(Hero, Canvas.GetLeft(Hero) - x);
            }


            if (isCameraVertical) {
                Canvas.SetTop(Mansion, Canvas.GetTop(Mansion) + y);

                y_RetrievePropsPosition();

                //Apenas para testes//
                Canvas.SetTop(CollisionGrid, Canvas.GetTop(CollisionGrid) + y);
            }
            else {

                Canvas.SetTop(Hero, Canvas.GetTop(Hero) - y);
            }
        }
        //..........................................................//

        private void CheckCollision() {

            //Aqui há o cálculo para determinar em quais células da matriz os vértices do herói estão:
            //....................................................................................................\\
            upper_row = (int)(((Canvas.GetTop(Hero) - Canvas.GetTop(Mansion)) / Cell_Height) + topSide);
            bottom_row = (int)((upper_row) + botSide);

            left_column = (int)((((Canvas.GetLeft(Hero)) - Canvas.GetLeft(Mansion)) / Cell_Width) + leftSide);
            right_column = left_column + (int)(rightSide);
            //....................................................................................................\\

            isInteractive = false;

            //Aqui procuro saber se as 4 células da matriz em que se situam os vértices do herói estão em células de colisão (1),
            //células de inimigos (3) ou células de itens (2) ou em nenhuma das três (0).
            //..............................................................................................................................................................................................................\\
            if (CollisionMatrix[upper_row, left_column] == COLLISION || CollisionMatrix[upper_row, right_column] == COLLISION || CollisionMatrix[bottom_row, left_column] == COLLISION || CollisionMatrix[bottom_row, right_column] == COLLISION) {

                BumpWithScenario();
            }
            else if (CollisionMatrix[upper_row, left_column] == ITEM || CollisionMatrix[upper_row, right_column] == ITEM || CollisionMatrix[bottom_row, left_column] == ITEM || CollisionMatrix[bottom_row, right_column] == ITEM) {

                Canvas.SetTop(infoBox, Canvas.GetTop(Hero) - Hero.Height / 2);
                Canvas.SetLeft(infoBox, Canvas.GetLeft(Hero) + Hero.Width / 2 - infoBox.Width / 2);

                isInteractive = true;
                infoBox.Opacity = 1;
            }
            else if (CollisionMatrix[upper_row, left_column] == ENEMY || CollisionMatrix[upper_row, right_column] == ENEMY || CollisionMatrix[bottom_row, left_column] == ENEMY || CollisionMatrix[bottom_row, right_column] == ENEMY) {

                int x = 0, y = 0;

                if (CollisionMatrix[upper_row, left_column] == ENEMY) {

                    x = left_column;
                    y = upper_row;
                }
                else if (CollisionMatrix[upper_row, right_column] == ENEMY) {

                    x = right_column;
                    y = upper_row;
                }
                else if (CollisionMatrix[bottom_row, left_column] == ENEMY) {

                    x = left_column;
                    y = bottom_row;
                }
                else if (CollisionMatrix[bottom_row, right_column] == ENEMY) {

                    x = right_column;
                    y = bottom_row;
                }

                //Image foe = getEnemy(y, x);
                CollisionMatrix[y, x] = GROUND;

                this.Frame.Navigate(typeof(BattleStage));
            }
            else {

                infoBox.Opacity = 0;
            }
        }

        //Ao apertar 'E' para interagir, é necessário saber se uma das célula em que se encontra os vértices do herói é uma célula de itens.
        //..............................................................................................//
        private void Interact() {

            Image item = new Image();
            int x = 0, y = 0;

            if (!isInteractive) return;

            if (CollisionMatrix[upper_row + 1, left_column] == COLLISION) {

                x = left_column;
                y = upper_row + 1;
            }
            else if (CollisionMatrix[upper_row + 1, right_column] == COLLISION) {

                x = right_column;
                y = upper_row + 1;
            }
            else if (CollisionMatrix[bottom_row + 1, left_column] == COLLISION) {

                x = left_column;
                y = bottom_row + 1;
            }
            else if (CollisionMatrix[bottom_row + 1, right_column] == COLLISION) {

                x = right_column;
                y = bottom_row + 1;
            }

            item = GetItem(y, x);
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

                CollisionGrid.RowDefinitions.Add(rd);

                for (int j = 0; j < divider; j++) {

                    ColumnDefinition cd = new ColumnDefinition();
                    cd.Width = new GridLength(x, GridUnitType.Pixel);

                    CollisionGrid.ColumnDefinitions.Add(cd);
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

                    CollisionGrid.Children.Add(border);


                    border.Tapped += GettingCell;
                }
            }

            Floor.Children.Add(CollisionGrid);
            Canvas.SetTop(CollisionGrid, Canvas.GetTop(Mansion));
            Canvas.SetLeft(CollisionGrid, Canvas.GetLeft(Mansion));
        }

        private void ExitInventory(object sender, RoutedEventArgs e) {

            Floor.Children.Remove(Inventory);
            isInventoryOpen = false;
        }

        private void GettingCell(object sender, TappedRoutedEventArgs e) {

            var cell = (Border)sender;
            cell.Background = new SolidColorBrush(Colors.Red);

            column = Grid.GetColumn(cell);
            row = Grid.GetRow(cell);

            Debug.WriteLine("CollisionMatrix[" + row + ", " + column + "] = COLLISION;");

        }

        private void Walk(object sender, object e) {

            //If statements para decidir qual imagem deve se mover: a do herói ou a do mapa,gerando a ilusão de uma câmera seguindo o personagem.
            if (Canvas.GetLeft(Hero) + (Hero.Width / 2) == ScreenWidth / 2) {

                if ((x < 0 && Canvas.GetLeft(Mansion) < 0) || (x > 0 && Canvas.GetLeft(Mansion) > ScreenWidth - Mansion.Width)) {

                    isCameraHorizontal = true;
                    Canvas.SetLeft(Mansion, Canvas.GetLeft(Mansion) - x);

                    x_movePropsAlongWithCamera();

                    //Apenas Teste//
                    Canvas.SetLeft(CollisionGrid, Canvas.GetLeft(CollisionGrid) - x);
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
                    Canvas.SetTop(CollisionGrid, Canvas.GetTop(CollisionGrid) - y);
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

             chosen = (Player)e.Parameter;

            idletoLeft = chosen.Sprite_idle_left;
            idletoRight = chosen.Sprite_idle_right;

            runtoLeft = chosen.Sprite_walking_left;
            runtoRight = chosen.Sprite_walking_right;

            direction = runtoRight;
            Hero.Source = idletoRight;
        }

        //A sentinela que vigia as teclas atentamente:
        private void KeySentinel(CoreWindow sender, KeyEventArgs e) {

            isMovementKey = true;

            int speed = 5;

            switch (e.VirtualKey) {

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

                    //Teste com a grid:
                    CollisionGrid.Opacity = (CollisionGrid.Opacity == 1) ? 0 : 1;
                    isMovementKey = false;
                    break;
                case Windows.System.VirtualKey.E:

                    isMovementKey = false;
                    Interact();
                    break;
                default:
                    isMovementKey = false;
                    break;
            }

            if (Hero.Source != direction && isMovementKey) Hero.Source = direction;
        }

        private void KeyDropped(CoreWindow sender, KeyEventArgs e) {

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

            Floor.Children.Remove(OptionsMenu);
            isOptionsMenuOpen = false;
        }
    }
}
