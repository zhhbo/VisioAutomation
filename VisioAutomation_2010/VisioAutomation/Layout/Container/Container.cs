﻿using System.Collections.Generic;
using IVisio = Microsoft.Office.Interop.Visio;
using VA = VisioAutomation;

namespace VisioAutomation.Layout.ContainerLayout
{
    public class Container
    {
        public string Text { get; set; }
        public List<ContainerItem> ContainerItems { get; set; }
        public IVisio.Shape VisioShape { get; set; }
        public VA.Drawing.Rectangle Rectangle;
        public short ShapeID;

        public Container(string text)
        {
            this.Text = text;
            this.ContainerItems = new List<ContainerItem>();
        }

        public ContainerItem Add(string text)
        {
            var ct = new ContainerItem(text);
            this.ContainerItems.Add(ct);
            return ct;
        }
    }
}
