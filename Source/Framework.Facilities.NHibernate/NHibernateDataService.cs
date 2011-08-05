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
using FluentNHibernate.Data;
using Framework.Core.Localization;
using Framework.Facilities.NHibernate.Filters;
using NHibernate;
using NHibernate.Linq;

using Framework.Core.Services;

namespace Framework.Facilities.NHibernate
{
    /// <summary>
    /// Default NHibernate implementation for <see cref="IDataService{TEntity}"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class NHibernateDataService<TEntity> : IDataService<TEntity>
    {
        #region Fields

        private readonly ISessionManager sessionManager;

        private ILogger logger = NullLogger.Instance;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NHibernateDataService&lt;TEntity&gt;"/> class.
        /// </summary>
        /// <param name="sessionManager">The session manager.</param>
        public NHibernateDataService(ISessionManager sessionManager)
        {
            this.sessionManager = sessionManager;
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
                    session = sessionManager.OpenSession();
                }
                else
                {
                    session = sessionManager.OpenSession(Alias);
                }
                session.EnableFilter("CultureFilter").SetParameter(CultureFilter.FilterParamName, Thread.CurrentThread.CurrentCulture.Name);

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
            ICriteria criteria = Session.CreateCriteria(typeof(TEntity));
            return criteria.List<TEntity>();
        }

        /// <summary>
        /// Retrieves items of <typeparamref name="TEntity"/> from repository paged.
        /// </summary>
        /// <param name="page">Zero-based index of page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>Not nullable collection of <typeparamref name="TEntity"/>.</returns>
        public virtual IEnumerable<TEntity> GetPaged(int page, int pageSize)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(TEntity));
            criteria.SetFirstResult(page * pageSize);
            criteria.SetMaxResults(pageSize);
            return criteria.List<TEntity>();
        }

        /// <summary>
        /// Saves specified entity to repository.
        /// </summary>
        /// <param name="entity">The entity to save.</param>
        /// <returns><c>true</c> if instance instance has been saved successfully; otherwise, <c>false</c>.</returns>
        public virtual bool Save(TEntity entity)
        {
            if (entity is Entity && (entity as Entity).Id > 0)
            {
                Session.Merge(entity);
            }
            else
            {
                if (entity is ILocalizable)
                {
                    ILocalizable localizableEntity = (ILocalizable) entity;
                    if (!localizableEntity.CurrentLocales.Contains(localizableEntity.CurrentLocale))
                    {
                        localizableEntity.CurrentLocales.Add(localizableEntity.CurrentLocale);
                    }
                }

                Session.SaveOrUpdate(entity);
            }

            Session.Flush();
            return true;
        }

        /// <summary>
        /// Removes specified entity from repository.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        /// <returns><c>true</c> if instance instance has been deleted successfully; otherwise, <c>false</c>.</returns>
        public virtual bool Delete(TEntity entity)
        {
            Session.Delete(entity);
            Session.Flush();
            return true;
        }

        /// <summary>
        /// Deletes all entities of type <typeparamref name="TEntity"/> from repository.
        /// </summary>
        public void DeleteAll()
        {
            Session.Delete(String.Format("from {0}", typeof(TEntity).Name));
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