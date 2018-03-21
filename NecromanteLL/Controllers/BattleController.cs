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
        private Player jogador;
        private Mob inimigo;


        public int Turno_atual { get => Turno_atual1; set => Turno_atual1 = value; }
        public int Player_initalHP { get => player_initalHP; set => player_initalHP = value; }
        public int Player_initialMP { get => player_initialMP; set => player_initialMP = value; }
        public int Player_initialDmg { get => player_initialDmg; set => player_initialDmg = value; }
        public int Player_initialDef { get => player_initialDef; set => player_initialDef = value; }
        public int Turno_atual1 { get => turno_atual; set => turno_atual = value; }
        public bool Turno_player { get => turno_player; set => turno_player = value; }
        public Player Jogador { get => jogador; set => jogador = value; }
        public Mob Inimigo { get => inimigo; set => inimigo = value; }

        public BattleController(Player jogador,Mob inimigo) {
            this.Jogador = jogador;
            this.Inimigo = inimigo;
        }



        /// <summary>
        /// Método de batalha para quando for usar skills 
        /// </summary>
        /// <param name="jogador"></param>
        /// <param name="inimigo"></param>
        /// <param name="option"></param>
        /// <param name="opSkill"></param>
        /// <returns></returns>
        public bool Battle(string option, string opSkill) {
            if (Turno_atual == 1) {
                Set_initialStatus(Jogador);
            }
            if (Jogador.IsAlive() == true && Inimigo.IsAlive() == true) {
                if (Turno_player == true) {
                    Skill_select(Jogador, opSkill, Inimigo);
                    Turno_player = false;
                    Turno_atual++;
                }
                else {
                    //Cada caso é um comportamento de mob
                    switch (Inimigo.Nome) {
                        case "Goblin":
                            Jogador.Take_dmg(Inimigo.Atk_base());
                            Turno_player = true;
                            Turno_atual++;
                            break;
                        default:
                            Jogador.Take_dmg(Inimigo.Atk_base());
                            Turno_player = true;
                            Turno_atual++;
                            break;
                    }
                }
            }
            if (Jogador.IsAlive() == false) {
                Reset_Status(Jogador);
                return false;
            }
            else {
                Reset_Status(Jogador);
                return true;
            }
        }
        /// <summary>
        /// Método de batalha para quando não for usar skills
        /// </summary>
        /// <param name="jogador"></param>
        /// <param name="inimigo"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public bool Battle(Player jogador, Mob inimigo,string option) {
            if (Turno_atual == 1) {
                Set_initialStatus(jogador);
            }
            if (jogador.IsAlive() == true && inimigo.IsAlive() == true) {
                if (Turno_player == true) { 
                    inimigo.Take_dmg(jogador.Atk_base());
                    Turno_player = false;
                    Turno_atual++;
                }
                else {
                    //Cada caso é um comportamento de mob
                    switch (inimigo.Nome) {
                        case "Goblin":
                            jogador.Take_dmg(inimigo.Atk_base());
                            Turno_player = true;
                            Turno_atual++;
                            break;
                        default:
                            jogador.Take_dmg(inimigo.Atk_base());
                            Turno_player = true;
                            Turno_atual++;
                            break;
                    }
                }
            }
            if (jogador.IsAlive() == false) {
                Reset_Status(jogador);
                return false; 
            }
            else {
                Reset_Status(jogador);
                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jogador"></param>
        /// <param name="skill_num"></param>
        /// <param name="inimigo"></param>
        /// <returns></returns>
        public int Skill_select(Player jogador, String skill_num, Mob inimigo) {
            int dmg_skill;
            int index = Int32.Parse(skill_num);
            dmg_skill = jogador.Skills[index - 1].executar(jogador);
            inimigo.Take_dmg(dmg_skill);
            return dmg_skill;
            /*
            switch (jogador.Nome_classe) {
                case "Warrior":
                    Warrior w = jogador as Warrior;
                    switch(skill_num) {
                        case "1":
                            dmg_skill = w.Skills[0].executar(jogador);
                            inimigo.Take_dmg(dmg_skill);
                            return dmg_skill;
                        default:
                            Debug.WriteLine("Skill Inválida, tente novamente.");
                            break;
                    }
                    break;
                //Casos adicionais devem ser colocados para cada classe do jogador
                case "Mago"://Criar um switch case para cada skill da classe
                    break;
                case "Arqueiro"://Criar um switch case para cada skill da classe
                    break;
                default:
                    return 0;
            }
            return 0;
            */
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
