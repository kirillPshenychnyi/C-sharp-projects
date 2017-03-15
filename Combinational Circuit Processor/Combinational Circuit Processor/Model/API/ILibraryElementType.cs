/***************************************************************************/

namespace LogicalModel.API
{
    /***************************************************************************/

	public interface ILibraryElementType
	{
		/***************************************************************************/

		LibraryElementKind.Enum elementKind
		{
			get;
		}

		void evaluate( ILogicalElement _element );
  	
		int calculateInputs(int _modifier );

		int calculateOutputs( int _modifier );

       	void checkModifier( int _modifier );

		/***************************************************************************/
	}
}

/***************************************************************************/