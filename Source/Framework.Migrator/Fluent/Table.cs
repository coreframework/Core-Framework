// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Table.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;

using ECM7.Migrator.Framework;

namespace Framework.Migrator.Fluent
{
    /// <summary>
    /// Provides fluent interface for table manipulation.
    /// </summary>
    public class Table : IMigrationPart
    {
        #region Fields

        private readonly List<IMigrationPart> parts = new List<IMigrationPart>();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Table"/> class.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        public Table(String tableName)
        {
            Name = tableName;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of table.
        /// </summary>
        /// <value>The name of table.</value>
        protected String Name { get; private set; }

        /// <summary>
        /// Gets the parts of table definition or modification.
        /// </summary>
        /// <value>The parts of table definition or modification.</value>
        protected List<IMigrationPart> Parts
        {
            get
            {
                return parts;
            }
        }

        #endregion

        #region Columns

        /// <summary>
        /// Adds the column with name specified.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>table instance.</returns>
        public virtual Column AddColumn(String columnName)
        {
            return new AddColumn(columnName, Name);
        }

        /// <summary>
        /// Adds the column with name and type specified.
        /// </summary>
        /// <param name="type">Type of the column.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>table instance.</returns>
        public virtual Column AddColumn(DbType type, String columnName)
        {
            return AddColumn(columnName).Type(type);
        }

        /// <summary>
        /// Adds Integer column with name specified.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>table instance.</returns>
        public Column Integer(String columnName)
        {
            return AddColumn(columnName).Integer();
        }

        /// <summary>
        /// Adds Long column with name specified.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>table instance.</returns>
        public Column Long(String columnName)
        {
            return AddColumn(columnName).Long();
        }

        /// <summary>
        /// Adds String column with name specified.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>table instance.</returns>
        public Column String(String columnName)
        {
            return AddColumn(columnName).String();
        }

        /// <summary>
        /// Adds Text column with name specified.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>table instance.</returns>
        public Column Text(String columnName)
        {
            return AddColumn(columnName).Text();
        }

        /// <summary>
        /// Adds DateTime column with name specified.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>table instance.</returns>
        public Column DateTime(String columnName)
        {
            return AddColumn(columnName).DateTime();
        }

        /// <summary>
        /// Adds Bool column with name specified.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>table instance.</returns>
        public Column Bool(String columnName)
        {
            return AddColumn(columnName).Bool();
        }

        /// <summary>
        /// Adds Double column with name specified.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>table instance.</returns>
        public Column Double(String columnName)
        {
            return AddColumn(columnName).Double();
        }

        /// <summary>
        /// Adds Decimal column with name specified.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>table instance.</returns>
        public Column Decimal(String columnName)
        {
            return AddColumn(columnName).Decimal();
        }

        #endregion

        #region Primary Key

        /// <summary>
        /// Add column with name and type specified and makes it primary key.
        /// </summary>
        /// <param name="columnType">Type of the column.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>table instance.</returns>
        public virtual Column PrimaryKey(DbType columnType, String columnName)
        {
            return AddColumn(columnType, columnName).PrimaryKey();
        }

        /// <summary>
        /// Add column with name specified and default primary key type (Long) and makes it primary key.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>table instance.</returns>
        public Column PrimaryKey(String columnName)
        {
            return PrimaryKey(Defaults.PrimaryKeyType, columnName).Identity();
        }

        /// <summary>
        /// Add column with default primary key name (Id) and default primary key type (Long) and makes it primary key.
        /// </summary>
        /// <returns>table instance.</returns>
        public Column PrimaryKey()
        {
            return PrimaryKey(Defaults.PrimaryKeyName);
        }

        #endregion

        #region Foreign Key

        /// <summary>
        /// Adds foreigns key.
        /// </summary>
        /// <param name="reference">The relationship name.</param>
        /// <returns>table instance.</returns>
        public virtual ForeignKey ForeignKey(String reference)
        {
            var foreignKey = new AddForeignKey(reference, Name);
            Parts.Add(foreignKey);
            return foreignKey;
        }

        #endregion

        #region IMigrationPart members

        /// <summary>
        /// Table is composite object. Migrate method executes all inner migration parts (column, keys, constraint manipulation).
        /// </summary>
        /// <param name="database">The database.</param>
        public virtual void Migrate(ITransformationProvider database)
        {
            Parts.ForEach(part => part.Migrate(database));
        }

        #endregion
    }
}