namespace CQRSRentACar.CQRSPattern.Queries.SliderQueries
{
    public class GetSliderByIdQuery
    {
        public int Id { get; set; }

        public GetSliderByIdQuery(int id)
        {
            Id = id;
        }
    }
}
