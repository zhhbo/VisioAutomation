using VisioAutomation.ShapeSheet;
using VisioAutomation.ShapeSheet.CellGroups;
using VisioAutomation.ShapeSheet.Query;

namespace VisioAutomation.Text
{
    class TextBlockCellsReader : SingleRowReader<Text.TextBlockCells>
    {
        public CellColumn BottomMargin { get; set; }
        public CellColumn LeftMargin { get; set; }
        public CellColumn RightMargin { get; set; }
        public CellColumn TopMargin { get; set; }
        public CellColumn DefaultTabStop { get; set; }
        public CellColumn Background { get; set; }
        public CellColumn BackgroundTransparency { get; set; }
        public CellColumn Direction { get; set; }
        public CellColumn VerticalAlign { get; set; }

        public TextBlockCellsReader()
        {
            this.BottomMargin = this.query.AddCell(SrcConstants.TextBlockBottomMargin, nameof(SrcConstants.TextBlockBottomMargin));
            this.LeftMargin = this.query.AddCell(SrcConstants.TextBlockLeftMargin, nameof(SrcConstants.TextBlockLeftMargin));
            this.RightMargin = this.query.AddCell(SrcConstants.TextBlockRightMargin, nameof(SrcConstants.TextBlockRightMargin));
            this.TopMargin = this.query.AddCell(SrcConstants.TextBlockTopMargin, nameof(SrcConstants.TextBlockTopMargin));
            this.DefaultTabStop = this.query.AddCell(SrcConstants.TextBlockDefaultTabStop, nameof(SrcConstants.TextBlockDefaultTabStop));
            this.Background = this.query.AddCell(SrcConstants.TextBlockBackground, nameof(SrcConstants.TextBlockBackground));
            this.BackgroundTransparency = this.query.AddCell(SrcConstants.TextBlockBackgroundTransparency, nameof(SrcConstants.TextBlockBackgroundTransparency));
            this.Direction = this.query.AddCell(SrcConstants.TextBlockDirection, nameof(SrcConstants.TextBlockDirection));
            this.VerticalAlign = this.query.AddCell(SrcConstants.TextBlockVerticalAlign, nameof(SrcConstants.TextBlockVerticalAlign));

        }

        public override Text.TextBlockCells CellDataToCellGroup(VisioAutomation.Utilities.ArraySegment<ShapeSheet.CellData> row)
        {
            var cells = new Text.TextBlockCells();
            cells.BottomMargin = row[this.BottomMargin];
            cells.LeftMargin = row[this.LeftMargin];
            cells.RightMargin = row[this.RightMargin];
            cells.TopMargin = row[this.TopMargin];
            cells.DefaultTabStop = row[this.DefaultTabStop];
            cells.TextBackground = row[this.Background];
            cells.TextBackgroundTransparency = row[this.BackgroundTransparency];
            cells.TextDirection = row[this.Direction];
            cells.VerticalAlign = row[this.VerticalAlign];
            return cells;
        }
    }
}