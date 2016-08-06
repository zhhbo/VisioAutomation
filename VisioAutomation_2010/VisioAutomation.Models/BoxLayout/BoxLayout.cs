using System.Collections.Generic;

namespace VisioAutomation.Models.BoxLayout
{
    public class BoxLayout
    {
        public Container Root { get; set; }

        public IEnumerable<Node> Nodes
        {
            get
            {
                Node rootn = this.Root;
                return Utilities.TreeOps.PreOrder(rootn, n => n.GetChildren());
            }
        }

        public void PerformLayout()
        {
            if (this.Root.Count < 1)
            {
                throw new AutomationException("Root must contain at least one child");
            }

            this.Root.CalculateSize();
            this.Place(new Drawing.Point(0, 0));
            this.Root.ReservedRectangle = this.Root.Rectangle;
        }

        private void Place(Drawing.Point origin)
        {
            this.Root._place(origin);
        }
    }
}