using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Circuits
{
    public class InputSource : Gate
    {
        protected bool highVoltage = false;

        public InputSource(int x, int y) 
        {
            // one output pin
            pins.Add(new Pin(this, false, 20));
            //move the input source and the pins to the position passed in
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
            pins[0].X = x + GAP * 3;
            pins[0].Y = y + GAP / 2 + 4;
        }

        /// <summary>
        /// Draws the gate in the clicked pos
        /// </summary>
        /// <param name="paper"></param>
        public override void Draw(Graphics paper)
        {
            Brush b = new SolidBrush(Color.Green);

            //Draw each of the pins
            foreach (Pin p in pins)
                p.Draw(paper);

            // if the input is SELECTED and high voltage is false  
            if (selected == true && highVoltage == false)
            {
                //toggle input   
                Console.WriteLine("selected once and set as on");
                highVoltage = true;
                selected = false;
            }
                   
            if (selected == true && highVoltage == true)
            {
                Console.WriteLine("selected once and set as off");
                highVoltage = false;
                selected = false;
            }

            if (highVoltage == false)
            {
                paper.DrawImage(Properties.Resources.InputIcon, Left, Top);
            }
            
            if (highVoltage == true)
            {
                paper.DrawImage(Properties.Resources.InputIconON, Left, Top);
            }

        }

        /// <summary>
        /// Evaluate() method of an InputSource will just return its internal on/off boolean variable
        /// </summary>
        /// <returns></returns>
        public override bool Evaluate()
        {
            return highVoltage;
        }


        public override Gate Clone()
        {
            return new InputSource(0, 0);
        }

    }

}
