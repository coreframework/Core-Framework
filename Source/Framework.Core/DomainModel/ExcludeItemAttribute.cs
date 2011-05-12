// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExcludeItemAttribute.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Framework.Core.DomainModel
{
    /// <summary>
    /// Makes field not available for user.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class ExcludeItemAttribute : Attribute
    {        
    }
}