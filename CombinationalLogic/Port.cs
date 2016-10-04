using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombinationalLogic
{
    public abstract class Port
    {
        public Port( string _name )
        {
            m_name = _name;
        }

        public string Name
        {
            get
            {
                return m_name;
            }
        }

        public abstract bool Value
        {
            get;
            set;
        }

        private readonly string m_name;    

    } // class Port

} // namespace CombinationalLogic
