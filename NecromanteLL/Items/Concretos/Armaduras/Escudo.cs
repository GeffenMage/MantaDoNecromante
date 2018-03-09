using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace NecromanteLL {
    public class Escudo:Armaduras {
        public Escudo(String nome, int dmg, int def, int hp_up, int mp_up) {
            this.Nome = nome;
            this.Dmg = dmg;
            this.Def = def;
            this.Hp_up = hp_up;
            this.Mp_up = mp_up;
            Escudo m = new Escudo("Escudo de madeira", 0, 20, 10, 0);
            //this.Sprite = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/Knight/walkRight.gif"));
        }
    }
}
