using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenRec.Tools
{
    internal static class InputTool
    {
        // ok uhh this was not worth it :sob: (I'll add more later!!)
        public static ConsoleKeyInfo ReadInput()
        {
            Console.WriteLine();
            Console.Write("[ ]");
            Console.CursorLeft -= 2;
            ConsoleKeyInfo A = Console.ReadKey();
            return A;
        }
    }
}
