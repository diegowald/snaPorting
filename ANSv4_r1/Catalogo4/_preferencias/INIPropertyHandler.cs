using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo._preferencias
{
    internal static class INIPropertyHandler
    {
        public static void loadINI(this IINIProperty prop)
        {
            try
            {
                prop.loading = true;
                prop.value = Funciones.modINIs.ReadINI(prop.SectionName, prop.KeyName, prop.DefaultValue);
                prop.setDisplayName(prop.DisplayName);
            }
            finally
            {
                prop.loading = false;
            }
        }

        public static void saveINI(this IINIProperty prop)
        {

            if (prop.DefaultValue != prop.value)
             {
                Funciones.modINIs.WriteINI(prop.SectionName, prop.KeyName, prop.value);
             }
            else
            {
                Funciones.modINIs.DeleteKeyINI (prop.SectionName, prop.KeyName);
            }

        }

        public static void resetINI(this IINIProperty prop)
        {
            try
            {
                prop.loading = true;
                prop.value = prop.DefaultValue;
                prop.setDisplayName(prop.DisplayName);
            }
            finally
            {
                prop.loading = false;
            }

        }

        public static void onSave(this IINIProperty prop)
        {
            saveINI(prop);
        }

        internal static void onLoad(this IINIProperty prop)
        {
            loadINI(prop);
        }

        internal static void onReset(this IINIProperty prop)
        {
            resetINI(prop);
        }
    }
}
