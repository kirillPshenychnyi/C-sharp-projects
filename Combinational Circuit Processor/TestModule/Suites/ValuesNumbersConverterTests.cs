/***************************************************************************/

using Microsoft.VisualStudio.TestTools.UnitTesting;

/***************************************************************************/

namespace TestModule.Suites
{
    /***************************************************************************/

    using LogicalModel.API;
    using LogicalModel.Implementation;
  	using Numbers = System.Collections.Generic.List< int >;
    using LogicSet = System.Collections.Generic.List< LogicalModel.API.LogicValue.Enum >;

    /***************************************************************************/
	
	[ TestClass ]
	public class ValuesNumbersConverterTests
    {
        /***************************************************************************/

        [ TestMethod ]
        public void simpleConvert()
        {            
          LogicValuesNumbersConverter converter = new LogicValuesNumbersConverter();
                   
          LogicSet set1 = new LogicSet();
          
          set1.Add( LogicValue.Enum.Low );
          set1.Add( LogicValue.Enum.Low );
          set1.Add( LogicValue.Enum.Low );
          
          //000    
          Assert.AreEqual( converter.executeOnSimpleLogicSet( set1 ), 0 );
             
          //001     
          set1[ 0 ] = LogicValue.Enum.High;
                
          Assert.AreEqual( converter.executeOnSimpleLogicSet( set1 ), 1 );
         
          //010
          
          set1[ 1 ] = LogicValue.Enum.High;
          set1[ 0 ] = LogicValue.Enum.Low;     
            
          Assert.AreEqual( converter.executeOnSimpleLogicSet( set1 ), 2 );
          
          //011          
          set1[ 0 ] = LogicValue.Enum.High;
            
          Assert.AreEqual( converter.executeOnSimpleLogicSet( set1 ), 3 );
		  
          //100
          set1[ 2 ] = LogicValue.Enum.High;
	      set1[ 1 ] = LogicValue.Enum.Low;
          set1[ 0 ] = LogicValue.Enum.Low;
            
          Assert.AreEqual( converter.executeOnSimpleLogicSet( set1 ), 4 );
		
          //101
		  set1[ 0 ] = LogicValue.Enum.High;

		  Assert.AreEqual( converter.executeOnSimpleLogicSet( set1 ), 5 );

		}

        /***************************************************************************/

        [ TestMethod ]
        public void convert()
        {
       
          LogicValuesNumbersConverter converter = new LogicValuesNumbersConverter();
          
	      ILineCollection lines = BaseLineCollection.createLineCollection( 3 );

	        // 000

           int linesCount = lines.Size;
 	       for ( int i = 0; i <linesCount; ++i )
                lines[ i ] = new Line();
            
            //000
	        lines[ 0 ].Value = LogicValue.Enum.Low;    
	        lines[ 1 ].Value = LogicValue.Enum.Low;
	        lines[ 2 ].Value = LogicValue.Enum.Low;

	        Numbers result = converter.toNumbers( lines, 0, 2 );

	        Assert.AreEqual( result.Count, 1 );
	        Assert.AreEqual( result[ 0 ], 0 );

	        // 111
	        lines[ 0 ].Value = LogicValue.Enum.High;    
	        lines[ 1 ].Value = LogicValue.Enum.High;
	        lines[ 2 ].Value = LogicValue.Enum.High;

	        result = converter.toNumbers( lines, 0, 2 );

	        Assert.AreEqual( result.Count, 1 );
	        Assert.AreEqual( result[ 0 ], 7 );

	        // 00X
	        lines[ 0 ].Value = LogicValue.Enum.DontCare; 
            lines[ 1 ].Value = LogicValue.Enum.Low;
	        lines[ 2 ].Value = LogicValue.Enum.Low;

	        result= converter.toNumbers( lines, 0, 2 );

	        Assert.AreEqual( result.Count, 2 );

	        Assert.IsTrue( result.Contains( 0 ) );
	        Assert.IsTrue( result.Contains( 1 ) );

	        // 0X0
	        lines[ 0 ].Value = LogicValue.Enum.Low; 
 	        lines[ 1 ].Value = LogicValue.Enum.DontCare; 

	        result= converter.toNumbers( lines, 0, 2 );

	        Assert.AreEqual( result.Count, 2 );

	        Assert.IsTrue( result.Contains( 0 ) );
	        Assert.IsTrue( result.Contains( 2 ) );

	        // X00
	        lines[ 1 ].Value = LogicValue.Enum.Low; 
 	        lines[ 2 ].Value = LogicValue.Enum.DontCare; 

	        result= converter.toNumbers( lines, 0, 2 );

	        Assert.AreEqual( result.Count, 2 );

	        Assert.IsTrue( result.Contains( 0 ) );
	        Assert.IsTrue( result.Contains( 4 ) );

	        // XX0
	        lines[ 1 ].Value = LogicValue.Enum.DontCare; 

	        result= converter.toNumbers( lines, 0, 2 );

	        Assert.AreEqual( result.Count, 4 );

	        Assert.IsTrue( result.Contains( 0 ) );
	        Assert.IsTrue( result.Contains( 2 ) );
	        Assert.IsTrue( result.Contains( 4 ) );
	        Assert.IsTrue( result.Contains( 6 ) );

	        // XXX
            lines[ 0 ].Value = LogicValue.Enum.DontCare; 

	        result= converter.toNumbers( lines, 0, 2 );

	        Assert.AreEqual( result.Count, 8 );

	        Assert.IsTrue( result.Contains( 0 ) );
	        Assert.IsTrue( result.Contains( 1 ) );
	        Assert.IsTrue( result.Contains( 2 ) );
	        Assert.IsTrue( result.Contains( 3 ) );
	        Assert.IsTrue( result.Contains( 4 ) );
	        Assert.IsTrue( result.Contains( 5 ) );
	        Assert.IsTrue( result.Contains( 6 ) );
	        Assert.IsTrue( result.Contains( 7 ) );
		}

        /***************************************************************************/

        [ TestMethod ]
        public void convertNumberToSet()
        {
         
          LogicValuesNumbersConverter converter = new LogicValuesNumbersConverter();
          
		  LogicSet result = converter.toLogicSet( 0, 2 );
          
          //0->00     
          Assert.AreEqual( result.Count, 2 );

          Assert.AreEqual( result[ 0 ], LogicValue.Enum.Low );
          Assert.AreEqual( result[ 1 ], LogicValue.Enum.Low );
               
          result = converter.toLogicSet( 0, 4 );
          
          //0->0000
          Assert.AreEqual( result.Count, 4 );

          Assert.AreEqual( result[ 0 ], LogicValue.Enum.Low );
          Assert.AreEqual( result[ 1 ], LogicValue.Enum.Low );
          Assert.AreEqual( result[ 2 ], LogicValue.Enum.Low );
          Assert.AreEqual( result[ 3 ], LogicValue.Enum.Low );
             
          result = converter.toLogicSet( 10, 4 );
          
          //10->1010
          Assert.AreEqual( result.Count, 4 );

          Assert.AreEqual( result[ 0 ], LogicValue.Enum.Low );
          Assert.AreEqual( result[ 1 ], LogicValue.Enum.High );
          Assert.AreEqual( result[ 2 ], LogicValue.Enum.Low );
          Assert.AreEqual( result[ 3 ], LogicValue.Enum.High );
                 
           result = converter.toLogicSet( 15, 4 );
         
          //15-> 1111              
          Assert.AreEqual( result.Count, 4 );

          Assert.AreEqual( result[ 0 ], LogicValue.Enum.High );
          Assert.AreEqual( result[ 1 ], LogicValue.Enum.High );
          Assert.AreEqual( result[ 2 ], LogicValue.Enum.High );
          Assert.AreEqual( result[ 3 ], LogicValue.Enum.High );
          
          result = converter.toLogicSet( 1, 4 );
          
          //1-> 0001            
          Assert.AreEqual( result.Count, 4 );

          Assert.AreEqual( result[ 0 ], LogicValue.Enum.High );
          Assert.AreEqual( result[ 1 ], LogicValue.Enum.Low );
          Assert.AreEqual( result[ 2 ], LogicValue.Enum.Low );
          Assert.AreEqual( result[ 3 ], LogicValue.Enum.Low );
        
		}
    }
}

/***************************************************************************/
