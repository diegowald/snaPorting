using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Catalogo._preferencias
{
    /// <summary>
    /// Interaction logic for TextEditINI.xaml
    /// </summary>
    public partial class TextEditINI : UserControl, IINIProperty
    {
        private string _displayName;
        private string _sectionName;
        private string _keyName;
        private string _defaultValue;
        private bool _requiresPasswordToEdit;

        private bool _loading;

        public TextEditINI(string DisplayName, string SectionName, string KeyName, string DefaultValue = null, bool RequiresPasswordToEdit = false)
        {
            _loading = false;
            _displayName = DisplayName;
            InitializeComponent();
            _sectionName = SectionName;
            _keyName = KeyName;
            _defaultValue = DefaultValue;
            _requiresPasswordToEdit = RequiresPasswordToEdit;
            if (_requiresPasswordToEdit)
            {
                Visibility = System.Windows.Visibility.Hidden;
            }

        }

        public string value
        {
            get
            {
                return EditBox1.Text;
            }
            set
            {
                EditBox1.Text = value;
            }
        }

        public bool loading
        {
            get
            {
                return _loading;
            }
            set
            {
                _loading = value;
            }
        }

        public string SectionName
        {
            get
            {
                return _sectionName;
            }
        }

        public string KeyName
        {
            get
            {
                return _keyName;
            }
        }

        public string DefaultValue
        {
            get
            {
                return _defaultValue;
            }
        }

        public string DisplayName
        {
            get
            {
                return _displayName;
            }
            set
            {
                _displayName = value;
            }
        }

        public void setDisplayName(string displayName)
        {
            Label1.Content = displayName;
        }


        public void onEnablePasswrodProtectedControl()
        {
            if (_requiresPasswordToEdit)
            {
                Visibility = System.Windows.Visibility.Visible;
            }
        }
    }
}
