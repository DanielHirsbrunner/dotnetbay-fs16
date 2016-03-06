﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetBay.Core;
using DotNetBay.Core.Execution;
using DotNetBay.Data.FileStorage;
using DotNetBay.Interfaces;

namespace DotNetBay.WPF {
    public class ServiceDirectory {

        private static ServiceDirectory instance;

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
        private ServiceDirectory() {
            // Dienst zur zentralen Datenverwaltung initialisieren 
            this.mainRepository = new FileSystemMainRepository("appdata2.json");
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
        public static ServiceDirectory GetInstance {
            get {
                if (ServiceDirectory.instance == null) {
                    instance = new ServiceDirectory();
                }
                return instance;
            }
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Dienst zur zentralen Datenverwaltung
        /// </summary>
        public IMainRepository GetMainRepository => this.mainRepository;

        /// <summary>
        /// Dienst zum betrieben und überwachen der Auktionen
        /// </summary>
        public IAuctionRunner GetAuctionRunner => this.auctionRunner;

        /// <summary>
        /// Dienst zum betrieben und überwachen der Auktionen
        /// </summary>
        public IMemberService GetMemberService => this.memberService;

        /// <summary>
        /// Dienst zum Verwaltung der Auktionen
        /// </summary>
        public IAuctionService GetAuctionService => this.auctionService;

        #endregion
    }
}
