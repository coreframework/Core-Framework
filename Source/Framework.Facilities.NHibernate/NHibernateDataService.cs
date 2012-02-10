// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NHibernateDataService.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Castle.Core.Logging;
using Castle.Facilities.NHibernateIntegration;
using Castle.Services.Transaction;
using Framework.Core.Localization;
using Framework.Core.Services;
using Framework.Facilities.NHibernate.Filters;
using Framework.Facilities.NHibernate.Helpers;
using NHibernate;
using NHibernate.Criterion;

namespace Framework.Facilities.NHibernate
{
    /// <summary>
    /// Default NHibernate implementation for <see cref="IDataService{TEntity}"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    [Transactional]
    public class NHibernateDataService<TEntity> : IDataService<TEntity> where TEntity : class
    {
        #region Fields

        /// <summary>
        /// Session manager.
        /// </summary>
        protected readonly ISessionManager SessionManager;

        private ILogger logger = NullLogger.Instance;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NHibernateDataService&lt;TEntity&gt;"/> class.
        /// </summary>
        /// <param name="sessionManager">The session manager.</param>
        public NHibernateDataService(ISessionManager sessionManager)
        {
            SessionManager = sessionManager;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the alias.
        /// </summary>
        /// <value>The alias.</value>
        public virtual String Alias { get; set; }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public ILogger Logger
        {
            get
            {
                return logger;
            }
            set
            {
                logger = value;
            }
        }

        /// <summary>
        /// Gets the nhibernate session associated with current service.
        /// </summary>
        /// <value>The nhibernate session.</value>
        protected virtual ISession Session
        {
            get
            {
                ISession session;

                if (String.IsNullOrEmpty(Alias))
                {
                    session = SessionManager.OpenSession();
                }
                else
                {
                    session = SessionManager.OpenSession(Alias);
                }
                session.EnableFilter("CultureFilter").SetParameter(CultureFilter.FilterParamName, Thread.CurrentThread.CurrentCulture.Name)
                    .SetParameter(CultureFilter.DefaultCultureFilterParamName, CultureHelper.DefaultCultureName);

                return session;
            }
        }

        #endregion

        #region IDataService members

        /// <summary>
        /// Finds entity by id specified.
        /// </summary>
        /// <param name="id">Entity identifier.</param>
        /// <returns>Entity or null if a record is not found matching the provided Id.</returns>
        public virtual TEntity Find(long id)
        {
            var entity = Session.Get<TEntity>(id);

            return entity;
        }

        /// <summary>
        /// Retrieves all items of <typeparamref name="TEntity"/> from repository.
        /// </summary>
        /// <returns>Not nullable collection of <typeparamref name="TEntity"/>.</returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            var query = CreateQuery();

            return query.AsEnumerable();
        }

        /// <summary>
        /// Retrieves items of <typeparamref name="TEntity"/> from repository paged.
        /// </summary>
        /// <param name="page">Zero-based index of page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>Not nullable collection of <typeparamref name="TEntity"/>.</returns>
        public virtual IEnumerable<TEntity> GetPaged(int page, int pageSize)
        {
            var query = CreateQuery().Skip(page * pageSize).Take(pageSize);

            return query.AsEnumerable();
        }

        /// <summary>
        /// Gets the count of entities.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The entity count.</returns>
        public virtual long Count(ICriteria criteria)
        {
            var countCriteria = (ICriteria)criteria.Clone();
            countCriteria.SetProjection(Projections.RowCount());
            long count;
            Int64.TryParse(countCriteria.List()[0].ToString(), out count);

            return count;
        }

        /// <summary>
        /// Saves specified entity to repository.
        /// </summary>
        /// <param name="entity">The entity to save.</param>
        /// <returns><c>true</c> if instance instance has been saved successfully; otherwise, <c>false</c>.</returns>
        [Transaction]
        public virtual bool Save(TEntity entity)
        {
            if (entity is ILocalizable)
            {
                var localizableEntity = (ILocalizable)entity;
                if (!localizableEntity.ContainsLocale(localizableEntity.CurrentLocale))
                {
                    localizableEntity.AddLocale(localizableEntity.CurrentLocale);
                }
            }

            Session.SaveOrUpdate(entity);
            Session.Flush();
            return true;
        }

        /// <summary>
        /// Removes specified entity from repository.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        /// <returns><c>true</c> if instance instance has been deleted successfully; otherwise, <c>false</c>.</returns>
        [Transaction]
        public virtual bool Delete(TEntity entity)
        {
            Session.Delete(entity);
            Session.Flush();

            return true;
        }

        /// <summary>
        /// Deletes all entities of type <typeparamref name="TEntity"/> from repository.
        /// </summary>
        [Transaction]
        public void DeleteAll()
        {
            Session.Delete(String.Format("from {0}", typeof(TEntity).FullName));
            Session.Flush();
        }

        /// <summary>
        /// Creates the LINQ query used to evaluate an expression tree.
        /// </summary>
        /// <returns>Query object used to evaluate an expression tree.</returns>
        public virtual IQueryable<TEntity> CreateQuery()
        {
            return Session.Linq<TEntity>();
        }

        #endregion
    }
}