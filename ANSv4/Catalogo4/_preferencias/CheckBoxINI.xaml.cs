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
    /// Interaction logic for CheckBoxINI.xaml
    /// </summary>
    public partial class CheckBoxINI : UserControl, IINIProperty
    {
        private string _displayName;
        private string _sectionName;
        private string _keyName;
        private string _defaultValue;
        private string _trueValue;
        private string _falseValue;
        private bool _requiresPasswordToEdit;
        private string _password;

        private bool _loading;

        public CheckBoxINI(string DisplayName, string SectionName, string KeyName, string TrueValue, string FalseValue, string DefaultValue = null, bool RequiresPasswordToEdit = false, string Password = "")
        {
            _loading = false;
            _displayName = DisplayName;
            InitializeComponent();
            _sectionName = SectionName;
            _keyName = KeyName;
            _defaultValue = DefaultValue;
            _trueValue = TrueValue;
            _falseValue = FalseValue;
            _requiresPasswordToEdit = RequiresPasswordToEdit;
            _password = Password;
        }

        private void check_Checked(object sender, RoutedEventArgs e)
        {
            if (_loading)
            {
                return;
            }
        }

        public string value
        {
            get
            {
                bool isChecked = (check.IsChecked.HasValue ? check.IsChecked.Value : false);
                return (isChecked ? _trueValue : _falseValue);
            }
            set
            {
                check.IsChecked = (value == _trueValue);
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
            check.Content = displayName;
        }

        private void check_Click_1(object sender, RoutedEventArgs e)
        {
            e.Handled = false;
        }

        private void check_Unchecked_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
