using System;
using System.Collections.Generic;
using System.Text;

namespace GraphApi.DirectGraph.Weighted
{
    internal class DirectedEdge
    {
        private int _v, _w;
        private double _weight;

        public double Weight => _weight;

        public DirectedEdge(int v, int w, double weight)
        {
            _v = v;
            _w = w;
            _weight = weight;
        }

        public int GetFrom()
        {
            return _v;
        }

        public int GetTo()
        {
            return _w;
        }

        public int CompareTo(DirectedEdge other)
        {
            if (_weight < other.Weight)
            {
                return -1;
            }
            else if (_weight > other.Weight)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public override string ToString()
        {
            return $"directed edge:{_v}->{_w}, {_weight}";
        }
    }
}
