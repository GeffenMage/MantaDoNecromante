using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG.Personagens.Inimigos;

namespace RPG
{
    class Program
    {
        static void Main(string[] args)
        {
            BattleController btc = new BattleController();
            Warrior p1 = new Warrior("Jogador");
            Goblin g1 = new Goblin(1);
            Goblin g2 = new Goblin(1);
            Ogro o1 = new Ogro(1);
            btc.Battle(p1, o1);
            Console.ReadKey();
            btc.Battle(p1, g2);
            Console.ReadKey();
        }
    }
}
