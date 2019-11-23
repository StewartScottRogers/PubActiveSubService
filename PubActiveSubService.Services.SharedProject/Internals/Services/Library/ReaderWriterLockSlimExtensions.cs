namespace System.Threading {
    internal static class ReaderWriterLockSlimExtensions {

        /// <summary>
        ///    using (TheReaderWriterLockSlim.ReadLock()) {
        /// 
        ///    }
        ///    
        ///    using (TheReaderWriterLockSlim.UpgadableReadLockToken()) {
        /// 
        ///    }
        ///    
        ///    using (TheReaderWriterLockSlim.WriteLock()) {
        /// 
        ///    }
        /// </summary>
        private sealed class ReadLockToken : IDisposable {
            private ReaderWriterLockSlim ReaderWriterLockSlim;

            public ReadLockToken(ReaderWriterLockSlim readerWriterLockSlim) {
                ReaderWriterLockSlim = readerWriterLockSlim;
                readerWriterLockSlim.EnterReadLock();
            }
            public void Dispose() {
                if (ReaderWriterLockSlim == null)
                    return;

                ReaderWriterLockSlim.ExitReadLock();
                ReaderWriterLockSlim = null;
            }
        }

        private sealed class UpgadableReadLockToken : UpgadableRead {
            private ReaderWriterLockSlim ReaderWriterLockSlim;

            public IDisposable WriteLock() => new WriteLockToken(ReaderWriterLockSlim);

            public UpgadableReadLockToken(ReaderWriterLockSlim readerWriterLockSlim) {
                ReaderWriterLockSlim = readerWriterLockSlim;
                readerWriterLockSlim.EnterUpgradeableReadLock();
            }
            public void Dispose() {
                if (ReaderWriterLockSlim == null)
                    return;

                ReaderWriterLockSlim.ExitUpgradeableReadLock();
                ReaderWriterLockSlim = null;
            }
        }

        private sealed class WriteLockToken : IDisposable {
            private ReaderWriterLockSlim ReaderWriterLockSlim;
            public WriteLockToken(ReaderWriterLockSlim readerWriterLockSlim) {
                ReaderWriterLockSlim = readerWriterLockSlim;
                readerWriterLockSlim.EnterWriteLock();
            }

            public void Dispose() {
                if (ReaderWriterLockSlim == null)
                    return;

                ReaderWriterLockSlim.ExitWriteLock();
                ReaderWriterLockSlim = null;
            }
        }

        public static IDisposable ReadLock(this ReaderWriterLockSlim readerWriterLockSlim) => new ReadLockToken(readerWriterLockSlim);

        public static UpgadableRead UpgadableReadLock(this ReaderWriterLockSlim readerWriterLockSlim) => new UpgadableReadLockToken(readerWriterLockSlim);


        public static IDisposable WriteLock(this ReaderWriterLockSlim readerWriterLockSlim) => new WriteLockToken(readerWriterLockSlim);

        public interface UpgadableRead : IDisposable {
            IDisposable WriteLock();
        }
    }
}