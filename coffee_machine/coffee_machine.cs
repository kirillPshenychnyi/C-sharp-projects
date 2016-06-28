using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coffee_machine
{
    public class CoffeeMachine
    {

/***************************************************************************/

        public enum Recipe { Espresso, Americano };

        public enum Strength { Light, Medium, Strong };

        public static int WATER_FOR_ESPRESSO = 120, WATER_FOR_AMERICANO = 200;
        public static int WATER_FOR_WASHING = 500;
        public static int BEANS_FOR_LIGHT = 4, BEANS_FOR_MEDIUM = 8, BEANS_FOR_STRONG = 12;

 /***************************************************************************/

        public CoffeeMachine(int _maxBeans, int _maxWater, int _maxPortions)
        {
            if (_maxWater <= 0 || _maxPortions <= 0 || _maxBeans <= 0)
                throw new ArgumentException("Incorrect initial parameters");

            m_maxBeans = _maxBeans;
            m_maxVolume = _maxWater;
            m_maxPortions = _maxPortions;
            m_currentVolume = 0;
            m_currentWaste = 0;
            m_currentBeans = 0;
        }

        public int loadBeans()
        {
            int loaded = m_maxBeans - m_currentBeans;

            m_currentBeans = m_maxBeans;

            return loaded;
        }

        public int loadWater()
        {
            int loaded = m_maxVolume - m_currentVolume;

            m_currentVolume = m_maxVolume;

            return loaded;
        }

        public int getFreeWastePortions()
        {
            return m_maxPortions - m_currentWaste;
        }

        public int getBeansWeight()
        {
            return m_currentBeans;
        }

        public int getWaterVolume()
        {
            return m_currentVolume;
        }

        public void cleanWaste()
        {
            m_currentWaste = 0;
        }

        public bool makeCoffee(Recipe _recipe, Strength _strength)
        {
            if (m_maxPortions == m_currentWaste)
                return false;

            int beans = getBeansForStrength(_strength);

            int water = getWaterForRecipe(_recipe);

            if (m_currentBeans < beans || m_currentVolume < water)
                return false;

            m_currentBeans -= beans;
            m_currentVolume -= water;
            m_currentWaste++;

            return true;

        }

        public void washMachine()
        {
            if (m_maxVolume >= WATER_FOR_WASHING)
                m_currentVolume -= WATER_FOR_WASHING;
        }

        public bool canMake()
        {
            return
                m_currentVolume >= WATER_FOR_ESPRESSO &&
                m_currentBeans >= BEANS_FOR_LIGHT &&
                m_currentWaste < m_maxPortions;
        }

        private int getWaterForRecipe(Recipe _recipe)
        {
            switch (_recipe)
            {
                case Recipe.Espresso:
                    return WATER_FOR_ESPRESSO;

                case Recipe.Americano:
                    return WATER_FOR_AMERICANO;

                default:
                    return 0;
            }
        }

        private int getBeansForStrength(Strength _strength)
        {
            switch (_strength)
            {
                case Strength.Light:
                    return BEANS_FOR_LIGHT;

                case Strength.Medium:
                    return BEANS_FOR_MEDIUM;

                case Strength.Strong:
                    return BEANS_FOR_STRONG;

                default:
                    return 0;
            }
        }

/***************************************************************************/

        private int m_maxBeans;

        private int m_maxVolume;

        private int m_maxPortions;

        private int m_currentBeans;

        private int m_currentVolume;

        private int m_currentWaste;

/***************************************************************************/

    }
}
