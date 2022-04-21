using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Helper
{
    public static class FileManager
    {

        public static string Save(string root , string folder , IFormFile imageFile)
        {
            string filename = imageFile.FileName;

            if(filename.Length >= 64)    
            {
                filename = filename.Substring(filename.Length - 64, 64);
            }

            filename = Guid.NewGuid().ToString() + filename;

            string path = Path.Combine(root, folder, filename);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }

            return filename;
        }

        public static void Delete(string root , string folder , string filename)
        {

            string path = Path.Combine(root, folder, filename);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }


        }

    }
}
