using System;
using System.IO;
using LiquidNun.Interfaces;

namespace LiquidNun.Directory.OneDrive
{
    /// <summary>
    /// Implements <see cref="IFileReader"/> on top of a <see cref="Stream"/> downloaded
    /// from OneDrive so that file content can be consumed line by line.
    /// </summary>
    public sealed class FileReader : IFileReader, IDisposable
    {
        private readonly TextReader _reader;
        private bool _disposed;

        /// <summary>
        /// Initialises a new <see cref="FileReader"/> that reads from
        /// <paramref name="contentStream"/>.
        /// </summary>
        /// <param name="contentStream">
        /// The content stream obtained from OneDrive (must not be <see langword="null"/>).
        /// </param>
        internal FileReader(Stream contentStream)
        {
            if (contentStream is null) throw new ArgumentNullException(nameof(contentStream));
            _reader = new StreamReader(contentStream);
        }

        /// <summary>
        /// Reads the next line of text from the file and returns it, or
        /// <see langword="null"/> if the end of the stream has been reached.
        /// </summary>
        /// <remarks>
        /// Although <see cref="IFileReader.ReadLine"/> is typed as returning
        /// <see cref="string"/> (non-nullable), <see cref="System.IO.StreamReader.ReadLine"/>
        /// legitimately returns <see langword="null"/> at end-of-file.  The null-forgiving
        /// operator (<c>!</c>) is used here to satisfy the compiler's nullable analysis;
        /// at runtime, <see langword="null"/> is still propagated to callers that check for
        /// it (consistent with the rest of the LiquidNun <see cref="IFileReader"/>
        /// implementations).
        /// </remarks>
        public string ReadLine() => _reader.ReadLine()!;

        /// <summary>
        /// Closes the underlying stream and releases all associated resources.
        /// </summary>
        public void Close() => Dispose();

        /// <inheritdoc/>
        public void Dispose()
        {
            if (!_disposed)
            {
                _reader.Dispose();
                _disposed = true;
                GC.SuppressFinalize(this);
            }
        }
    }
}
