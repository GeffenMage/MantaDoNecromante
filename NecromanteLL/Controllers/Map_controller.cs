using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Controls;

namespace NecromanteLL {
    public class Map_controller {

            
        private List<Itens> itens;
        private List<Mob> mobs;
        private Itens[,] itens_do_mapa;
        private Mob[,] Mobs_do_mapa;
        private Player jogador;
        Random item_num;

        internal List<Itens> Itens { get => Itens; set => Itens = value; }


        public Map_controller() {
            itens = new List<Itens>();
            mobs = new List<Mob>();
            Mobs_do_mapa = new Mob[100, 100];
            itens_do_mapa = new Itens[100, 100];
            item_num = new Random();
            mobs.Add(new Goblin(1));
            mobs.Add(new Goblin(2));
            mobs.Add(new Goblin(3));
            mobs.Add(new Ogro(1));
            mobs.Add(new Ogro(2));
            mobs.Add(new Ogro(3));
            mobs.Add(new Fantasma(1));
            mobs.Add(new Fantasma(2));
            mobs.Add(new Fantasma(3));
            mobs.Add(new Laposo(1));
            mobs.Add(new Laposo(2));
            mobs.Add(new Laposo(3));

            itens.Add(new Escudo("Escudo de madeira", 0, 20, 10, 0, "ms-appx:///GameAssets/Characters/itens/escudo_1.png"));
            itens.Add(new Escudo("Escudo de metal", 0, 40, 10, 0, "ms-appx:///GameAssets/Characters/itens/escudo_2.png"));
            itens.Add(new Espada("Espada de madeira", 20, 0, 10, 0, "ms-appx:///GameAssets/Characters/itens/espada_1.png"));
            itens.Add(new Espada("Espada de metal", 40, 0, 10, 0, "ms-appx:///GameAssets/Characters/itens/espada_2.png"));
            itens.Add(new Arco("Arco de madeira", 20, 0, 10, 0, "ms-appx:///GameAssets/Characters/itens/arco_1.png"));
            itens.Add(new Arco("Arco de metal", 40, 0, 10, 0, "ms-appx:///GameAssets/Characters/itens/arco_2.png"));
            itens.Add(new Cajado("Cajado de madeira", 20, 0, 0, 10, "ms-appx:///GameAssets/Characters/itens/cajado_1.png"));
            itens.Add(new Cajado("Cajado de metal", 40, 0, 0, 10, "ms-appx:///GameAssets/Characters/itens/cajado_2.png"));
            itens.Add(new Bota("Bota de Couro", 0, 20, 0, 10, "ms-appx:///GameAssets/Characters/itens/bota_1.png"));
            itens.Add(new Bota("Bota de Bronze", 10, 20, 10, 0, "ms-appx:///GameAssets/Characters/itens/bota_2.png"));
            itens.Add(new Calca("Calca de Couro", 0, 20, 0, 10, "ms-appx:///GameAssets/Characters/itens/pant_t.png"));
            itens.Add(new Calca("Calca de Bronze", 10, 30, 10, 0, "ms-appx:///GameAssets/Characters/itens/pant_t.png"));
            itens.Add(new Capacete("Capacete de Couro", 0, 20, 10, 0, "ms-appx:///GameAssets/Characters/itens/capacete_1.png"));
            itens.Add(new Capacete("Capacete de Bronze", 0, 50, 10, 0, "ms-appx:///GameAssets/Characters/itens/capacete_2.png"));
            itens.Add(new Cota("Cota de Couro", 10, 10, 0, 10, "ms-appx:///GameAssets/Characters/itens/cota_1.png"));
            itens.Add(new Cota("Cota de Bronze", 20, 20, 10, 10, "ms-appx:///GameAssets/Characters/itens/cota_2.png"));
            itens.Add(new Luva("Luva de Couro", 10, 20, 0, 10, "ms-appx:///GameAssets/Characters/itens/luva_1.png"));
            itens.Add(new Luva("Luva de Bronze", 20, 30, 20, 10, "ms-appx:///GameAssets/Characters/itens/luva_2.png"));
        }
        
        public void setItem(int pos_x,int pos_y) {
            
            Itens[] vet = itens.ToArray();
            //vet[0].Sprite.Source = new BitmapImage(new Uri("ms-appx:///GameAssets/Maps/chest_idle.png"));
           
            itens_do_mapa[pos_x, pos_y] = vet[item_num.Next(0,18)];
            //itens_do_mapa[pos_x, pos_y] = vet[0];

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
            vet[0].Sprite.Source = new BitmapImage(new Uri("ms-appx:///GameAssets/Characters/enemies/exqueleton/skeletonIdle.gif"));
            //Mobs_do_mapa[pos_x, pos_y] = vet[mob_num.Next(0,12)];
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
