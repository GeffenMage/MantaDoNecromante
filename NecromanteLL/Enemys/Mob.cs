using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace NecromanteLL {
    public abstract class Mob {

        private String nome;
        private int hp_atual, hp_total;
        private int mp_atual, mp_total;
        private int lvl,given_xp;
        private int base_dmg, base_def;
        private BitmapImage sprite_idle_left;
        private BitmapImage sprite_idle_right;
        private BitmapImage sprite_walking_left;
        private BitmapImage sprite_walking_right;

        public string Nome { get => nome; set => nome = value; }
        public int Hp_atual { get => hp_atual; set => hp_atual = value; }
        public int Hp_total { get => hp_total; set => hp_total = value; }
        public int Mp_atual { get => mp_atual; set => mp_atual = value; }
        public int Mp_total { get => mp_total; set => mp_total = value; }
        public int Lvl { get => lvl; set => lvl = value; }
        public int Given_xp { get => given_xp; set => given_xp = value; }
        public int Base_dmg { get => base_dmg; set => base_dmg = value; }
        public int Base_def { get => base_def; set => base_def = value; }
        public BitmapImage Sprite_idle_left { get => sprite_idle_left; set => sprite_idle_left = value; }
        public BitmapImage Sprite_idle_right { get => sprite_idle_right; set => sprite_idle_right = value; }
        public BitmapImage Sprite_walking_left { get => sprite_walking_left; set => sprite_walking_left = value; }
        public BitmapImage Sprite_walking_right { get => sprite_walking_right; set => sprite_walking_right = value; }

        public int Give_xp() {
            return Given_xp;
        }

        public bool IsAlive() {
            if (Hp_atual > 0) {
                return true;
            }
            else {
                return false;
            }
        }

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
