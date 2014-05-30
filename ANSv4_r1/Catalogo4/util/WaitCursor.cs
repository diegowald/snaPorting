using System;
using System.Text;
using System.IO;
using System.Windows.Input;

namespace Catalogo.varios
{
    public class WaitCursor : IDisposable
    {
        private Cursor _previousCursor;

        public WaitCursor()
        {
            _previousCursor = Mouse.OverrideCursor;

            Mouse.OverrideCursor = Cursors.Wait;
        }

        #region IDisposable Members

        public void Dispose()
        {
            Mouse.OverrideCursor = _previousCursor;
        }

        #endregion
    }
}

//http://stackoverflow.com/questions/307004/changing-the-cursor-in-wpf-sometimes-works-sometimes-doesnt