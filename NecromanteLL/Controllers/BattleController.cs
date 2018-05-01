using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecromanteLL {
    public class BattleController {

        private int player_initalHP, player_initialMP, player_initialDmg, player_initialDef;
        private int turno_atual = 1;
        private bool turno_player = true;
        private bool extra_turn = false;
        private Player jogador;
        private Mob inimigo;
        private int dice_num;
        private int menorCusto;

        public int Turno_atual { get => Turno_atual1; set => Turno_atual1 = value; }
        public int Player_initalHP { get => player_initalHP; set => player_initalHP = value; }
        public int Player_initialMP { get => player_initialMP; set => player_initialMP = value; }
        public int Player_initialDmg { get => player_initialDmg; set => player_initialDmg = value; }
        public int Player_initialDef { get => player_initialDef; set => player_initialDef = value; }
        public int Turno_atual1 { get => turno_atual; set => turno_atual = value; }
        public bool Turno_player { get => turno_player; set => turno_player = value; }
        public Player Jogador { get => jogador; set => jogador = value; }
        public Mob Inimigo { get => inimigo; set => inimigo = value; }
        public int Dice_num { get => dice_num; set => dice_num = value; }

        public BattleController(Player jogador,Mob inimigo) {
            this.Jogador = jogador;
            this.Inimigo = inimigo;
            Set_initialStatus(Jogador);
            //Seta o custo mais barato de mana das skills do inimigo
            menorCusto = Inimigo.Skills[0].Custo_mp;
            foreach (Skill s in Inimigo.Skills) {
                if (menorCusto > s.Custo_mp) {
                    menorCusto = s.Custo_mp;
                }
            }
        }
        
        //----------------------------------------------//
        //---------Eventos para a tela saber -----------//
          public delegate void TurnChangeEventHandler();
          public delegate void EnemyAttackEventHandler();
          public delegate void PlayerDeathEventHandler();
          public delegate void EnemyDeathEventHandler();
          public delegate void NoManaEventHandler();
          public event NoManaEventHandler PlayerHasNoMana;
          public event EnemyAttackEventHandler EnemyTurn;
          public event TurnChangeEventHandler PlayerTurn;
          public event PlayerDeathEventHandler PlayerDeath;
          public event EnemyDeathEventHandler EnemyDeath;
        //----------------------------------------------//
        
        /// <summary>
        /// IA básica do inimigo, escolhe com 50% de chance atacar ou usar skill
        /// </summary>
        public void EnemyChoice() {
            int choice;

            Random rand = new Random();
            choice = rand.Next(1, 101);
            
            if (choice < 50 || Inimigo.Mp_atual < menorCusto) {
                Jogador.Take_dmg(Inimigo.Atk_base());
            }
            else{
                CastSkill();
            }
        }


        /// <summary>
        /// Rola um dado de 0 a 100 
        /// </summary>
        public void RollDice() {
            Random rand = new Random();
            Dice_num =  rand.Next(0, 101);
            SetTurno();
        }

        /// <summary>
        /// Verifica o valor obtido no dado e aplica um turno extra
        /// </summary>
        private void SetTurno() {
            if (Turno_player == true && Dice_num == 0) {
                Turno_player = false;
            }
            else if (Dice_num <= Jogador.Lvl || Dice_num <= Inimigo.Lvl) {
                extra_turn = true;
            }
            else if (Turno_player == false && Dice_num == 0) {
                Turno_player = true;
            }
        }

        /// <summary>
        /// Verifica se o jogador e o inimigo estão vivos e retorna uma código para cada caso de morte
        /// </summary>
        /// <returns>Retorna 0 para ambos vivos,Retorna 1 para vitória do jogador e 2 para derrota</returns>
        public void IsAlive() {
            if(Jogador.IsAlive()==true && Inimigo.IsAlive() == true) {
                //return;
            }
            else if(Jogador.IsAlive()==true && Inimigo.IsAlive() == false) {
                Reset_Status(Jogador);
                EnemyDeath();
               //return 1;//Código para vitória do jogador
            }
            else if (Jogador.IsAlive() == false) {
                PlayerDeath();
               // return 2;//Código para derrota do jogador
            }
            else {
                IsAlive();
            }
        }


        /// <summary>
        /// Ataca o inimigo ou o jogador dependendo de quem for o turno
        /// </summary>
        /// <returns></returns>
        public void Atacar() {
            if (turno_player == true) {
                inimigo.Take_dmg(jogador.Atk_base());
                Turno_player = false;
                Turno_atual++;
                if (extra_turn == true) {
                    Turno_player = true;
                }
                //CHAMAR EVENTO
                this.EnemyTurn();
            }
            else {
                EnemyChoice();
                Turno_player = true;
                Turno_atual++;
                if (extra_turn == true) {
                    Turno_player = false;
                }
                //CHAMAR EVENTO
                this.PlayerTurn();
            }
            IsAlive();
        }

        /// <summary>
        /// Casta Skill do Player recebendo o objeto skill que deseja castar como parâmetro
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        public void CastSkill(Skill skill) {
            if (Turno_player == true) {
                Skill s = (from sk in Jogador.Skills where sk.Skill_name == skill.Skill_name select sk).FirstOrDefault<Skill>();
                int dmg = s.executar(Jogador);
                //Caso ocorra um bug e possa usar skill sem ter lv descomente essa parte
                //if (dmg == -2) {
                //    return -2;//Código para quando o jogador não tiver lvl para usar a skill
                //}
                if (dmg == -1) {
                    PlayerHasNoMana();
                    //return -1;//Código para quando o jogador não tiver mana para usar a skill
                }
                else {
                    inimigo.Take_dmg(dmg);
                    IsAlive();
                    this.EnemyTurn();
                }
            }
            else {
                IsAlive();
                this.EnemyTurn();
            }
            
        }

        /// <summary>
        /// Casta skill aleatória do inimigo
        /// </summary>
        /// <returns></returns>
        public void CastSkill() {
            Random num = new Random();
            Skill[] vet = Inimigo.Skills.ToArray();
            int dmg = vet[num.Next(0,3)].executar(Inimigo);
            if (dmg == -1) {
                CastSkill();//Quando o inimigo não puder usar a skill aleatória
            }
            else {
                Jogador.Take_dmg(dmg);
                Turno_player = true;
                Turno_atual++;
                IsAlive();
            }
        }


        public void Set_initialStatus(Player jogador) {
            Player_initalHP = jogador.Hp_total;
            Player_initialMP = jogador.Mp_total;
            Player_initialDmg = jogador.Base_dmg;
            Player_initialDef = jogador.Base_def;
        }

        public void Reset_Status(Player jogador) {
            jogador.Base_def = Player_initialDef;
            jogador.Base_dmg = Player_initialDmg;
            jogador.Hp_total = Player_initalHP;
            jogador.Mp_total = Player_initialMP;

        }
    }
}
