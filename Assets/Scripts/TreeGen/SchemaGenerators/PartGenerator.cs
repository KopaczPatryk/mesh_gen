using System;
using Assets.Scripts.TreeGen.Parts;
using UnityEngine;

namespace Assets.Scripts.TreeGen.SchemaGenerators {
    public class PartGenerator {
        public System.Random Random { get; }

        public PartGenerator(System.Random random) {
            Random = random;
        }

        public Part GenerateTree() {
            Part beginning = new Part();
            beginning.direction = Vector3.up * 3;

            // beginning.Append(new Part());
            // beginning.Append(new Part(new Part()));
            // beginning.Append(new Part(new Part(new Part(), new Part())));
            // beginning.Append(new Part(new Part(new Part())));

            generate(beginning);

            return beginning;
        }

        public void generate(Part segment) {
            var type = randBetween(0, 4);

            if (type == 0) { //split
                var c = randBetween(1, 4);
                // Debug.Log($"Added split {c}");
                for (int i = 0; i < c; i++) {
                    var s = new Part();
                    // s.direction = new Vector3(randBetween(1, 3), randBetween(1, 3), randBetween(1, 3));
                    generate(s);
                    segment.Append(s);
                }
            } else if (type < 3) { //branch
                // Debug.Log("Added branch");
                var s = new Part();
                // s.direction = new Vector3(randBetween(1, 3), randBetween(1, 3), randBetween(1, 3));

                generate(s);
                segment.Append(s);
            } else {
                // Debug.Log("Added leaf");
            }
        }

        protected double randBetween(double min, double max) {
            return min + (Random.NextDouble() * max - min);
        }

        protected float randBetween(float min, float max) {
            return (float) (min + (Random.NextDouble() * max - min));
        }

        protected int randBetween(int min, int max) {
            return Random.Next(min, max);
        }

        protected Vector3 GetCirclePoint(float ang, float radius = 5) {
            Vector3 vector = new Vector3(
                (float) Math.Cos(Mathf.Deg2Rad * ang) * radius,
                0,
                (float) Math.Sin(Mathf.Deg2Rad * ang) * radius
            );
            // Debug.Log(vector);
            return vector;
        }
    }
}