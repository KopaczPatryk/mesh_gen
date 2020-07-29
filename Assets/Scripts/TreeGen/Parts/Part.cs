using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.Scripts.Helpers;

namespace Assets.Scripts.TreeGen.Parts {
    public class Part : IEnumerable<Part> {
        public Vector3 direction { get; set; } = Vector3.up;
        public float baseWidth = 0.2f;
        public float targetWidth = 0.2f;
        public List<Part> children = new List<Part>();

        private System.Random rnd = InstanceFactory.GetInstance().GetRandom();

        public Part() {
            direction = new Vector3(
                            (float)rnd.RandomBetween(-2, 2),
                            (float)rnd.RandomBetween(1, 2),
                            (float)rnd.RandomBetween(-2, 2)
                            );
        }
        public Part(params Part[] segments) {
            direction = new Vector3(
                            (float)rnd.RandomBetween(-2, 2),
                            (float)rnd.RandomBetween(1, 2),
                            (float)rnd.RandomBetween(-2, 2)
                            );
            segments.ToList().ForEach(s => this.Append(s));
        }

        IEnumerator<Part> IEnumerable<Part>.GetEnumerator() {
            return children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return children.GetEnumerator();
        }

        internal void Append(Part part) {
            children.Add(part);
        }
    }
}
