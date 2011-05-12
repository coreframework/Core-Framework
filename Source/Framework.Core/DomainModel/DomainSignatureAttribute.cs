// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DomainSignatureAttribute.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Framework.Core.DomainModel
{
    /// <summary>
    /// Facilitates indicating which property(s) describe the unique signature of an 
    /// entity. See <see cref="GenericEntity{TId}.GetTypeSpecificSignatureProperties" /> for when this is leveraged.
    /// </summary>
    /// <remarks>
    /// This is intended for use with <see cref="Entity" />.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DomainSignatureAttribute : Attribute
    {
    }
}