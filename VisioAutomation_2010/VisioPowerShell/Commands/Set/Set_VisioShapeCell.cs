﻿using System.Collections;
using SMA = System.Management.Automation;
using IVisio = Microsoft.Office.Interop.Visio;

namespace VisioPowerShell.Commands.Set
{
    [SMA.Cmdlet(SMA.VerbsCommon.Set, VisioPowerShell.Nouns.VisioShapeCell)]
    public class Set_VisioShapeCell : VisioCmdlet
    {
        [SMA.Parameter(Mandatory = false, Position = 0)]
        public Hashtable Hashtable { get; set; }

        [SMA.Parameter(Mandatory = false)]
        public SMA.SwitchParameter BlastGuards { get; set; }

        [SMA.Parameter(Mandatory = false)]
        public SMA.SwitchParameter TestCircular { get; set; }

        [SMA.Parameter(Mandatory = false)]
        public IVisio.Shape[] Shapes { get; set; }

        protected override void ProcessRecord()
        {
            var target_shapes = this.Shapes ?? this.Client.Selection.GetShapes();
            var t = new VisioAutomation.Scripting.TargetShapes(target_shapes);
            this.Client.ShapeSheet.SetShapeCells(t, this.Hashtable, this.BlastGuards, this.TestCircular);
        }
    }
}