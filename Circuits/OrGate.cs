using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circuits
{
    public class OrGate : Gate
    {
        /// <summary>
        /// This class implements an OR gate with two inputs
        /// and one output.
        /// </summary>

        public OrGate(int x, int y)
        {
            // two input pins
            pins.Add(new Pin(this, true, 20));
            pins.Add(new Pin(this, true, 20));
            // two output pins
            pins.Add(new Pin(this, false, 20));
            //move the gate and the pins to the position passed in
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
            pins[0].X = x - GAP + 3;
            pins[0].Y = y + GAP + 5;
            pins[1].X = x - GAP + 3;
            pins[1].Y = y + HEIGHT - GAP + 5;
            pins[2].X = x + WIDTH + GAP + 30;
            pins[2].Y = y + HEIGHT / 2 + 5;
        }

        public override void Draw(Graphics paper)
        {
            //Draw each of the pins
            foreach (Pin p in pins)
                p.Draw(paper);

            // if the gate is selected draw in red else draws it normal
            if (selected)
            {
                paper.DrawImage(Properties.Resources.OrGateAllRed, Left, Top);
            }
            else
            {
                paper.DrawImage(Properties.Resources.OrGate, Left, Top);
            }

        }

        public override bool Evaluate()
        {
            Gate gateA = pins[0].InputWire.FromPin.Owner;
            Gate gateB = pins[1].InputWire.FromPin.Owner;

            return gateA.Evaluate() || gateB.Evaluate();
        }


        public override Gate Clone()
        {
            return new OrGate(0, 0);
        }
    }
}
