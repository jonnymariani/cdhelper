namespace CDHelper.Structs
{
    public struct Commands
    {
        public const string PreFix = ":cd";
        public const string GetJukeboxCds = "juke";
        public const string GetCdInfoFromMarket = "market";
        public const string Help = "help";
        public const string Export = "export";
        public const string AutoSearch = "autosearch";
        public const string Language = "language";
    }
    
    public struct ExportSuffix
    {
        public const string All = "all";
        public const string Room = "room";
        public const string Jukebox = "juke";
        public const string Inventory = "inv";
    }

}
