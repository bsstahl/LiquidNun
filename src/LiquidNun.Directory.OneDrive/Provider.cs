using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Azure.Core;
using Azure.Identity;
using LiquidNun.Interfaces;
using Microsoft.Graph;

namespace LiquidNun.Directory.OneDrive
{
    /// <summary>
    /// Implements <see cref="IDirectoryService"/> using Microsoft OneDrive via the
    /// Microsoft Graph API.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <b>Authentication</b><br/>
    /// This class delegates authentication to the Azure.Identity library.
    /// Choose a <see cref="TokenCredential"/> appropriate for your scenario:
    /// <list type="bullet">
    ///   <item>
    ///     <term><see cref="DefaultAzureCredential"/></term>
    ///     <description>
    ///       Recommended for Azure-hosted services. Tries managed identity, environment
    ///       variables, Visual Studio, Azure CLI, and interactive browser in order.
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <term><see cref="ClientSecretCredential"/></term>
    ///     <description>
    ///       Suitable for daemon / server-to-server access where a specific Azure AD
    ///       application registration with a client secret is available.
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <term><see cref="InteractiveBrowserCredential"/></term>
    ///     <description>
    ///       Opens a browser window so the end-user can sign in interactively.
    ///       Ideal for desktop or CLI tools.
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <term><see cref="DeviceCodeCredential"/></term>
    ///     <description>
    ///       Prompts the user to visit a URL and enter a device code.  Useful in
    ///       headless environments.
    ///     </description>
    ///   </item>
    /// </list>
    /// </para>
    /// <para>
    /// <b>Required permissions (Microsoft Graph scopes)</b><br/>
    /// The credential must be granted at least the <c>Files.Read</c> delegated scope
    /// (or <c>Files.Read.All</c> for application-only access) so that all interface
    /// methods can succeed.  Write operations are not performed by this provider.
    /// </para>
    /// <para>
    /// <b>Paths</b><br/>
    /// All path parameters are drive-root-relative, using forward-slash separators,
    /// e.g. <c>/Documents/Reports/Q1.xlsx</c>.  An empty string, <c>"/"</c>, or
    /// <c>"\"</c> all refer to the drive root.
    /// </para>
    /// </remarks>
    public class Provider : IDirectoryService
    {
        private static readonly string[] _defaultScopes =
            { "https://graph.microsoft.com/.default" };

        private readonly IOneDriveClient _client;

        /// <summary>
        /// Initialises a new <see cref="Provider"/> that accesses the signed-in user's
        /// OneDrive using the supplied <paramref name="credential"/>.
        /// </summary>
        /// <param name="credential">
        /// An Azure.Identity <see cref="TokenCredential"/> that has been granted the
        /// required Microsoft Graph permissions.  Use <see cref="DefaultAzureCredential"/>
        /// for Azure-hosted workloads, or <see cref="InteractiveBrowserCredential"/> /
        /// <see cref="DeviceCodeCredential"/> for user-interactive scenarios.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="credential"/> is <see langword="null"/>.
        /// </exception>
        public Provider(TokenCredential credential)
        {
            if (credential is null) throw new ArgumentNullException(nameof(credential));
            var graphClient = new GraphServiceClient(credential, _defaultScopes);
            _client = new GraphOneDriveClient(graphClient);
        }

        /// <summary>
        /// Initialises a new <see cref="Provider"/> for daemon / service applications
        /// that authenticate with a client secret (application-only flow).
        /// </summary>
        /// <param name="tenantId">The Azure Active Directory tenant (directory) ID.</param>
        /// <param name="clientId">The application (client) ID of the Azure AD app registration.</param>
        /// <param name="clientSecret">
        /// The client secret generated for the Azure AD app registration.
        /// Store this value in a secrets manager (e.g. Azure Key Vault) rather than
        /// embedding it in source code or configuration files.
        /// </param>
        /// <param name="driveId">
        /// The identifier of the OneDrive drive to target.  For a SharePoint document
        /// library or a user drive that is known ahead of time, pass the drive ID here.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when any of the parameters is <see langword="null"/> or empty.
        /// </exception>
        public Provider(string tenantId, string clientId, string clientSecret, string driveId)
            : this(BuildClientSecretCredential(tenantId, clientId, clientSecret), driveId)
        { }

        /// <summary>
        /// Initialises a new <see cref="Provider"/> that accesses a specific OneDrive
        /// drive using the supplied <paramref name="credential"/> and <paramref name="driveId"/>.
        /// Use this overload when you know the drive ID ahead of time, or for
        /// application-only access that cannot rely on the <c>/me/drive</c> endpoint.
        /// </summary>
        /// <param name="credential">
        /// An Azure.Identity <see cref="TokenCredential"/> that has been granted the
        /// required Microsoft Graph permissions.
        /// </param>
        /// <param name="driveId">The identifier of the OneDrive drive to target.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="credential"/> or <paramref name="driveId"/> is
        /// <see langword="null"/> or empty.
        /// </exception>
        public Provider(TokenCredential credential, string driveId)
        {
            if (credential is null) throw new ArgumentNullException(nameof(credential));
            if (string.IsNullOrEmpty(driveId)) throw new ArgumentNullException(nameof(driveId));
            var graphClient = new GraphServiceClient(credential, _defaultScopes);
            _client = new GraphOneDriveClient(graphClient, driveId);
        }

        /// <summary>
        /// Internal constructor that injects a pre-built <see cref="IOneDriveClient"/>
        /// for unit-testing purposes.
        /// </summary>
        internal Provider(IOneDriveClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        // ── IDirectoryService ─────────────────────────────────────────────────

        /// <inheritdoc/>
        /// <exception cref="Exceptions.DirectoryNotFoundException">
        /// Thrown when the specified folder path does not exist in the drive.
        /// </exception>
        public IEnumerable<string> GetFiles(string pathName)
        {
            var items = _client.GetChildrenAsync(pathName).GetAwaiter().GetResult();

            if (items is null)
                throw new Exceptions.DirectoryNotFoundException(pathName);

            return items.Where(i => i.IsFile).Select(i => i.Path);
        }

        /// <inheritdoc/>
        /// <exception cref="Exceptions.FileNotFoundException">
        /// Thrown when the specified file does not exist in the drive.
        /// </exception>
        public string ReadAllText(string filePath)
        {
            var stream = _client.GetFileContentStreamAsync(filePath).GetAwaiter().GetResult();

            if (stream is null)
                throw new Exceptions.FileNotFoundException(filePath);

            using (var reader = new StreamReader(stream))
                return reader.ReadToEnd();
        }

        /// <inheritdoc/>
        public bool FileExists(string filePath)
        {
            var item = _client.GetItemAsync(filePath).GetAwaiter().GetResult();
            return item is not null && item.IsFile;
        }

        /// <inheritdoc/>
        public bool Exists(string pathName)
        {
            var item = _client.GetItemAsync(pathName).GetAwaiter().GetResult();
            return item is not null;
        }

        /// <inheritdoc/>
        /// <exception cref="Exceptions.FileNotFoundException">
        /// Thrown when the specified file does not exist in the drive.
        /// </exception>
        public IFileReader OpenFileForRead(string filePath)
        {
            var stream = _client.GetFileContentStreamAsync(filePath).GetAwaiter().GetResult();

            if (stream is null)
                throw new Exceptions.FileNotFoundException(filePath);

            return new FileReader(stream);
        }

        // ── helpers ───────────────────────────────────────────────────────────

        private static ClientSecretCredential BuildClientSecretCredential(
            string tenantId, string clientId, string clientSecret)
        {
            if (string.IsNullOrEmpty(tenantId))
                throw new ArgumentNullException(nameof(tenantId));
            if (string.IsNullOrEmpty(clientId))
                throw new ArgumentNullException(nameof(clientId));
            if (string.IsNullOrEmpty(clientSecret))
                throw new ArgumentNullException(nameof(clientSecret));

            return new ClientSecretCredential(tenantId, clientId, clientSecret);
        }
    }
}
