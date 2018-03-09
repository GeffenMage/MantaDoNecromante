using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace NecromanteLL {
    public abstract class Player 
    {
       private String nome;
       private int hp_atual, hp_total;
       private int mp_atual, mp_total;
       private int xp_atual, xp_total,lvl;
       private int base_dmg, base_def;
       private String nome_classe;
       private List<Skill> skills;
       private List<Itens> inventario;
       private Itens cabeca;
       private Itens maos;
       private Itens pes;
       private Itens inferior;
       private Itens torso;
       private Itens mao_esq;
       private Itens mao_dir;
       private BitmapImage sprite_idle_left;
       private BitmapImage sprite_idle_right;
       private BitmapImage sprite_walking_left;
       private BitmapImage sprite_walking_right;

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
        internal List<Itens> Inventario { get => inventario; set => inventario = value; }
        internal Itens Cabeca { get => cabeca;}
        internal Itens Maos { get => maos;}
        internal Itens Pes { get => pes;}
        internal Itens Inferior { get => inferior;}
        internal Itens Torso { get => torso;}
        internal Itens Mao_esq { get => mao_esq; }
        internal Itens Mao_dir { get => mao_dir;}
        public BitmapImage Sprite_idle_left { get => sprite_idle_left; set => sprite_idle_left = value; }
        public BitmapImage Sprite_idle_right { get => sprite_idle_right; set => sprite_idle_right = value; }
        public BitmapImage Sprite_walking_left { get => sprite_walking_left; set => sprite_walking_left = value; }
        public BitmapImage Sprite_walking_right { get => sprite_walking_right; set => sprite_walking_right = value; }

        //Implementar interface gráfica de movimento para o personagem
        public bool IsLvUP() {
            if (Xp_atual >= Xp_total) {
                return true;
            }
            else {
                return false;
            }
        }
        // Virtula para poder realizar override
        public virtual void LvUp() {
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
            int damage_add=0;
            foreach (Itens i in Inventario){
                if (i.Equipado==true) {
                    damage_add += i.Dmg;
                }
            }
            return Base_dmg+damage_add;
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

        public bool Equipar(Itens item) {
            if(item is Espada && Mao_dir == null) {
                mao_dir = item;
                Mao_dir.Equipado = true;
                Base_def += Mao_dir.Def;
                Mp_total += Mao_dir.Mp_up;
                Hp_total += Mao_dir.Hp_up;
                return true;
            }
            else if(item is Escudo && Mao_esq == null) {
                mao_esq = item;
                Mao_esq.Equipado = true;
                Base_def += Mao_esq.Def;
                Mp_total += Mao_esq.Mp_up;
                Hp_total += Mao_esq.Hp_up;
                return true;
            }
            else if(item is Luva && Maos == null) {
                maos = item;
                Maos.Equipado = true;
                Base_def += Maos.Def;
                Mp_total += Maos.Mp_up;
                Hp_total += Maos.Hp_up;
                return true;
            }
            else if (item is Calca && Inferior == null) {
                inferior = item;
                Inferior.Equipado = true;
                Base_def += Inferior.Def;
                Mp_total += Inferior.Mp_up;
                Hp_total += Inferior.Hp_up;
                return true;
            }
            else if (item is Capacete && Cabeca == null) {
                cabeca = item;
                Cabeca.Equipado = true;
                Base_def += Cabeca.Def;
                Mp_total += Cabeca.Mp_up;
                Hp_total += Cabeca.Hp_up;
                return true;
            }
            else if (item is Bota && Pes == null) {
                pes = item;
                Pes.Equipado = true;
                Base_def += Pes.Def;
                Mp_total += Pes.Mp_up;
                Hp_total += Pes.Hp_up;
                return true;
            }
            else if (item is Cajado && Mao_dir == null) {
                mao_dir = item;
                Mao_dir.Equipado = true;
                Base_def += Mao_dir.Def;
                Mp_total += Mao_dir.Mp_up;
                Hp_total += Mao_dir.Hp_up;
                return true;
            }
            else if (item is Arco && Mao_dir == null) {
                mao_dir = item;
                Mao_dir.Equipado = true;
                Base_def += Mao_dir.Def;
                Mp_total += Mao_dir.Mp_up;
                Hp_total += Mao_dir.Hp_up;
                return true;
            }
            else if (item is Cota && Torso == null) {
                torso = item;
                Torso.Equipado = true;
                Base_def += Torso.Def;
                Mp_total += Torso.Mp_up;
                Hp_total += Torso.Hp_up;
                return true;
            }
            else {
                return false;
            }

        }
        
        public bool Desequipar(String slot) {
            switch (slot) {
                case "Maos":
                    if (maos != null) {
                        Maos.Equipado = false;
                        Base_def -= Maos.Def;
                        Mp_total -= Maos.Mp_up;
                        Hp_total -= Maos.Hp_up;
                        maos = null;
                        return true;
                    }
                    else {
                        return false;
                    }
                case "Pes":
                    if (Pes != null) {
                        Pes.Equipado = false;
                        Base_def -= Pes.Def;
                        Mp_total -= Pes.Mp_up;
                        Hp_total -= Pes.Hp_up;
                        pes = null;
                        return true;
                    }
                    else {
                        return false;
                    }
                case "Torso":
                    if (Torso != null) {
                        Torso.Equipado = false;
                        Base_def -= Torso.Def;
                        Mp_total -= Torso.Mp_up;
                        Hp_total -= Torso.Hp_up;
                        torso = null;
                        return true;
                    }
                    else {
                        return false;
                    }
                case "Inferior":
                    if (Inferior != null) {
                        Inferior.Equipado = false;
                        Base_def -= Inferior.Def;
                        Mp_total -= Inferior.Mp_up;
                        Hp_total -= Inferior.Hp_up;
                        inferior = null;
                        return true;
                    }
                    else {
                        return false;
                    }
                case "Cabeca":
                    if (Cabeca != null) {
                        Cabeca.Equipado = false;
                        Base_def -= Cabeca.Def;
                        Mp_total -= Cabeca.Mp_up;
                        Hp_total -= Cabeca.Hp_up;
                        cabeca = null;
                        return true;
                    }
                    else {
                        return false;
                    }
                case "Mao Esquerda":
                    if (Mao_esq != null) {
                        Mao_esq.Equipado = false;
                        Base_def -= Mao_esq.Def;
                        Mp_total -= Mao_esq.Mp_up;
                        Hp_total -= Mao_esq.Hp_up;
                        mao_esq = null;
                        return true;
                    }
                    else {
                        return false;
                    }
                case "Mao Direita":
                    if (Mao_dir != null) {
                        Mao_dir.Equipado = false;
                        Base_def -= Mao_dir.Def;
                        Mp_total -= Mao_dir.Mp_up;
                        Hp_total -= Mao_dir.Hp_up;
                        mao_dir = null;
                        return true;
                    }
                    else {
                        return false;
                    }
                default:
                    return false;
            }
        }


    }
}
