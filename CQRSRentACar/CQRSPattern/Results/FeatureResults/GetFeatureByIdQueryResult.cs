namespace CQRSRentACar.CQRSPattern.Results.FeatureResults
{
    public class GetFeatureByIdQueryResult
    {
        public int FeatureId { get; set; }
        public string FeatureTitle { get; set; }
        public string FeatureDescription { get; set; }
        public string FeatureIcon { get; set; }
    }
}
