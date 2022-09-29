using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Circuits
{
    public abstract class Gate
    {
        // left is the left-hand edge of the main part of the gate.
        // So the input pins are further left than left.
        protected int left;

        // top is the top of the whole gate
        protected int top;

        // width and height of the main part of the gate
        protected const int WIDTH = 40;
        protected const int HEIGHT = 40;
        // length of the connector legs sticking out left and right
        protected const int GAP = 10;

        protected Brush selectedBrush = Brushes.Red;
        protected Brush normalBrush = Brushes.LightGray;

        //Has the gate been selected
        protected bool selected = false;

        //list of pins
        protected List<Pin> pins = new List<Pin>();

        // position of gate
        protected Point pos = new Point();

        /// <summary>
        /// Gets and sets whether the gate is selected or not.
        /// </summary>
        public bool Selected
        {
            get { return selected; }
            set { selected = value; }
        }

        /// <summary>
        /// Gets the left hand edge of the gate.
        /// </summary>
        public int Left
        {
            get { return left; }
        }

        /// <summary>
        /// Gets the top edge of the gate.
        /// </summary>
        public int Top
        {
            get { return top; }
        }


        /// <summary>
        /// Gets the list of pins for the gate.
        /// </summary>
        public List<Pin> Pins
        {
            get { return pins; }
        }

        /// <summary>
        /// Moves the gate to the position specified.
        /// </summary>
        /// <param name="x">The x position to move the gate to</param>
        /// <param name="y">The y position to move the gate to</param>
        public virtual void MoveTo(int x, int y)
        {
            //Set the position of the gate to the values passed in
            left = x;
            top = y;
        }

        public virtual Point Position()
        {
            Point pos = new Point(Left, Top);
            return pos;  

        }


        /// <summary>
        /// Checks if the gate has been clicked on.
        /// </summary>
        /// <param name="x">The x position of the mouse click</param>
        /// <param name="y">The y position of the mouse click</param>
        /// <returns>True if the mouse click position is inside the gate</returns>
        public virtual bool IsMouseOn(int x, int y)
        {
            if (left <= x && x < left + WIDTH
                && top <= y && y < top + HEIGHT)
                return true;
            else
                return false;
        }

        //public abstract Pin FindPin(Point point);

        /// <summary>
        /// Draws the gate in the normal colour or in the selected colour.
        /// </summary>
        /// <param name="paper"></param>
        public virtual void Draw(Graphics paper)
        {
            Brush brush;
            //Check if the gate has been selected
            if (selected)
            {
                brush = selectedBrush;
            }
            else
            {
                brush = normalBrush;
            }
        }

        /// <summary>
        ///  abstract Evaluate() method that returns a boolean result
        /// </summary>
        public abstract bool Evaluate();

        /// <summary>
        ///  abstract Clone() method that returns a fresh copy of the gate.
        /// </summary>
        public abstract Gate Clone();

    }
}
