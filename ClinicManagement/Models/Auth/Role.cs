namespace ClinicManagement.Models.Auth
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Permission flags
        public bool CanCreate { get; set; }
        public bool CanRead { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public bool CanExecuteRawQueries { get; set; }
        public bool CanAskPromotion { get; set; }
        public bool CanAcceptPromotions { get; set; }
        public bool CanViewPromotionsList { get; set; }
        public bool CanManageUsers { get; set; }
        public bool CanViewUserData { get; set; }
        public bool CanDownloadCsv { get; set; }

        public IEnumerable<User> Users { get; set; }
    }
}
