using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel.Implementation
{
    using API;

    public class OneLineCollection : BaseLineCollection
    {
        public OneLineCollection( ILine _line )
        {
            m_line = _line;
        }

        public override int Size
        {
            get
            {
                return 1;
            }
        }

        public override ILine this[ int i ]
        {
            get
            {
                checkIndex( i );
                return m_line;
            }

            set
            {
                checkIndex( i );
                m_line = value;
            }   
        }

        /***************************************************************************/

        private ILine m_line;

        /***************************************************************************/
    }
}
