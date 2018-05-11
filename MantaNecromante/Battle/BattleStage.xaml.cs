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
        private string nomeSkill;
        private int DmgPlayer, DmgMob, MpPlayer;

        public BattleStage() {

            this.InitializeComponent();
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Adjuster.AdjustWindow(Floor);
            SetAllMenusReady();
            Floor.Children.Remove(Turn1);
            Floor.Children.Remove(Turn2);
            song.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///GameAssets/Songs/Battle.mp3"));
            song.Play();
            RemoverMenus();


        }

        private void RemoverMenus() {

            Floor.Children.Remove(ResultadosPane);
        }

        public void NameSkills() {

            Skill0.Content = battleController.Jogador.Skills.ElementAt(0).Skill_name;
            Skill1.Content = battleController.Jogador.Skills.ElementAt(1).Skill_name;
            Skill2.Content = battleController.Jogador.Skills.ElementAt(2).Skill_name;
        }

        /// <summary>
        /// Método trata o caso do player não possuir mana para usar a skill escolhida
        /// </summary>
        public void NoManaAvalible() {
            Infobox.Text = "Você não tem mana o suficiente para usar " + nomeSkill;
        }

        private void BlockButtons() {
            
            Skill0.Click -= BotaoSkill;
            Skill1.Click -= BotaoSkill;
            Skill2.Click -= BotaoSkill;
            
            Ataque.Click -= BotaoAtacar;
                    
        }

        /// <summary>
        /// Método que trata a morte do inimigo
        /// </summary>
        public void EnemyIsDead() {
            Progress_Bar_Update();
            battleController.Jogador.Gain_xp(battleController.Inimigo.Give_xp());
            BlockButtons();
            RemoverMenus();

            Floor.Children.Add(ResultadosPane);

            ResultadosText.Text = "Você Derrotou " + battleController.Inimigo.Nome +
            "\n Você ganhou " + battleController.Inimigo.Given_xp + " de xp";
            
            ResultadosBotao.Content = "Voltar ao Castelo";
            
            this.Frame.GoBack();

        }

        
        /// <summary>
        /// Método que trata a morte do jogador
        /// </summary>
        public void PlayerIsDead() {
            //Mostrar uma mensagem que o player morreu e encerrar a batalha
            Progress_Bar_Update();

            RemoverMenus();
            Floor.Children.Add(ResultadosPane);

            ResultadosText.Text = "Você morreu!";
            ResultadosBotao.Content = "Voltar ao Menu Inicial";
            
            BlockButtons();
            
        }

        
        /// <summary>
        /// Trata o Evento que ocorre no battleController quando o turno muda para o Player
        /// </summary>
        public void TurnChangeToPlayer() {

            removeAll();

            Floor.Children.Add(Turn1);

            //Mostrar que o turno mudou para o jogador
            Progress_Bar_Update();

            
        }

        void removeAll()
        {

            Floor.Children.Remove(Turn1);
            Floor.Children.Remove(Turn2);
        }

        /// <summary>
        /// Trata o Evento que ocorre no battleController quando o turno muda para o Inimigo
        /// </summary>
        public void TurnChangeToEnemy() {
            //Mostrar que o turno mudou para o inimigo


            removeAll();

            Floor.Children.Add(Turn2);

            Progress_Bar_Update();
            battleController.EnemyChoice();
            DmgPlayer -= battleController.Inimigo.Hp_atual;
            MpPlayer = MpPlayer - battleController.Jogador.Mp_atual;
            if (nomeSkill == Ataque.Content.ToString()) {
                Infobox.Text = "Player" + " causou " + DmgPlayer + " de dano";
            }
            else {
                Infobox.Text = "Player usou "+nomeSkill + "("+MpPlayer+" MP)"+" e causou " + DmgPlayer + " de dano";
            }

        }

        public void Progress_Bar_Update() {


            Progress_HP_chosen.Value = battleController.Jogador.Hp_atual;
            Progress_MP_chosen.Value = battleController.Jogador.Mp_atual;
            

            Progress_HP_Mob.Value = battleController.Inimigo.Hp_atual;
            Progress_MP_Mob.Value = battleController.Inimigo.Mp_atual;

            
        }

        public void Progress_Bar() {
            // Progess bar dinamica para o Hp do jogador

            Progress_HP_chosen.Minimum = 0;
            Progress_HP_chosen.Maximum = battleController.Jogador.Hp_total;
            Progress_HP_chosen.Value  = battleController.Jogador.Hp_atual;
            
            // Progess bar dinamica para o Mp do jogador
            Progress_MP_chosen.Minimum = 0;
            Progress_MP_chosen.Maximum = battleController.Jogador.Mp_total;
            Progress_MP_chosen.Value = battleController.Jogador.Mp_atual;

            // Progess bar dinamica para o Mp do Mob
            Progress_HP_Mob.Minimum = 0;
            Progress_HP_Mob.Maximum = battleController.Inimigo.Hp_total;
            Progress_HP_Mob.Value = battleController.Inimigo.Hp_atual;

            // Progess bar dinamica para o Mp do Mob
            Progress_MP_Mob.Minimum = 0;
            Progress_MP_Mob.Maximum = battleController.Inimigo.Mp_total;
            Progress_MP_Mob.Value = battleController.Inimigo.Mp_atual;
            
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
            battleController.PlayerTurn += TurnChangeToPlayer;
            battleController.EnemyTurn += TurnChangeToEnemy;
            battleController.PlayerDeath += PlayerIsDead;
            battleController.EnemyDeath += EnemyIsDead;
            battleController.PlayerHasNoMana += NoManaAvalible;
            Progress_Bar();
            NameSkills();
            if (battleController.Turno_player == true) {
                RollBox.Text = "O jogador Começa";
            }
            else {
                RollBox.Text = battleController.Inimigo.Nome + " Começa";
            }
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

        private void BotaoAtacar(object sender, RoutedEventArgs e) {
            nomeSkill = ((Button)sender).Content.ToString();
            DmgPlayer = battleController.Inimigo.Hp_atual;
            battleController.Atacar();   
        }

        private void BotaoSkill(object sender, RoutedEventArgs e) {

            Button b = sender as Button;

            int Skill = Convert.ToInt16(b.Name.Replace("Skill", ""));

            nomeSkill = b.Content.ToString();

            MpPlayer = battleController.Jogador.Mp_atual;
            DmgPlayer = battleController.Inimigo.Hp_atual;

            battleController.CastSkill(battleController.Jogador.Skills[Skill]);

        }
       
    }
}
