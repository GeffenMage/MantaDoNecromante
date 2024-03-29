﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Controls;

namespace NecromanteLL {
    public class Goblin : Mob {

        public Goblin(int lvl) {
            Skills = new List<Skill>();
            Sprite = new Image();

            switch (lvl){
                case 1:
                    Nome = "Goblin";
                    Hp_total = 300; Hp_atual = Hp_total;
                    Mp_total = 150; Mp_atual = Mp_total;
                    Given_xp = 1000; Lvl = 1;
                    Base_def = 20; Base_dmg = 50;
                    //Inicializa os sprites do inimigo
                    Sprite = new Image();
                    Sprite.Height = 95 * 8 / 3;
                    Sprite.Width = 40 * 8 / 3;
                    Sprite.Source= new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/enemies/goblin/GoblinParadoLeft.gif"));
                    Sprite_ataque_left = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/enemies/goblin/GoblinAtaqueLeft.gif"));
                    AtaqueLenght = 0.98;

                    //Inicializa as skills do inimigo
                    Skills.Add(new Skill("Fall I", 0, 20, 1, 0, 0, 0, 0, 30));
                    Skills.Add(new Skill("Luck I", 80, 0, 1, 0, 0, 0, 0, 150));
                    Skills.Add(new Skill("Unique I", 40, 0, 1, 0, 0, 10, 10, 100));

                    break;
                case 2:
                    Nome = "Goblin defensivo";
                    Hp_total = 450; Hp_atual = Hp_total;
                    Mp_total = 200; Mp_atual = Mp_total;
                    Given_xp = 3000; Lvl = 2;
                    Base_def = 30; Base_dmg = 70;
                    Sprite = new Image();
                    Sprite.Height = 95 * 8 / 3;
                    Sprite.Width = 40 * 8 / 3;

                    Sprite.Source = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/enemies/goblin/GoblinParadoLeft.gif"));
                    Sprite_ataque_left = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/enemies/goblin/GoblinAtaqueLeft.gif"));
                    AtaqueLenght = 0.98;

                    //Inicializa as skills do inimigo
                    Skills.Add(new Skill("Fall II", 0, 20, 1, 0, 0, 0, 0, 50));
                    Skills.Add(new Skill("Luck III", 100, 0, 2, 0, 0, 0, 0, 200));
                    Skills.Add(new Skill("Unique II", 120, 0, 2, 0, 0, 20, 60, 100));

                    break;
                case 3:
                    Nome = "Goblin atacante";
                    Hp_total = 600; Hp_atual = Hp_total;
                    Mp_total = 300; Mp_atual = Mp_total;
                    Given_xp = 7000; Lvl = 3;
                    Base_def = 40; Base_dmg = 90;
                    //Inicializa os sprites do inimigo
                    Sprite = new Image();
                    Sprite.Height = 95 * 8 / 3;
                    Sprite.Width = 40 * 8 / 3;

                    Sprite.Source = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/enemies/goblin/GoblinParadoLeft.gif"));
                    Sprite_ataque_left = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/enemies/goblin/GoblinAtaqueLeft.gif"));
                    AtaqueLenght = 0.98;
                    //Inicializa as skills do inimigo
                    Skills.Add(new Skill("Fall III", 0, 20, 1, 0, 0, 0, 0, 70));
                    Skills.Add(new Skill("Luck III", 120, 0, 2, 0, 0, 0, 0, 250));
                    Skills.Add(new Skill("Unique III", 140, 0, 2, 0, 0, 60, 20, 100));

                    break;
            }
            
        }

    }
}
