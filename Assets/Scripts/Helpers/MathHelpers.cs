using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Helpers
{
    public static class MathHelpers
    {
        public static int Modulo(int a, int n)
        {
            return a - (int)Math.Floor((float)a / n) * n;
        }
    }
}
