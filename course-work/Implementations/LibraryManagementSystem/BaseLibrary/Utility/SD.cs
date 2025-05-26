namespace BaseLibrary.Utility
{
    public class SD
    {
        public enum Role
        {
            Admin,
            Librarian, 
            Member
        }

        public enum ReservationStatus
        {
            Active,
            Approved,
            Renewalled,
            Cancelled,
            Returned,
            Rejected, 
            Lost
        }

    }
}
