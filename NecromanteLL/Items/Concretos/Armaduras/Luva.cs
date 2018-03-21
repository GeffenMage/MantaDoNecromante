﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace NecromanteLL {
    public class Luva : Armaduras {
        public Luva(String nome, int dmg, int def, int hp_up, int mp_up, String figura) {
            this.Nome = nome;
            this.Dmg = dmg;
            this.Def = def;
            this.Hp_up = hp_up;
            this.Mp_up = mp_up;

            this.Sprite.Source = new BitmapImage(new Uri(figura));
        }
    }
}
