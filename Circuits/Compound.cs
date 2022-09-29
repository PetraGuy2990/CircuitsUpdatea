using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circuits
{
    public class Compound : Gate
    {

        /// <summary>
        /// list of gates
        /// </summary>
        public List<Gate> Gates;

        /// <summary>
        /// initialises a compound gate
        /// </summary>
        public Compound()
        {
            Gates = new List<Gate>();
            selected = false;

        }


        /// <summary>
        /// selected property so that all gates are selected or none
        /// </summary>
        public new bool Selected
        {
            get { return selected; }
            set
            {
                foreach(Gate gate in Gates)
                {
                    gate.Selected = value;
                }
            }
        }

        public override void Draw(Graphics paper)
        {
            Gates.ForEach(gate => gate.Draw(paper));

        }

        /// <summary>
        /// method that adds a gate to the gates list
        /// </summary>
        public void AddGate(List<Gate> sgates)
        {
            ExtractCompundGates(sgates);
            CloneGates(sgates);
            CloneWires(sgates);
        }

        public override void MoveTo(int x, int y)
        {
            //calculate x an y distance from each gate from the first gate 
            //loop
            //2nd gate - 3rd gates  

            int xDis = 0;
            int yDis = 0;

            for (int i = 1; i < Gates.Count; i++)
            {
                //gate at currrent index - 1st gate 
                xDis = Gates[i].Left - Gates[0].Left;
                yDis = Gates[i].Top - Gates[0].Top;
                //put into moveto method
                Gates[i].MoveTo(x + xDis, y + yDis);

                xDis = 0;
                yDis = 0;
            }

        }

        public override bool IsMouseOn(int x, int y)
        {
            return Gates.Any (gate => gate.IsMouseOn(x, y));
        }


        public override Gate Clone()
        {
            Compound clone = new Compound();
            //clone.AddGate(Gates);

            foreach (Gate g in Gates)
            {
                clone.Gates.Add(g);
            }
            return clone;
        }

        public override bool Evaluate()
        {
            foreach (Gate g in Gates)
            {
                if (g is OutputLamp)
                {
                    g.Evaluate();
                }
            }

            return false;
        }

        private void CloneGates (List<Gate> sgates)
        {
            int gL = int.MaxValue;
            int gT = gL;
            int gR = int.MinValue;
            int gB = gR;

            foreach (Gate gate in sgates)
            {


                //int xDis = 0;
                //int yDis = 0;

                //for (int i = 1; i < Gates.Count; i++)
                //{
                //    //gate at currrent index - 1st gate 
                //    xDis = Gates[i].Left - Gates[0].Left;
                //    yDis = Gates[i].Top - Gates[0].Top;
                //    //put into moveto method
                //    Gates[i].MoveTo(x + xDis, y + yDis);

                //    xDis = 0;
                //    yDis = 0;
                //}

                Gates.Add(gate.Clone());

                int left = gate.Position().X;
                int top = gate.Position().Y;
                int right = gate.Position().X + 50;
                int bottom = gate.Position().Y + 50;

                if (left < gL)
                {
                    gL = left;
                }
                if (top < gT)
                {
                    gT = top;   
                }
                if (right < gR)
                {
                    right = gR;
                }
                if (bottom < gB)
                {
                    bottom = gB;
                }

            }
        }


        private void ExtractCompundGates(List<Gate> gates)
        {
            List<Gate> components= new List<Gate>();

            gates.ForEach(gate =>
            {
                if (gate is Compound compound)
                    components.AddRange(compound.Gates);
            });

            gates.RemoveAll(gate => gate is Compound);
            gates.AddRange(components);
        }

        private void CloneWires(List<Gate> sgates)
        {
            //for each gates in the compound
            foreach (Gate gate in sgates)
            {
                //for each pins in the gates
                foreach (Pin pin in gate.Pins)
                {
                    //if 
                    if (pin.connection == null) continue;

                    Gate outoutgate = pin.connection.FromPin.Owner;

                    if (!sgates.Contains(outoutgate)) continue;

                    int sender = sgates.IndexOf(outoutgate);
                    int reciever = sgates.IndexOf(gate);
                    int input = gate.Pins.IndexOf(pin);
                    int output = gate.Pins.IndexOf(pin.connection.FromPin); 

                    Gates[reciever].Pins[input].connection = new Wire 
                        (from: Gates[sender].Pins[output], to: Gates[reciever].Pins[input]);
                }
            }
        }

    }
}
