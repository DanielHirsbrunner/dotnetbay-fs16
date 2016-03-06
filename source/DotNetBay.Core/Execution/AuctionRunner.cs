using System;
using System.Threading;

using DotNetBay.Interfaces;

namespace DotNetBay.Core.Execution {
    public class AuctionRunner : IAuctionRunner {
        private readonly TimeSpan checkInterval;

        private readonly Auctioneer auctioneer;

        private Timer timer;

        private static AuctionRunner instance = null;


        private AuctionRunner(IMainRepository repository, TimeSpan checkInterval) {
            this.checkInterval = checkInterval;
            this.timer = new Timer(this.Callback, null, Timeout.Infinite, Timeout.Infinite);

            this.auctioneer = new Auctioneer(repository);
        }

        /// <summary>
        /// Der Auktionator welcher die Auktionen leitet
        /// </summary>
        public IAuctioneer Auctioneer {
            get { return this.auctioneer; }
        }

        public static IAuctionRunner GetInstance(IMainRepository repository) {
            return AuctionRunner.GetInstance(repository, TimeSpan.FromSeconds(5));
        }

        public static IAuctionRunner GetInstance(IMainRepository repository, TimeSpan checkInterval) {
            if (AuctionRunner.instance == null) {
                instance = new AuctionRunner(repository, checkInterval);
            }
            return instance;
        }

        #region public Methoden

        public void Start() {
            this.timer.Change(this.checkInterval, this.checkInterval);
        }

        public void Stop() {
            this.timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        public void RunOnce() {
            this.auctioneer.DoAllWork();
        }

        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                // free managed resources
                if (this.timer != null) {
                    this.timer.Dispose();
                    this.timer = null;
                }
            }

            // free native resources if there are any.
        }

        private void Callback(object state) {
            this.auctioneer.DoAllWork();
        }
    }
}