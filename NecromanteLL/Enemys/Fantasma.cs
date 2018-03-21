using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Controls;

namespace NecromanteLL {
    public class Fantasma : Mob {

        public Fantasma(int lvl) {
            switch (lvl) {
                case 1:
                    Nome = "Espirito";
                    Hp_total = 1200; Hp_atual = Hp_total;
                    Mp_total = 600; Mp_atual = Mp_total;
                    Given_xp = 1200; Lvl = 30;
                    Base_def = 60; Base_dmg = 200;
                    //Inicializa os sprites do inimigo
                    Sprite = new Image();
                    //Sprite.Source = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/enemies/exqueleton/skeletonIdle.gif"));
                    break;
                case 2:
                    Nome = "Espirito Vingativo";
                    Hp_total = 1500; Hp_atual = Hp_total;
                    Mp_total = 800; Mp_atual = Mp_total;
                    Given_xp = 1500; Lvl = 40;
                    Base_def = 70; Base_dmg = 240;
                    //Inicializa os sprites do inimigo
                    Sprite = new Image();
                    //Sprite.Source = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/enemies/exqueleton/skeletonIdle.gif"));
                    break;
                case 3:
                    Nome = "Ceifador";
                    Hp_total = 2000; Hp_atual = Hp_total;
                    Mp_total = 1000; Mp_atual = Mp_total;
                    Given_xp = 2000; Lvl = 60;
                    Base_def = 80; Base_dmg = 260;
                    //Inicializa os sprites do inimigo
                    Sprite = new Image();
                    //Sprite.Source = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/enemies/exqueleton/skeletonIdle.gif"));
                    break;
            }

        }

    }
}
