using System;
using System.Collections.Generic;
using System.Text;

namespace GraphApi.UndirectedGraph
{
    /// <summary>
    /// Represents a undirected edge
    /// </summary>
    internal class Edge : IComparable<Edge>
    {
        private int _v, _w;
        private double _weight;

        public double Weight => _weight;

        public Edge(int v, int w, double weight)
        {
            _v = v;
            _w = w;
            _weight = weight;
        }

        public int GetEitherVertex()
        {
            return _v;
        }

        /// <summary>
        /// Simply returns the other vertex that is not 'v'. Will always return an edge
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public int GetOtherVertex(int v)
        {
            return (_v == v) ? _w : _v;
        }

        public int CompareTo(Edge other)
        {
            if(_weight < other.Weight)
            {
                return -1;
            }
            else if(_weight > other.Weight)
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
            return $"edge:{_v}-{_w} {_weight}";
        }
    }
}
