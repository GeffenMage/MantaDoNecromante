using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecromanteLL {
    class Map_controller {
        private List<Itens> itens;
        internal List<Itens> Itens { get => Itens; set => Itens = value; }
        public Map_controller() {
            itens = new List<Itens>();
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
       
    }
}
