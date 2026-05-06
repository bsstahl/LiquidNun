using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Graph;
using Microsoft.Graph.Drives.Item;
using Microsoft.Graph.Models;
using Microsoft.Graph.Models.ODataErrors;

namespace LiquidNun.Directory.OneDrive
{
    /// <summary>
    /// Production implementation of <see cref="IOneDriveClient"/> that delegates to
    /// a <see cref="GraphServiceClient"/> obtained from the Microsoft Graph SDK.
    /// </summary>
    internal sealed class GraphOneDriveClient : IOneDriveClient
    {
        private readonly GraphServiceClient _graphClient;
        private string? _driveId;

        /// <summary>
        /// Creates a client that lazily resolves the signed-in user's OneDrive on
        /// first use by calling <c>GET /me/drive</c>.
        /// </summary>
        internal GraphOneDriveClient(GraphServiceClient graphClient)
        {
            _graphClient = graphClient ?? throw new ArgumentNullException(nameof(graphClient));
        }

        /// <summary>
        /// Creates a client targeting a specific, known OneDrive drive ID.  Use
        /// this constructor for application-only (daemon) scenarios where there is
        /// no signed-in user and the drive ID is supplied out-of-band.
        /// </summary>
        internal GraphOneDriveClient(GraphServiceClient graphClient, string driveId)
        {
            _graphClient = graphClient ?? throw new ArgumentNullException(nameof(graphClient));
            _driveId = !string.IsNullOrEmpty(driveId) ? driveId
                : throw new ArgumentNullException(nameof(driveId));
        }

        // ── IOneDriveClient ───────────────────────────────────────────────────

        /// <inheritdoc/>
        public async Task<IEnumerable<DriveItemInfo>?> GetChildrenAsync(string path)
        {
            try
            {
                var drive = await GetDriveBuilderAsync().ConfigureAwait(false);
                DriveItemCollectionResponse? response;

                if (IsRoot(path))
                    response = await drive.Items["root"].Children.GetAsync().ConfigureAwait(false);
                else
                    response = await drive.Root.ItemWithPath(path).Children.GetAsync().ConfigureAwait(false);

                return MapItems(response?.Value, path);
            }
            catch (ODataError ex) when (ex.ResponseStatusCode == (int)HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        /// <inheritdoc/>
        public async Task<Stream?> GetFileContentStreamAsync(string filePath)
        {
            try
            {
                var drive = await GetDriveBuilderAsync().ConfigureAwait(false);

                return IsRoot(filePath)
                    ? await drive.Root.Content.GetAsync().ConfigureAwait(false)
                    : await drive.Root.ItemWithPath(filePath).Content.GetAsync().ConfigureAwait(false);
            }
            catch (ODataError ex) when (ex.ResponseStatusCode == (int)HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        /// <inheritdoc/>
        public async Task<DriveItemInfo?> GetItemAsync(string path)
        {
            try
            {
                var drive = await GetDriveBuilderAsync().ConfigureAwait(false);
                DriveItem? item;

                if (IsRoot(path))
                    item = await drive.Root.GetAsync().ConfigureAwait(false);
                else
                    item = await drive.Root.ItemWithPath(path).GetAsync().ConfigureAwait(false);

                return item is null ? null : MapItem(item, path);
            }
            catch (ODataError ex) when (ex.ResponseStatusCode == (int)HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        // ── helpers ───────────────────────────────────────────────────────────

        private async Task<DriveItemRequestBuilder> GetDriveBuilderAsync()
        {
            if (_driveId is null)
            {
                var drive = await _graphClient.Me.Drive.GetAsync().ConfigureAwait(false);
                _driveId = drive?.Id
                    ?? throw new InvalidOperationException(
                        "Unable to retrieve the signed-in user's OneDrive. " +
                        "Ensure the credential has the Files.Read (or Files.ReadWrite) delegated permission.");
            }

            return _graphClient.Drives[_driveId];
        }

        private static bool IsRoot(string path)
            => string.IsNullOrWhiteSpace(path) || path == "/" || path == "\\";

        private static IEnumerable<DriveItemInfo> MapItems(
            IEnumerable<DriveItem>? items, string parentPath)
        {
            if (items is null)
                return Enumerable.Empty<DriveItemInfo>();

            return items.Select(item =>
            {
                string name = item.Name ?? string.Empty;
                string itemPath = CombinePaths(parentPath, name);
                return new DriveItemInfo(name, itemPath, item.File is not null, item.Folder is not null);
            });
        }

        private static DriveItemInfo MapItem(DriveItem item, string requestedPath)
        {
            string name = item.Name ?? ExtractItemName(requestedPath);
            return new DriveItemInfo(name, requestedPath, item.File is not null, item.Folder is not null);
        }

        /// <summary>
        /// Derives a display name from the tail segment of a path when the drive item's
        /// <c>Name</c> field is absent (e.g., when mapping the drive root itself).
        /// </summary>
        private static string ExtractItemName(string path)
            => System.IO.Path.GetFileName(path.TrimEnd('/').TrimEnd('\\')) ?? string.Empty;

        private static string CombinePaths(string parent, string child)
        {
            string trimmed = parent.TrimEnd('/').TrimEnd('\\');
            return $"{trimmed}/{child}";
        }
    }
}
