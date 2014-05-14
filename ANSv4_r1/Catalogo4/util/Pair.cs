using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo.util
{
    public class Pair<T, U>
    {
        private T _first;
        private U _second;

        public Pair(T f, U s)
        {
            _first = f;
            _second = s;
        }

        public T first
        {
            get
            {
                return _first;
            }
            set
            {
                _first = value;
            }
        }

        public U second
        {
            get
            {
                return _second;
            }
            set
            {
                _second = value;
            }
        }
    }
}
