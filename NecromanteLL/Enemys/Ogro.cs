using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Controls;

namespace NecromanteLL {
    public class Ogro : Mob {

        public Ogro(int lvl) {
            Skills = new List<Skill>();
            Sprite = new Image();
            switch (lvl) {
                case 1:
                    Nome = "Ogro Verde";
                    Hp_total = 800; Hp_atual = Hp_total;
                    Mp_total = 100; Mp_atual = Mp_total;
                    Given_xp = 1000; Lvl = 20;
                    Base_def = 60; Base_dmg = 110;
                    //Inicializa os sprites do inimigo

                    Sprite.Source = new BitmapImage(new Uri("ms-appx:///GameAssets/Menu/Sprites/OgroParado1.gif"));

                    //Inicializa as skills do inimigo
                    Skills.Add(new Skill("Blast I", 30, 0, 1, 0, 0, 0, 0, 200));
                    Skills.Add(new Skill("Regen Hp I", 40, 0, 1, 150, 0, 0, 10, 0));
                    Skills.Add(new Skill("Bloodlust I", 30, 100, 1, 0, 0, 60, -40, 120));

                    break;
                case 2:
                    Nome = "Ogro Negro";
                    Hp_total = 1000; Hp_atual = Hp_total;
                    Mp_total = 150; Mp_atual = Mp_total;
                    Given_xp = 1300; Lvl = 25;
                    Base_def = 70; Base_dmg = 130;
                    //Inicializa os sprites do inimigo

                    Sprite.Source = new BitmapImage(new Uri("ms-appx:///GameAssets/Menu/Sprites/OgroParado.gif"));

                    //Inicializa as skills do inimigo
                    Skills.Add(new Skill("Blast II", 50, 0, 1, 0, 0, 0, 0, 400));
                    Skills.Add(new Skill("Regen Hp II", 60, 0, 1, 300, 0, 0, 20, 0));
                    Skills.Add(new Skill("Bloodlust II", 50, 130, 1, 0, 0, 80, -35, 200));

                    break;
                case 3:
                    Nome = "Ogro Chefe";
                    Hp_total = 1200; Hp_atual = Hp_total;
                    Mp_total = 230; Mp_atual = Mp_total;
                    Given_xp = 1700; Lvl = 30;
                    Base_def = 80; Base_dmg = 150;
                    //Inicializa os sprites do inimigo

                    Sprite.Source = new BitmapImage(new Uri("ms-appx:///GameAssets/Menu/Sprites/OgroParado.gif"));

                    //Inicializa as skills do inimigo
                    Skills.Add(new Skill("Blast III", 70, 0, 1, 0, 0, 0, 0, 600));
                    Skills.Add(new Skill("Regen Hp III", 80, 0, 1, 450, 0, 0, 30, 0));
                    Skills.Add(new Skill("Bloodlust III", 80, 160, 1, 0, 0, 110, -30, 260));

                    break;
            }

        }

    }
}
