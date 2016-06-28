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
            _runner.addTest("test_waste_one_portion", wastePortionsSmallCan);
            _runner.addTest("test_waste_multiple_portions", wastePortionsMultipleCoffees);
            _runner.addTest("test_wash_enough_water", washEnoughWater);
            _runner.addTest("test_wash_not_enough_water", washNotEnoughWater);
            _runner.addTest("test_can_make_coffee", makeWhenLoadedAndNoWaste);
            _runner.addTest("test_cannot_make_without_beans", cannotMakeCoffeeWithoutBeans);
            _runner.addTest("test_cannot_make_without_water", cannot_make_without_water);
            _runner.addTest("test_correct_resource_use", correctResourceUse);
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

        private static void wastePortionsMultipleCoffees()
        {
            CoffeeMachine m = new CoffeeMachine(1000, 2000, 10 );
            m.loadBeans();
            m.loadWater();
            Debug.Assert(m.getFreeWastePortions() == 10);

            bool res = true;
            for (int i = 0; i < 3; i++)
                res &= m.makeCoffee(CoffeeMachine.Recipe.Espresso, CoffeeMachine.Strength.Light);

            Debug.Assert(res);
            Debug.Assert(m.getFreeWastePortions() == 7);
        }

        private static void washEnoughWater()
        {
            CoffeeMachine m = new CoffeeMachine(1000, 2000, 10 );
            m.loadWater();
            Debug.Assert(m.getWaterVolume() == 2000);
            m.washMachine();
            Debug.Assert(m.getWaterVolume() == (2000 - CoffeeMachine.WATER_FOR_WASHING));
        }

        private static void washNotEnoughWater()
        {
            CoffeeMachine m = new CoffeeMachine(1000, CoffeeMachine.WATER_FOR_WASHING / 2, 10 );
            m.loadWater();
            Debug.Assert(m.getWaterVolume() == CoffeeMachine.WATER_FOR_WASHING / 2);
            m.washMachine();
            Debug.Assert(m.getWaterVolume() == 0);
        }

        private static void makeWhenLoadedAndNoWaste()
        {
            CoffeeMachine m = new CoffeeMachine(1000, 2000, 10 );
            m.loadBeans();
            m.loadWater();

            Debug.Assert(m.canMake());
        }

        private static void cannotMakeCoffeeWithoutBeans()
        {
            CoffeeMachine m = new CoffeeMachine(1000, 2000, 10 );
            m.loadWater();
            Debug.Assert(!m.canMake());
            bool res = m.makeCoffee(CoffeeMachine.Recipe.Espresso, CoffeeMachine.Strength.Light);
            Debug.Assert(!res);
        }

        private static void cannot_make_without_water()
        {
            CoffeeMachine m = new CoffeeMachine(1000, 2000, 10 );
            m.loadBeans();
            Debug.Assert(!m.canMake());
            bool res = m.makeCoffee(CoffeeMachine.Recipe.Espresso, CoffeeMachine.Strength.Light);
            Debug.Assert(!res);
        }

        private static void correctResourceUse()
        {
            CoffeeMachine m = new CoffeeMachine(1000, 2000, 10 );

            bool res;

            m.loadBeans();
            m.loadWater();
            res = m.makeCoffee(CoffeeMachine.Recipe.Espresso, CoffeeMachine.Strength.Light);
            Debug.Assert(res);
            Debug.Assert(m.getBeansWeight() == (1000 - CoffeeMachine.BEANS_FOR_LIGHT));
            Debug.Assert(m.getWaterVolume() == (2000 - CoffeeMachine.WATER_FOR_ESPRESSO));
            Debug.Assert(m.getFreeWastePortions() == 9);

            m.loadBeans();
            m.loadWater();
            res = m.makeCoffee(CoffeeMachine.Recipe.Espresso, CoffeeMachine.Strength.Medium);
            Debug.Assert(res);
            Debug.Assert(m.getBeansWeight() == (1000 - CoffeeMachine.BEANS_FOR_MEDIUM));
            Debug.Assert(m.getWaterVolume() == (2000 - CoffeeMachine.WATER_FOR_ESPRESSO));
            Debug.Assert(m.getFreeWastePortions() == 8);

            m.loadBeans();
            m.loadWater();
            res = m.makeCoffee(CoffeeMachine.Recipe.Espresso, CoffeeMachine.Strength.Strong);
            Debug.Assert(res);
            Debug.Assert(m.getBeansWeight() == (1000 - CoffeeMachine.BEANS_FOR_STRONG));
            Debug.Assert(m.getWaterVolume() == (2000 - CoffeeMachine.WATER_FOR_ESPRESSO));
            Debug.Assert(m.getFreeWastePortions() == 7);

            m.loadBeans();
            m.loadWater();
            res = m.makeCoffee(CoffeeMachine.Recipe.Americano, CoffeeMachine.Strength.Light);
            Debug.Assert(res);
            Debug.Assert(m.getBeansWeight() == (1000 - CoffeeMachine.BEANS_FOR_LIGHT));
            Debug.Assert(m.getWaterVolume() == (2000 - CoffeeMachine.WATER_FOR_AMERICANO));
            Debug.Assert(m.getFreeWastePortions() == 6);

            m.loadBeans();
            m.loadWater();
            res = m.makeCoffee(CoffeeMachine.Recipe.Americano, CoffeeMachine.Strength.Medium);
            Debug.Assert(res);
            Debug.Assert(m.getBeansWeight() == (1000 - CoffeeMachine.BEANS_FOR_MEDIUM));
            Debug.Assert(m.getWaterVolume() == (2000 - CoffeeMachine.WATER_FOR_AMERICANO));
            Debug.Assert(m.getFreeWastePortions() == 5);

            m.loadBeans();
            m.loadWater();
            res = m.makeCoffee(CoffeeMachine.Recipe.Americano, CoffeeMachine.Strength.Strong);
            Debug.Assert(res);
            Debug.Assert(m.getBeansWeight() == (1000 - CoffeeMachine.BEANS_FOR_STRONG));
            Debug.Assert(m.getWaterVolume() == (2000 - CoffeeMachine.WATER_FOR_AMERICANO));
            Debug.Assert(m.getFreeWastePortions() == 4);
        }

    }
}
