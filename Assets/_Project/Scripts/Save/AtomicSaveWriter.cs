using System.IO;

namespace TheLastSprout.Save
{
    public class AtomicSaveWriter
    {
        public bool WriteSafely(string targetPath, string content)
        {
            string tempPath = targetPath + ".tmp";
            string backupPath = targetPath + ".bak";

            try
            {
                File.WriteAllText(tempPath, content);

                if (File.Exists(targetPath))
                {
                    File.Replace(tempPath, targetPath, backupPath);
                }
                else
                {
                    File.Move(tempPath, targetPath);
                }

                return true;
            }
            catch (IOException)
            {
                // Log exception
                return false;
            }
        }
    }
}
