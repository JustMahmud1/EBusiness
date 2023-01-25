namespace ProjectEBusiness.Extensions
{
    public static class CreateFileExt
    {
       public static string CreateFile(this IFormFile file,string environment,string path)
        {
            string imgname = Guid.NewGuid() + file.FileName;
            string fullPath = Path.Combine(environment,path, imgname);
            using (FileStream stream = new FileStream(fullPath,FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return imgname;
        }
    }
}
