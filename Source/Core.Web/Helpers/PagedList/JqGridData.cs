using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Core.Web.Helpers.PagedList
{
    /// <summary>
    /// This type is designed to conform to the structure required by the JqGrid JavaScript component. 
    /// It has all of the properties required by the grid. When this type is serialized to JSON, the resulting 
    /// JSON will be in the structure expected by the grid when it fetches pages of data via AJAX calls.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Jq",
        Justification = "JqGrid is the correct name of the JavaScript component this type is designed to support.")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Jq",
        Justification = "JqGrid is the correct name of the JavaScript component this type is designed to support.")]
    public class JqGridData
    {
        /// <summary>
        /// The number of pages which should be displayed in the paging controls at the bottom of the grid.
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// The current page number which should be highlighted in the paging controls at the bottom of the grid.
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// The total number of records in the entire data set, not just the portion returned in Rows.
        /// </summary>
        public int Records { get; set; }
        /// <summary>
        /// See the JqGrid documentation for repeatitems. This property controls how the grid interprets the Rows 
        /// property. When set false, Rows is presumed to contain a list of objects where the property name is 
        /// the grid column name in the property value is the value which should be displayed in the grid. When 
        /// set true, the ID and the non-ID values would be stored separately.
        /// </summary>
        public static bool RepeatItems { get { return false; } }
        /// <summary>
        /// The data that will actually be displayed in the grid.
        /// </summary>
        public IEnumerable Rows { get; set; }
        /// <summary>
        /// Arbitrary data to be returned to the grid along with the row data. Leave null if not using. Must be serializable to JSON!
        /// </summary>
        public object UserData { get; set; }
    }
}