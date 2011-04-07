import sys
import os

# generate common ShapeSheet objects

#type, cellsrc, name
XFORMCELLS = """
double    |    PinX  	   |      PinX  	  
double    |    PinY  	   |      PinY  	  
double    |    LocPinX	   |      LocPinX	  
double    |    LocPinY	   |      LocPinY	  
double    |    Width 	   |      Width 	  
double    |    Height 	   |      Height 	  
double    |    Angle 	   |      Angle 	  
"""

CONTROLCELLS = """
int |   Controls_CanGlue   |   CanGlue
int |   Controls_Tip    |   Tip
double    |   Controls_X 	   |   X
double    |   Controls_Y  	   |   Y
int    |   Controls_YCon   |   YBehavior
int    |   Controls_XCon   |   XBehavior
int    |   Controls_XDyn   |   XDynamics
int    |   Controls_YDyn   |   YDynamics
"""


def printtop() :
    print """
using System;
using System.Collections.Generic;
using System.Linq;
using VA = VisioAutomation;
using IVisio = Microsoft.Office.Interop.Visio;
using VisioAutomation.Extensions;
    """
    
def gencode_for_cells(text,classname,queryname,qt,si) :
    lines = text.strip()
    lines = text.split("\n");
    lines = [l for l in lines if len(l)]


    printtop()

    print "----------------------------------"
    print "public class", classname
    print "{"

    data = []
    for line in lines :
        tokens = [ t.strip() for t in line.split("|")]
        celltype = tokens[0]
        cellsrc = tokens[1]
        cellname = tokens[2]
        data.append((celltype,cellsrc,cellname))
        
        print "public VA.ShapeSheet.CellData<", celltype, ">" , cellname, "{ get; set; }"

    if (qt=="Cell") :
        print """
                public void Apply(VA.ShapeSheet.Update.SIDSRCUpdate update, short id)
                {
                    this._Apply((src, f) => update.SetFormulaIgnoreNull(id, src, f));
                }

                public void Apply(VA.ShapeSheet.Update.SRCUpdate update)
                {
                    this._Apply((src, f) => update.SetFormulaIgnoreNull(src, f));
                }
        """
    elif (qt=="Section") :
        
        print """
            public void Apply(VA.ShapeSheet.Update.SRCUpdate update, short row)
            {
                this._Apply((src, f) => update.SetFormulaIgnoreNull(src, f),row);
            }

            public void Apply(VA.ShapeSheet.Update.SIDSRCUpdate update, short id, short row )
            {
                this._Apply((src, f) => update.SetFormulaIgnoreNull(id, src, f), row);
            }
        """




    if (qt=="Cell") :
        print "internal void _Apply( System.Action<VA.ShapeSheet.SRC,VA.ShapeSheet.FormulaLiteral> func)"
    elif (qt=="Section") :
        print "internal void _Apply( System.Action<VA.ShapeSheet.SRC,VA.ShapeSheet.FormulaLiteral> func, short row)"


    print "{"

    for celltype,cellsrc,cellname in data:
        if (qt=="Cell") :
            print"            func(ShapeSheet.SRCConstants.", cellsrc , " , this." , cellname , ".Formula);"
        elif (qt=="Section") :
            print"            func(VA.ShapeSheet.SRCConstants.", cellsrc , ".ForRow(row) , this." , cellname , ".Formula);"

    print "}"    

    print "}"    

    print "----------------------------------"
    printtop()

    print
    print "public class", queryname, ": VA.ShapeSheet.Query." + qt + "Query"
    print "{"
    for celltype,cellsrc,cellname in data:
        print"            public VA.ShapeSheet.Query." + qt + "QueryColumn", cellname , " {get; set;}"

    print
    print "public ",queryname,"() :"
    print "            base(IVisio.VisSectionIndices.",si,")"
    print "{"    
    for celltype,cellsrc,cellname in data:
        if (qt=="Cell"):
            print "    this.", cellname," = this.AddColumn(VA.ShapeSheet.SRCConstants.", cellsrc,", \""+cellname+"\");"
        elif (qt=="Section"):
            print "    this.", cellname," = this.AddColumn(VA.ShapeSheet.SRCConstants.", cellsrc,".Cell, \""+cellname+"\");"
    print "}"    

    print 
    print "}"    

gencode_for_cells(XFORMCELLS, "XFormCells", "XFormQuery","Cell","")
gencode_for_cells(CONTROLCELLS, "ControlCells", "ControlQuery","Section","visSectionControls")
    
    
