using System;
using System.Text;
using System.IO;
using System.Windows.Input;

namespace Catalogo.varios
{
    public class OverrideCursor : IDisposable
    {
        static System.Collections.Stack<Cursor> s_Stack = new System.Collections.Stack<Cursor>();

        public OverrideCursor(Cursor changeToCursor)
        {
            s_Stack.Push(changeToCursor);

            if (Mouse.OverrideCursor != changeToCursor)
                Mouse.OverrideCursor = changeToCursor;
        }

        public void Dispose()
        {
            s_Stack.Pop();

            Cursor cursor = s_Stack.Count > 0 ? s_Stack.Peek() : null;

            if (cursor != Mouse.OverrideCursor)
                Mouse.OverrideCursor = cursor;
        }

    }
}

//http://stackoverflow.com/questions/307004/changing-the-cursor-in-wpf-sometimes-works-sometimes-doesnt