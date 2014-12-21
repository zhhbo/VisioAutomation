using IVisio = Microsoft.Office.Interop.Visio;
using MOC = Microsoft.Office.Core;
using VisioAutomation.Extensions;
using VA = VisioAutomation;
using System.Linq;

namespace VisioPowerTools
{
    public partial class VisioPowerToolsAddIn
    {
        private void CmdThrowException(MOC.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            var application = this.Application;
            var documents = application.Documents;
            var doc = documents[0];
            string n = doc.Name;
        }

        private void CmdShowFillTool(MOC.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            var form = new FormFillDesigner();
            this.add_anchor_window(form);
        }

        private void CmdShowColorTool(MOC.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            var form = new FormColorTool();
            this.add_anchor_window(form);
        }

        private void CmdShowArrangeTool(MOC.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            var form = new FormArrangeTool();
            this.add_anchor_window(form);
        }

        private void CmdShowStackingTool(MOC.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            var form = new FormStackingTool();
            this.add_anchor_window(form);
        }

        private void CmdShowLayoutTool(MOC.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            var form = new FormLayoutTool();
            this.add_anchor_window(form);
        }

        private void CmdShowSelectionTool(MOC.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            var form = new FormSelectionTool();
            this.add_anchor_window(form);
        }

        private void CmdShowViewTool(MOC.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            var form = new FormViewTool();
            this.add_anchor_window(form);
        }
        private void CmdCloseDocumentsAndAppWithoutSaving()
        {
            this.CmdCloseDocumentsWithoutSaving();
            var ss = VisioPowerToolsAddIn.Client;
            var app = ss.VisioApplication;

            app.Quit(true);

        }


        private void CmdCloseDocumentsWithoutSaving()
        {
            var ss = VisioPowerToolsAddIn.Client;
            var app = ss.VisioApplication;
            var docs = app.Documents;
            if (docs.Count<1)
            {
                return;
            }

            var unsaved_docs = docs.AsEnumerable()
                .Where(doc => doc.Saved == false).ToList();


            if (unsaved_docs.Count>0)
            {
                // there are some unsaved docs - we ask if it is ok to force them to go away without saving
                var result = System.Windows.Forms.MessageBox.Show(
                    "Close all documents without saving?",
                    "Confirm close",
                    System.Windows.Forms.MessageBoxButtons.YesNo,
                    System.Windows.Forms.MessageBoxIcon.Question,
                    System.Windows.Forms.MessageBoxDefaultButton.Button2);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    ss.Document.CloseAllWithoutSaving();
                }
            }
            else
            {
                // all the docs are saved - go ahead and close everything
                ss.Document.CloseAllWithoutSaving();
            }

        }

        public static void CmdResizeFitToContents()
        {
            var ss = VisioPowerToolsAddIn.Client;
            ss.Page.ResizeToFitContents(new VA.Drawing.Size(0, 0), true);
        }

        public static void CmdAlignLeft()
        {
            var ss = VisioPowerToolsAddIn.Client;
            ss.Arrange.Align(null,VA.Drawing.AlignmentHorizontal.Left);
        }

        public static void CmdAlignCenter()
        {
            var ss = VisioPowerToolsAddIn.Client;
            ss.Arrange.Align(null,VA.Drawing.AlignmentHorizontal.Center);
        }

        public static void CmdAlignRight()
        {
            var ss = VisioPowerToolsAddIn.Client;
            ss.Arrange.Align(null,VA.Drawing.AlignmentHorizontal.Right);
        }

        public static void CmdAlignTop()
        {
            var ss = VisioPowerToolsAddIn.Client;
            ss.Arrange.Align(null,VA.Drawing.AlignmentVertical.Top);
        }

        public static void CmdAlignMiddle()
        {
            var ss = VisioPowerToolsAddIn.Client;
            ss.Arrange.Align(null,VA.Drawing.AlignmentVertical.Center);
        }

        public static void CmdAlignBottom()
        {
            var ss = VisioPowerToolsAddIn.Client;
            ss.Arrange.Align(null,VA.Drawing.AlignmentVertical.Bottom);
        }

        public static void CmdSnapPosition()
        {
            var addin = Globals.VisioPowerToolsAddIn;
            double d = addin.addinprefs.SnapUnit;
            var ss = VisioPowerToolsAddIn.Client;
            // TODO: fix
            //ss.Arrange.SnapCorner(d, d, VA.Layout.SnapCornerPosition.LowerLeft);
        }

        public static void CmdSnapSize()
        {
            var addin = Globals.VisioPowerToolsAddIn;
            double d = addin.addinprefs.SnapUnit;
            var ss = VisioPowerToolsAddIn.Client;
            ss.Arrange.SnapSize(null,d, d);
        }

        public static void CmdCopySize()
        {
            var ss = VisioPowerToolsAddIn.Client;
            ss.Format.CopySize();
        }

        public static void CmdPasteSize()
        {
            var ss = VisioPowerToolsAddIn.Client;
            ss.Format.PasteSize(null,true,true);
        }

        public static void CmdPasteWidth()
        {
            var ss = VisioPowerToolsAddIn.Client;
            ss.Format.PasteSize(null, true, false);
        }

        public static void CmdPasteHeight()
        {
            var ss = VisioPowerToolsAddIn.Client;
            ss.Format.PasteSize(null, false, true);
        }

        public static void CmdSwitchCase()
        {
            var ss = VisioPowerToolsAddIn.Client;
            ss.Text.ToogleCase(null);
        }

        public static void CmdSelectAll()
        {
            var ss = VisioPowerToolsAddIn.Client;
            ss.Selection.All();
        }

        public static void CmdSelectNone()
        {
            var ss = VisioPowerToolsAddIn.Client;
            ss.Selection.None();
        }

        public static void CmdInvertSelection()
        {
            var ss = VisioPowerToolsAddIn.Client;
            ss.Selection.Invert();
        }

        public static void CmdDistribute(IVisio.VisDistributeTypes s, bool gtg)
        {
            var addin = Globals.VisioPowerToolsAddIn;
            var ss = VisioPowerToolsAddIn.Client;
            if (!ss.Selection.HasShapes())
            {
                return;
            }

            var application = addin.Application;
            var active_window = application.ActiveWindow;
            var selection = active_window.Selection;
            selection.Distribute(s, gtg);
        }

        public static void CmdDistributeHorizontalSpacing()
        {
            CmdDistribute(IVisio.VisDistributeTypes.visDistHorzSpace, false);
        }

        public static void CmdDistributeHorizontalCenter()
        {
            CmdDistribute(IVisio.VisDistributeTypes.visDistHorzCenter, false);
        }

        public static void CmdDistributeVerticalSpacing()
        {
            CmdDistribute(IVisio.VisDistributeTypes.visDistVertSpace, false);
        }

        public static void CmdDistributeVerticalMiddle()
        {
            CmdDistribute(IVisio.VisDistributeTypes.visDistVertMiddle, false);
        }


        public static void CmdZoomOnSelection()
        {
            var ss = VisioPowerToolsAddIn.Client;
            ss.View.Zoom(VA.Scripting.Zoom.ToSelection);
        }

        public static void CmdPageResizeToFit()
        {
            var ss = VisioPowerToolsAddIn.Client;
            ss.Page.ResizeToFitContents(new VA.Drawing.Size(0, 0), true);
        }

        public static void CmdPageDuplicate()
        {
            var ss = VisioPowerToolsAddIn.Client;
            ss.Page.Duplicate();
        }

        public static void CmdPageResetOrigin()
        {
            var ss = VisioPowerToolsAddIn.Client;
            ss.Page.ResetOrigin(ss.Page.Get());
        }

        public static void CmdExportAsSVGXHTML()
        {
            var ss = Client;
            if (!ss.HasActiveDocument)
            {
                System.Windows.Forms.MessageBox.Show("Open or create a new Drawing to export it.");
                return;
            }

            var form = new FormExportSelectionAsFormat(FormExportSelectionAsFormat.enumExportFormat.ExportSVGXHTML);
            form.ShowDialog();
        }

        public static void CmdExportAsXAML()
        {
            var ss = Client;
            if (!ss.HasActiveDocument)
            {
                System.Windows.Forms.MessageBox.Show("Open or create a new Drawing to export it.");
                return;
            }

            var form = new FormExportSelectionAsFormat(FormExportSelectionAsFormat.enumExportFormat.ExportXAML);
            form.ShowDialog();
        }

        public static void CmdPageImportFlowChartXML()
        {
            var ss = Client;
            var form = new FormImportFlowChartXML();
            form.ShowDialog();
        }

        private void CmdShowTextTool(MOC.CommandBarButton ctrl, ref bool cancel_default)
        {
            var form = new FormTextTool();
            this.add_anchor_window(form);
        }

        private void CmdShowFormatPainterTool(MOC.CommandBarButton ctrl, ref bool cancel_default)
        {
            var form = new FormFormatPainter();
            this.add_anchor_window(form);
        }

        private const double one_inch = 1.0;
        private const double half_inch = 1.0/2.0;
        private const double quarter_inch = 1.0/4.0;
        private const double eighth_inch = 1.0/8.0;
        private const double sixteenth_inch = 1.0/16.0;

        public static void CmdShapeSnapPositionOneInch()
        {
            // TODO: fix

            var ss = VisioPowerToolsAddIn.Client;
            //ss.Layout.SnapCorner(one_inch, one_inch, VA.Layout.SnapCornerPosition.LowerLeft);
        }

        public static void CmdShapeSnapPositionHalfInch()
        {
            // TODO: fix

            var ss = VisioPowerToolsAddIn.Client;
            //ss.Layout.SnapCorner(half_inch, half_inch, VA.Layout.SnapCornerPosition.LowerLeft);
        }

        public static void CmdShapeSnapPositionQuarterInch()
        {
            // TODO: fix

            var ss = VisioPowerToolsAddIn.Client;
            //ss.Layout.SnapCorner(quarter_inch, quarter_inch, VA.Layout.SnapCornerPosition.LowerLeft);
        }

        public static void CmdShapeSnapPositionEighthInch()
        {
            // TODO: fix

            var ss = VisioPowerToolsAddIn.Client;
            //ss.Layout.SnapCorner(eighth_inch, eighth_inch, VA.Layout.SnapCornerPosition.LowerLeft);
        }

        public static void CmdShapeSnapPositionSixteenthInch()
        {
            // TODO: fix

            var ss = VisioPowerToolsAddIn.Client;
            //ss.Layout.SnapCorner(1.0 / 16.0, 1.0 / 16.0, VA.Layout.SnapCornerPosition.LowerLeft);
        }

        public static void CmdShapeSnapSizeOneInch()
        {
            var ss = VisioPowerToolsAddIn.Client;
            ss.Arrange.SnapSize(null,one_inch, one_inch);
        }

        public static void CmdShapeSnapSizeHalfInch()
        {
            var ss = VisioPowerToolsAddIn.Client;
            ss.Arrange.SnapSize(null,half_inch, half_inch);
        }

        public static void CmdShapeSnapSizeQuarterInch()
        {
            var ss = VisioPowerToolsAddIn.Client;
            ss.Arrange.SnapSize(null,quarter_inch, quarter_inch);
        }

        public static void CmdShapeSnapSizeEighthInch()
        {
            var ss = VisioPowerToolsAddIn.Client;
            ss.Arrange.SnapSize(null,eighth_inch, eighth_inch);
        }

        public static void CmdShapeSnapSizeSixteenthInch()
        {
            var ss = VisioPowerToolsAddIn.Client;
            ss.Arrange.SnapSize(null,sixteenth_inch, sixteenth_inch);
        }
    }
}