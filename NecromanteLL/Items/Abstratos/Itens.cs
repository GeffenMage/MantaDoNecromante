using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecromanteLL {
    abstract class Itens {
        String nome,descricao;
        int def, dmg, hp_up, mp_up;
        bool equipado;
        String tipo;//O tipo sera onde o item será equipado no personagem

        public string Nome { get => nome; set => nome = value; }
        public string Descricao { get => descricao; set => descricao = value; }
        public int Def { get => def; set => def = value; }
        public int Dmg { get => dmg; set => dmg = value; }
        public int Hp_up { get => hp_up; set => hp_up = value; }
        public int Mp_up { get => mp_up; set => mp_up = value; }
        public bool Equipado { get => equipado; set => equipado = value; }
        public string Tipo { get => tipo; set => tipo = value; }
    }
}
