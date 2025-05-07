namespace CDHelper.Utils
{
    public static class DefaultMarketCD
    {
        private static string Name { get; set; } = "Epic Flail";

        public static void SetName(string _name)
        {
            Name = _name;
        }
        
        public static string GetName()
        {
            return Name;
        }

    }
}
