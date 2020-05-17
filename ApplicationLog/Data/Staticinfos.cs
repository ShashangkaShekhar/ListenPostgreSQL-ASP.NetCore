using System;

namespace ApplicationLog.Data
{
    public static class Staticinfos
    {
        public const String conString = @"Host=localhost;Database=applicationlog;Username=postgres;Password=sa@12345";
        public const String qruryInsert = "INSERT INTO tbllog(logdetails, logdate) VALUES(@logdetails, @logdate)";
        public const String qrurySelect = "SELECT id, logdetails, logdate FROM tbllog Order By id desc limit 1";
    }
}
