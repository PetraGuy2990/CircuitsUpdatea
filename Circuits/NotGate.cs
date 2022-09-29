using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Circuits
{
    /// <summary>
    /// This is a class for a NOT gate
    /// </summary>
    public class NotGate : Gate
    {
        /// <summary>
        /// Initialises the NOT gate
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public NotGate(int x, int y) 
        {
            // one input pin
            pins.Add(new Pin(this, true, 20));
            // one output pin
            pins.Add(new Pin(this, false, 20));
            //move the gate and the pins to the position passed in
            MoveTo(x, y);
        }

        public override void MoveTo(int x, int y)
        {
            // moves pins and the gate in this method
            //Debugging message
            Console.WriteLine("pins = " + pins.Count);
            //Set the position of the gate to the values passed in
            left = x;
            top = y;
            // must move the pins too
            pins[0].X = x - GAP - 10;
            pins[0].Y = y + GAP + 20 - 3;
            pins[1].X = x + WIDTH + GAP + 17;
            pins[1].Y = y + (HEIGHT / 2) + 5;

        }

        /// <summary>
        /// Draws the gate in the clicked pos
        /// </summary>
        /// <param name="paper"></param>
        public override void Draw(Graphics paper)
        {
            //Draw each of the pins
            foreach (Pin p in pins)
                p.Draw(paper);

            // if the gate is selected it will draw the red NOT gate
            if (selected)
            {
                paper.DrawImage(Properties.Resources.NotGateAllRed, Left, Top);
            }
            // else draws the normal NOT gate
            else
            { 
            paper.DrawImage(Properties.Resources.NotGate, Left, Top);
            }
        }

        public override bool Evaluate()
        {
            Gate gateA = pins[0].InputWire.FromPin.Owner;

            return !gateA.Evaluate();
        }

        public override Gate Clone()
        {
            return new NotGate(50, 50);       
        }

    }
}
