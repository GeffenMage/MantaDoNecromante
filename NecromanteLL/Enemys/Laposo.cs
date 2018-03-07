﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace NecromanteLL {
    public class Laposo : Mob {

        public Laposo(int lvl) {
            switch (lvl) {
                case 1:
                    Nome = "Laposo Hello";
                    Hp_total = 300; Hp_atual = Hp_total;
                    Mp_total = 150; Mp_atual = Mp_total;
                    Given_xp = 100; Lvl = 5;
                    Base_def = 20; Base_dmg = 50;
                    //Inicializa os sprites do inimigo
                    //Sprite_idle_right = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/Knight/idleRight.gif"));
                    //Sprite_idle_left = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/Knight/idleLeft.gif"));
                    //Sprite_walking_left = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/Knight/walkLeft.gif"));
                    //Sprite_walking_right = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/Knight/walkRight.gif"));
                    break;
                case 2:
                    Nome = "Laposo World";
                    Hp_total = 450; Hp_atual = Hp_total;
                    Mp_total = 200; Mp_atual = Mp_total;
                    Given_xp = 300; Lvl = 10;
                    Base_def = 30; Base_dmg = 70;
                    //Inicializa os sprites do inimigo
                    //Sprite_idle_right = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/Knight/idleRight.gif"));
                    //Sprite_idle_left = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/Knight/idleLeft.gif"));
                    //Sprite_walking_left = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/Knight/walkLeft.gif"));
                    //Sprite_walking_right = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/Knight/walkRight.gif"));
                    break;
                case 3:
                    Nome = "Laposo HelloWorld";
                    Hp_total = 600; Hp_atual = Hp_total;
                    Mp_total = 300; Mp_atual = Mp_total;
                    Given_xp = 700; Lvl = 15;
                    Base_def = 40; Base_dmg = 90;
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
