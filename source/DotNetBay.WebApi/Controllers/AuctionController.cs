using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DotNetBay.Core;
using DotNetBay.Model.BO;
using DotNetBay.WebApi.DTO;

namespace DotNetBay.WebApi.Controllers {
    public class AuctionController : ApiController {

        [HttpGet]
        [Route("api/auctions")]
        public IEnumerable<AuctionDTO> GetAllAuctions() {
            var auctions = ServiceLocator.GetInstance.GetAuctionService.GetAll();
            List<AuctionDTO> res = new List<AuctionDTO>();
            foreach (Auction auction in auctions) res.Add(AuctionDTO.GetAuctionDtoByAuction(auction));

            return res;
        }

        [HttpGet]
        [Route("api/auctions/{id}")]
        public AuctionDTO GetAuction(int id) {
            return AuctionDTO.GetAuctionDtoByAuction(ServiceLocator.GetInstance.GetAuctionService.GetById(id));
        }

        [HttpGet]
        [Route("api/auctions/{id}/Image")]
        public byte[] GetAuctionImage(int id) {
            return ServiceLocator.GetInstance.GetAuctionService.GetById(id)?.Image;
        }

        [HttpPost]
        [Route("api/auctions/{id}/image")]
        public async Task<IHttpActionResult> AddImageForAuction(int id) {
            var streamProvider = await this.Request.Content.ReadAsMultipartAsync();
            foreach (var file in streamProvider.Contents) {
                var image = await file.ReadAsByteArrayAsync();
                Auction auction = ServiceLocator.GetInstance.GetAuctionService.GetById(id);
                if (auction != null) {
                    auction.Image = image;
                    ServiceLocator.GetInstance.GetAuctionService.Save(auction);
                }
            }
            return this.Ok();
        }

        [HttpPost]
        [Route("api/auctions/{id}")]
        public IHttpActionResult AddAuction(AuctionDTO auction) {
            Auction a = AuctionDTO.GetAuctionByAuctionDTO(auction);
            ServiceLocator.GetInstance.GetAuctionService.Save(a);
            return this.Ok();
        }

        [HttpPost]
        [Route("api/auctions/{id}/bid/{amount}")]
        public IHttpActionResult AddBidForAuction(int id, double amount) {
            Auction auction = ServiceLocator.GetInstance.GetAuctionService.GetById(id);
            ServiceLocator.GetInstance.GetAuctionService.PlaceBid(auction, amount);
            return this.Ok();
        }

    }
}
