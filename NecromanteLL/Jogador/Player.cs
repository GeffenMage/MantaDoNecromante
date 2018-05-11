using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Controls;

namespace NecromanteLL {
    public abstract class Player {
        public double skilllenght;
        public double atacklenght;
        private String nome;
        private int hp_atual, hp_total;
        private int mp_atual, mp_total;
        private int xp_atual, xp_total, lvl;
        private int base_dmg, base_def;
        private String nome_classe;
        private List<Skill> skills;
        private List<Itens> inventario = new List<Itens>();
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
        public List<Skill> Skills { get => skills; set => skills = value; }
        public List<Itens> Inventario { get => inventario; set => inventario = value; }
        public Itens Cabeca { get => cabeca; }
        public Itens Maos { get => maos; }
        public Itens Pes { get => pes; }
        public Itens Inferior { get => inferior; }
        public Itens Torso { get => torso; }
        public Itens Mao_esq { get => mao_esq; }
        public Itens Mao_dir { get => mao_dir; }
        public BitmapImage Sprite_idle_left { get => sprite_idle_left; set => sprite_idle_left = value; }
        public BitmapImage Sprite_idle_right { get => sprite_idle_right; set => sprite_idle_right = value; }
        public BitmapImage Sprite_walking_left { get => sprite_walking_left; set => sprite_walking_left = value; }
        public BitmapImage Sprite_walking_right { get => sprite_walking_right; set => sprite_walking_right = value; }
        public BitmapImage Sprite_ataque_right;
        public BitmapImage Sprite_skill_right;
        public BitmapImage Sprite_death;

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
            xp_atual = xp_atual - xp_total;
            Xp_total *= 2;
            hp_atual = Hp_total;
            mp_atual = Mp_total;
            if (IsLvUP() == true) {
                LvUp();
            }
        }

        public void Gain_xp(int xp_gain) {
            Xp_atual += xp_gain;
            if (IsLvUP() == true) {
                LvUp();
            }
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
            int damage_add = 0;
            foreach (Itens i in Inventario) {
                if (i != null) {
                    if (i.Equipado == true) {
                        damage_add += i.Dmg;
                    }
                }
            }
            return Base_dmg + damage_add;
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

        public void Consumir(Itens item) {
            this.hp_atual += item.Hp_up;
            this.mp_atual += item.Mp_up;
        }

        public bool Equipar(Itens item) {
            if (item is Espada ) {
                    
                    if (mao_dir !=null) {
                        return false;
                    }


                    mao_dir = item;
                    Mao_dir.Equipado = true;
                    Base_def += Mao_dir.Def;
                    Mp_total += Mao_dir.Mp_up;
                    Hp_total += Mao_dir.Hp_up;
                    return true;
                
                
            }
            else if (item is Escudo) {

                    if (mao_esq != null) {
                        return false;
                    }

                    mao_esq = item;
                    Mao_esq.Equipado = true;
                    Base_def += Mao_esq.Def;
                    Mp_total += Mao_esq.Mp_up;
                    Hp_total += Mao_esq.Hp_up;
                    return true;
                
                
            }
            else if (item is Luva) {
                    if (maos != null) {
                        return false;
                    }

                    maos = item;
                    Maos.Equipado = true;
                    Base_def += Maos.Def;
                    Mp_total += Maos.Mp_up;
                    Hp_total += Maos.Hp_up;
                    return true;
                
                
            }
            else if (item is Calca) {
                    if (inferior != null) {
                        return false;
                    }

                    inferior = item;
                    Inferior.Equipado = true;
                    Base_def += Inferior.Def;
                    Mp_total += Inferior.Mp_up;
                    Hp_total += Inferior.Hp_up;
                    return true;
               
                
            }
            else if (item is Capacete) {
                    if (cabeca != null) {
                        return false;
                    }
                    cabeca = item;
                    Cabeca.Equipado = true;
                    Base_def += Cabeca.Def;
                    Mp_total += Cabeca.Mp_up;
                    Hp_total += Cabeca.Hp_up;
                    return true;
                
                
            }
            else if (item is Bota) {
                    if (pes != null) {
                        return false;
                    }
                    pes = item;
                    Pes.Equipado = true;
                    Base_def += Pes.Def;
                    Mp_total += Pes.Mp_up;
                    Hp_total += Pes.Hp_up;
                    return true;
                
                
            }
            else if (item is Cajado) {
                    if (mao_dir != null) {
                        return false;
                    }
                    mao_dir = item;
                    Mao_dir.Equipado = true;
                    Base_def += Mao_dir.Def;
                    Mp_total += Mao_dir.Mp_up;
                    Hp_total += Mao_dir.Hp_up;
                    return true;
                
                
            }
            else if (item is Arco) {
                    if (mao_dir != null) {
                        return false;
                    }
                    mao_dir = item;
                    Mao_dir.Equipado = true;
                    Base_def += Mao_dir.Def;
                    Mp_total += Mao_dir.Mp_up;
                    Hp_total += Mao_dir.Hp_up;
                    return true;
                
                
            }
            else if (item is Cota) {
                    if (torso != null) {
                        return false;
                    }
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

        public bool Desequipar(Itens item) {
            if (item is Espada) {
                Mao_dir.Equipado = false;
                Base_def -= Mao_dir.Def;
                Mp_total -= Mao_dir.Mp_up;
                Hp_total -= Mao_dir.Hp_up;
                mao_dir = null;
                return true;
            }
            else if (item is Escudo) {
                Mao_esq.Equipado = false;
                Base_def -= Mao_esq.Def;
                Mp_total -= Mao_esq.Mp_up;
                Hp_total -= Mao_esq.Hp_up;
                mao_esq = null;
                return true;
            }
            else if (item is Luva) {
                Maos.Equipado = false;
                Base_def -= Maos.Def;
                Mp_total -= Maos.Mp_up;
                Hp_total -= Maos.Hp_up;
                maos = null;
                return true;
            }
            else if (item is Calca) {
                Inferior.Equipado = false;
                Base_def -= Inferior.Def;
                Mp_total -= Inferior.Mp_up;
                Hp_total -= Inferior.Hp_up;
                inferior = null;
                return true;
            }
            else if (item is Capacete) {
                Cabeca.Equipado = false;
                Base_def -= Cabeca.Def;
                Mp_total -= Cabeca.Mp_up;
                Hp_total -= Cabeca.Hp_up;
                cabeca = null;
                return true;
            }
            else if (item is Bota) {
                Pes.Equipado = false;
                Base_def -= Pes.Def;
                Mp_total -= Pes.Mp_up;
                Hp_total -= Pes.Hp_up;
                pes = null;
                return true;
            }
            else if (item is Cajado) {
                Mao_dir.Equipado = false;
                Base_def -= Mao_dir.Def;
                Mp_total -= Mao_dir.Mp_up;
                Hp_total -= Mao_dir.Hp_up;
                mao_dir = null;
                return true;
            }
            else if (item is Arco) {
                Mao_dir.Equipado = false;
                Base_def -= Mao_dir.Def;
                Mp_total -= Mao_dir.Mp_up;
                Hp_total -= Mao_dir.Hp_up;
                mao_dir = null;
                return true;
            }
            else if (item is Cota) {
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

        }
    }
}
