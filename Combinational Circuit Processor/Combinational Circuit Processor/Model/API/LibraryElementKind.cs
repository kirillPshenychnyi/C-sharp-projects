
/***************************************************************************/

namespace LogicalModel.API
{
    public class LibraryElementKind
    {
        /***************************************************************************/

        public enum Enum
        {
            AND
         ,  OR
         ,  XOR
         ,  NAND
         ,  NOR
         ,  NXOR
         ,  Inverter
         ,  MUX 
         ,  DMX
         ,  ENC
         ,  DC
         ,  Port
        }

        /***************************************************************************/

        public static string toString( Enum _enum )
        {
            switch ( _enum )
            {
                case Enum.AND:
                    return "AND";
                case Enum.OR:
                    return "OR";
                case Enum.XOR:
                    return "XOR";
                case Enum.NAND:
                    return "NAND";
                case Enum.NOR:
                    return "NOR";
                case Enum.NXOR:
                    return "NXOR";
                case Enum.Inverter:
                    return "Inverter";
                case Enum.MUX:
                    return "MUX";
                case Enum.DMX:
                    return "DMX";
                case Enum.ENC:
                    return "ENC";
                case Enum.DC:
                    return "DC";
                case Enum.Port:
                    return "Port";
                default:
                    return "";
            }
        }

        /***************************************************************************/

        public static bool isSimpleElement( Enum _enum )
        {
            switch ( _enum )
            {
                case Enum.AND:
                case Enum.OR:
                case Enum.XOR:
                case Enum.NAND:
                case Enum.NOR:
                case Enum.NXOR:
                    return true;
                default:
                    return false;
            }
        }

        /***************************************************************************/
    }
}

/***************************************************************************/
