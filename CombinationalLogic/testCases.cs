using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CombinationalLogic
{
    /***************************************************************************/   

    class TestCases
    {
        /***************************************************************************/

        public static void fillRunner(TestRunner _runner)
        {
            _runner.addTest("simple buffer test", bufferTest);
            _runner.addTest("simple inverter test", inverterTest);
            _runner.addTest("simple AND", simpleAND);
            _runner.addTest("simple OR", simpleOR);
            _runner.addTest("simple XOR", simpleXOR);
            _runner.addTest("simple NAND", simpleNAND);
            _runner.addTest("simple NOR", simpleNOR);
            _runner.addTest("simple XNOR", simpleXNOR);
            _runner.addTest("half adder", halfAdder);
        }

        /***************************************************************************/

        private static void bufferTest()
        {
            CombinationalScheme buffer = new CombinationalScheme("simple buffer");

            buffer.addInputPort("a");

            buffer.addUnaryElement(Element.OperationType.Enum.Buffer,"buffer", "a");

            buffer.addOutputPort("out","buffer");

            buffer.setInputPortValue("a", true);

            Debug.Assert(buffer.getValueFromPort("out") == true);

            buffer.setInputPortValue("a", false);

            Debug.Assert(buffer.getValueFromPort("out") == false);
        }
        
        private static void inverterTest()
        {
            CombinationalScheme buffer = new CombinationalScheme("simple inverter");

            buffer.addInputPort("a");

            buffer.addUnaryElement(Element.OperationType.Enum.NOT, "inverter", "a");

            buffer.addOutputPort("out", "inverter");

            buffer.setInputPortValue("a", true);

            Debug.Assert(buffer.getValueFromPort("out") == false);

            buffer.setInputPortValue("a", false);

            Debug.Assert(buffer.getValueFromPort("out") == true);
        }

        private static void simpleAND()
        {
            CombinationalScheme andElement = new CombinationalScheme("simple and");

            andElement.addInputPort("a");
            andElement.addInputPort("b");

            andElement.addBinaryElement(Element.OperationType.Enum.AND, "and_element", "a", "b");

            andElement.addOutputPort("out", "and_element");

            andElement.setInputPortValue("a", true);
            andElement.setInputPortValue("b", true);

            Debug.Assert(andElement.getValueFromPort("out") == true);

            andElement.setInputPortValue("a", false);
            Debug.Assert(andElement.getValueFromPort("out") == false);

            andElement.setInputPortValue("b", false);
            Debug.Assert(andElement.getValueFromPort("out") == false);

        }

        private static void simpleOR()
        {
            CombinationalScheme orElement = new CombinationalScheme("simple or");

            orElement.addInputPort("a");
            orElement.addInputPort("b");

            orElement.addBinaryElement(Element.OperationType.Enum.OR, "or_element", "a", "b");

            orElement.addOutputPort("out", "or_element");

            orElement.setInputPortValue("a", true);
            orElement.setInputPortValue("b", true);

            Debug.Assert(orElement.getValueFromPort("out") == true);

            orElement.setInputPortValue("a", false);
            Debug.Assert(orElement.getValueFromPort("out") == true);

            orElement.setInputPortValue("b", false);
            Debug.Assert(orElement.getValueFromPort("out") == false);

        }

        private static void simpleXOR()
        {
            CombinationalScheme xorElement = new CombinationalScheme("simple xor");

            xorElement.addInputPort("a");
            xorElement.addInputPort("b");

            xorElement.addBinaryElement(Element.OperationType.Enum.XOR, "xor_element", "a", "b");

            xorElement.addOutputPort("out", "xor_element");

            xorElement.setInputPortValue("a", true);
            xorElement.setInputPortValue("b", true);

            Debug.Assert(xorElement.getValueFromPort("out") == false);

            xorElement.setInputPortValue("b", false);
            Debug.Assert(xorElement.getValueFromPort("out") == true);

            xorElement.setInputPortValue("a", false);
            Debug.Assert(xorElement.getValueFromPort("out") == false);
        }

        private static void simpleNAND()
        {
            CombinationalScheme nandElement = new CombinationalScheme("simple nand");

            nandElement.addInputPort("a");
            nandElement.addInputPort("b");

            nandElement.addBinaryElement(Element.OperationType.Enum.AND, "and_element", "a", "b");

            nandElement.addUnaryElement(Element.OperationType.Enum.NOT, "nand", "and_element");

            nandElement.addOutputPort("out", "nand");

            nandElement.setInputPortValue("a", true);
            nandElement.setInputPortValue("b", true);

            Debug.Assert(nandElement.getValueFromPort("out") == false);

            nandElement.setInputPortValue("b", false);
            Debug.Assert(nandElement.getValueFromPort("out") == true);

            nandElement.setInputPortValue("a", false);
            Debug.Assert(nandElement.getValueFromPort("out") == true);
        }

        private static void simpleNOR()
        {
            CombinationalScheme norElement = new CombinationalScheme("simple nor");

            norElement.addInputPort("a");
            norElement.addInputPort("b");

            norElement.addBinaryElement(Element.OperationType.Enum.OR, "OR", "a", "b");
            norElement.addUnaryElement(Element.OperationType.Enum.NOT, "NOT", "OR");

            norElement.addOutputPort("c", "NOT");

            norElement.setInputPortValue("a", false);
            norElement.setInputPortValue("b", false);
            Debug.Assert( norElement.getValueFromPort("c") == true);

            norElement.setInputPortValue("a", true);
            Debug.Assert(norElement.getValueFromPort("c") == false);

            norElement.setInputPortValue("a", false);
            norElement.setInputPortValue("b", true);
            Debug.Assert(norElement.getValueFromPort("c") == false);

            norElement.setInputPortValue("a", true);
            norElement.setInputPortValue("a", true);
            Debug.Assert(norElement.getValueFromPort("c") == false);

        }

        private static void simpleXNOR()
        {
            CombinationalScheme xnorElement = new CombinationalScheme("simple xor");

            xnorElement.addInputPort("a");
            xnorElement.addInputPort("b");

            xnorElement.addBinaryElement(Element.OperationType.Enum.XOR, "xor_element", "a", "b");
            xnorElement.addUnaryElement(Element.OperationType.Enum.NOT, "xnor_element", "xor_element");

            xnorElement.addOutputPort("out", "xnor_element");

            xnorElement.setInputPortValue("a", true);
            xnorElement.setInputPortValue("b", true);

            Debug.Assert(xnorElement.getValueFromPort("out") == true);

            xnorElement.setInputPortValue("b", false);
            Debug.Assert(xnorElement.getValueFromPort("out") == false);

            xnorElement.setInputPortValue("a", false);
            xnorElement.setInputPortValue("b", true);
            Debug.Assert(xnorElement.getValueFromPort("out") == false);

            xnorElement.setInputPortValue("b", false);
            Debug.Assert(xnorElement.getValueFromPort("out") == true);

        }

        private static void halfAdder()
        {
            /*
                x y s c
                0 0 0 0 
                0 1 1 0
                1 0 1 0
                1 1 0 1
            */

            CombinationalScheme halfAdder = new CombinationalScheme("half_adder");

            halfAdder.addInputPort("x");
            halfAdder.addInputPort("y");

            halfAdder.addBinaryElement(Element.OperationType.Enum.XOR, "xor_adder", "x", "y");
            halfAdder.addBinaryElement(Element.OperationType.Enum.AND, "carry", "x", "y");

            halfAdder.addOutputPort("s", "xor_adder");
            halfAdder.addOutputPort("c", "carry");

            Debug.Assert(halfAdder.getValueFromPort("s") == false);
            Debug.Assert(halfAdder.getValueFromPort("c") == false);

            halfAdder.setInputPortValue("x", true);

            Debug.Assert(halfAdder.getValueFromPort("s") == true);
            Debug.Assert(halfAdder.getValueFromPort("c") == false);

            halfAdder.setInputPortValue("x", false);
            halfAdder.setInputPortValue("x", true);

            Debug.Assert(halfAdder.getValueFromPort("s") == true);
            Debug.Assert(halfAdder.getValueFromPort("c") == false);

            halfAdder.setInputPortValue("x", true);
            halfAdder.setInputPortValue("y", true);

            Debug.Assert(halfAdder.getValueFromPort("s") == false);
            Debug.Assert(halfAdder.getValueFromPort("c") == true);

        }

    } // class TestRunner

    /***************************************************************************/

} // namespace CombinationalLogic
