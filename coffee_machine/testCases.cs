using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace coffee_machine
{
    class TestCases
    {
        public static void fillRunner( TestRunner _runner )
        {
            _runner.addTest("test_coffee_constructor", coffeeConstructor);
            _runner.addTest("test_coffee_constructor_wrong_params", coffeeConstructorWrongParams);
            _runner.addTest("test_coffee_constructor_wrong_water", constructorWrongWaterVolume);
            _runner.addTest("test_coffee_constructor_wrong_waste_space", constructorWrongWasteSpace);
            _runner.addTest("test_load_water", loadWater);
            _runner.addTest("test_load_beans", loadBeans);
            _runner.addTest("test_waste_portions", wastePortionsSmallCan);

        }

        private static void coffeeConstructor()
        {
            CoffeeMachine m = new CoffeeMachine(1000, 2000, 16 );

            Debug.Assert( m.getBeansWeight() == 0 );
            Debug.Assert( m.getWaterVolume() == 0 );
            Debug.Assert( m.getFreeWastePortions() == 16 );
        }

        private static void coffeeConstructorWrongParams()
        {
            try
            {
                CoffeeMachine m = new CoffeeMachine(0, 2000, 16 );

                Debug.Assert(false,"Exception must be thrown");
            }
            catch ( ArgumentException _ex )
	        {
                Debug.Assert( _ex.Message == "Incorrect initial parameters" );
            }
        }

        private static void constructorWrongWaterVolume()
        {
            try
            {
                CoffeeMachine m = new CoffeeMachine(2000, 0, 16);

                Debug.Assert(false, "Exception must be thrown");
            }
            catch (ArgumentException _ex)
            {
                Debug.Assert(_ex.Message == "Incorrect initial parameters");
            }
        }

        private static void constructorWrongWasteSpace()
        {
            try
            {
                CoffeeMachine m = new CoffeeMachine(1000, 2000, 0);

                Debug.Assert(false,"Exception must be thrown");

            }
            catch (ArgumentException _ex)
            {
                Debug.Assert(_ex.Message == "Incorrect initial parameters");
            }
        }

        private static void loadWater()
        {
            CoffeeMachine m = new CoffeeMachine(1000, 2000, 16 );
            m.loadBeans();

            int usedWater = m.loadWater();
            Debug.Assert(usedWater == 2000);

            Debug.Assert(m.getWaterVolume() == 2000);

            bool res = m.makeCoffee(CoffeeMachine.Recipe.Espresso, CoffeeMachine.Strength.Light);
        
            Debug.Assert(res);      

            usedWater = m.loadWater();
            Debug.Assert( usedWater == CoffeeMachine.WATER_FOR_ESPRESSO );
        }

        private static void loadBeans()
        {
            CoffeeMachine m = new CoffeeMachine(1000, 2000, 16 );
            m.loadWater();

            int usedBeans = m.loadBeans();
            Debug.Assert(usedBeans == 1000);
            Debug.Assert(m.getBeansWeight() == 1000); 

            bool res = m.makeCoffee(CoffeeMachine.Recipe.Espresso, CoffeeMachine.Strength.Light);
            Debug.Assert(res);
               
            usedBeans = m.loadBeans();
            Debug.Assert(usedBeans == CoffeeMachine.BEANS_FOR_LIGHT);
        }

        private static void wastePortionsSmallCan()
        {
            CoffeeMachine m = new CoffeeMachine(1000, 2000, 1 );
            m.loadBeans();
            m.loadWater();
            Debug.Assert(m.getFreeWastePortions() == 1);
             

            bool res = m.makeCoffee(CoffeeMachine.Recipe.Espresso, CoffeeMachine.Strength.Light);
            Debug.Assert(res);

            Debug.Assert(m.getFreeWastePortions() == 0);

            res = m.makeCoffee(CoffeeMachine.Recipe.Espresso, CoffeeMachine.Strength.Light);
            Debug.Assert(!res);

            m.cleanWaste();
            Debug.Assert(m.getFreeWastePortions() == 1);
            Debug.Assert(!res);
        }

    }
}
