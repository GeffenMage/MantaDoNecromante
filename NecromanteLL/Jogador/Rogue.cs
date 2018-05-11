using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Controls;
    
namespace NecromanteLL {
    public class Rogue : Player {
         
        //Construtor setando os valores base do warrior
        public Rogue(String nome) {
            
            //Inicializa atributos do personagem
            
            this.Nome = nome;
            Lvl = 1; Xp_atual = 0; Xp_total = 1000;
            Hp_total = 400; Hp_atual = Hp_total;
            Mp_total = 200; Mp_atual = Mp_total;
            Base_def = 40; Base_dmg = 70;
            Nome_classe = "Rogue";
            //Inicializa os sprites do personagem

           
            

            Sprite_idle_right = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/rogue/RogueParadaRight.gif"));
            Sprite_idle_left = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/rogue/RogueParadaLeft.gif"));
            Sprite_walking_right = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/rogue/RogueAndarRight.gif"));
            Sprite_walking_left = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/rogue/RogueAndarLeft.gif"));
            Sprite_ataque_right = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/heroes/rogue/RogueAtaque.gif"));
            Sprite_skill_right = null;
            Sprite_death = null;

            //Cria e inicializa as skills da classe do personagem
            Skills = new List<Skill>();
            Skills.Add(new Skill("BackStab", 20, 0, 1, 0, 0, 0, 0, 200));
            Skills.Add(new Skill("BloodVision", 70, 30, 5, 0, 0, 30, 10, 200));
            Skills.Add(new Skill("Shadow", 120, 0, 1, 0, 0, 0, 0, 600));
            atacklenght = 0.9;
        }

        public override void LvUp() {
            Lvl++;
            Xp_atual = Xp_atual - Xp_total;
            Xp_total *= 2;
            Hp_total += 30;
            Mp_total += 30;
            Base_def += 10;
            Base_dmg += 15;
            Hp_atual = Hp_total;
            Mp_atual = Mp_total;
            if (IsLvUP() == true) {
                LvUp();
            }
        }
    }
}
