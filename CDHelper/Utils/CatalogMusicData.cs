using CDHelper.Models;
using CDHelper.Structs;

namespace CDHelper.Utils
{
    public static class CatalogMusicData
    {
        public static List<MusicData> Data { get; set; } = [];


        public static void SetData(string hotel)
        {
            switch (hotel)
            {
                case "com.br":
                    LoadComBr();
                    DefaultMarketCD.SetName("Epic Flail");
                    FurniIds.CD = 2322;
                    break;

                case "com":
                    LoadCom();
                    DefaultMarketCD.SetName("Tapes From Goa");
                    FurniIds.CD = 2607;
                    break;

                case "es":
                    LoadEs();
                    DefaultMarketCD.SetName("TraxDisco");
                    FurniIds.CD = 2319;
                    break;

                case "fi":
                    LoadFi();
                    DefaultMarketCD.SetName("Tapes From Goa");
                    FurniIds.CD = 8118;
                    break;

                case "it":
                    LoadIt();
                    DefaultMarketCD.SetName("Tapes From Goa");
                    FurniIds.CD = 2382;
                    break;

                case "nl":
                    LoadNl();
                    DefaultMarketCD.SetName("Epic Flail");
                    FurniIds.CD = 2322;
                    break;

                case "de":
                    LoadDe();
                    DefaultMarketCD.SetName("Musik CD");
                    FurniIds.CD = 2320;
                    break;

                case "fr":
                    LoadFr();
                    DefaultMarketCD.SetName("CD");
                    FurniIds.CD = 2320;
                    break;

                case "com.tr":
                    LoadTr();
                    DefaultMarketCD.SetName("Goa Kasetleri");
                    FurniIds.CD = 2377;
                    break;

                default:
                    break;
            }
        }

        private static void LoadComBr()
        {
            List<MusicData> list =
            [
                new MusicData(553139, "Xmas Magic", "Silent Aurora"),
                new MusicData(553112, "Habbowood", "Michael Bauble"),
                new MusicData(553114, "Furni Face", "Lady BlaBla"),
                new MusicData(553115, "Gold Coin Digger", "Kayne Quest"),
                new MusicData(553116, "I Write Bans not Tragedies", "Pixel! at the Disco"),
                new MusicData(553117, "Haven't Friend Request You Yet", "Michael Bauble"),
                new MusicData(553118, "Park Adventure", "Kallomies"),
                new MusicData(553119, "Pet Romance", "Lady BlaBla"),
                new MusicData(553120, "Pixelrazzi", "Lady BlaBla"),
                new MusicData(553121, "Push the Call for Help", "BanzaiBabes"),
                new MusicData(553122, "The Ballad of Bonnie Blonde", "Pixel! at the Disco"),
                new MusicData(553123, "The Good Trade", "Kayne Quest"),
                new MusicData(553124, "Too Lost In Lido", "BanzaiBabes"),
                new MusicData(553125, "Touch the Skyscraper", "Kayne Quest"),
                new MusicData(553126, "Party Trax", "Aerokid"),
                new MusicData(553127, "Phuturistic Chilled Trax", "Aerokid"),
                new MusicData(553128, "Tapes from Goa", "Habnosis"),
                new MusicData(553129, "Alley Cat in Trouble", "Rage Against the Fuse"),
                new MusicData(553130, "Who Dares Stacks", "Rage Against the Fuse"),
                new MusicData(553131, "Electric Pixels", "Habbo de Gaia"),
                new MusicData(553132, "Galactic Disco", "DJ Bobba feat. Habboway"),
                new MusicData(553133, "Epic Flail", "Habbocalyptica"),

                new MusicData(553134, "68B Attack Sub", "Habnosis"),
                new MusicData(553135, "The Habstep", "Habnosis"),
                new MusicData(553136, "Caliente Street", "Barrio Bobba"),
                new MusicData(553137, "Habbo Libre", "Ana Stan Band"),
                new MusicData(553138, "Uuh Aah", "DJ Bobba"),
                new MusicData(553113, "About VIP Now", "BanzaiBabes"),
            ];

            Data = list;
        }

        private static void LoadCom()
        {
            List<MusicData> list =
            [
                new MusicData(897008, "Habbowood", "Michael Bauble"),
                new MusicData(897009, "About VIP Now", "BanzaiBabes"),
                new MusicData(897010, "Furni Face", "Lady BlaBla"),
                new MusicData(897011, "Gold Coin Digger", "Kayne Quest"),
                new MusicData(897012, "I Write Bans not Tragedies", "Pixel! at the Disco"),
                new MusicData(897013, "Haven't Friend Request You Yet", "Michael Bauble"),
                new MusicData(897014, "Park Adventure", "Kallomies"),
                new MusicData(897015, "Pet Romance", "Lady BlaBla"),
                new MusicData(897016, "Pixelrazzi", "Lady BlaBla"),
                new MusicData(897017, "Push the Call for Help", "BanzaiBabes"),
                new MusicData(897018, "The Ballad of Bonnie Blonde", "Pixel! at the Disco"),
                new MusicData(897019, "The Good Trade", "Kayne Quest"),
                new MusicData(897020, "Too Lost In Lido", "BanzaiBabes"),
                new MusicData(897021, "Touch the Skyscraper", "Kayne Quest"),
                new MusicData(897022, "Party Trax", "Aerokid"),
                new MusicData(897023, "Phuturistic Chilled Trax", "Aerokid"),
                new MusicData(897024, "Tapes from Goa", "Habnosis"),
                new MusicData(897025, "Alley Cat in Trouble", "Rage Against the Fuse"),
                new MusicData(897026, "Who Dares Stacks", "Rage Against the Fuse"),
                new MusicData(907090, "Electric Pixels", "Habbo de Gaia"),
                new MusicData(920871, "Galactic Disco", "DJ Bobba feat. Habboway"),
                new MusicData(920872, "Epic Flail", "Habbocalyptica"),
                new MusicData(923425, "68B Attack Sub", "Habnosis"),
                new MusicData(923426, "The Habstep", "Habnosis"),
                new MusicData(923427, "Caliente Street", "Barrio Bobba"),
                new MusicData(923428, "Habbo Libre", "Ana Stan Band"),
                new MusicData(923429, "Uuh Aah", "DJ Bobba"),
                new MusicData(923430, "Xmas Magic", "Silent Aurora")
            ];


            Data = list;
        }

        private static void LoadEs()
        {
            List<MusicData> list =
            [
                new MusicData(951691, "Habbowood", "Michael Bauble"),
                new MusicData(951692, "About VIP Now", "BanzaiBabes"),
                new MusicData(951693, "Furni Face", "Lady BlaBla"),
                new MusicData(951694, "Gold Coin Digger", "Kayne Quest"),
                new MusicData(951695, "I Write Bans not Tragedies", "Pixel! at the Disco"),
                new MusicData(951696, "Haven't Friend Request You Yet", "Michael Bauble"),
                new MusicData(951697, "Park Adventure", "Kallomies"),
                new MusicData(951698, "Pet Romance", "Lady BlaBla"),
                new MusicData(951699, "Pixelrazzi", "Lady BlaBla"),
                new MusicData(951700, "Push the Call for Help", "BanzaiBabes"),
                new MusicData(951701, "The Ballad of Bonnie Blonde", "Pixel! at the Disco"),
                new MusicData(951702, "The Good Trade", "Kayne Quest"),
                new MusicData(951703, "Too Lost In Lido", "BanzaiBabes"),
                new MusicData(951704, "Touch the Skyscraper", "Kayne Quest"),
                new MusicData(951705, "Party Trax", "Aerokid"),
                new MusicData(951706, "Phuturistic Chilled Trax", "Aerokid"),
                new MusicData(951707, "Tapes from Goa", "Habnosis"),
                new MusicData(951708, "Alley Cat in Trouble", "Rage Against the Fuse"),
                new MusicData(951709, "Who Dares Stacks", "Rage Against the Fuse"),
                new MusicData(951710, "Electric Pixels", "Habbo de Gaia"),
                new MusicData(951711, "Galactic Disco", "DJ Bobba feat. Habboway"),
                new MusicData(951712, "Epic Flail", "Habbocalyptica"),
                new MusicData(951713, "68B Attack Sub", "Habnosis"),
                new MusicData(951714, "The Habstep", "Habnosis"),
                new MusicData(951715, "Caliente Street", "Barrio Bobba"),
                new MusicData(951716, "Habbo Libre", "Ana Stan Band"),
                new MusicData(951717, "Uuh Aah", "DJ Bobba"),
                new MusicData(951718, "Xmas Magic", "Silent Aurora"),
            ];


            Data = list;
        }

        private static void LoadFr()
        {
            List<MusicData> list =
            [
                new MusicData(733566, "Habbowood", "Michael Bauble"),
                new MusicData(733567, "About VIP Now", "BanzaiBabes"),
                new MusicData(733568, "Furni Face", "Lady BlaBla"),
                new MusicData(733569, "Gold Coin Digger", "Kayne Quest"),
                new MusicData(733570, "I Write Bans not Tragedies", "Pixel! at the Disco"),
                new MusicData(733571, "Haven't Friend Request You Yet", "Michael Bauble"),
                new MusicData(733572, "Park Adventure", "Kallomies"),
                new MusicData(733573, "Pet Romance", "Lady BlaBla"),
                new MusicData(733574, "Pixelrazzi", "Lady BlaBla"),
                new MusicData(733575, "Push the Call for Help", "BanzaiBabes"),
                new MusicData(733576, "The Ballad of Bonnie Blonde", "Pixel! at the Disco"),
                new MusicData(733577, "The Good Trade", "Kayne Quest"),
                new MusicData(733578, "Too Lost In Lido", "BanzaiBabes"),
                new MusicData(733579, "Touch the Skyscraper", "Kayne Quest"),
                new MusicData(733580, "Party Trax", "Aerokid"),
                new MusicData(733581, "Phuturistic Chilled Trax", "Aerokid"),
                new MusicData(733582, "Tapes from Goa", "Habnosis"),
                new MusicData(733583, "Alley Cat in Trouble", "Rage Against the Fuse"),
                new MusicData(733584, "Who Dares Stacks", "Rage Against the Fuse"),
                new MusicData(733585, "Electric Pixels", "Habbo de Gaia"),
                new MusicData(733586, "Galactic Disco", "DJ Bobba feat. Habboway"),
                new MusicData(733587, "Epic Flail", "Habbocalyptica"),
                new MusicData(733588, "68B Attack Sub", "Habnosis"),
                new MusicData(733589, "The Habstep", "Habnosis"),
                new MusicData(733590, "Caliente Street", "Barrio Bobba"),
                new MusicData(733591, "Habbo Libre", "Ana Stan Band"),
                new MusicData(733592, "Uuh Aah", "DJ Bobba"),
                new MusicData(733593, "Xmas Magic", "Silent Aurora"),
            ];

            Data = list;
        }

        private static void LoadDe()
        {
            List<MusicData> list =
            [
                new MusicData(665519, "Habbowood", "Michael Bauble"),
                new MusicData(665520, "About VIP Now", "BanzaiBabes"),
                new MusicData(665521, "Furni Face", "Lady BlaBla"),
                new MusicData(665522, "Gold Coin Digger", "Kayne Quest"),
                new MusicData(665523, "I Write Bans not Tragedies", "Pixel! at the Disco"),
                new MusicData(665524, "Haven't Friend Request You Yet", "Michael Bauble"),
                new MusicData(665525, "Park Adventure", "Kallomies"),
                new MusicData(665526, "Pet Romance", "Lady BlaBla"),
                new MusicData(665527, "Pixelrazzi", "Lady BlaBla"),
                new MusicData(665528, "Push the Call for Help", "BanzaiBabes"),
                new MusicData(665529, "The Ballad of Bonnie Blonde", "Pixel! at the Disco"),
                new MusicData(665530, "The Good Trade", "Kayne Quest"),
                new MusicData(665531, "Too Lost In Lido", "BanzaiBabes"),
                new MusicData(665532, "Touch the Skyscraper", "Kayne Quest"),
                new MusicData(665533, "Party Trax", "Aerokid"),
                new MusicData(665534, "Phuturistic Chilled Trax", "Aerokid"),
                new MusicData(665535, "Tapes from Goa", "Habnosis"),
                new MusicData(665536, "Alley Cat in Trouble", "Rage Against the Fuse"),
                new MusicData(665537, "Who Dares Stacks", "Rage Against the Fuse"),
                new MusicData(665538, "Electric Pixels", "Habbo de Gaia"),
                new MusicData(665539, "Galactic Disco", "DJ Bobba feat. Habboway"),
                new MusicData(665540, "Epic Flail", "Habbocalyptica"),
                new MusicData(667741, "68B Attack Sub", "Habnosis"),
                new MusicData(667742, "The Habstep", "Habnosis"),
                new MusicData(667743, "Caliente Street", "Barrio Bobba"),
                new MusicData(667744, "Habbo Libre", "Ana Stan Band"),
                new MusicData(667745, "Uuh Aah", "DJ Bobba"),
                new MusicData(667746, "Xmas Magic", "Silent Aurora"),
            ];


            Data = list;
        }

        private static void LoadNl()
        {
            List<MusicData> list =
            [
                new MusicData(1152392, "Habbowood", "Michael Bauble"),
                new MusicData(1152393, "About VIP Now", "BanzaiBabes"),
                new MusicData(1152394, "Furni Face", "Lady BlaBla"),
                new MusicData(1152395, "Gold Coin Digger", "Kayne Quest"),
                new MusicData(1152396, "I Write Bans not Tragedies", "Pixel! at the Disco"),
                new MusicData(1152397, "Haven't Friend Request You Yet", "Michael Bauble"),
                new MusicData(1152398, "Park Adventure", "Kallomies"),
                new MusicData(1152399, "Pet Romance", "Lady BlaBla"),
                new MusicData(1152400, "Pixelrazzi", "Lady BlaBla"),
                new MusicData(1152401, "Push the Call for Help", "BanzaiBabes"),
                new MusicData(1152402, "The Ballad of Bonnie Blonde", "Pixel! at the Disco"),
                new MusicData(1152403, "The Good Trade", "Kayne Quest"),
                new MusicData(1152404, "Too Lost In Lido", "BanzaiBabes"),
                new MusicData(1152405, "Touch the Skyscraper", "Kayne Quest"),
                new MusicData(1152406, "Party Trax", "Aerokid"),
                new MusicData(1152407, "Phuturistic Chilled Trax", "Aerokid"),
                new MusicData(1152408, "Tapes from Goa", "Habnosis"),
                new MusicData(1152409, "Alley Cat in Trouble", "Rage Against the Fuse"),
                new MusicData(1152410, "Who Dares Stacks", "Rage Against the Fuse"),
                new MusicData(1152411, "Electric Pixels", "Habbo de Gaia"),
                new MusicData(1152412, "Galactic Disco", "DJ Bobba feat. Habboway"),
                new MusicData(1152413, "Epic Flail", "Habbocalyptica"),
                new MusicData(1152414, "68B Attack Sub", "Habnosis"),
                new MusicData(1152415, "The Habstep", "Habnosis"),
                new MusicData(1152416, "Caliente Street", "Barrio Bobba"),
                new MusicData(1152417, "Habbo Libre", "Ana Stan Band"),
                new MusicData(1152418, "Uuh Aah", "DJ Bobba"),
                new MusicData(1152419, "Xmas Magic", "Silent Aurora"),
            ];

            Data = list;
        }

        private static void LoadIt()
        {
            List<MusicData> list =
            [
                new MusicData(182242, "Habbowood", "Michael Bauble"),
                new MusicData(182243, "About VIP Now", "BanzaiBabes"),
                new MusicData(182244, "Furni Face", "Lady BlaBla"),
                new MusicData(182245, "Gold Coin Digger", "Kayne Quest"),
                new MusicData(182246, "I Write Bans not Tragedies", "Pixel! at the Disco"),
                new MusicData(182247, "Haven't Friend Request You Yet", "Michael Bauble"),
                new MusicData(182248, "Park Adventure", "Kallomies"),
                new MusicData(182249, "Pet Romance", "Lady BlaBla"),
                new MusicData(182250, "Pixelrazzi", "Lady BlaBla"),
                new MusicData(182251, "Push the Call for Help", "BanzaiBabes"),
                new MusicData(182252, "The Ballad of Bonnie Blonde", "Pixel! at the Disco"),
                new MusicData(182253, "The Good Trade", "Kayne Quest"),
                new MusicData(182254, "Too Lost In Lido", "BanzaiBabes"),
                new MusicData(182255, "Touch the Skyscraper", "Kayne Quest"),
                new MusicData(182256, "Party Trax", "Aerokid"),
                new MusicData(182257, "Phuturistic Chilled Trax", "Aerokid"),
                new MusicData(182258, "Tapes from Goa", "Habnosis"),
                new MusicData(182259, "Alley Cat in Trouble", "Rage Against the Fuse"),
                new MusicData(182260, "Who Dares Stacks", "Rage Against the Fuse"),
                new MusicData(182261, "Electric Pixels", "Habbo de Gaia"),
                new MusicData(182262, "Galactic Disco", "DJ Bobba feat. Habboway"),
                new MusicData(182263, "Epic Flail", "Habbocalyptica"),
                new MusicData(182264, "68B Attack Sub", "Habnosis"),
                new MusicData(182265, "The Habstep", "Habnosis"),
                new MusicData(182266, "Caliente Street", "Barrio Bobba"),
                new MusicData(182267, "Habbo Libre", "Ana Stan Band"),
                new MusicData(182268, "Uuh Aah", "DJ Bobba"),
                new MusicData(182269, "Xmas Magic", "Silent Aurora"),
            ];


            Data = list;
        }

        private static void LoadFi()
        {
            List<MusicData> list =
            [
                new MusicData(488339, "Habbowood", "Michael Bauble"),
                new MusicData(488340, "About VIP Now", "BanzaiBabes"),
                new MusicData(488341, "Furni Face", "Lady BlaBla"),
                new MusicData(488342, "Gold Coin Digger", "Kayne Quest"),
                new MusicData(488343, "I Write Bans not Tragedies", "Pixel! at the Disco"),
                new MusicData(488344, "Haven't Friend Request You Yet", "Michael Bauble"),
                new MusicData(488345, "Park Adventure", "Kallomies"),
                new MusicData(488346, "Pet Romance", "Lady BlaBla"),
                new MusicData(488347, "Pixelrazzi", "Lady BlaBla"),
                new MusicData(488348, "Push the Call for Help", "BanzaiBabes"),
                new MusicData(488349, "The Ballad of Bonnie Blonde", "Pixel! at the Disco"),
                new MusicData(488350, "The Good Trade", "Kayne Quest"),
                new MusicData(488351, "Too Lost In Lido", "BanzaiBabes"),
                new MusicData(488352, "Touch the Skyscraper", "Kayne Quest"),
                new MusicData(488353, "Party Trax", "Aerokid"),
                new MusicData(488354, "Phuturistic Chilled Trax", "Aerokid"),
                new MusicData(488355, "Tapes from Goa", "Habnosis"),
                new MusicData(488356, "Alley Cat in Trouble", "Rage Against the Fuse"),
                new MusicData(488357, "Who Dares Stacks", "Rage Against the Fuse"),
                new MusicData(488358, "Electric Pixels", "Habbo de Gaia"),
                new MusicData(488359, "Galactic Disco", "DJ Bobba feat. Habboway"),
                new MusicData(488360, "Epic Flail", "Habbocalyptica"),
                new MusicData(488361, "68B Attack Sub", "Habnosis"),
                new MusicData(488362, "The Habstep", "Habnosis"),
                new MusicData(488363, "Caliente Street", "Barrio Bobba"),
                new MusicData(488364, "Habbo Libre", "Ana Stan Band"),
                new MusicData(488365, "Uuh Aah", "DJ Bobba"),
                new MusicData(488366, "Xmas Magic", "Silent Aurora"),
            ];

            Data = list;
        }

        private static void LoadTr()
        {
            List<MusicData> list =
            [
                new MusicData(448, "Habbowood", "Michael Bauble"),
                new MusicData(449, "About VIP Now", "BanzaiBabes"),
                new MusicData(450, "Furni Face", "Lady BlaBla"),
                new MusicData(451, "Gold Coin Digger", "Kayne Quest"),
                new MusicData(452, "I Write Bans not Tragedies", "Pixel! at the Disco"),
                new MusicData(453, "Haven't Friend Request You Yet", "Michael Bauble"),
                new MusicData(454, "Park Adventure", "Kallomies"),
                new MusicData(455, "Pet Romance", "Lady BlaBla"),
                new MusicData(456, "Pixelrazzi", "Lady BlaBla"),
                new MusicData(457, "Push the Call for Help", "BanzaiBabes"),
                new MusicData(458, "The Ballad of Bonnie Blonde", "Pixel! at the Disco"),
                new MusicData(459, "The Good Trade", "Kayne Quest"),
                new MusicData(460, "Too Lost In Lido", "BanzaiBabes"),
                new MusicData(461, "Touch the Skyscraper", "Kayne Quest"),
                new MusicData(462, "Party Trax", "Aerokid"),
                new MusicData(463, "Phuturistic Chilled Trax", "Aerokid"),
                new MusicData(464, "Tapes from Goa", "Habnosis"),
                new MusicData(465, "Alley Cat in Trouble", "Rage Against the Fuse"),
                new MusicData(466, "Who Dares Stacks", "Rage Against the Fuse"),
                new MusicData(468, "Electric Pixels", "Habbo de Gaia"),
                new MusicData(469, "Galactic Disco", "DJ Bobba feat. Habboway"),
                new MusicData(470, "Epic Flail", "Habbocalyptica"),
                new MusicData(471, "68B Attack Sub", "Habnosis"),
                new MusicData(472, "The Habstep", "Habnosis"),
                new MusicData(473, "Caliente Street", "Barrio Bobba"),
                new MusicData(474, "Habbo Libre", "Ana Stan Band"),
                new MusicData(475, "Uuh Aah", "DJ Bobba"),
                new MusicData(476, "Xmas Magic", "Silent Aurora"),
            ];



            Data = list;
        }

    }

}
