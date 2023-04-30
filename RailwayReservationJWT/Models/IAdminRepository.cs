namespace RailwayReservationJWT.Models
{
    public interface IAdminRepository
    {
        public Admin Get(string name);
        public List<Admin> GetAll();
        public Admin Delete(string name);
        public void Add(Admin airlineAuthority);
        public Admin Update(string name, Admin airlineAuthority);
    }
}