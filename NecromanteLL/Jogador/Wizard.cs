using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Controls;

namespace NecromanteLL {
    public class Wizard : Player {
        //Construtor setando os valores base do warrior 
        
        public Wizard(String nome) {
          
            //Inicializa atributos do personagem
            this.Nome = nome;
            Lvl = 1; Xp_atual = 0; Xp_total = 1000;
            Hp_total = 400; Hp_atual = Hp_total;
            Mp_total = 350; Mp_atual = Mp_total;
            Base_def = 30; Base_dmg = 60;
            Nome_classe = "Wizard";
            //Inicializa os sprites do personagem
            
            Sprite_idle_right = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/mage/MagoParadoRight.gif"));
            Sprite_idle_left = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/mage/MagoParadoLeft.gif"));
            Sprite_walking_left = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/mage/MagoAndarLeft.gif"));
            Sprite_walking_right = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/mage/MagoAndarRight.gif"));
            Sprite_ataque_right = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/mage/MagoAtaque.gif"));
            Sprite_skill_right = null;
            Sprite_death = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/mage/MagoMorre.gif"));

            //Cria e inicializa as skills da classe do personagem
            Skills = new List<Skill>();
            Skills.Add(new Skill("Fireball", 40, 0, 1, 0, 0, 0, 0, 300));
            Skills.Add(new Skill("IceArmor", 100, 0, 5, 0, 0, 0, 40, 0));
            Skills.Add(new Skill("Meteor", 300, 0, 10, 0, 0, 0, 0, 800));
            atacklenght = 0.91;
        }

        public override void LvUp() {
            Lvl++;
            Xp_atual = Xp_atual - Xp_total;
            Xp_total *= 2;
            Hp_total += 20;
            Mp_total += 40;
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
