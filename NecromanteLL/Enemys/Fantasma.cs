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
            Skills = new List<Skill>();
            Sprite = new Image();
            switch (lvl) {
                case 1:
                    Nome = "Espirito";
                    Hp_total = 1200; Hp_atual = Hp_total;
                    Mp_total = 600; Mp_atual = Mp_total;
                    Given_xp = 1200; Lvl = 30;
                    Base_def = 60; Base_dmg = 200;
                    //Inicializa os sprites do inimigo
                    Sprite = new Image();
                    Sprite.Height = 118 * 8 / 3;
                    Sprite.Width = 50 * 8 / 3;
                    Sprite.Source = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/mage/MagoParadoLeft.gif"));

                    //Inicializa as skills do inimigo
                    Skills.Add(new Skill("Atk Up I", 0, 20, 1, 0, 0, 40, 0, 0));
                    Skills.Add(new Skill("Regen MP I", 60, 0, 1, 0, 180, 0, 0, 0));
                    Skills.Add(new Skill("Drain Life I", 80, 0, 1, 100, 0, 0, 0, 100));

                    break;
                case 2:
                    Nome = "Espirito Vingativo";
                    Hp_total = 1500; Hp_atual = Hp_total;
                    Mp_total = 800; Mp_atual = Mp_total;
                    Given_xp = 1500; Lvl = 40;
                    Base_def = 70; Base_dmg = 240;
                    //Inicializa os sprites do inimigo
                    Sprite = new Image();
                    Sprite.Height = 118 * 8 / 3;
                    Sprite.Width = 50 * 8 / 3;
                    Sprite.Source = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/hero/mage/MagoParadoLeft.gif"));

                    //Inicializa as skills do inimigo
                    Skills.Add(new Skill("Fall II", 0, 20, 1, 0, 0, 0, 0, 70));
                    Skills.Add(new Skill("Regen Mp II", 80, 0, 1, 0, 240, 0, 0, 0));
                    Skills.Add(new Skill("Drain Life II", 120, 0, 1, 250, 0, 0, 0, 250));

                    break;
                case 3:
                    Nome = "Ceifador";
                    Hp_total = 2000; Hp_atual = Hp_total;
                    Mp_total = 1000; Mp_atual = Mp_total;
                    Given_xp = 2000; Lvl = 60;
                    Base_def = 80; Base_dmg = 260;
                    //Inicializa os sprites do inimigo
                    Sprite = new Image();
                    Sprite.Height = 118 * 8 / 3;
                    Sprite.Width = 50 * 8 / 3;

                    Sprite.Source = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/hero/mage/MagoParadoLeft.gif"));

                    //Inicializa as skills do inimigo
                    Skills.Add(new Skill("Fall III", 0, 20, 1, 0, 0, 0, 0, 70));
                    Skills.Add(new Skill("Regen Mp III", 120, 0, 1, 0, 360, 0, 0, 0));
                    Skills.Add(new Skill("Drain Life III", 190, 0, 1, 500, 0, 0, 0, 500));

                    break;
            }

        }

    }
}
