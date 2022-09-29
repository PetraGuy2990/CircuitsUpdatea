using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Circuits
{
    public class OutputLamp : Gate
    {
        protected bool on = false;

        public OutputLamp(int x, int y) 
        {
            // one inout pin
            pins.Add(new Pin(this, true, 8));
            //move the input source and the pins to the position passed in
            MoveTo(x, y);
        }

        public override void MoveTo(int x, int y)
        {
            //Debugging message
            Console.WriteLine("pins = " + pins.Count);
            //Set the position of the gate to the values passed in
            left = x;
            top = y;
            // must move the pins too
            pins[0].X = x + 5;
            pins[0].Y = y + 20;
        }


        /// <summary>
        /// Draws the gate in the clicked pos
        /// </summary>
        /// <param name="paper"></param>
        public override void Draw(Graphics paper)
        {
            Brush b = new SolidBrush(Color.Yellow);

            //Draw each of the pins
            foreach (Pin p in pins)
                p.Draw(paper);

            if (on == false)
            {
                paper.DrawImage(Properties.Resources.OutputIconOFF, Left, Top);
                Console.WriteLine("off"); //debug
            }

            else if (on == true)
            {
                paper.DrawImage(Properties.Resources.OutputIcon, Left, Top);
                Console.WriteLine("on"); //debug
            }


        }

        /// <summary>
        /// The Evaluate() method of an OutputLamp will evaluate its input pin (like gateA of the AndGate code) 
        /// and set its internal on/off boolean variable to the result of this evaluation.  So the output lamps
        /// trigger a series of Evaluate() calls to other gates in the circuit, then they display and remember the
        /// boolean results of the evaluations.
        /// </summary>
        /// <returns></returns>
        public override bool Evaluate()
        {

            if (pins[0].InputWire != null)
            {
                Gate gateA = pins[0].InputWire.FromPin.Owner;
                on = gateA.Evaluate();
                Console.WriteLine("on " + on);                
            }
            else
            {
                MessageBox.Show("False");
                on = false;
            }
            return on;
        }


        public override Gate Clone()
        {
            return new OutputLamp(0, 0);
        }
    }
}
