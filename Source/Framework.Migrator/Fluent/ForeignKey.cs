// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ForeignKey.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Data;

using ECM7.Migrator.Framework;

using Framework.Core.Helpers;

using ForeignKeyConstraint = ECM7.Migrator.Framework.ForeignKeyConstraint;

namespace Framework.Migrator.Fluent
{
    /// <summary>
    /// <para>
    /// Provides fluent interface for foreign key building. Reference is the name of relationship.
    /// Names of primary key table and column, foreign key column is generated by comvention using reference name,
    /// but can be overrided.
    /// </para>
    /// <list type="bullet">
    ///     <listheader>
    ///         <term>By default:</term>
    ///     </listheader>
    ///     <item>
    ///         <term>Primary key table</term>
    ///         <description>Reference name in singular form.</description>
    ///     </item>
    ///     <item>
    ///         <term>Primary key column</term>
    ///         <description>Default primary key name - Id.</description>
    ///     </item>
    ///     <item>
    ///         <term>Foreign key column</term>
    ///         <description>Reference name in singular form + Id.</description>
    ///     </item>
    ///     <item>
    ///         <term>Foreign key name</term>
    ///         <description>FK_ + Foreign Key Table + _ + Reference name in singular form.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public class ForeignKey : IMigrationPart, IColumn
    {
        #region Fields

        private String foreignKeyColumn;

        private String primaryKeyTable;

        private String primaryKeyColumn;

        private String name;

        private bool isRequired = true;

        private bool generateColumn = true;

        private ForeignKeyConstraint deleteConstraint = ForeignKeyConstraint.NoAction;
        
        private ForeignKeyConstraint updateConstraint = ForeignKeyConstraint.NoAction;

        private DbType columnType = Defaults.PrimaryKeyType;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ForeignKey"/> class.
        /// </summary>
        /// <param name="reference">The reference.</param>
        /// <param name="foreignKeyTable">The foreign key table.</param>
        public ForeignKey(String reference, String foreignKeyTable)
        {
            Reference = reference;
            ForeignKeyTable = foreignKeyTable;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of relationship.
        /// </summary>
        /// <value>The name of relationship.</value>
        public virtual String Reference { get; private set; }

        /// <summary>
        /// Gets the name of foreign key table.
        /// </summary>
        /// <value>The name of foreign key table.</value>
        public virtual String ForeignKeyTable { get; private set; }

        /// <summary>
        /// Gets the name of foreign key column.
        /// </summary>
        /// <value>The name of foreign key column.</value>
        public String ForeignKeyColumn
        {
            get
            {
                if (String.IsNullOrEmpty(foreignKeyColumn))
                {
                    foreignKeyColumn = String.Format(Defaults.ForeignKeyColumnTemplate, Reference);
                }
                return foreignKeyColumn;
            }
            private set
            {
                foreignKeyColumn = value;
            }
        }

        /// <summary>
        /// Gets the name of primary key table.
        /// </summary>
        /// <value>The name of primary key table.</value>
        public String PrimaryKeyTable
        {
            get
            {
                if (String.IsNullOrEmpty(primaryKeyTable))
                {
                    primaryKeyTable = Inflector.Pluralize(Reference);
                }
                return primaryKeyTable;
            }
            private set
            {
                primaryKeyTable = value;
            }
        }

        /// <summary>
        /// Gets the name of primary key column.
        /// </summary>
        /// <value>The name of primary key column.</value>
        public String PrimaryKeyColumn
        {
            get
            {
                if (String.IsNullOrEmpty(primaryKeyColumn))
                {
                    primaryKeyColumn = Defaults.PrimaryKeyName;
                }
                return primaryKeyColumn;
            }
            private set
            {
                primaryKeyColumn = value;
            }
        }

        /// <summary>
        /// Gets the name of DeleteConstraint.
        /// </summary>
        /// <value>The name of DeleteConstraint.</value>
        public String Name
        {
            get
            {
                if (String.IsNullOrEmpty(name))
                {
                    name = String.Format(Defaults.ForeignKeyNameTemplate, ForeignKeyTable, Inflector.Pluralize(Reference));
                }
                return name;
            }
            private set
            {
                name = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether foreign key column is required.
        /// </summary>
        /// <value>
        ///     <c>true</c> if foreign key column is required; otherwise, <c>false</c>.
        /// </value>
        public bool IsRequired
        {
            get
            {
                return isRequired;
            }
            private set
            {
                isRequired = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether foreign key column must be generated.
        /// </summary>
        /// <value>
        ///     <c>true</c> if foreign key column must be generated; otherwise, <c>false</c>.
        /// </value>
        public bool GenerateColumn
        {
            get
            {
                return generateColumn;
            }
            private set
            {
                generateColumn = value;
            }
        }

        /// <summary>
        /// Gets the DeleteConstraint behaviour.
        /// </summary>
        /// <value>The DeleteConstraint.</value>
        public ForeignKeyConstraint DeleteConstraint
        {
            get
            {
                return deleteConstraint;
            }
            private set
            {
                deleteConstraint = value;
            }
        }

        /// <summary>
        /// Gets the UpdateConstraint behaviour.
        /// </summary>
        /// <value>The UpdateConstraint.</value>
        public ForeignKeyConstraint UpdateConstraint
        {
            get
            {
                return updateConstraint;
            }
            private set
            {
                updateConstraint = value;
            }
        }

        /// <summary>
        /// Gets the data type of foreign key column.
        /// </summary>
        /// <value>The data type of foreign key column.</value>
        public DbType ColumnType
        {
            get
            {
                return columnType;
            }
            private set
            {
                columnType = value;
            }
        }

        #endregion

        #region Build methods

        /// <summary>
        /// Changes the name of foreign key column.
        /// </summary>
        /// <param name="columnName">Name of foreign key column.</param>
        /// <returns>foreign key instance.</returns>
        public ForeignKey Column(String columnName)
        {
            ForeignKeyColumn = columnName;
            return this;
        }

        /// <summary>
        /// Changes the name of primary key table.
        /// </summary>
        /// <param name="tableName">Name of primary key table.</param>
        /// <returns>foreign key instance.</returns>
        public ForeignKey Table(String tableName)
        {
            PrimaryKeyTable = tableName;
            return this;
        }

        /// <summary>
        /// Changes the name of primary key column.
        /// </summary>
        /// <param name="columnName">Name of primary key column.</param>
        /// <returns>foreign key instance.</returns>
        public ForeignKey PrimaryKey(String columnName)
        {
            PrimaryKeyColumn = columnName;
            return this;
        }

        /// <summary>
        /// Changes the name of DeleteConstraint.
        /// </summary>
        /// <param name="foreignKeyName">Name of DeleteConstraint.</param>
        /// <returns>foreign key instance.</returns>
        public ForeignKey Named(String foreignKeyName)
        {
            Name = foreignKeyName;
            return this;
        }

        /// <summary>
        /// Changes the DeleteConstraint behaviour for delete.
        /// </summary>
        /// <param name="foreignKeyConstraint">DeleteConstraint behaviour.</param>
        /// <returns>foreign key instance.</returns>
        public ForeignKey OnDelete(ForeignKeyConstraint foreignKeyConstraint)
        {
            DeleteConstraint = foreignKeyConstraint;
            return this;
        }

        /// <summary>
        /// Changes the DeleteConstraint behaviour for delete.
        /// </summary>
        /// <param name="foreignKeyConstraint">DeleteConstraint behaviour.</param>
        /// <returns>foreign key instance.</returns>
        public ForeignKey OnUpdate(ForeignKeyConstraint foreignKeyConstraint)
        {
            UpdateConstraint = foreignKeyConstraint;
            return this;
        }

        /// <summary>
        /// Sets the data type of foreign key column.
        /// </summary>
        /// <param name="foreignKeyColumnType">Data type of foreign key column.</param>
        /// <returns>foreign key instance.</returns>
        public ForeignKey Type(DbType foreignKeyColumnType)
        {
            ColumnType = foreignKeyColumnType;
            return this;
        }

        /// <summary>
        /// Makes foreign key column not required.
        /// </summary>
        /// <returns>foreign key instance.</returns>
        public ForeignKey NotRequired()
        {
            IsRequired = false;
            return this;
        }

        /// <summary>
        /// Avoids foreign key column adding or removing.
        /// </summary>
        /// <returns>foreign key instance.</returns>
        public ForeignKey WithoutColumn()
        {
            GenerateColumn = false;
            return this;
        }

        #endregion

        #region IMigrationPart members

        /// <summary>
        /// Implementation of this method is speicific to operation kind (add foreign key and remove foreign key).
        /// </summary>
        /// <param name="database">The database.</param>
        public virtual void Migrate(ITransformationProvider database)
        {
            // Do nothing.
        }

        #endregion

        #region IColumn members

        /// <summary>
        /// Gets the column specification.
        /// </summary>
        /// <returns>Column specification.</returns>
        public ECM7.Migrator.Framework.Column GetColumn()
        {
            return new ECM7.Migrator.Framework.Column(ForeignKeyColumn, columnType, IsRequired ? ColumnProperty.NotNull : ColumnProperty.Null);
        }

        #endregion
    }
}