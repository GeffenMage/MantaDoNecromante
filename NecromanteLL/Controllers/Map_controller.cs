using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace NecromanteLL {
    class Map_controller {

            
        private List<Itens> itens;
        private List<Mob> mobs;
        private Itens[,] itens_do_mapa;
        private Mob[,] Mobs_do_mapa;
        private Player jogador;

        internal List<Itens> Itens { get => Itens; set => Itens = value; }


        public Map_controller() {
            itens = new List<Itens>();
            mobs = new List<Mob>();
            Mobs_do_mapa = new Mob[100, 100];
            itens_do_mapa = new Itens[100, 100];
            mobs.Add(new Goblin(2));


            itens.Add(new Escudo("Escudo de madeira", 0, 20, 10, 0));
            itens.Add(new Escudo("Escudo de metal", 0, 40, 10, 0));
            itens.Add(new Espada("Espada de madeira", 20, 0, 10, 0));
            itens.Add(new Espada("Espada de metal", 40, 0, 10, 0));
            itens.Add(new Arco("Arco de madeira", 20, 0, 10, 0));
            itens.Add(new Arco("Arco de metal", 40, 0, 10, 0));
            itens.Add(new Cajado("Cajado de madeira", 20, 0, 0, 10));
            itens.Add(new Cajado("Cajado de metal", 40, 0, 0, 10));
            itens.Add(new Bota("Bota de Couro", 0, 20, 0, 10));
            itens.Add(new Bota("Bota de Bronze", 10, 20, 10, 0));
            itens.Add(new Calca("Calca de Couro", 0, 20, 0, 10));
            itens.Add(new Calca("Calca de Bronze", 10, 30, 10, 0));
            itens.Add(new Capacete("Capacete de Couro", 0, 20, 10, 0));
            itens.Add(new Capacete("Capacete de Bronze", 0, 50, 10, 0));
            itens.Add(new Cota("Cota de Couro", 10, 10, 0, 10));
            itens.Add(new Cota("Cota de Bronze", 20, 20, 10, 10));
            itens.Add(new Luva("Luva de Couro", 10, 20, 0, 10));
            itens.Add(new Luva("Luva de Bronze", 20, 30, 20, 10));
        }
        
        public void setItem(int pos_x,int pos_y) {
            Random item_num;
            item_num = new Random();
            Itens[] vet = itens.ToArray();
            vet[0].Sprite = new BitmapImage(new Uri("ms-appx:///GameAssets/Maps/chest_idle.png"));
            //itens_do_mapa[pos_x, pos_y] = vet[item_num.Next(18)];
            itens_do_mapa[pos_x, pos_y] = vet[0];

        }

        public Itens FindIten(int pos_x,int pos_y) {
            if (itens_do_mapa[pos_x, pos_y] != null) {
                return itens_do_mapa[pos_x, pos_y];
            }
            else {
                return null;
            }
        }

        public void setMob(int pos_x,int pos_y) {
            Random mob_num;
            mob_num = new Random();
            Mob[] vet = mobs.ToArray();
            vet[0].Sprite = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/enemies/exqueleton/skeleton-idle.gif"));
            //Mobs_do_mapa[pos_x, pos_y] = vet[mob_num.Next(12)];
            Mobs_do_mapa[pos_x, pos_y] = vet[0];
        }

        public Mob FindMob(int pos_x, int pos_y) {
            if (Mobs_do_mapa[pos_x, pos_y] != null) {
                return Mobs_do_mapa[pos_x, pos_y];
            }
            else {
                return null;
            }
        }

    }
}
