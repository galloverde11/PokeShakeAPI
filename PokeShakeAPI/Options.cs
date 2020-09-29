namespace PokeShakeAPI
{
    public class BaseOptions
    {
        public string DefaultSiteURL { get; set; }
        public string SpeciesEndpoint { get; set; }
        public string UserAgent { get; set; }
        public bool RandomDescription { get; set; }
        public bool UseCache { get; set; }
        public string CacheSuffix { get; set; }
        public string ResourceNotFoundMsg { get; set; }
    }
    public class PokeOptions : BaseOptions
    {
        public const string Poke = "Pokemon";
    }

    public class ShakeOptions : BaseOptions
    {
        public const string Shake = "Shakespeare";
    }
}
