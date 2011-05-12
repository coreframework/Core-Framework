using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;

namespace Core.Web.Helpers.PagedList
{
    public static class JqGridExtensions
    {
        /// <summary>
        /// Converts a queryable expression into a format suitable for the JqGrid component, when 
        /// serialized to JSON. Use this method when returning data that the JqGrid component will 
        /// fetch via AJAX. Take the result of this method, and then serialize to JSON. This method 
        /// will also apply paging to the data.
        /// </summary>
        /// <typeparam name="T">The type of the element in baseList. Note that this type should be 
        /// an anonymous type or a simple, named type with no possibility of a cycle in the object 
        /// graph. The default JSON serializer will throw an exception if the object graph it is 
        /// serializing contains cycles.</typeparam>
        /// <param name="baseList">The list of records to display in the grid.</param>
        /// <param name="currentPage">A 1-based index indicating which page the grid is about to display.</param>
        /// <param name="rowsPerPage">The maximum number of rows which the grid can display at the moment.</param>
        /// <param name="orderBy">A string expression containing a column name and an optional ASC or 
        /// DESC. You can, separate multiple column names as with SQL.</param>
        /// <param name="searchQuery">The value which the user typed into the search box, if any. Can be 
        /// null/empty.</param>
        /// <param name="searchColumns">An array of strings containing the names of properties in the 
        /// element type of baseList which should be considered when searching the data for searchQuery.</param>
        /// <returns>A structure containing all of the fields required by the JqGrid. You can then call 
        /// a JSON serializer, passing this result.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Jq",
            Justification = "JqGrid is the correct name of the JavaScript component this type is designed to support.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Jq",
            Justification = "JqGrid is the correct name of the grid component we use.")]
        public static JqGridData ToJqGridData<T>(this IQueryable<T> baseList,
            int currentPage,
            int rowsPerPage,
            string orderBy,
            string searchQuery,
            IEnumerable<string> searchColumns)
        {
            return ToJqGridData(baseList, currentPage, rowsPerPage, orderBy, searchQuery, searchColumns, null);
        }

        /// <summary>
        /// Converts a queryable expression into a format suitable for the JqGrid component, when 
        /// serialized to JSON. Use this method when returning data that the JqGrid component will 
        /// fetch via AJAX. Take the result of this method, and then serialize to JSON. This method 
        /// will also apply paging to the data.
        /// </summary>
        /// <typeparam name="T">The type of the element in baseList. Note that this type should be 
        /// an anonymous type or a simple, named type with no possibility of a cycle in the object 
        /// graph. The default JSON serializer will throw an exception if the object graph it is 
        /// serializing contains cycles.</typeparam>
        /// <param name="baseList">The list of records to display in the grid.</param>
        /// <param name="currentPage">A 1-based index indicating which page the grid is about to display.</param>
        /// <param name="rowsPerPage">The maximum number of rows which the grid can display at the moment.</param>
        /// <param name="orderBy">A string expression containing a column name and an optional ASC or 
        /// DESC. You can, separate multiple column names as with SQL.</param>
        /// <param name="searchQuery">The value which the user typed into the search box, if any. Can be 
        /// null/empty.</param>
        /// <param name="searchColumns">An array of strings containing the names of properties in the 
        /// element type of baseList which should be considered when searching the data for searchQuery.</param>
        /// <param name="userData">Arbitrary data to be returned to the grid along with the row data. Leave 
        /// null if not using. Must be serializable to JSON!</param>
        /// <returns>A structure containing all of the fields required by the JqGrid. You can then call 
        /// a JSON serializer, passing this result.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Jq",
            Justification = "JqGrid is the correct name of the JavaScript component this type is designed to support.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Jq",
            Justification = "JqGrid is the correct name of the grid component we use.")]
        public static JqGridData ToJqGridData<T>(this IQueryable<T> baseList,
            int currentPage,
            int rowsPerPage,
            string orderBy,
            string searchQuery,
            IEnumerable<string> searchColumns,
            object userData)
        {
            IQueryable<T> filteredList = ListAddSearchQuery(baseList, searchQuery, searchColumns);
            IEnumerable<T> orderedList = (orderBy != null) ? filteredList.OrderBy(orderBy) : filteredList;
            var pagedModel = new PagedList<T>(orderedList, currentPage - 1, rowsPerPage);
            return pagedModel.ToJqGridData(userData);
        }

        /// <summary>
        /// Converts a paged list into a format suitable for the JqGrid component, when 
        /// serialized to JSON. Use this method when returning data that the JqGrid component will 
        /// fetch via AJAX. Take the result of this method, and then serialize to JSON. This method 
        /// will also apply paging to the data.
        /// </summary>
        /// <typeparam name="T">The type of the element in baseList. Note that this type should be 
        /// an anonymous type or a simple, named type with no possibility of a cycle in the object 
        /// graph. The default JSON serializer will throw an exception if the object graph it is 
        /// serializing contains cycles.</typeparam>
        /// <param name="list">The list of records to display in the grid.</param>
        /// <returns>A structure containing all of the fields required by the JqGrid. You can then call 
        /// a JSON serializer, passing this result.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Jq",
            Justification = "JqGrid is the correct name of the JavaScript component this type is designed to support.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Jq",
            Justification = "JqGrid is the correct name of the grid component we use.")]
        public static JqGridData ToJqGridData<T>(this PagedList<T> list)
        {
            return ToJqGridData(list, null);
        }

        /// <summary>
        /// Converts a paged list into a format suitable for the JqGrid component, when 
        /// serialized to JSON. Use this method when returning data that the JqGrid component will 
        /// fetch via AJAX. Take the result of this method, and then serialize to JSON. This method 
        /// will also apply paging to the data.
        /// </summary>
        /// <typeparam name="T">The type of the element in baseList. Note that this type should be 
        /// an anonymous type or a simple, named type with no possibility of a cycle in the object 
        /// graph. The default JSON serializer will throw an exception if the object graph it is 
        /// serializing contains cycles.</typeparam>
        /// <param name="list">The list of records to display in the grid.</param>
        /// <param name="userData">Arbitrary data to be returned to the grid along with the row data. 
        /// Leave null if not using. Must be serializable to JSON!</param>
        /// <returns>A structure containing all of the fields required by the JqGrid. You can then call 
        /// a JSON serializer, passing this result.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Jq",
            Justification = "JqGrid is the correct name of the JavaScript component this type is designed to support.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Jq",
            Justification = "JqGrid is the correct name of the grid component we use.")]
        public static JqGridData ToJqGridData<T>(this PagedList<T> list, object userData)
        {
            return new JqGridData()
            {
                Page = list.PageIndex + 1,
                Records = list.TotalItemCount,
                Rows = from record in list
                       select record,
                Total = list.PageCount,
                UserData = userData
            };
        }

        /// <summary>
        /// Adds a Where to a Queryable list of entity instances.  In other words, filter the list 
        /// based on the search parameters passed.
        /// </summary>
        /// <typeparam name="T">Entity type contained within the list</typeparam>
        /// <param name="baseList">Unfiltered list</param>
        /// <param name="searchQuery">Whatever the user typed into the search box</param>
        /// <param name="searchColumns">List of entity properties which should be included in the 
        /// search.  If any property in an entity instance begins with the search query, it will 
        /// be included in the result.</param>
        /// <returns>Filtered list.  Note that the query will not actually be executed until the 
        /// IQueryable is enumerated.</returns>
        private static IQueryable<T> ListAddSearchQuery<T>(IQueryable<T> baseList, string searchQuery, IEnumerable<string> searchColumns)
        {
            if ((String.IsNullOrEmpty(searchQuery)) | (searchColumns == null)) return baseList;
            const string strpredicateFormat = "{0}.ToString().StartsWith(@0)";
            var searchExpression = new System.Text.StringBuilder();
            string orPart = String.Empty;
            foreach (string column in searchColumns)
            {
                searchExpression.Append(orPart);
                searchExpression.AppendFormat(strpredicateFormat, column, searchQuery);
                orPart = " OR ";
            }
            var filteredList = baseList.Where(searchExpression.ToString(), searchQuery);
            return filteredList;
        }
    }
}