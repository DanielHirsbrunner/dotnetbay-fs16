using DotNetBay.Interfaces;

namespace DotNetBay.Data.EF {
    public class EFMainRepositoryFactory : IRepositoryFactory {
        public void Dispose() {
            this.repository = null;
        }

        private IMainRepository repository;

        public IMainRepository CreateMainRepository() {
            if (this.repository == null) {
                this.repository = new EFMainRepository();
            }
            return this.repository;
        }
    }
}
