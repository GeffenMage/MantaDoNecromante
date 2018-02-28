using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    abstract class Player 
    {
       private String nome;
       private int hp_atual, hp_total;
       private int mp_atual, mp_total;
       private int xp_atual, xp_total,lvl;
       private int base_dmg, base_def;
       private String nome_classe;
       private List<Skill> skills;

        public string Nome { get => nome; set => nome = value; }
        public int Hp_atual { get => hp_atual; set => hp_atual = value; }
        public int Hp_total { get => hp_total; set => hp_total = value; }
        public int Mp_atual { get => mp_atual; set => mp_atual = value; }
        public int Mp_total { get => mp_total; set => mp_total = value; }
        public int Xp_atual { get => xp_atual; set => xp_atual = value; }
        public int Xp_total { get => xp_total; set => xp_total = value; }
        public int Lvl { get => lvl; set => lvl = value; }
        public int Base_dmg { get => base_dmg; set => base_dmg = value; }
        public int Base_def { get => base_def; set => base_def = value; }
        public string Nome_classe { get => nome_classe; set => nome_classe = value; }
        internal List<Skill> Skills { get => skills; set => skills = value; }

        //Implementar interface gráfica de movimento para o personagem
        public bool IsLvUP() {
            if (Xp_atual >= Xp_total) {
                return true;
            }
            else {
                return false;
            }
        }

        public void LvUp() {
            Lvl++;
            Xp_total *= 2;
        }

        public void Gain_xp(int xp_gain) {
            Xp_atual += xp_gain;
        }


        public bool IsManaAvaliable(int custo_de_mana) {
            if (Mp_atual >= custo_de_mana) {
                return true;
            }
            else {
                return false;
            }

            
        }

        public bool IsAlive() {
            if (Hp_atual > 0) {
                return true;
            }
            else {
                return false;
            }
        }

        //Necessario modificar esse método caso quiser adicionar modificadores para ataque
        public int Atk_base() {
            return Base_dmg;
        }

        public bool Take_dmg(int dmg) {
            if (dmg > Base_def) {
                Hp_atual -= dmg;
                return true;
            }
            else {
                return false;
            }
        }

    }
}
