namespace CQRSRentACar.CQRSPattern.Queries.ContactMessageQueries
{
    public class GetContactMessageByIdQuery
    {
        public int Id { get; set; }
        
        public GetContactMessageByIdQuery(int id)
        {
            Id = id;
        }
    }
}
