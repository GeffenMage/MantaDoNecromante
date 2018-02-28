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
        public void Battle(Player jogador,Mob inimigo) {
            Set_initialStatus(jogador);
            int turno_atual = 1;
            String option;
            bool turno_player = true;
            while (jogador.IsAlive()==true && inimigo.IsAlive()== true) {
                Debug.Clear();
                Display_player_status(jogador);
                Debug.WriteLine("Turno:" + turno_atual);
                Debug.WriteLine("===================");
                Display_mob_status(inimigo);
                if (turno_player == true) {
                    Display_player_menu(jogador);
                    option = Debug.ReadLine();
                    if(option == "2") {
                        Display_player_skills_Menu(jogador);
                        option = Debug.ReadLine();
                        Debug.WriteLine(inimigo.Nome + " recebeu " + Skill_select(jogador, option, inimigo)+ " de dano");
                        Debug.ReadKey();
                        turno_player = false;
                        turno_atual++;
                    }
                    else {
                        inimigo.Take_dmg(jogador.Atk_base());
                        Debug.WriteLine(inimigo.Nome + " recebeu " + jogador.Atk_base() + " de dano");
                        Debug.ReadKey();
                        turno_player = false;
                        turno_atual++;
                        //
                    }
                }
                else {
                    //Cada caso é um comportamento de mob
                    switch(inimigo.Nome){
                        case "Goblin":
                            jogador.Take_dmg(inimigo.Atk_base());
                            Debug.WriteLine("Voce recebeu:" + inimigo.Base_dmg + " de dano");
                            Debug.ReadKey();
                            turno_player = true;
                            turno_atual++;
                            break;
                        default:
                            jogador.Take_dmg(inimigo.Atk_base());
                            Debug.WriteLine("Voce recebeu:" + inimigo.Base_dmg + " de dano");
                            Debug.ReadKey();
                            turno_player = true;
                            turno_atual++;
                            break;
                    }
                }
            }
            if (jogador.IsAlive() == false) {
                Debug.Clear();
                Debug.WriteLine("GAME OVER\nVOCE MORREU");
                return;
            }
            else {
                Victory(jogador, inimigo);
                return;
            }
        }
        //Mudar retorno para string quando implementar o front-end
        public void Display_player_status(Player jogador) {
            Debug.WriteLine("Player:" + jogador.Nome + "\nLV:" + jogador.Lvl);
            Debug.Write("HP:" + jogador.Hp_atual + "/" + jogador.Hp_total);
            Debug.WriteLine("||MP:" + jogador.Mp_atual + "/" + jogador.Mp_total);
            Debug.WriteLine("XP:" + jogador.Xp_atual + "/" + jogador.Xp_total);
        }
        //Mudar retorno para string quando implementar o front-end
        public void Display_mob_status(Mob inimigo) {
            Debug.WriteLine("Enemy:" + inimigo.Nome + "\nLV:" + inimigo.Lvl);
            Debug.Write("HP:" + inimigo.Hp_atual + "/" + inimigo.Hp_total);
            Debug.WriteLine("||MP:" + inimigo.Mp_atual + "/" + inimigo.Mp_total);
        }

        //Mudar retorno para string quando implementar o front-end
        public void Display_player_menu(Player jogador) {
            Debug.WriteLine("===================");
            Debug.WriteLine("Atacar[1]\nSkills[2]");
        }
        
        //Mudar retorno para String e criar uma variavél string que é concatenada a cada passagem do foreach
        //quando implementar o front-end
        public void Display_player_skills_Menu(Player jogador) {
            
            foreach( Skill s in jogador.Skills){
                int i = 1;
                //Será necessário modificar esse if else para skills que custem mana e hp
                if (s.Custo_hp == 0) {
                    Debug.WriteLine(s.Skill_name + "[" + i + "] Custo:" + s.Custo_mp + "MP");
                }
                else {
                    Debug.WriteLine(s.Skill_name + "[" + i + "] Custo:" + s.Custo_hp + "HP");
                }
                i++;
            }
            
            
        }

        
        public int Skill_select(Player jogador,String skill_num, Mob inimigo) {
            int dmg_skill;
            int index = Int32.Parse(skill_num);
            dmg_skill = jogador.Skills[index-1].executar(jogador);
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

        public void Victory(Player jogador,Mob inimigo) {
            Debug.Clear();
            Debug.WriteLine("Resultados da Batalha");
            Debug.WriteLine("====================");
            Debug.WriteLine("HP: " + jogador.Hp_atual + "/" + jogador.Hp_total);
            Debug.WriteLine("MP: " + jogador.Mp_atual + "/" + jogador.Mp_total);
            Debug.WriteLine("XP: " + jogador.Xp_atual + "/" + jogador.Xp_total+" +"+inimigo.Given_xp);
            jogador.Gain_xp(inimigo.Give_xp());
            Reset_Status(jogador);
            if (jogador.IsLvUP() == true) {
                jogador.LvUp();
                Debug.WriteLine("Voce avancou para o Level " + jogador.Lvl);
                Debug.ReadKey();
            }
            else {
                return;
            }
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
