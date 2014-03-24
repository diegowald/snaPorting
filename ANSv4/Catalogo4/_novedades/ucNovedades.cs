using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Catalogo._novedades
{
    public partial class ucNovedades : UserControl
    {
        public ucNovedades()
        {
            InitializeComponent();
            util.BackgroundTasks.ChequeoNovedades checker = new util.BackgroundTasks.ChequeoNovedades(util.BackgroundTasks.BackgroundTaskBase.JOB_TYPE.Asincronico);
            checker.run();
        }
    }
}
