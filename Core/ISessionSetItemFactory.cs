using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using phirSOFT.Applications.MusicStand.Contract;

namespace phirSOFT.Applications.MusicStand.Core
{
    /// <summary>
    ///     Provides a factory that can serialize and deserialize <see cref="ISessionSetItem" />s into an xml representation.
    /// </summary>
    /// <remarks>
    ///     <see cref="ISessionSetItemFactory" />s a re resolved using attributes. When a <see cref="ISessionSetItem" /> should
    ///     be serialized, it is checked, wheter it is attributed with an <see cref="SessionSetItemFactoryAttribute" />
    ///     attribute. If it is present, that factory is requested from the container to serialize the
    ///     <see cref="ISessionSetItem" />. The <see cref="ISessionSetItemFactory" /> must be attributed with at least one
    ///     <see cref="ProvideSessionSetItemFactoryAttribute" /> to signal the node mapping to the container. Theese attributes are used
    ///     to find the correct <see cref="ISessionSetItemFactory" /> to deserialize an xml node.
    /// </remarks>
    public interface ISessionSetItemFactory
    {
        /// <inheritdoc cref="DeserializeAsync(XmlElement, CancellationToken)" />
        Task<ISessionSetItem> DeserializeAsync(XmlElement element);

        /// <inheritdoc
        ///     cref="SerializeAsync(ISessionSetItem,XmlDocument, CancellationToken)" />
        Task<XmlElement> SerializeAsync(ISessionSetItem sessionSetItem, XmlDocument parent);

        /// <summary>
        ///     Deserializes an <paramref name="element" /> into an <see cref="ISessionSetItem" />.
        /// </summary>
        /// <param name="element">The <see cref="XmlElement" /> to deserialize.</param>
        /// <param name="cancellationToken">An optional cancelation token to cancel the deserialization process.</param>
        /// <returns>A task that contains the deserialized <see cref="ISessionSetItem" />.</returns>
        Task<ISessionSetItem> DeserializeAsync(XmlElement element, CancellationToken cancellationToken);

        /// <summary>
        ///     Serialized an <paramref name="sessionSetItem" /> into an <see cref="XmlElement" />.
        /// </summary>
        /// <param name="sessionSetItem">The <see cref="ISessionSetItem" /> to serialize.</param>
        /// <param name="parent">The <see cref="XmlDocument" /> that will hold the serialized <see cref="ISessionSetItem" />.</param>
        /// <param name="cancellationToken">An optional cancelation token to cancel the serialization process.</param>
        /// <returns>A task that contains the serialized <see cref="ISessionSetItem" />.</returns>
        Task<XmlElement> SerializeAsync(ISessionSetItem sessionSetItem, XmlDocument parent,
            CancellationToken cancellationToken);
    }
}