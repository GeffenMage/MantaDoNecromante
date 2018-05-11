using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Controls;

namespace NecromanteLL {
    public class Laposo : Mob {

        public Laposo(int lvl) {
            Skills = new List<Skill>();
            Sprite = new Image();

            switch (lvl) {
                case 1:
                    Nome = "Laposo Hello";
                    Hp_total = 2500; Hp_atual = Hp_total;
                    Mp_total = 1500; Mp_atual = Mp_total;
                    Given_xp = 205000; Lvl = 25;
                    Base_def = 90; Base_dmg = 300;
                    //Inicializa os sprites do inimigo
                   
                    Sprite = new Image();
                    Sprite.Height = 105 * 8 / 3;
                    Sprite.Width = 20 * 8 / 3;

                    Sprite.Source = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/enemies/exqueleton/skeletonIdle.gif"));
                    Sprite_ataque_left = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/enemies/exqueleton/SkeletonAtaque.gif"));
                    AtaqueLenght = 1.8;

                    Skills.Add(new Skill("POG I", 200, 0, 20, 0, 0, 0, 0, 900));
                    Skills.Add(new Skill("GOTO I", 250, 0, 25, 0, 0, 50, 0, 250));
                    Skills.Add(new Skill("If-False I", 300, 0, 30, 666, 0, 20, 20, 100));

                    break;
                case 2:
                    Nome = "Laposo World";
                    Hp_total = 3000; Hp_atual = Hp_total;
                    Mp_total = 1700; Mp_atual = Mp_total;
                    Given_xp = 210000; Lvl = 27;
                    Base_def = 100; Base_dmg = 320;
                    //Inicializa os sprites do inimigo
                    Sprite = new Image();
                    Sprite.Height = 105 * 8 / 3;
                    Sprite.Width = 20 * 8 / 3;

                    Sprite.Source = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/enemies/exqueleton/skeletonIdle.gif"));
                    Sprite_ataque_left = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/enemies/exqueleton/SkeletonAtaque.gif"));
                    AtaqueLenght = 1.8;

                    Skills.Add(new Skill("POG I", 200, 0, 20, 0, 0, 0, 0, 900));
                    Skills.Add(new Skill("GOTO I", 250, 0, 25, 0, 0, 50, 0, 250));
                    Skills.Add(new Skill("If-False I", 300, 0, 30, 666, 0, 20, 20, 100));
                    break;
                case 3:
                    Nome = "Laposo HelloWorld";
                    Hp_total = 5500; Hp_atual = Hp_total;
                    Mp_total = 2000; Mp_atual = Mp_total;
                    Given_xp = 220000; Lvl = 30;
                    Base_def = 190; Base_dmg = 630;
                    //Inicializa os sprites do inimigo
                    Sprite = new Image();
                    Sprite.Height = 105 * 8 / 3;
                    Sprite.Width = 20 * 8 / 3;

                    Sprite.Source = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/enemies/exqueleton/skeletonIdle.gif"));
                    Sprite_ataque_left = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/enemies/exqueleton/SkeletonAtaque.gif"));
                    AtaqueLenght = 1.8;

                    Skills.Add(new Skill("POG I", 200, 0, 20, 0, 0, 0, 0, 900));
                    Skills.Add(new Skill("GOTO I", 250, 0, 25, 0, 0, 50, 0, 250));
                    Skills.Add(new Skill("If-False I", 300, 0, 30, 666, 0, 20, 20, 100));
                    break;
            }

        }

    }
}
