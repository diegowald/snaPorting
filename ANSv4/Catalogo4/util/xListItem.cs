namespace Catalogo.Funciones
{
    class xListItemComboBox
    {
        private string m_Descrip;

        private int m_ID;
        public xListItemComboBox(string name, int id)
        {
            m_Descrip = name;
            m_ID = id;
        }

        public xListItemComboBox()
        {
            m_Descrip = "";
            m_ID = 0;
        }

        public int ID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }

        public string Descrip
        {
            get { return m_Descrip; }
            set { m_Descrip = value; }
        }

        public override string ToString()
        {
            return m_Descrip;
        }

    }
}
