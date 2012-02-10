// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Column.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Data;
using ECM7.Migrator.Framework;

namespace Framework.Migrator.Fluent
{
    /// <summary>
    /// Provides fluent interface for column building.
    /// </summary>
    public class Column : IMigrationPart, IColumn
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Column"/> class.
        /// </summary>
        /// <param name="columnColumnColumnName">Name of the column.</param>
        /// <param name="tableColumnName">Name of the table.</param>
        public Column(String columnColumnColumnName, String tableColumnName)
        {
            Name = columnColumnColumnName;
            TableName = tableColumnName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Column"/> class.
        /// </summary>
        /// <param name="columnType">Column type.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="tableName">Name of the table.</param>
        public Column(DbType columnType, String columnName, String tableName) : this(columnName, tableName)
        {
            ColumnType = columnType;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the table.
        /// </summary>
        /// <value>The name of the table.</value>
        public String TableName { get; private set; }

        /// <summary>
        /// Gets the name of column.
        /// </summary>
        /// <value>The name of column.</value>
        public String Name { get; private set; }

        /// <summary>
        /// Gets the column type.
        /// </summary>
        /// <value>The column type.</value>
        public DbType ColumnType { get; private set; }

        /// <summary>
        /// Gets the column length.
        /// </summary>
        /// <value>The column length.</value>
        public int? ColumnLength { get; private set; }

        /// <summary>
        /// Gets the column scale.
        /// </summary>
        /// <value>The column scale.</value>
        public int? ColumnScale { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the column is nullable.
        /// </summary>
        /// <value>
        ///     <c>true</c> if column is nullable; otherwise, <c>false</c>.
        /// </value>
        public bool IsNullable { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the column is primary key.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the column is primary key; otherwise, <c>false</c>.
        /// </value>
        public bool IsPrimaryKey { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the column is identity.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the column is identity; otherwise, <c>false</c>.
        /// </value>
        public bool IsIdentity { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the column is unique.
        /// </summary>
        /// <value><c>true</c> if the column is unique; otherwise, <c>false</c>.</value>
        public bool IsUnique { get; private set; }

        /// <summary>
        /// Gets the column default value.
        /// </summary>
        /// <value>The default value.</value>
        public object DefaultValue { get; private set; }

        /// <summary>
        /// Gets the column properties.
        /// </summary>
        /// <value>The column properties.</value>
        public ColumnProperty ColumnProperties { get; private set; }

        #endregion

        #region Build methods

        /// <summary>
        /// Adds column property.
        /// </summary>
        /// <param name="property">The column property.</param>
        /// <returns>column instance.</returns>
        public Column AddProperty(ColumnProperty property)
        {
            ColumnProperties |= property;
            return this;
        }

        /// <summary>
        /// Removes column property.
        /// </summary>
        /// <param name="property">The column property.</param>
        /// <returns>column instance.</returns>
        public Column RemoveProperty(ColumnProperty property)
        {
            ColumnProperties &= ~property;
            return this;
        }

        /// <summary>
        /// Makes the column nullable.
        /// </summary>
        /// <returns>column instance.</returns>
        public Column Null()
        {
            IsNullable = true;
            return this;
        }

        /// <summary>
        /// Makes the column primary key.
        /// </summary>
        /// <returns>column instance.</returns>
        public Column PrimaryKey()
        {
            IsPrimaryKey = true;
            IsNullable = false;
            return this;
        }

        /// <summary>
        /// Makes the column identity.
        /// </summary>
        /// <returns>column instance.</returns>
        public Column Identity()
        {
            IsIdentity = true;
            return this;
        }

        /// <summary>
        /// Makes the column unique.
        /// </summary>
        /// <returns>column instance.</returns>
        public Column Unique()
        {
            IsUnique = true;
            return this;
        }

        /// <summary>
        /// Sets the length for column.
        /// </summary>
        /// <param name="columnLength">Length of the column.</param>
        /// <returns>column instance.</returns>
        public Column Length(int columnLength)
        {
            ColumnLength = columnLength;
            return this;
        }

        /// <summary>
        /// Sets the scale for column.
        /// </summary>
        /// <param name="columnScale">Scale of the column.</param>
        /// <returns>column instance.</returns>
        public Column Scale(int columnScale)
        {
            ColumnScale = columnScale;
            return this;
        }

        /// <summary>
        /// Sets the column default value.
        /// </summary>
        /// <param name="columnDefaultValue">The column default value.</param>
        /// <returns>column instance.</returns>
        public Column Default(Object columnDefaultValue)
        {
            DefaultValue = columnDefaultValue;
            return this;
        }

        /// <summary>
        /// Sets the column type.
        /// </summary>
        /// <param name="columnType">Type of the column.</param>
        /// <returns>column instance.</returns>
        public Column Type(DbType columnType)
        {
            ColumnType = columnType;
            return this;
        }

        /// <summary>
        /// Sets column type to Integer (32 bit integer number).
        /// </summary>
        /// <returns>column instance.</returns>
        public Column Integer()
        {
            return Type(DbType.Int32);
        }

        /// <summary>
        /// Sets column type to Long (64 bit integer number).
        /// </summary>
        /// <returns>column instance.</returns>
        public Column Long()
        {
            return Type(DbType.Int64);
        }

        /// <summary>
        /// Sets column type to String.
        /// </summary>
        /// <returns>column instance.</returns>
        public Column String()
        {
            return Type(DbType.String);
        }

        /// <summary>
        /// Sets column type to Text.
        /// </summary>
        /// <returns>column instance.</returns>
        public Column Text()
        {
            return Type(DbType.String).Length(Defaults.MaxStringLength + 1);
        }

        /// <summary>
        /// Sets column type to DateTime.
        /// </summary>
        /// <returns>column instance.</returns>
        public Column DateTime()
        {
            return Type(DbType.DateTime);
        }

        /// <summary>
        /// Sets column type to Bool.
        /// </summary>
        /// <returns>column instance.</returns>
        public Column Bool()
        {
            return Type(DbType.Boolean);
        }

        /// <summary>
        /// Sets column type to Double.
        /// </summary>
        /// <returns>column instance.</returns>
        public Column Double()
        {
            return Type(DbType.Double);
        }

        /// <summary>
        /// Sets column type to Decimal.
        /// </summary>
        /// <returns>column instance.</returns>
        public Column Decimal()
        {
            return Type(DbType.Decimal);
        }

        #endregion

        #region IMigrationPart members

        /// <summary>
        /// Implementation of this method is speicific to operation kind (add column, change column, remove column).
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
            if (IsNullable)
            {
                AddProperty(ColumnProperty.Null);
                RemoveProperty(ColumnProperty.NotNull);
            }
            else
            {
                AddProperty(ColumnProperty.NotNull);
                RemoveProperty(ColumnProperty.Null);
            }

            AddProperty(ColumnProperty.PrimaryKey, IsPrimaryKey);
            AddProperty(ColumnProperty.Identity, IsIdentity);
            AddProperty(ColumnProperty.Unique, IsUnique);

            return new ECM7.Migrator.Framework.Column(Name, GetColumnType(), ColumnProperties, DefaultValue);
        }

        #endregion

        #region Helper members

        private void AddProperty(ColumnProperty property, bool value)
        {
            if (value)
            {
                AddProperty(property);
            }
        }

        private ColumnType GetColumnType()
        {
            var columnType = new ColumnType(ColumnType);
            columnType.Length = ColumnLength ?? Defaults.ColumnSize;
            if (ColumnScale != null)
            {
                columnType.Scale = ColumnScale;
            }
            return columnType;
        }

        #endregion        
    }
}