namespace ResturanShemronKabab.Utility
{
    public static class FileUtility
    {
        //TODO Refrence id from db
        public static bool IsValidImageSize(this IFormFile file,int RefrenceID)
        {
            long fileSize = file.Length;
            if (fileSize < 7000 || fileSize > 2097152)
            {
                return false;
            }
            return true;
        }
        //TODO Checking Different File Header in C# FOR IMAGE AND PDF
        public static bool ISValidImage(this IFormFile file)
        {

            return false;
        }
    }
}
