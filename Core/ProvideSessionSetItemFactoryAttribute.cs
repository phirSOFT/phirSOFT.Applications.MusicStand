using System;
using phirSOFT.Applications.MusicStand.Contract;

namespace phirSOFT.Applications.MusicStand.Core
{
    /// <summary>
    ///     Annotates an <see cref="ISessionSetItemFactory" /> to map this factory to a specific node type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class ProvideSessionSetItemFactoryAttribute : Attribute
    {
        /// <summary>
        ///     Annotates an <see cref="ISessionSetItemFactory" /> to map this factory to a specific node type.
        /// </summary>
        /// <param name="producedType">The type of the deserialized <see cref="ISessionSetItem" /></param>
        /// <param name="xmlNamespace">he xml namespace to used for the serialized data.</param>
        public ProvideSessionSetItemFactoryAttribute(Type producedType, string xmlNamespace)
        {
            ProducedType = producedType;
            XmlNamespace = xmlNamespace;
            ElementTag = producedType.Name;
        }

        /// <summary>
        ///     The type of the deserialized <see cref="ISessionSetItem" />
        /// </summary>
        public Type ProducedType { get; }

        /// <summary>
        ///     The xml namespace to used for the serialized data.
        /// </summary>
        public string XmlNamespace { get; }

        /// <summary>
        ///     Gets or sets the name of the element tag of the seriazed data. Usually this is the name of the
        ///     <see cref="ProducedType" />.
        /// </summary>
        public string ElementTag { get; set; }
    }
}