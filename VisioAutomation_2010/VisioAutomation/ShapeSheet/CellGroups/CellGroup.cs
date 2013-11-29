using IVisio = Microsoft.Office.Interop.Visio;
using VA = VisioAutomation;
using System.Collections.Generic;

namespace VisioAutomation.ShapeSheet.CellGroups
{
    public abstract class CellGroup : BaseCellGroup
    {
        private static void check_query(VA.ShapeSheet.Query.CellQuery query)
        {
            if (query.Columns.Count < 1)
            {
                throw new VA.AutomationException("Query must contain at least 1 Column");
            }

            if (query.Sections.Count != 0)
            {
                throw new VA.AutomationException("Query should not contain contain any sections");
            }
        }

        protected static IList<T> _GetCells<T,X>(
            IVisio.Page page, IList<int> shapeids, 
            VA.ShapeSheet.Query.CellQuery query, 
            QueryResultToObject<T,X> f)
        {
            check_query(query);

            var data_for_shapes = query.GetFormulasAndResults<X>(page, shapeids);
            var list = new List<T>(shapeids.Count);
            foreach (var data_for_shape in data_for_shapes)
            {
                var cells = f(data_for_shape);
                list.Add(cells);
            }
            return list;
        }

        protected static T _GetCells<T,X>(
            IVisio.Shape shape, 
            VA.ShapeSheet.Query.CellQuery query, 
            QueryResultToObject<T,X> f)
        {
            check_query(query);

            var data_for_shape = query.GetFormulasAndResults<X>(shape);
            var cells = f(data_for_shape);
            return cells;
        }
    }
}