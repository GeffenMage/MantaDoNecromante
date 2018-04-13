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
                    Hp_total = 300; Hp_atual = Hp_total;
                    Mp_total = 150; Mp_atual = Mp_total;
                    Given_xp = 1000; Lvl = 5;
                    Base_def = 30; Base_dmg = 70;
                    //Inicializa os sprites do inimigo
                    Sprite = new Image();
                    //Sprite.Source = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/enemies/exqueleton/skeletonIdle.gif"));

                    Skills.Add(new Skill("POG I", 40, 0, 1, 10, 10, 10, 10, 20));
                    Skills.Add(new Skill("Sem Mudanças I", 70, 0, 1, 0, 0, 0, 0, 250));
                    Skills.Add(new Skill("// I", 70, 0, 1, 10, 0, 10, 0, 80));

                    break;
                case 2:
                    Nome = "Laposo World";
                    Hp_total = 450; Hp_atual = Hp_total;
                    Mp_total = 200; Mp_atual = Mp_total;
                    Given_xp = 3000; Lvl = 10;
                    Base_def = 40; Base_dmg = 90;
                    //Inicializa os sprites do inimigo
                    Sprite = new Image();
                    //Sprite.Source = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/enemies/exqueleton/skeletonIdle.gif"));

                    Skills.Add(new Skill("POG II", 60, 0, 1, 20, 20, 20, 20, 40));
                    Skills.Add(new Skill("Sem Mudanças II", 100, 0, 1, 0, 0, 0, 0, 350));
                    Skills.Add(new Skill("// II", 100, 0, 1, 20, 0, 20, 0, 100));

                    break;
                case 3:
                    Nome = "Laposo HelloWorld";
                    Hp_total = 600; Hp_atual = Hp_total;
                    Mp_total = 300; Mp_atual = Mp_total;
                    Given_xp = 7000; Lvl = 15;
                    Base_def = 50; Base_dmg = 110;
                    //Inicializa os sprites do inimigo
                    Sprite = new Image();
                    //Sprite.Source = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/enemies/exqueleton/skeletonIdle.gif"));

                    Skills.Add(new Skill("POG III", 80, 0, 1, 30, 30, 30, 30, 60));
                    Skills.Add(new Skill("Sem Mudanças III", 120, 0, 1, 0, 0, 0, 0, 450));
                    Skills.Add(new Skill("// III", 120, 0, 1, 40, 0, 40, 0, 140));

                    break;
            }

        }

    }
}
