using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG.Personagens.Inimigos;

namespace RPG {
    class BattleController {
        int player_initalHP, player_initialMP, player_initialDmg, player_initialDef;
        public void Battle(Player jogador, Mob inimigo) {
            Set_initialStatus(jogador);
            int turno_atual = 1;
            String option = "2";
            bool turno_player = true;
            while (jogador.IsAlive() == true && inimigo.IsAlive() == true) {
                if (turno_player == true) {
                    if (option == "2") {
                        Skill_select(jogador, option, inimigo);
                        turno_player = false;
                        turno_atual++;
                    }
                    else {
                        inimigo.Take_dmg(jogador.Atk_base());
                        turno_player = false;
                        turno_atual++;
                        //
                    }
                }
                else {
                    //Cada caso é um comportamento de mob
                    switch (inimigo.Nome) {
                        case "Goblin":
                            jogador.Take_dmg(inimigo.Atk_base());
                            turno_player = true;
                            turno_atual++;
                            break;
                        default:
                            jogador.Take_dmg(inimigo.Atk_base());
                            turno_player = true;
                            turno_atual++;
                            break;
                    }
                }
            }
            if (jogador.IsAlive() == false) {
                Debug.WriteLine("GAME OVER\nVOCE MORREU");
                return;
            }
            else {
                //Victory(jogador, inimigo);
                return;
            }
        }

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
            player_initalHP = jogador.Hp_total;
            player_initialMP = jogador.Mp_total;
            player_initialDmg = jogador.Base_dmg;
            player_initialDef = jogador.Base_def;
        }

        public void Reset_Status(Player jogador) {
            jogador.Base_def = player_initialDef;
            jogador.Base_dmg = player_initialDmg;
            jogador.Hp_total = player_initalHP;
            jogador.Mp_total = player_initialMP;

        }
    }
}
