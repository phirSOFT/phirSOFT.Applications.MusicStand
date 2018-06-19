using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace phirSOFT.Applications.MusicStand.Contract
{
    /// <summary>
    ///     Provides the interface to operate on <see cref="ISessionSet" />
    /// </summary>
    public interface ISessionSetProvider
    {
        /// <summary>
        /// Tries to open the <see cref="ISessionSet"/> at <paramref name="path"/>.
        /// If <paramref name="path"/> is <see langword="null"/> a new <see cref="ISessionSet"/> is returned.
        /// </summary>
        /// <param name="path">The uri of the <see cref="ISessionSet"/> to open.</param>
        /// <param name="cancellationToken">The cancelation Token to cancel the operation.</param>
        /// <returns>A task containing the opened <see cref="ISessionSet"/></returns>
        Task<ISessionSet> OpenOrCreateAsync(Uri path, CancellationToken cancellationToken);

        /// <inheritdoc cref="OpenOrCreateAsync(Uri,CancellationToken)"/>
        Task<ISessionSet> OpenOrCreateAsync(Uri path);

        /// <inheritdoc cref="StoreAsync(ISessionSet, CancellationToken)"/>
        Task StoreAsync(ISessionSet sessionSet);

        /// <summary>
        ///     Stores a session set to disk. 
        /// </summary>
        /// <param name="sessionSet">The <see cref="ISessionSet"/> to store.</param>
        /// <param name="cancellationToken">The cancelation Token to cancel the opereraion</param>
        /// <returns>A task that completes when the <see cref="ISessionSet"/> was stored.</returns>
        /// <remarks>If the <see cref="ISessionSet"/> was created using <see langword="null"/> as path. The application propmts for a path.</remarks>
        Task StoreAsync(ISessionSet sessionSet, CancellationToken cancellationToken);

        /// <inheritdoc cref="StoreAsAsync(ISessionSet, Uri, CancellationToken)"/>
        Task<ISessionSet> StoreAsAsync(ISessionSet songSet, Uri path);

        /// <summary>
        /// Stores the <paramref name="songSet"/> at a given <paramref name="path"/>
        /// </summary>
        /// <param name="songSet">The SessionSet to store.</param>
        /// <param name="path">The location to store the SessionSet</param>
        /// <param name="cancellationToken">The cancelation Token to cancel the opereraion</param>
        /// <returns>A task that completes when the store operation completes and returns the new created session set.</returns>
        Task<ISessionSet> StoreAsAsync(ISessionSet songSet, Uri path, CancellationToken cancellationToken);
    }
}
