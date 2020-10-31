using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSockets
{
    class Program
    {
        static void Main(string[] args)
        {
            Servidor s = new Servidor("localhost", 8080);
            s.Start();
            Console.ReadKey();
        }
    }
}
