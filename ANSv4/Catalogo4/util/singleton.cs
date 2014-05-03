using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo.util
{
    public class singleton<T> where T : new()
    {
        private static T _instance = default(T);

        public static T instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                }
                return _instance;
            }
        }

        public void Dispose()
        {
        }
    }
}