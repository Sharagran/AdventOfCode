using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace _8
{
    internal class CircuitManager
    {
        public List<Circuit> circuits = new List<Circuit>();
        public CircuitManager()
        {

        }

        public bool Connect(Vector3 a, Vector3 b)
        {
            var aCircuit = FindCircuit(a);
            var bCircuit = FindCircuit(b);

            
            if (aCircuit != null && bCircuit != null)
            {
                // no duplicate connections
                if (aCircuit == bCircuit)
                    return false;

                if (aCircuit != bCircuit)
                {
                    if(circuits.Count == 2)
                    {
                        Console.WriteLine("Last merge:");
                        Console.WriteLine(Convert.ToInt32(a.X) * Convert.ToInt32(b.X));
                    }
                    MergeCircuits(aCircuit, bCircuit);
                }
            }

            if (aCircuit == null && bCircuit == null)
            {
                circuits.Add(new Circuit(a, b));
            }
            else
            {
                if (aCircuit != null)
                {
                    aCircuit.Add(b);
                }
                else if (bCircuit != null)
                {
                    bCircuit.Add(a);
                }
            }

            return true;
        }

        public override string ToString()
        {
            return circuits.Count.ToString();
        }

        private Circuit? FindCircuit(Vector3 junctionBox)
        {
            return circuits.Find(c => c.Contains(junctionBox));
        }

        private void MergeCircuits(Circuit a, Circuit b)
        {
            var c = new Circuit();
            var merge = a.junctionBoxes.Concat(b.junctionBoxes);
            circuits.Remove(a);
            circuits.Remove(b);

            c.junctionBoxes = merge.ToList();
            circuits.Add(c);
        }
    }

    internal class Circuit
    {
        public List<Vector3> junctionBoxes;

        public Circuit()
        {
            junctionBoxes = new List<Vector3>() {};
        }

        public Circuit(Vector3 a, Vector3 b)
        {
            junctionBoxes = new List<Vector3>() { a, b };
        }

        public void Add(Vector3 v)
        {
            junctionBoxes.Add(v);
        }

        public bool Contains(Vector3 v)
        {
            return junctionBoxes.Contains(v);
        }

        public override string ToString()
        {
            return this.junctionBoxes.Count.ToString();
        }
    }
}
