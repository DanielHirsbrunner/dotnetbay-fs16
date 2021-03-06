﻿using DotNetBay.Core.Execution;
using DotNetBay.Data.EF;
using DotNetBay.Data.FileStorage;
using DotNetBay.Interfaces;

namespace DotNetBay.Core {
    public class ServiceLocator {

        private static ServiceLocator instance;

        #region Instanzvariablen

        private readonly IMainRepository mainRepository;
        private readonly IAuctionRunner auctionRunner;
        private readonly IMemberService memberService;
        private readonly IAuctionService auctionService;

        #endregion

        #region Konstruktor
        /// <summary>
        /// 
        /// </summary>
        private ServiceLocator() {
            // Dienst zur zentralen Datenverwaltung initialisieren 
            this.mainRepository = new EFMainRepository();
            //this.mainRepository = new FileSystemMainRepository("C:\\Temp\\appdata2.json");
            this.mainRepository.SaveChanges();
            // Dienst zum betrieben und überwachen der Auktionen initialisieren
            this.auctionRunner = AuctionRunner.GetInstance(this.mainRepository);
            this.auctionRunner.Start();
            // Dienst zur Administration von Members (Seller, Bidder, etc.) initialisieren
            this.memberService = new SimpleMemberService(this.mainRepository);
            // Dienst zur Verwaltung der Auktionen
            this.auctionService = new AuctionService(this.mainRepository, this.memberService);

        }
        #endregion

        #region Instanz Property
        /// <summary>
        /// 
        /// </summary>
        public static ServiceLocator GetInstance {
            get {
                if (ServiceLocator.instance == null) {
                    instance = new ServiceLocator();
                }
                return instance;
            }
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Dienst zur zentralen Datenverwaltung
        /// </summary>
        public IMainRepository GetMainRepository => new EFMainRepository();//this.mainRepository;

        /// <summary>
        /// Dienst zum betrieben und überwachen der Auktionen
        /// </summary>
        public IAuctionRunner GetAuctionRunner => AuctionRunner.GetInstance(this.GetMainRepository);// this.auctionRunner;

        /// <summary>
        /// Dienst zum betrieben und überwachen der Auktionen
        /// </summary>
        public IMemberService GetMemberService => new SimpleMemberService(this.GetMainRepository);//this.memberService;

        /// <summary>
        /// Dienst zum Verwaltung der Auktionen
        /// </summary>
        public IAuctionService GetAuctionService => new AuctionService(this.GetMainRepository, this.GetMemberService);//this.auctionService;

        #endregion
    }
}
