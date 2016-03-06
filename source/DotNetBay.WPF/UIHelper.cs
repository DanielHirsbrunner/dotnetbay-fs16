using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using DotNetBay.Model;

namespace DotNetBay.WPF {

    /// <summary>
    /// Generelle Hilfsfunktionen für den Presentation Layer
    /// </summary>
    public static class UIHelper {

        #region public static void CreateDemoData()
        /// <summary>
        /// Generiert Demo Daten falls noch keine Daten erfasst sind.
        /// </summary>
        public static void CreateDemoData() {
            // der memberService administriert käufer und verkäufer
            if (!ServiceDirectory.GetInstance.GetAuctionService.GetAll().Any()) {
                var me = ServiceDirectory.GetInstance.GetMemberService.GetCurrentMember();
                ServiceDirectory.GetInstance.GetAuctionService.Save(
                    new Auction {
                        Title = "REMINGTON 5 Schreibmaschine",
                        Description = "REMINGTON 5 Antike Schreibmaschine J 1886. Typenhelbel mit 84 Zeichen Unteraufschlag Baujahr 1186 Konsturkteur: Wyckoff Seamann & Benedict! USA",
                        StartDateTimeUtc = DateTime.UtcNow.AddSeconds(10),
                        EndDateTimeUtc = DateTime.UtcNow.AddDays(14),
                        StartPrice = 1500,
                        CurrentPrice = 1500,
                        Seller = me,
                        Image = GetImageAsByteArray("C:\\SourceTree\\FHNW\\dnead\\source\\Images\\auktion1.jpg")
                    });
                ServiceDirectory.GetInstance.GetAuctionService.Save(new Auction {
                    Title = "Oldtimer Motorrad",
                    Description = "BSA A7 500 OHW TWIN, MODELL 1949 VETERANENFAHRZEUG- EINTRAG!! ",
                    StartDateTimeUtc = DateTime.UtcNow.AddDays(3),
                    EndDateTimeUtc = DateTime.UtcNow.AddDays(17),
                    StartPrice = 8900,
                    CurrentPrice = 8900,
                    Seller = me,
                    Image = GetImageAsByteArray("C:\\SourceTree\\FHNW\\dnead\\source\\Images\\auktion2.jpg")
                });
                ServiceDirectory.GetInstance.GetAuctionService.Save(new Auction {
                    Title = "Antike Qing Vase",
                    Description = "Diese Porzellan Deckelvase, datiert in das China der Qing Zeit (1644-1912), ist mit floralen Dekor in blauweisser Unterglasur Malerei verziert. Schauseitig ist sie mit einer Malerei von drei Gelehrten geschmückt. Die Malerei ist von sehr guter Qualität, man fühlt den Farbauftrag auf dem Porzellan. Diese Vase war wohl Gegenstand einer Auktion bei Sothebys. Die Bruchstücke und die Etiketten waren in der Vase aufbewahrt. Auf der Unterseite ist eine blaue Vierzeichenmarke Marke zu erkennen.",
                    StartDateTimeUtc = DateTime.UtcNow.AddDays(6),
                    EndDateTimeUtc = DateTime.UtcNow.AddDays(20),
                    StartPrice = 4950,
                    CurrentPrice = 4950,
                    Seller = me,
                    Image = GetImageAsByteArray("C:\\SourceTree\\FHNW\\dnead\\source\\Images\\auktion3.jpg")
                });
            }
        }
        #endregion

        #region public static byte[] GetImageAsByteArray(string uri)
        /// <summary>
        /// Liest ein Bild anhand der übergebenen Uri in den speicher und gibt es als BitArray zurück
        /// </summary>
        /// <param name="uri">Pfad zum Bild</param>
        /// <returns></returns>
        public static byte[] GetImageAsByteArray(string uri) {
            var img = BitmapFrame.Create(new Uri(uri));
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(img);
            encoder.QualityLevel = 100;
            byte[] bit = new byte[0];
            using (MemoryStream stream = new MemoryStream()) {
                encoder.Frames.Add(img);
                encoder.Save(stream);
                bit = stream.ToArray();
                stream.Close();
            }
            return bit;
        }
        #endregion
    }
}
