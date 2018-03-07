using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace NecromanteLL {
    public class Ogro : Mob {

        public Ogro(int lvl) {
            switch (lvl) {
                case 1:
                    Nome = "Ogro Verde";
                    Hp_total = 800; Hp_atual = Hp_total;
                    Mp_total = 100; Mp_atual = Mp_total;
                    Given_xp = 1000; Lvl = 20;
                    Base_def = 60; Base_dmg = 110;
                    //Inicializa os sprites do inimigo
                    //Sprite_idle_right = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/Knight/idleRight.gif"));
                    //Sprite_idle_left = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/Knight/idleLeft.gif"));
                    //Sprite_walking_left = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/Knight/walkLeft.gif"));
                    //Sprite_walking_right = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/Knight/walkRight.gif"));
                    break;
                case 2:
                    Nome = "Ogro Negro";
                    Hp_total = 1000; Hp_atual = Hp_total;
                    Mp_total = 150; Mp_atual = Mp_total;
                    Given_xp = 1300; Lvl = 25;
                    Base_def = 70; Base_dmg = 130;
                    //Inicializa os sprites do inimigo
                    //Sprite_idle_right = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/Knight/idleRight.gif"));
                    //Sprite_idle_left = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/Knight/idleLeft.gif"));
                    //Sprite_walking_left = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/Knight/walkLeft.gif"));
                    //Sprite_walking_right = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/Knight/walkRight.gif"));
                    break;
                case 3:
                    Nome = "Ogro Chefe";
                    Hp_total = 1200; Hp_atual = Hp_total;
                    Mp_total = 150; Mp_atual = Mp_total;
                    Given_xp = 1700; Lvl = 30;
                    Base_def = 80; Base_dmg = 150;
                    //Inicializa os sprites do inimigo
                    //Sprite_idle_right = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/Knight/idleRight.gif"));
                    //Sprite_idle_left = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/Knight/idleLeft.gif"));
                    //Sprite_walking_left = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/Knight/walkLeft.gif"));
                    //Sprite_walking_right = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/Knight/walkRight.gif"));
                    break;
            }

        }

    }
}
