using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Controls;

namespace NecromanteLL {
    public class Warrior : Player {
        
        //Construtor setando os valores base do warrior
        public Warrior(String nome) {
            
            

            //Inicializa atributos do personagem
            this.Nome = nome;
            Lvl = 1; Xp_atual = 0; Xp_total = 1000;
            Hp_total = 500; Hp_atual = Hp_total;
            Mp_total = 250; Mp_atual = Mp_total;
            Base_def = 30; Base_dmg = 80;
            Nome_classe = "Warrior";
            //Inicializa os sprites do personagem
            Sprite_idle_right = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/knight/idleRight.gif"));
            Sprite_idle_left = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/knight/idleLeft.gif"));
            Sprite_walking_left = new  BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/knight/walkLeft.gif"));
            Sprite_walking_right = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/knight/walkRight.gif"));
            Sprite_ataque_right = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/knight/WarriorAtaque.gif"));
            Sprite_skill_right = null;
            Sprite_death = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/knight/WarriorMorre.gif"));
            
            //Cria e inicializa as skills da classe do personagem
            Skills = new List<Skill>();
            Skills.Add(new Skill("Stomp", 30, 0, 1, 0, 0, 0, 0, 250));
            Skills.Add(new Skill("WarCry", 40, 0, 5, 0, 0, 20, 20, 0));
            Skills.Add(new Skill("Berserk", 30, 80, 10, 0, 0, 100, -40, 400));
            atacklenght = 1;
        }
            // Tentativa de override
            public override void LvUp() {
                Lvl++;
                Xp_atual = Xp_atual - Xp_total;
                Xp_total *= 2;
                Hp_total += 50;
                Mp_total += 10;
                Base_def += 5;
                Base_dmg += 20;
                Hp_atual = Hp_total;
                Mp_atual = Mp_total;
                if (IsLvUP() == true) {
                    LvUp();
                }

        }
    }
}

