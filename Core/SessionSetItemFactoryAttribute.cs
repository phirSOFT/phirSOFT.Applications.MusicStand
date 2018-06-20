using System;
using phirSOFT.Applications.MusicStand.Contract;

namespace phirSOFT.Applications.MusicStand.Core
{
    /// <summary>
    ///     Specifies the <see cref="ISessionSetItemFactory" /> to be used to serialize the annotated
    ///     <see cref="ISessionSetItem" />.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class SessionSetItemFactoryAttribute : Attribute
    {
        /// <summary>
        ///     Specifies the <see cref="ISessionSetItemFactory" /> to be used to serialize the annotated
        ///     <see cref="ISessionSetItem" />.
        /// </summary>
        /// <param name="factoryType">The type of the factory to use.</param>
        public SessionSetItemFactoryAttribute(Type factoryType)
        {
            FactoryType = factoryType;
        }

        /// <summary>
        ///     Gets the type of the factory to use.
        /// </summary>
        public Type FactoryType { get; }
    }
}