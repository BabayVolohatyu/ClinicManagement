namespace ClinicManagement.Models.Auth
{
    public enum RoleType
    {
        Unauthorized = -1,
        Guest = 1,
        Authorized = 2,
        Operator = 3,
        Admin = 4
    }

    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public RoleType Type { get; set; }

        // Permissions
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

        public IEnumerable<User>? Users { get; set; }

        // Static presets
        public static Role Guest => new Role
        {
            Id = 1,
            Name = "Guest",
            Type = RoleType.Guest,
            CanCreate = false,
            CanRead = true,
            CanUpdate = false,
            CanDelete = false,
            CanExecuteRawQueries = false,
            CanAskPromotion = false,
            CanAcceptPromotions = false,
            CanViewPromotionsList = false,
            CanManageUsers = false,
            CanViewUserData = false,
            CanDownloadCsv = false
        };

        public static Role Authorized => new Role
        {
            Id = 2,
            Name = "Authorized",
            Type = RoleType.Authorized,
            CanCreate = true,
            CanRead = true,
            CanUpdate = true,
            CanDelete = true,
            CanExecuteRawQueries = false,
            CanAskPromotion = true,
            CanAcceptPromotions = false,
            CanViewPromotionsList = false,
            CanManageUsers = false,
            CanViewUserData = false,
            CanDownloadCsv = false
        };

        public static Role Operator => new Role
        {
            Id = 3,
            Name = "Operator",
            Type = RoleType.Operator,
            CanCreate = true,
            CanRead = true,
            CanUpdate = true,
            CanDelete = true,
            CanExecuteRawQueries = true,
            CanAskPromotion = true,
            CanAcceptPromotions = false,
            CanViewPromotionsList = false,
            CanManageUsers = false,
            CanViewUserData = false,
            CanDownloadCsv = true
        };

        public static Role Admin => new Role
        {
            Id = 4,
            Name = "Admin",
            Type = RoleType.Admin,
            CanCreate = true,
            CanRead = true,
            CanUpdate = true,
            CanDelete = true,
            CanExecuteRawQueries = true,
            CanAskPromotion = false,
            CanAcceptPromotions = true,
            CanViewPromotionsList = true,
            CanManageUsers = true,
            CanViewUserData = true,
            CanDownloadCsv = true
        };
    }
}
