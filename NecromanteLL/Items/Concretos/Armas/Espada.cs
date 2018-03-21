using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Controls;

namespace NecromanteLL {
    public class Espada : Armas {
        public Espada(String nome,int dmg,int def,int hp_up,int mp_up, String figura) {
            this.Nome = nome;
            this.Dmg = dmg;
            this.Def = def;
            this.Hp_up = hp_up;
            this.Mp_up = mp_up;
            this.Sprite = new Image();

            this.Sprite.Source = new BitmapImage(new Uri(figura));
        }
    }
}
