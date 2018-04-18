using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Extension;
using Windows.UI.Core;
using NecromanteLL;
using Windows.Media.Playback;
using Windows.Media.Core;
using Windows.ApplicationModel.Core;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace MantaNecromante.MainBattle {
    
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class BattleStage : Page {

        private bool isOptionsMenuOpen;
        private BattleController battleController;
        private MediaPlayer song = new MediaPlayer();

        public BattleStage() {

            this.InitializeComponent();
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            battleController.PlayerTurn += TurnChangeToPlayer;
            battleController.EnemyTurn += TurnChangeToEnemy;
            battleController.PlayerDeath += PlayerIsDead;
            battleController.EnemyDeath += EnemyIsDead;
            battleController.PlayerHasNoMana += NoManaAvalible;
            Adjuster.AdjustWindow(Floor);
            SetAllMenusReady();
            song.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///GameAssets/Songs/Battle.mp3"));
            song.Play();

        }
        


        /// <summary>
        /// Método trata o caso do player não possuir mana para usar a skill escolhida
        /// </summary>
        public void NoManaAvalible() {

        }


        /// <summary>
        /// Método que trata a morte do inimigo
        /// </summary>
        public void EnemyIsDead() {

        }


        /// <summary>
        /// Método que trata a morte do jogador
        /// </summary>
        public void PlayerIsDead() {

        }

        /// <summary>
        /// Trata o Evento que ocorre no battleController quando o turno muda para o Player
        /// </summary>
        public void TurnChangeToPlayer() {

        }

        /// <summary>
        /// Trata o Evento que ocorre no battleController quando o turno muda para o Inimigo
        /// </summary>
        public void TurnChangeToEnemy() {

        }

        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args) {
            
            if (args.VirtualKey == Windows.System.VirtualKey.Escape) {

                if (!isOptionsMenuOpen) {

                    Floor.Children.Add(OptionsMenu);
                }
                else {

                    Floor.Children.Remove(OptionsMenu);
                }

                isOptionsMenuOpen = !isOptionsMenuOpen;
            }
        }

        private void SetAllMenusReady() {

            Floor.Children.Remove(OptionsMenu);
            isOptionsMenuOpen = false;
        }

        private void BattleStage_KeyDown(object sender, KeyRoutedEventArgs e) {

            if (e.Key == Windows.System.VirtualKey.Escape) {

                if (!isOptionsMenuOpen) {

                    Floor.Children.Add(OptionsMenu);
                }
                else {

                    Floor.Children.Remove(OptionsMenu);
                }

                isOptionsMenuOpen = !isOptionsMenuOpen;
            } 
        }

        private void Exit(object sender, RoutedEventArgs e) {
            Window.Current.CoreWindow.KeyDown -= CoreWindow_KeyDown;
            CoreApplication.Exit();
        }

        private void Continue(object sender, RoutedEventArgs e) {

            Floor.Children.Remove(OptionsMenu);
            isOptionsMenuOpen = false;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            battleController = (BattleController) e.Parameter;
            this.KeyDown += BattleStage_KeyDown;
            Hero.Source = battleController.Jogador.Sprite_idle_right;
            Foe.Source = battleController.Inimigo.Sprite.Source;
        }

        private void Menu_Click(object sender, RoutedEventArgs e) {

                if (!isOptionsMenuOpen) {

                    Floor.Children.Add(OptionsMenu);
                }
                else {

                    Floor.Children.Remove(OptionsMenu);
                }

                isOptionsMenuOpen ^= false;
            
        }
    }
}
