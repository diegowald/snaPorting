using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo._preferencias
{
    public interface IINIProperty
    {
        string value
        {
            get;
            set;
        }

        bool loading
        {
            get;
            set;
        }

        string SectionName
        {
            get;
        }

        string KeyName
        {
            get;
        }
        
        string DefaultValue
        {
            get;
        }

        string DisplayName
        {
            get;
            set;
        }

        void setDisplayName(string displayName);
    }
}
