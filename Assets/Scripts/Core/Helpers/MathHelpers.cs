using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Helpers {
    public static class MathHelpers {
        public static int Modulo(int a, int n) {
            return a - (int)Math.Floor((float)a / n) * n;
        }

        // public static int RandomBetween(this System.Random random, float min, float max) {
        //     var diff = max - min;
        //     return (int)((random.NextDouble() * diff) + min);
        //     // return (int)(min + (random.NextDouble() * max - min));
        // }
        public static double RandomBetween(this System.Random random, float min, float max) {
            var diff = max - min;
            return ((random.NextDouble() * diff) + min);
            // return (int)(min + (random.NextDouble() * max - min));
        }
    }
}
